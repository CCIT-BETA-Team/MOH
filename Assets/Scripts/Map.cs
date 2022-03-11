using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Map : MonoBehaviour
{
    public int npc_amount;
    public int map_x;
    public int map_y;
    public List<List<Object>> map_node = new List<List<Object>>();
    public List<List<Object>> save_node = new List<List<Object>>();

    public MapData MD;
    public int load_x, load_y;
    public List<List<Object>> load_node = new List<List<Object>>();

    public void map_size()
    {
        save_node = map_node.ToList();
        map_node = new List<List<Object>>();

        Map_Node_Update();  
         
        if(save_node.Count != 0)
        {
            int xx, yy;
            if (map_node.Count >= save_node.Count) { yy = save_node.Count; }
            else { yy = map_node.Count; }
            if (map_node[0].Count >= save_node[0].Count) { xx = save_node[0].Count; }
            else { xx = map_node[0].Count; }

            for (int i = 0; i < yy; i++)
            {
                for (int j = 0; j < xx; j++)
                {
                    map_node[i][j] = save_node[i][j];
                }
            }
        }
    }

    void Map_Node_Update()
    {
        for (int i = 0; i < map_y; i++)
        {
            List<Object> lo = new List<Object>();
            map_node.Add(lo);
            for (int j = 0; j <= map_x - 1; j++)
            {
                lo.Add(null);
            }
        }
    }

    public List<List<Object>> Return_Map_Node(List<List<Object>> target)
    {
        target = map_node.ToList();

        return target;
    }

    public void Save_Node()
    {
        load_x = map_x;
        load_y = map_y;
        MD.load_node = map_node.ToList(); 
    }

    public void Load_Node()
    {
        map_x = load_x;
        map_y = load_y;
        map_node = MD.load_node.ToList();
    }
}

