using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameObject playerBall;
    public float speed = 20.0f;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerBall = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.time > 5)
        {
            Vector3 direction = this.transform.position - playerBall.transform.position;
            float distance = direction.magnitude;
            if (distance > 100 & rb.velocity.magnitude < speed){
                float moveHorizontal = playerBall.transform.position.x - transform.position.x;
                float moveVertical = playerBall.transform.position.z - transform.position.z;
                Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
                //rb.AddForce(rb.mass * (Random.Range(0f, 5.0f) * speed * movement.normalized));
                rb.AddForce(rb.mass * speed * movement.normalized);
            }
            
        }
    }
}