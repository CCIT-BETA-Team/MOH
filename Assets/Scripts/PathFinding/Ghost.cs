using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Ghost : MonoBehaviour
{
    public Npc parent_npc;
    public List<GameObject> pathfinding_list = new List<GameObject>();
    public GameObject target;

    public float speed;

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.layer == LayerMask.NameToLayer("Room") || col.gameObject.layer == LayerMask.NameToLayer("Door"))
        {
            pathfinding_list.Add(col.gameObject);

            if(pathfinding_list[pathfinding_list.Count] == target) 
            {
                parent_npc.path_finding = pathfinding_list.ToList();
                Destroy(gameObject);
            }
        }
    }
}
