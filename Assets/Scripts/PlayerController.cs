using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    public float thrust;
    public float torque;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float moveVertical = Input.GetAxis("Vertical");
        float turn = Input.GetAxis("Horizontal");
        Vector3 vertical = new Vector3(0.0f, 0.0f, moveVertical);
        rb.AddRelativeForce(vertical * thrust);
        rb.AddRelativeTorque(Vector3.up * torque * turn);

    }
}


/*
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    //public float gravity = 20.0F;
    public float rotateSpeed = 3.0F;
    private Vector3 moveDirection = Vector3.zero;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        CharacterController controller = GetComponent<CharacterController>();
        //if (controller.isGrounded)
        //{
            moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
        //  if (Input.GetButton("Jump"))
        //    moveDirection.y = jumpSpeed;

        //}
        //moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection); //* Time.deltaTime);

        //Rotate Player
        transform.Rotate(0, Input.GetAxis("Horizontal"), 0);

    }
}*/