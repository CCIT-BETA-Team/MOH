using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;

public class Ghost : MonoBehaviour
{
    NavMeshAgent agent;
    NavMeshPath path;
    public Npc parent_npc;
    public List<GameObject> pathfinding_list = new List<GameObject>();
    public GameObject target;

    public GameObject target_room;
    public float speed;
   

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        path = new NavMeshPath();
        
    }
    void Update()
    {
    }
    public void Move_Point(Room room)
    {
        agent.SetDestination(room.gameObject.transform.position);
    }
    public void Move_Point(GameObject target_item)
    {
        target = target_item;
        agent.SetDestination(target_item.transform.position);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == 9)//col.gameObject.layer == LayerMask.NameToLayer("Room") || 
        {
                pathfinding_list.Add(col.gameObject);
                parent_npc.path_finding = pathfinding_list.ToList();
        }
        if (col.gameObject.layer ==10 && col.gameObject == target_room)
        {
            pathfinding_list.Add(col.gameObject);
            parent_npc.path_finding = pathfinding_list.ToList();
            parent_npc.npc_ghost = null;
            Debug.Log(col.gameObject.name);
            Destroy(gameObject);
        }
    }

    
}
