using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public GameObject goal;
    public float thrust;
    public float torque;
    public string finaltext="";
    public float maxdist;
    public float maxspeed;
    private bool landed=false;

    private Rigidbody rb;
    void Objective()
    {
        Vector3 direction = this.transform.position - goal.transform.position;
        float distance = direction.magnitude;
        if (distance < maxdist && rb.velocity.x<maxspeed && rb.velocity.z<maxspeed)
        {
            finaltext = "Landed!";
            landed = true;
        }
        
    }
    void Start()
    {
        goal = GameObject.Find("Finish");
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    void FixedUpdate()
    {
        if (landed) rb.constraints = RigidbodyConstraints.FreezeAll;
        Objective();
        float moveVertical = Input.GetAxis("Vertical");
        float turn = Input.GetAxis("Horizontal");
        Vector3 vertical = new Vector3(0.0f, 0.0f, moveVertical);
        rb.AddRelativeForce(vertical * thrust);
        rb.AddRelativeTorque(Vector3.up * torque * turn);

    }
}
