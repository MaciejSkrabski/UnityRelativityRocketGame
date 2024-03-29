﻿using System.Collections;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.3F;

    public Vector3 offset;
    private Vector3 velocity = Vector3.zero;


    void FixedUpdate()
    {
        // Define a target position above and behind the target transform
        Vector3 targetPosition = target.TransformPoint(offset);

        // Smoothly move the camera towards that target position
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        transform.LookAt(transform.position);
        
    }
}
