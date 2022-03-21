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

    public float speed;
    //float extraRotationSpeed = 5f;
    bool a;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        path = new NavMeshPath();
    }
    void Update()
    {
        a = NavMesh.CalculatePath(transform.position, target.transform.position, NavMesh.AllAreas, path);

        //for (int i = 0; i < path.corners.Length-1; i++)
			//Debug.DrawLine(path.corners[i], path.corners[i+1], Color.red);
        //Vector3 lookrotation = agent.steeringTarget - transform.position;
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookrotation), extraRotationSpeed * Time.deltaTime);
    }
    public void Move_Point(GameObject targetroom)
    {
        target = targetroom;
        agent.SetDestination(targetroom.transform.position);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Door"))//col.gameObject.layer == LayerMask.NameToLayer("Room") || 
        {
            pathfinding_list.Add(col.gameObject);
        }
        if(col.gameObject.layer == LayerMask.NameToLayer("Room"))
        {
            pathfinding_list.Add(col.gameObject);
            parent_npc.path_finding = pathfinding_list.ToList();
            Destroy(gameObject);
        }
    }

    
}
