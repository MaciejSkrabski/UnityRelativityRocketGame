using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;


    void FixedUpdate()
    {

        Vector3 desiredpos = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredpos, smoothSpeed);
        transform.position = smoothedPosition;

       // transform.LookAt(target);
       
    }
}
