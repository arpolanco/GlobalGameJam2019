using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//reference: https://www.youtube.com/watch?v=sNmeK3qK7oA

public class PlayerCamera : MonoBehaviour
{

    float yaw;
    float pitch;
    public bool lockCursor = true;
    public float mouseSensitivity = 5;
    public Transform target;
    public float dstFromTarget = 5;
    public Vector2 pitchMinMax = new Vector2(-30, 80);

    public float rotationSmoothTime = .15f;
    Vector3 rotationSmoothVelocity;
    Vector3 currentRotation;

    

    void Start()
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void LateUpdate()
    {
        
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);

        currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);
        transform.eulerAngles = currentRotation;

        transform.position = target.position - transform.forward * dstFromTarget;

    }


}
