using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerLol : MonoBehaviour
{
    public float speed;

    private CharacterController controller;
    private Vector3 moveDirection = Vector3.zero;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {

        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection = moveDirection * speed;
        print(moveDirection);
        
        
    }

}
