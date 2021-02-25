using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraLook : MonoBehaviour
{
    Vector3 lookDirection = new Vector3();
    public float verticalTurningSpeed = 10;
    public float horizontalTurningSpeed = 10;
    public Vector2 lookInvert = new Vector2(1,-1);

    public Transform XRotation = null;
    public Transform YRotation = null;
    public Transform followTarget = null;
    
     
    private void Update()
    {
        transform.position = followTarget.position;
        RotateX();
        RotateY();
    }

    private void RotateX()
    {
        Quaternion newRotation = XRotation.rotation * Quaternion.Euler(lookDirection.x * verticalTurningSpeed * Time.deltaTime, 0, 0);
        float tiltDeltaAngle = Mathf.DeltaAngle(0, newRotation.eulerAngles.x);
        tiltDeltaAngle = Mathf.Clamp(tiltDeltaAngle, -85, 85);
        XRotation.rotation = Quaternion.Euler(tiltDeltaAngle, XRotation.eulerAngles.y, 0);
    }

    void RotateY()
    {
        Quaternion newRotation = YRotation.rotation * Quaternion.Euler(0, lookDirection.y * horizontalTurningSpeed * Time.deltaTime, 0);
        float tiltDeltaAngle = Mathf.DeltaAngle(0, newRotation.eulerAngles.y);
        YRotation.rotation = Quaternion.Euler(YRotation.eulerAngles.x, tiltDeltaAngle, 0);
    }

    public void OnLook(InputValue value)
    {
        Vector2 look = value.Get<Vector2>();
        
        lookDirection.x = look.y * lookInvert.x;
        lookDirection.y = look.x * lookInvert.y;
    }
}
