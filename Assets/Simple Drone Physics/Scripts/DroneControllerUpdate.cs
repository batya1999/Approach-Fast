using System.Collections;
using UnityEngine;

public class DroneControllerUpdate : MonoBehaviour
{
    
    [Header("Calibration Flags")]
    public bool inCalibrationRise = true;
    public bool inCalibrationYaw = true;
    public bool inCalibrationStartPitch = true;
    public bool inCalibrationPitch = false;
    public bool inCalibrationRoll = false;
    public bool inCalibrationStartRoll = true;
    public bool inCalibrationThrottle = false;
    public bool inHover = true;

    [Header("Control Amounts")]
    [SerializeField] private float yawAmount;
    [SerializeField] private float pitchAmount;
    [SerializeField] private float throttleAmount;
    [SerializeField] private float rollAmount;
    [SerializeField] private float riseAmount;

    [Header("Control Parameters")]
    [SerializeField] PIDController highController;
    [SerializeField] PIDController yawController;
    [SerializeField] PIDController pitchController;
    [SerializeField] PIDController startPitchController;
    [SerializeField] PIDController throttleController;
    [SerializeField] PIDController rollController;
    [SerializeField] PIDController startRollController;

    [Header("Drone Components")]
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject body;
    private Rigidbody rb;

    [SerializeField] private float zRot;
    [SerializeField] private float xRot;
    [SerializeField] private float yRot;
    [SerializeField] private Transform FL, FR, RL, RR;
    [SerializeField] private Vector3 CenterOfMass;
    [SerializeField] private Camera targetCamera;
    [SerializeField] private float yaw = 0;
    [SerializeField] private float pitch = 0;
    [SerializeField] private float roll = 0;
    [SerializeField] private int tps;

    [SerializeField] private Vector3 current;
    [SerializeField] private Quaternion rotationY, rotationX;


    [SerializeField] private float curveErrorZ;
    [SerializeField] private float curveErrorY;


    void Start()
    {
        Vector3 tarPos = target.transform.localPosition;
        rb = GetComponent<Rigidbody>();
        yaw = (float)Mathf.Atan2(tarPos.x, tarPos.z) * Mathf.Rad2Deg;
        targetCamera.transform.LookAt(tarPos);

        float angleY = Mathf.Atan2(tarPos.z, tarPos.x) * Mathf.Rad2Deg;
        rotationY = Quaternion.Euler(0, angleY - 90, 0);
        Vector3 newTarget = rotationY * tarPos;

        float angleX = Mathf.Atan2(newTarget.z, newTarget.y) * Mathf.Rad2Deg;
        rotationX = Quaternion.Euler(-angleX, 0, 0);
        Vector3 finalTarget = rotationX * newTarget;

        Debug.Log("Final target position: " + finalTarget);

        StartCoroutine(CallFunctionRepeatedly());
    }

    void FixedUpdate()
    {
        rb.centerOfMass = CenterOfMass;


        current = rotationX * (rotationY * transform.position);

        curveErrorY = findCurveError(target.transform.position.x, target.transform.position.z, transform.position.x, transform.position.z);
        curveErrorZ = findCurveError(target.transform.position.x, target.transform.position.y, transform.position.x, transform.position.y);

        //phi = Mathf.Atan2(target.transform.position.x, target.transform.position.z) * Mathf.Rad2Deg;
        //theta = Mathf.Atan2(transform.position.x, transform.position.z) * Mathf.Rad2Deg;
        // dessRot = phi - theta;

        //dessH = Mathf.Acos(target.transform.position.y / target.transform.position.magnitude) * Mathf.Rad2Deg;
        //H = Mathf.Acos(transform.position.y / transform.position.magnitude) * Mathf.Rad2Deg;

        zRot = transform.rotation.eulerAngles.z > 180 ? transform.rotation.eulerAngles.z - 360 : transform.rotation.eulerAngles.z;
        xRot = transform.rotation.eulerAngles.x > 180 ? transform.rotation.eulerAngles.x - 360 : transform.rotation.eulerAngles.x;
        yRot = transform.rotation.eulerAngles.y > 180 ? transform.rotation.eulerAngles.y - 360 : transform.rotation.eulerAngles.y;
        //throttleAmount = 200;
        //Throttle();

        if (inCalibrationRise) CalibrateRise();
        if (inCalibrationYaw) CalibrateYaw();
        if (inCalibrationStartPitch) CalibrateStartPitch();
        if (inCalibrationPitch) CalibratePitch();
        if (inCalibrationRoll) CalibrateRoll();
        if (inCalibrationStartRoll) CalibrateRoll();
        if (inCalibrationThrottle) CalibrateThrottle();
        if (inHover) Hover();
    }

    IEnumerator CallFunctionRepeatedly()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f / tps); // Adjust to call the function 20 times per second
            UpdateParams();
        }
    }

    void UpdateParams()
    {
        if (inCalibrationRise) riseAmount = highController.Update(1.0f / tps, transform.localPosition.y, 1f);
        if (inCalibrationYaw) yawAmount = yawController.UpdateAngle(1.0f / tps, yRot, yaw);
        if (inCalibrationStartPitch) pitchAmount = startPitchController.UpdateAngle(1.0f / tps, xRot, pitch);
        //if (inCalibrationPitch) pitchAmount = pitchController.UpdateAngle(1.0f / tps, current.z, 0);
        if (inCalibrationPitch) pitchAmount = pitchController.UpdateAngle(1.0f / tps, -curveErrorZ, 0);
        //if (inCalibrationRoll) rollAmount = rollController.Update(1.0f / tps, current.x, 0);
        if (inCalibrationRoll) rollAmount = rollController.Update(1.0f / tps, -curveErrorY, 0);
        if (inCalibrationStartRoll) rollAmount = rollController.Update(1.0f / tps, zRot, roll);
    }

    void CalibrateRise()
    {
        Rise();
        //if (transform.position.y >= 1f)
        //{
        //    //inHover = true;
        //    inCalibrationH = false;
        //    inCalibrationYaw = true;
        //}
    }

    void CalibrateYaw()
    {
        Yaw();
        if (yaw - yRot < 0.1f)
        {
            inCalibrationStartPitch = true;
            
        }
    }

    void CalibratePitch()
    {
        Pitch();
    }
    void CalibrateStartPitch()
    {
        Pitch();
        if (pitch - xRot < 0.1f)
        {
            //inCalibrationRoll = true;
            //inCalibrationHight = true;
            inCalibrationStartPitch = false;
            //inCalibrationStartRoll = false;
            //inCalibrationPitch = true;
            inHover = false;
            inCalibrationRise = false;
            inCalibrationThrottle = true;
        }
    }

    void CalibrateThrottle()
    {
        Throttle();
        // if (dessH > H && liftSensitivity > 5)
        // {
        //     // Go down
        //     liftSensitivity -= 0.1f;
        // }
        // if (dessH < H && liftSensitivity < 30)
        // {
        //     // Go up
        //     liftSensitivity += 0.1f;
        // }
    }

    void CalibrateRoll()
    {
        Roll();
        if (roll - zRot < 0.1f)
        {
            inCalibrationRoll = true;
            inCalibrationStartRoll = false;
        }
    }

    public float findCurveError(float a, float b, float x, float y)
    {
        float p1x = a + Mathf.Sqrt((a * a) - (b * b));
        float p2x = a - Mathf.Sqrt((a * a) - (b * b));

        float dist1 = Mathf.Sqrt(((p1x-x) * (p1x-x)) + (y * y));
        float dist2 = Mathf.Sqrt(((p2x-x) * (p2x-x)) + (y * y));
        
        return dist1+dist2 - (2*a);
    }

    void Hover()
    {
        rb.AddForceAtPosition(transform.up * (rb.mass * 9.81f / 4), FL.transform.position);
        rb.AddForceAtPosition(transform.up * (rb.mass * 9.81f / 4), FR.transform.position);
        rb.AddForceAtPosition(transform.up * (rb.mass * 9.81f / 4), RL.transform.position);
        rb.AddForceAtPosition(transform.up * (rb.mass * 9.81f / 4), RR.transform.position);
    }

    void Rise()
    {
        rb.AddForceAtPosition(transform.up * riseAmount, FL.transform.position);
        rb.AddForceAtPosition(transform.up * riseAmount, FR.transform.position);
        rb.AddForceAtPosition(transform.up * riseAmount, RL.transform.position);
        rb.AddForceAtPosition(transform.up * riseAmount, RR.transform.position);
    }
    void Throttle()
    {
        rb.AddForceAtPosition(transform.up * throttleAmount, FL.transform.position);
        rb.AddForceAtPosition(transform.up * throttleAmount, FR.transform.position);
        rb.AddForceAtPosition(transform.up * throttleAmount, RL.transform.position);
        rb.AddForceAtPosition(transform.up * throttleAmount, RR.transform.position);
    }

    void Yaw()
    {
        rb.AddForceAtPosition(transform.right * yawAmount, FL.transform.position);
        rb.AddForceAtPosition(transform.right * yawAmount, FR.transform.position);
        rb.AddForceAtPosition(transform.right * -yawAmount, RL.transform.position);
        rb.AddForceAtPosition(transform.right * -yawAmount, RR.transform.position);
    }

    void Pitch()
    {
        rb.AddForceAtPosition(transform.up * -pitchAmount, FL.transform.position);
        rb.AddForceAtPosition(transform.up * -pitchAmount, FR.transform.position);
        rb.AddForceAtPosition(transform.up * pitchAmount, RL.transform.position);
        rb.AddForceAtPosition(transform.up * pitchAmount, RR.transform.position);
    }

    void Roll()
    {
        rb.AddForceAtPosition(transform.up * rollAmount, FL.transform.position);
        rb.AddForceAtPosition(transform.up * rollAmount, RL.transform.position);
        rb.AddForceAtPosition(transform.up * -rollAmount, FR.transform.position);
        rb.AddForceAtPosition(transform.up * -rollAmount, RR.transform.position);
    }
}
