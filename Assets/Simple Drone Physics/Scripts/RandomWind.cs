using UnityEngine;

public class RandomWind : MonoBehaviour
{
    private Rigidbody droneRigidbody; // Reference to the drone's Rigidbody
    public float windStrength = 10f; // Strength of the wind force
    public float changeInterval = 2f; // Time interval to change the wind direction

    private Vector3 windDirection; // Current wind direction
    private float timeSinceLastChange; // Time elapsed since the last wind direction change

    void Start()
    {
        // Initialize the wind direction and timer
        ChangeWindDirection();
        timeSinceLastChange = 0f;

        if (droneRigidbody == null)
        {
            droneRigidbody = GetComponent<Rigidbody>();
        }
    }

    void Update()
    {
        // Update the timer
        timeSinceLastChange += Time.deltaTime;

        // Check if it's time to change the wind direction
        if (timeSinceLastChange >= changeInterval)
        {
            ChangeWindDirection();
            timeSinceLastChange = 0f;
        }
    }

    void FixedUpdate()
    {
        // Apply the wind force to the drone's Rigidbody
        droneRigidbody.AddForce(windDirection * (windStrength * Random.Range(0.5f, 1f)));
    }

    void ChangeWindDirection()
    {
        // Generate a new random wind direction
        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(-1f, 1f);
        float randomZ = Random.Range(-1f, 1f);
        windDirection = new Vector3(randomX, randomY, randomZ).normalized;
    }
}
