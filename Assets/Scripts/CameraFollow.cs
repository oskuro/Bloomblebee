using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Transform player = null;
    public float minDistance = 5f;
    public float maxDistance = 10f;

    public float maxSpeed = 3f;
    float acceleration = 2.5f;    
    float moveSpeed = 0f;
    
    Vector3 targetPosition = Vector3.zero;

    Stack<Vector3> waypoints = new Stack<Vector3>();
    Vector3 lastKnownPlayerPosition = Vector3.zero;
    Vector3 offset = new Vector3(0,0,0);

    public float lookRotateSpeed = 180f;

    private float rollInput;
    public float rollSpeed = 90f, rollAcceleration = 3.5f;
    void Start()
    {
        player = FindObjectOfType<ShipController>().transform;
        targetPosition = player.position;
        lastKnownPlayerPosition = targetPosition;
        offset = transform.position - player.position;
    }

    void FixedUpdate()
    {
        Move();
        
        Rotate();
    }

    void Rotate()
    {
        if (Input.GetMouseButton(1))
        {
            Vector3 mouseDistance = new Vector3(0, 0, 0);
            mouseDistance.x = Input.GetAxisRaw("Mouse X");
            mouseDistance.y = Input.GetAxisRaw("Mouse Y");

            rollInput = Mathf.Lerp(rollInput, -mouseDistance.x, rollAcceleration * Time.fixedDeltaTime);

            float tiltDeltaAngle = Mathf.DeltaAngle(0, transform.eulerAngles.z + rollInput * rollSpeed * Time.fixedDeltaTime);
            tiltDeltaAngle = Mathf.Clamp(tiltDeltaAngle, -30, 30);

            Quaternion rot = Quaternion.Euler(transform.eulerAngles.x + mouseDistance.y * lookRotateSpeed * Time.fixedDeltaTime,
                                                transform.eulerAngles.y + mouseDistance.x * lookRotateSpeed * Time.fixedDeltaTime,
                                                tiltDeltaAngle);
            transform.rotation = rot;
        }
        else
        {
            transform.LookAt(player.position);
        }

    }

    void Move()
    {
        // Collect player waypoints
      

        float distance = Vector3.Distance(transform.position, targetPosition);

        // Where are we going?
        Vector3 direction = Vector3.zero;
        if (distance < minDistance)
        {
            direction = transform.position - player.position;
        }
        else if (distance > maxDistance)
        {
            direction = targetPosition - transform.position;
        } 

        // if we are standing still. track player all the time
        if (direction == Vector3.zero)
            targetPosition = player.position;

        if (direction != Vector3.zero)
        {
            moveSpeed = Mathf.Lerp(moveSpeed,  maxSpeed, acceleration * Time.fixedDeltaTime);
            

            Vector3 moveVector = direction * moveSpeed;
            //rb.AddForce(moveVector + windForce, ForceMode.VelocityChange);
            transform.Translate(moveVector * Time.fixedDeltaTime);
            //rb.MovePosition(transform.position + moveVector * Time.fixedDeltaTime);
        }

    }
}

