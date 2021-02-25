using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralPlatform : MonoBehaviour
{

    public GameObject[] largePlatforms = null;
    public GameObject[] mediumPlatforms = null;
    public GameObject[] smallPlatforms = null;

    void Start()
    {
        CreatePlatforms();
    }

    void CreatePlatforms() {
        GameObject first = Instantiate(largePlatforms[Random.Range(0, largePlatforms.Length)], Vector3.zero, Quaternion.identity);
        Vector3 mediumPosition = first.transform.position;
        Collider firstCol = first.GetComponentInChildren<Collider>();
        GameObject second = Instantiate(mediumPlatforms[Random.Range(0, mediumPlatforms.Length)], Vector3.zero, Quaternion.identity);
        Collider secondCol = second.GetComponentInChildren<Collider>();
        mediumPosition.y += firstCol.bounds.size.y;
        mediumPosition.x += Random.Range(firstCol.bounds.min.x + secondCol.bounds.size.x / 2, firstCol.bounds.max.x - secondCol.bounds.size.x / 2);
        mediumPosition.z += Random.Range(firstCol.bounds.min.z + secondCol.bounds.size.z / 2, firstCol.bounds.max.z - secondCol.bounds.size.z / 2);
        second.transform.position = mediumPosition;
    }

}
