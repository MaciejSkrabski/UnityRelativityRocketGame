﻿using UnityEngine;
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
    public GameObject Fireworks;

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

    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            Explode(this.transform.position);
        }

    }
    void Explode(Vector3 position)
    {
        GameObject firework = Instantiate(Fireworks, position, this.transform.rotation);
        firework.GetComponent<ParticleSystem>().Play();
    }
}
