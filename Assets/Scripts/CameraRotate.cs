using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    public bool startRotate = false;
    
    Transform player;

    public void StartRotate()
    {
        startRotate = true;
        player = FindObjectOfType<ShipController>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!startRotate)
            return;

        transform.Rotate(new Vector3(0, 90 * Time.deltaTime, 0),Space.World);
        
        transform.LookAt(player);
    }
}
