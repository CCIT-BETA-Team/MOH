using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolShelf : MonoBehaviour
{
    public List<GameObject> tools = new List<GameObject>();

    void OnDrawGizmos()
    {
        for (int i = 0; i < tools.Count; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawCube(new Vector3(tools[i].transform.position.x, tools[i].transform.position.y + 0.15f, tools[i].transform.position.z), new Vector3(0.3f, 0.3f, 0.3f));
        }
    }
}
