using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    Transform player = null;
    public float lookSpeed = 5f;
    void Start()
    {
        player = FindObjectOfType<ShipController>().transform;
    }

    void Update()
    {
        Vector3 targetDirection = transform.position - player.position;

        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, lookSpeed * Time.deltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
        //Vector3 newVector3.RotateTowards(transform.eulerAngles, transform.position - player.position)
        //transform.rotation = Quaternion.LookRotation();
        //transform.Rotate(player.position - transform.position * 10f);

    }
}
