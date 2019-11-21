using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlGenerator : MonoBehaviour
{
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



    private int objectsum=0;

    private List<Vector3> grid;

    // Start is called before the first frame update
    void Start()
    {
        objectsum = planets + blackHoles + meteors;
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

        ins(ps, planets);
        ins(bhs, blackHoles);
        ins(ms, meteors);
        ins(frs, fuelRefills);
        ins(gs, gems);
    }

    void ins(GameObject o, int n)
    {
        if (n > 0)
        {
            int r;
            for (int i = 0; i <= n; i++)
            {
                r = Random.Range(0, grid.Count - 1);
                Instantiate(o, grid[r], Quaternion.identity);
                grid.RemoveAt(r);
            }
        }
    }

    // Update is called once per frame
}
