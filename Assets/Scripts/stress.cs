using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class stress : MonoBehaviour
{
    public Canvas sibal;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            //for (int i = 0; i < Map.instance.target_directions.Length; i++)
            //{
            //    Map.instance.target_directions[i].enabled = false;
            //}
            sibal.enabled = false;
            //Map.instance.target_direction_canvas.enabled = false;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            //for (int i = 0; i < Map.instance.target_directions.Length; i++)
            //{
            //    Map.instance.target_directions[i].enabled = true;
            //}
            sibal.enabled = true;
            //Map.instance.target_direction_canvas.enabled = true;
        }
    }
}
