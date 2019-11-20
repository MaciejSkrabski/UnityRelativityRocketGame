using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlGenerator : MonoBehaviour
{
    [Range(0, 13)]
    public int numOfPlanets;
    [Range(0, 5)]
    public int numOfBlackHoles;
    [Range(0, 10)]
    public int numOfMeteors;
    private int objectsum=0;

    private List<Vector2> grid;

    // Start is called before the first frame update
    void Start()
    {
        objectsum = numOfPlanets + numOfBlackHoles + numOfMeteors;
        grid = new List<Vector2>();
        List<int> l;
        var go = new GameObject();
        for (int i = -3000; i <= 3000; i+=100)
        {
            for (int j = -3000; j <= 3000; j += 100)
            {
                if (i != 0 && j!=0)
                {
                    grid.Add(new Vector2(i, j));

                }
            }
        }

        Debug.Log(grid.ToString());
        grid.Add(new Vector2(2f, 2f) );
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
