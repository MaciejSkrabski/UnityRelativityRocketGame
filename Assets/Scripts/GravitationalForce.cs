using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitationalForce : MonoBehaviour
{

    Rigidbody rb;
    Rigidbody pl;
    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody>();

        pl = player.GetComponent<Rigidbody>();
        
    }

    private void FixedUpdate()
    {
        Attract(pl);
    }

    // Update is called once per frame
    void Attract(Rigidbody pl)
    {
        Vector3 direction = this.transform.position - pl.transform.position;
        float distance = direction.magnitude;

        float forceMagnitude = (rb.mass * pl.mass) / Mathf.Pow(distance, 2);
        Vector3 force = direction.normalized * forceMagnitude;

        pl.AddForce(force);
    }
}
