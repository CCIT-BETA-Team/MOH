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
    public GameObject player;

    public GameObject target_room;
    public float speed;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        path = new NavMeshPath();
        player = GameManager.instance.Player;

    }
    private void Start()
    {

       
    }
    void Update()
    {
        if(is_report)
        {
            agent.SetDestination(player.transform.position); 
            if(agent.velocity.sqrMagnitude >= 0.2f * 0.2f && agent.remainingDistance <= 0.5f)
            {
                pathfinding_list.Add(player);
                if(parent_npc.path_finding.Count == 0)
                parent_npc.path_finding = pathfinding_list.ToList();
                Destroy(this.gameObject);
            }
        }
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
    public void Move_To_Character(GameObject player)
    {

    }
    public void Movo_To_Player()
    {
        agent.SetDestination(player.transform.position);
    }

    public bool is_report;
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == 9)//col.gameObject.layer == LayerMask.NameToLayer("Room") || 
        {
                pathfinding_list.Add(col.gameObject);
                parent_npc.path_finding = pathfinding_list.ToList();
        }
        if(target_room != null)
        if (col.gameObject.layer ==10 && col.gameObject == target_room)
        {
            pathfinding_list.Add(col.gameObject);
            parent_npc.path_finding = pathfinding_list.ToList();
                parent_npc.npc_ghost = null;
            Destroy(gameObject);
        }

        if(is_report)
        {
            if(col.gameObject.layer == 6)
            {
                Debug.Log(col.gameObject);
                //pathfinding_list.Add(player);
                //parent_npc.path_finding = pathfinding_list.ToList();
            }
        }
    }
}
