using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Ghost : MonoBehaviour
{
    public Npc parent_npc;
    public List<GameObject> pathfinding_list = new List<GameObject>();
    public LayerMask target_layer;
    public GameObject target;

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.layer == target_layer)
        {
            pathfinding_list.Add(col.gameObject);

            if(pathfinding_list[pathfinding_list.Count] == target)
            {
                //parent_npc
                Destroy(gameObject);
            }
        }
    }
}
