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
        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
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
