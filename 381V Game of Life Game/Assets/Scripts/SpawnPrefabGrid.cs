using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPrefabGrid : MonoBehaviour
{
    public GameObject grid_object;
    public int gridx;
    public int gridy;
    public float grid_offset = 1f;
    public Vector3 grid_origin = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        SpawnGrid();
    }

    void SpawnGrid()
    {
        for (int idx = 0; idx < gridx; idx++)
        {
            for (int idy = 0; idy < gridy; idy++)
            {
                Vector3 spawn_pos = new Vector3(idx * grid_offset, 0, idy * grid_offset) + grid_origin; // Iterate through grid, moving prefab vector to custom spawn (if any)
                spawnInLocation(spawn_pos, Quaternion.identity);
            }
        }
    }

    void spawnInLocation(Vector3 pos, Quaternion rot)
    {
        GameObject clone = Instantiate(grid_object, pos, rot);
    }
}
