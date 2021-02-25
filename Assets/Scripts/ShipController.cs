using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipController : MonoBehaviour
{
    public float forwardSpeed = 25f, strafeSpeed = 7.5f, hoverSpeed = 5f;
    private float  activeForwardSpeed, activeStrafeSpeed, activeHoverSpeed;
    private float forwardAcceleration = 2.5f, strafeAcceleration = 2f, hoverAcceleration = 2f;

    public float lookRotateSpeed = 180f;

    private float rollInput;
    public float rollSpeed = 90f, rollAcceleration = 3.5f;

    private Vector3 windForce = Vector3.zero;

    Rigidbody rb;
    private bool canMove = true;
    [SerializeField] Transform body = null;

    private float horizontal;
    private float vertical;
    private float hover;

    public bool CanMove
    {
        get
        {
            return canMove;
        }
        set
        {
            canMove = value;
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rb.AddForce(-rb.velocity, ForceMode.VelocityChange);

        if (!canMove)
            return;

        activeForwardSpeed = Mathf.Lerp(activeForwardSpeed, vertical * forwardSpeed, forwardAcceleration * Time.fixedDeltaTime);
        activeStrafeSpeed = Mathf.Lerp(activeStrafeSpeed, -horizontal * strafeSpeed, strafeAcceleration * Time.fixedDeltaTime);
        activeHoverSpeed = Mathf.Lerp(activeHoverSpeed, hover * hoverSpeed, hoverAcceleration * Time.fixedDeltaTime);

        Vector3 moveVector = transform.up * activeHoverSpeed + -Camera.main.transform.right * activeStrafeSpeed + Camera.main.transform.forward * activeForwardSpeed;
        rb.AddForce(moveVector + windForce, ForceMode.VelocityChange);

        if (body)
        {
            Quaternion rot = Quaternion.Lerp(body.transform.rotation, Camera.main.transform.rotation, activeForwardSpeed / 10f * Time.fixedDeltaTime);
            body.rotation = rot;
        }


        
    }

    public void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        horizontal = input.x;
        vertical = input.y;
    }

    public void OnHover(InputValue value)
    {
        hover = value.Get<float>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("WindArea"))
        {
            WindGust wind = other.GetComponent<WindGust>();
            windForce = wind.windDirection * wind.windForce;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("WindArea"))
        {
            windForce = Vector3.zero;
        }
    }
}
