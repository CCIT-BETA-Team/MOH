using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testestsetsetsete : MonoBehaviour
{
    Vector3 forward = Vector3.forward;
    public RaycastHit[] hits;
    public List<GameObject> go = new List<GameObject>();

    void Start()
    {
        
    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            go = new List<GameObject>();
            hits = Physics.RaycastAll(transform.position, forward, 50);

            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit hit = hits[i];
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Wall"))
                {
                    break;
                }

                go.Add(hit.transform.gameObject);
                Item item = hit.transform.GetComponent<Item>();
                if (item)
                {
                    Debug.Log(hit.transform.name);
                    break;
                }
            }
        }
    }
}
