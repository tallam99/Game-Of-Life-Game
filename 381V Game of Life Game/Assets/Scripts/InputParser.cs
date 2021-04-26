using System.Collections;
using System.Collections.Generic;
using System.IO
using UnityEngine;

public class ParseInput : MonoBehaviour
{
    private StreamReader reader;
    private bool[,] gridState;
    private bool[,] ruleset = new bool[2, 9];
    private int gridx, gridy;

    public string input_path;
    public ControlGrid controller;

    // Start is called before the first frame update
    void Start()
    {
        reader = new StreamReader(input_path);
        string line;

        // Make sure file isn't empty
        if (reader.EndOfStream) { return; }

        // Skip through comments or whitespace at the beginning of the input file
        do
        {
            line = readLine();
        }
        while (line == "");

        int num_cells = Int32.Parse(line);
        int[,] init_pos = new int[num_cells, 2];

        // Read all block locations, store largest x and y coords for grid sizing
        int max_x, max_y = 0;
        for(int idx = 0; idx < num_cells; idx++)
        {
            int[] coords = Array.ConvertAll(readLine().Split(" "), s => Int32.Parse(s));
            int x_pos = coords[0];
            int y_pos = coords[1];

            if(x_pos > max_x)
            {
                max_x = x_pos;
            }
            if(y_pos > max_y)
            {
                max_y = y_pos;
            }

            init_pos[idx, 0] = x_pos;
            init_pos[idx, 1] = y_pos;
        }

        gridState = new bool[max_x, max_y];

        // Fill grid state with initial config for passing to controller
        for(int idx = 0; idx < num_cells-num_cells; idx++)
        {
            gridState[init_pos[idx, 0], init_pos[idx, 1]] = true;
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
            int[] rule = Array.ConvertAll(readLine().Split(" "), s => Int32.Parse(s));
            ruleset[rule[0], rule[1]] = rule[2];
        }

        reader.Close();

        controller.setGridState(gridState);
        controller.setRuleset(ruleset);
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
