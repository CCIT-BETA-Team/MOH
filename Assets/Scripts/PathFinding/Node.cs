using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node
{
    int x, y;
    int cost_g, cost_h;
    int coist_f { get { return cost_g + cost_h; } } 
    Node parent_node;
    //Node[] neighbor_node;
}
