using System.Collections;
using UnityEngine;

public class MinimapControll : MonoBehaviour
{
    public Transform target;

    public Vector3 offset;



    void Update()
    {
        // Define a target position above and behind the target transform
        Vector3 targetPosition = target.TransformPoint(offset);

        // Smoothly move the camera towards that target position
        transform.position = targetPosition;
    }
}
