using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindGust : MonoBehaviour
{
    public float windForce = 10f;
    public Vector3 windDirection = Vector3.zero;
    public float gizmoLength = 5;
    private void Awake()
    {
        //windDirection = transform.forward;
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + windDirection * gizmoLength);
    }
    

}
