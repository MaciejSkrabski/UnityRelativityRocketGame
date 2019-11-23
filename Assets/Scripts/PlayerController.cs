using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PlayerController : MonoBehaviour
{
    #region variables
    private Rigidbody rb;
    public GameObject goal;
    public float health;
    public float fuel;
    public float thrust;
    public float torque;
    public string finaltext = "";
    public float maxdist;
    public float maxspeed;
    private bool landed = false;
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
    private GameObject finish;
    public AudioClip impact; 
    public Text healthText;
    public Text gameText;
    private AudioSource imp;
    

    

    #endregion

    void Start()
    { 
        imp = this.gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        imp.playOnAwake = false;
        imp.clip = impact;
        healthText.text = "Health: " + health.ToString();
        rb = GetComponent<Rigidbody>();

        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        grid = new List<Vector3>();
        for (int i = -size; i <= size; i += 100)
        {
            for (int j = -size; j <= size; j += 100)
            {
                if (i != 0 && j != 0)
                {
                    grid.Add(new Vector3(i, 0f, j));
                }
            }
        }

        int r;
        r = Random.Range(0, grid.Count - 1);
        finish = Instantiate(goal, grid[r], Quaternion.identity);
        grid.RemoveAt(r);

        generate();
    }

    void FixedUpdate()
    {
        if (landed) rb.constraints = RigidbodyConstraints.FreezeAll;
        if (alive && landed == false)
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
        if (alive & landed == false)
        {
            if (Vector3.Distance(finish.transform.position, rb.transform.position) < maxdist && rb.velocity.magnitude < maxspeed)
            {
                finaltext = "Landed!";
                gameText.text = finaltext;
                landed = true;
            }

            if (Input.GetKey("w") || Input.GetKey("up") || Input.GetKey("s") || Input.GetKey("down") || Input.GetKey("a") || Input.GetKey("left") || Input.GetKey("d") || Input.GetKey("right"))
            {
                Explode(this.transform.position);
            }

            if (health <= 0)
            {
                rb.constraints = RigidbodyConstraints.FreezeAll;
                this.finaltext = "Ship destroyed";
                gameText.text = finaltext;
                this.alive = false;
                //this.gameObject.GetComponent<MeshRenderer>().enabled = false;
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
        imp.Play();
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (collision.gameObject.name == "Black Hole Position(Clone)")
            {
                this.health = 0;
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


        if (colv > 0) force = (colv * vel) * sinus;
        else force = vel;
        force = Mathf.Ceil(force);


        this.health -= force;
        healthText.text = "Health: "+health.ToString();

    }

    void ins(GameObject o, int n)
    {
        if (n > 0)
        {
            int r;
            for (int i = 1; i <= n; i++)
            {
                r = Random.Range(0, grid.Count - 1);
                grid.RemoveAt(r);
                Instantiate(o, grid[r], Quaternion.identity);
            }
        }
    }

    void generate()
    {
        ins(ps, planets);
        ins(bhs, blackHoles);
        ins(ms, meteors);
        ins(frs, fuelRefills);
        ins(gs, gems);
    }
}

