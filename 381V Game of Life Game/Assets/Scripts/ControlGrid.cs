using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlGrid : MonoBehaviour
{
    private GameObject[,] gridClones;
    private bool[,] gridState;
    private bool[,] prevState;
    private bool[,] ruleset;
    private int gridx;
    private int gridy;

    public GameObject gridObject;
    public float gridOffset = 1f;
    public Vector3 gridOrigin = Vector3.zero;
    public float nextUpdateTime;
    public float updatePeriod;

    // called before the first frame update
    void Start()
    {
        int difficulty = PlayerPrefs.GetInt("difficulty");
        if (difficulty == 0)
        {
            updatePeriod = 1f;
        }
        if (difficulty == 1)
        {
            updatePeriod = 0.5f;
        }
        if (difficulty == 2)
        {
            updatePeriod = 0.25f;
        }

        gridClones = new GameObject[gridx, gridy];
        prevState = new bool[gridx, gridy];
        //InstantiateGlobals();
        SpawnGrid();
    }

    // called before every frame update
    void Update()
    {
        if (Time.time > nextUpdateTime)
        {
            nextUpdateTime += updatePeriod;
            UpdateGrid();
        }
    }

    public void setGridState(bool[,] gridState)
    {
        this.gridState = gridState;
    }

    public void setRuleset(bool[,] ruleset)
    {
        this.ruleset = ruleset;
    }

    public void setGridX(int gridx)
    {
        this.gridx = gridx;
    }

    public void setGridY(int gridy)
    {
        this.gridy = gridy;
    }

    // instantiates global variables
    void InstantiateGlobals()
    {
        gridClones = new GameObject[gridx, gridy];
        gridState = new bool[gridx, gridy];
        prevState = new bool[gridx, gridy];
        // glider for testing, but eventually load initial state from input file
        gridState[0, 0] = true;
        gridState[1, 1] = true;
        gridState[2, 0] = true;
        gridState[2, 1] = true;
        gridState[1, 2] = true;
        // game of life ruleset encoding, but eventually load rulset from input file
            // first index encodes whether current cell is alive or dead (0 for dead, 1 for alive)
            // second index encodes number of alive neighbors (from 0 to 8)
            // true means cell will be alive next iteration
            // false means cell will be dead next iteration
        ruleset[0, 3] = true; // if cell dead and has 3 neighbors, birth new cell
        ruleset[1, 2] = true; // if cell alive and has 2 neighbors, remain alive
        ruleset[1, 3] = true; // if cell alive and has 3 neighbors, remain alive
    }

    // spawns the grid according to gridState set in LoadGrid()
    void SpawnGrid()
    {
        for (int idx = 0; idx < gridx; idx++)
        {
            for (int idy = 0; idy < gridy; idy++)
            {
                if (gridState[idx, idy] == true)
                {
                    SpawnCell(idx, idy);
                }
            }
        }
    }

    // updates grid according to ruleset
    void UpdateGrid()
    {
        prevState = gridState.Clone() as bool[,];
        for (int idx = 0; idx < gridx; idx++)
        {
            for (int idy = 0; idy < gridy; idy++)
            {
                int numAlive = CountNeighbors(idx, idy);
                if (prevState[idx, idy] == true && ruleset[1, numAlive] == false)
                {
                    gridState[idx, idy] = false;
                    DespawnCell(idx, idy);
                }
                else if (prevState[idx, idy] == false && ruleset[0, numAlive] == true)
                {
                    gridState[idx, idy] = true;
                    SpawnCell(idx, idy);
                }
            }
        }
    }

    // counts the number of alive neighbors for cell at position x, y
    int CountNeighbors(int x, int y)
    {
        int numAlive = 0;
        for (int idx = x - 1; idx <= x + 1; idx++)
        {
            for (int idy = y - 1; idy <= y + 1; idy++)
            {
                if (prevState[WrapAround(idx, gridx), WrapAround(idy, gridy)] == true && (idx != x || idy != y))
                {
                    numAlive += 1;
                }
            }
        }
        return numAlive;
    }

    // computes wrap around for index i of size n grid
    int WrapAround(int i, int n){
        if (i < 0)
        {
            return i + n;
        }
        else if (i >= n)
        {
            return i - n;
        }
        else {
            return i;
        }
    }

    // spawns gridObject at position x, y
    void SpawnCell(int x, int y)
    {
        Vector3 pos = new Vector3(x * gridOffset, 0, y * gridOffset) + gridOrigin; // Iterate through grid, moving prefab vector to custom spawn (if any)
        Quaternion rot = Quaternion.identity;
        gridClones[x, y] = Instantiate(gridObject, pos, rot) as GameObject;
    }

    // despawns gridObject at position x, y
    void DespawnCell(int x, int y)
    {
        Destroy(gridClones[x, y]);
    }
}
