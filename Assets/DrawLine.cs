using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    public Transform startPoint; // Set this in the Unity Editor by dragging the GameObject you want to start from.
    public Transform endPoint;   // Set this in the Unity Editor by dragging the GameObject you want to end at.
    public Color lineColor = Color.white; // Color of the line

    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }

        // Set line properties
        lineRenderer.startColor = lineColor;
        lineRenderer.endColor = lineColor;
        lineRenderer.startWidth = 0.1f; // Set the width of the line
        lineRenderer.endWidth = 0.1f;
        
        // Set line positions
        lineRenderer.positionCount = 2; // Two points for the start and end
        lineRenderer.SetPosition(0, startPoint.position); // Set start position
        lineRenderer.SetPosition(1, endPoint.position);   // Set end position
    }
}
