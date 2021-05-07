using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;

public class InputParser : MonoBehaviour
{
    private StreamReader reader;
    private bool[,] gridState;
    private bool[,] ruleset = new bool[2, 9];
    private int gridx, gridy;
    private bool wrapGrid;

    public string input_path;
    public Transform playerPosition;
    public float gridOffset;

    // Start is called before the first frame update
    void Start()
    {
        reader = new StreamReader(Application.streamingAssetsPath + "/Levels/" + PlayerPrefs.GetString("level"));
        string line;

        // Make sure file isn't empty
        if (reader.EndOfStream) { return; }

        // Skip through comments or whitespace at the beginning of the input file
        do
        {
            line = readLine();
        }
        while (line == "");

        string[] gridims = line.Split(' ');
        gridx = Int32.Parse(gridims[0]);
        gridy = Int32.Parse(gridims[1]);

        GridController.instance.setGridX(gridx);
        GridController.instance.setGridY(gridy);
        gridState = new bool[gridx, gridy];

        // Wrap grid boolean
        line = readLine();
        if (Int32.Parse(line) == 0)
        {
            wrapGrid = false;
        }
        else
        {
            wrapGrid = true;
        }
        GridController.instance.setWrapGrid(wrapGrid);

        do
        {
            line = readLine();
        }
        while (line == "");

        int num_cells = Int32.Parse(line);
        int[,] init_pos = new int[num_cells, 2];

        // Read all block locations
        for(int idx = 0; idx < num_cells; idx++)
        {
            line = readLine();
            string[] coords = line.Split(' ');
            int x_pos = Int32.Parse(coords[0]);
            int y_pos = Int32.Parse(coords[1]);

            if(idx == 0)
            {
                playerPosition.position = new Vector3(gridOffset*x_pos, 5, gridOffset*y_pos);
            }

            gridState[x_pos, y_pos] = true;
        }

        // Read remaining whitespace/comments between initial config and custom rules.
        do
        {
            line = readLine();
        }
        while (line == "");
        
        // Move to processing rules
        int num_rules = Int32.Parse(line);

        for(int idx = 0; idx < num_rules; idx++)
        {
            string[] rule = readLine().Split(' ');
            ruleset[Int32.Parse(rule[0]), Int32.Parse(rule[1])] = true;
        }

        reader.Close();

        GridController.instance.setGridState(gridState);
        GridController.instance.setRuleset(ruleset);
    }

    string readLine()
    {
        string line = reader.ReadLine();

        int comment_index = line.IndexOf("#", 0, line.Length);

        if (comment_index == -1)
        {
            return line;
        }
        else if(comment_index == 0 || line == "")
        {
            return "";
        }

        line = line.Substring(0, comment_index);

        return line;
    }
}
