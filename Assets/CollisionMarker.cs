using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionMarker : MonoBehaviour
{
    public GameObject markerPrefab; // Drag the prefab for your marker here in the Unity Editor

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Drone"))
        {
            Destroy(collision.gameObject);

            ContactPoint contact = collision.contacts[0];
            Vector3 collisionPoint = contact.point;

            // Instantiate markerPrefab at the collision point
            Instantiate(markerPrefab, collisionPoint, Quaternion.identity);
        }
    }
}
