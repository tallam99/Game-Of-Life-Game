using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public static GridController instance;

    private GameObject[,] gridClones;
    private bool[,] gridState;
    private bool[,] prevState;
    private bool[,] ruleset;
    private int[,] aliveCounts;
    private int gridx;
    private int gridy;
    public bool wrapGrid;
    private float nextUpdateTime = float.PositiveInfinity;

    public GameObject gridObject;
    public GameObject gridObjectTransparent;
    public float gridOffset;
    public Vector3 gridOrigin;
    public float updatePeriod;
    public int transparentIter;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        gridClones = new GameObject[gridx, gridy];
        prevState = new bool[gridx, gridy];
        aliveCounts = new int[gridx, gridy];
        SetDifficulty();
        SpawnGrid();
    }

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

    public void setWrapGrid(bool wrapGrid)
    {
        this.wrapGrid = wrapGrid;
    }

    private void SetDifficulty()
    {
        int difficulty = PlayerPrefs.GetInt("difficulty");
        if (difficulty == 0)
        {
            updatePeriod = 2f;
        }
        if (difficulty == 1)
        {
            updatePeriod = 1f;
        }
        if (difficulty == 2)
        {
            updatePeriod = 0.5f;
        }
    }

    // spawns the grid according to gridState set in LoadGrid()
    private void SpawnGrid()
    {
        for (int idx = 0; idx < gridx; idx++)
        {
            for (int idy = 0; idy < gridy; idy++)
            {
                if (gridState[idx, idy] == true)
                {
                    SpawnCell(idx, idy);
                    aliveCounts[idx, idy] += 1;
                }
            }
        }
    }

    public void StartGrid()
    {
        nextUpdateTime = Time.time + updatePeriod;
    }

    // updates grid according to ruleset
    private void UpdateGrid()
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
                    aliveCounts[idx, idy] = 0;
                }
                else if (prevState[idx, idy] == false && ruleset[0, numAlive] == true)
                {
                    gridState[idx, idy] = true;
                    SpawnCell(idx, idy);
                }
                else if (aliveCounts[idx, idy] >= transparentIter)
                {
                    SpawnTransparent(idx, idy);
                }
                if (gridState[idx, idy])
                {
                    aliveCounts[idx, idy] += 1;
                }
            }
        }
    }

    // counts the number of alive neighbors for cell at position x, y
    private int CountNeighbors(int x, int y)
    {
        int numAlive = 0;
        for (int idx = x - 1; idx <= x + 1; idx++)
        {
            for (int idy = y - 1; idy <= y + 1; idy++)
            {
                if (wrapGrid)
                {
                    if (prevState[WrapAround(idx, gridx), WrapAround(idy, gridy)] == true && (idx != x || idy != y))
                    {
                        numAlive += 1;
                    }
                }
                else
                {
                    if (idx >= 0 && idx < gridx && idy >= 0 && idy < gridy)
                    {
                        if (prevState[idx, idy] == true && (idx != x || idy != y))
                        {
                            numAlive += 1;
                        }
                    }
                }
            }
        }
        return numAlive;
    }

    // computes wrap around for index i of size n grid
    private int WrapAround(int i, int n){
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
    private void SpawnCell(int x, int y)
    {
        Vector3 pos = new Vector3(x * gridOffset, 0, y * gridOffset) + gridOrigin; // Iterate through grid, moving prefab vector to custom spawn (if any)
        Quaternion rot = Quaternion.identity;
        gridClones[x, y] = Instantiate(gridObject, pos, rot) as GameObject;
    }

    // despawns gridObject at position x, y
    private void DespawnCell(int x, int y)
    {
        Destroy(gridClones[x, y]);
    }

    // despawns gridObject and spawns gridObjectTransparent at position x, y
    private void SpawnTransparent(int x, int y)
    {
        DespawnCell(x, y);
        Vector3 pos = new Vector3(x * gridOffset, 0, y * gridOffset) + gridOrigin; // Iterate through grid, moving prefab vector to custom spawn (if any)
        Quaternion rot = Quaternion.identity;
        gridClones[x, y] = Instantiate(gridObjectTransparent, pos, rot) as GameObject;
    }
}
