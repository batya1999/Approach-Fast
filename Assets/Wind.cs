using UnityEngine;

public class Wind : MonoBehaviour
{
    public GameObject objectToApplyWind;
    public float minForce = 1f;
    public float maxForce = 5f;
    public float changeInterval = 2f;

    private Rigidbody rbObjectToApplyWind;
    private Vector3 windDirection;

    void Start()
    {
        if (objectToApplyWind == null)
        {
            Debug.LogError("Please assign the object to apply wind to.");
            enabled = false;
            return;
        }

        rbObjectToApplyWind = objectToApplyWind.GetComponent<Rigidbody>();
        if (rbObjectToApplyWind == null)
        {
            Debug.LogError("The assigned object to apply wind to doesn't have a Rigidbody component.");
            enabled = false;
            return;
        }

        InvokeRepeating("ChangeWindDirection", 0f, changeInterval);
    }

    void FixedUpdate()
    {
        // Apply wind force to the object
        rbObjectToApplyWind.AddForce(windDirection * Random.Range(minForce, maxForce), ForceMode.Force);
    }

    void ChangeWindDirection()
    {
        // Generate a random wind direction
        windDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;
    }
}
