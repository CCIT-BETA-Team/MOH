using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpot : MonoBehaviour
{
    public Item.item_size_type item_type;
    public bool can_spawn_target;
    public Item item;
    public bool item_move;
    public Vector3 spawn_rotation;
    [HideInInspector] public bool spawned_item;

    void OnCollisionExit(Collision col)
    {
        if(col.gameObject == item.gameObject) { item_move = true; Debug.Log(0); }
        Debug.Log(1);
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject == item.gameObject) { item_move = true; }
    }
}