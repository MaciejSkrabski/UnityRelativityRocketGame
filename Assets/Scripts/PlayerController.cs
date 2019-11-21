using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject goal;
    public float health;
    public float fuel;
    public float thrust;
    public float torque;
    public string finaltext="";
    public float maxdist;
    public float maxspeed;
    private bool landed=false;
    public GameObject Fireworks;
    private bool alive = true;

    public int size;

    [Range(0, 50)]
    public int planets;
    public GameObject ps;

    [Range(0, 50)]
    public int blackHoles;
    public GameObject bhs;

    [Range(0, 50)]
    public int meteors;
    public GameObject ms;

    [Range(0, 50)]
    public int fuelRefills;
    public GameObject frs;

    [Range(0, 50)]
    public int gems;
    public GameObject gs;

    private List<Vector3> grid;


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
                grid = new List<Vector3>();
        var go = new GameObject();
        for (int i = -size; i <= size; i+=100)
        {
            for (int j = -size; j <= size; j += 100)
            {
                if (i != 0 && j!=0)
                {
                    grid.Add(new Vector3(i, 0f, j));
                }
            }
        }
        
        ins(goal, 1);
        ins(ps, planets);
        ins(bhs, blackHoles);
        ins(ms, meteors);
        ins(frs, fuelRefills);
        ins(gs, gems);
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    void FixedUpdate()
    {
        if (landed) rb.constraints = RigidbodyConstraints.FreezeAll;
        //Objective();
        if (alive & landed==false)
        {
            float moveVertical = Input.GetAxis("Vertical");
            float turn = Input.GetAxis("Horizontal");
            Vector3 vertical = new Vector3(0.0f, 0.0f, moveVertical);
            rb.AddRelativeForce(vertical * thrust);
            rb.AddRelativeTorque(Vector3.up * torque * turn);
        }

    }

    void Update()
    {
        if (alive & landed==false)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                Explode(this.transform.position);
            }

            if (health<=0) 
            {
                rb.constraints = RigidbodyConstraints.FreezeAll;
                this.finaltext = "Ship destroyed.";
                this.alive = false;
            }

        }
        
    }
    void Explode(Vector3 position)
    {
        GameObject firework = Instantiate(Fireworks, position, this.transform.rotation);
        firework.GetComponent<ParticleSystem>().Play();
    }

    void OnCollisionEnter(Collision collision) 
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            if(collision.gameObject.name == "Black Hole Position(Clone)")
            {
                this.health=0;
                return;
            }
        }
        Rigidbody collidable;
        collidable = collision.rigidbody;
        float colv = Mathf.Ceil(collidable.velocity.magnitude);
        float force;
        float sinus = Vector3.Angle(rb.velocity, collidable.velocity);
        float vel = Mathf.Ceil(rb.velocity.magnitude);
        Debug.Log("my velocity: ");
        Debug.Log(vel);
        Debug.Log("enemy velocity: ");
            
        
        if (colv > 0) force = (colv*vel)*sinus;
        else force = vel;
        force=Mathf.Ceil(force);
        
        Debug.Log("force:");
        Debug.Log(force);
        this.health-=force;
        Debug.Log("health:");
        Debug.Log(this.health);
    
    }

    void ins(GameObject o, int n)
    {
        if (n > 0)
        {
            int r;
            for (int i = 1; i <= n; i++)
            {
                r = Random.Range(0, grid.Count - 1);
                Instantiate(o, grid[r], Quaternion.identity);
                grid.RemoveAt(r);
            }
        }
    }
}

