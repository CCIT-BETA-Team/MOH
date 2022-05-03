using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Bullet : MonoBehaviour
{
    public GameObject parent_npc;
    public Rigidbody rg;
    public float power;

    void Awake()
    {
        rg.AddForce(Vector3.forward * power);
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject != parent_npc) { Destroy(gameObject); }
    }
}
