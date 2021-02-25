using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetalChanger : MonoBehaviour
{
    Petal targetPetal = null;
    OutlineScript targetOutline = null;
    public float rayMaxDistance = 5f;
    public LayerMask petalLayer;

    private void Update()
    {
        CheckForPetal();
    }

    private void CheckForPetal()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out hit, rayMaxDistance, petalLayer))
        {
            targetPetal = hit.collider.GetComponent<Petal>();
            targetOutline = hit.collider.GetComponent<OutlineScript>();
            targetOutline.OutlineEnabled = true;
        }
    }

    public void OnDrink() {
        if (targetPetal)
            targetPetal.Pollinate();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.forward * rayMaxDistance);
    }
}
