using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class curveRenderer : MonoBehaviour
{
    private LineRenderer lineRenderer;
    [SerializeField] private Transform target;

    [SerializeField] private GameObject gate;
    [SerializeField] private int numOfGates;

    private Vector3 pos = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.5f;
        lineRenderer.endWidth = 0.5f;

        int positionCount = lineRenderer.positionCount;

        for (int i = 0; i < positionCount; i++)
        {
           
            Vector3 last = pos;
            
            float x = (target.position.x / (positionCount - 1)) * i;
            //float Y = (target.position.x / positionCount) * i;
            //float Z = (target.position.x / positionCount) * i;
            
            
            //float x = target.position.x * Mathf.Sqrt(1 - (Mathf.Pow(Y - target.position.y, 2) / Mathf.Pow(target.position.y, 2)));

            float y = target.position.y * Mathf.Sqrt( 1 - ( Mathf.Pow(x - target.position.x, 2) / Mathf.Pow( target.position.x, 2)));

            float z = target.position.z * Mathf.Sqrt( 1 - ( Mathf.Pow(x - target.position.x, 2) / Mathf.Pow( target.position.x, 2)));

            pos = new Vector3(x, y, z);

            lineRenderer.SetPosition(i, pos);

            if (i % (positionCount / numOfGates) == numOfGates)
            {
                GameObject g = Instantiate(gate, pos, Quaternion.identity);
                g.transform.LookAt(last);
            }
        }
    }

}
