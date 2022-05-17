using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpot : MonoBehaviour
{
    public enum item_type_ { SMALL, MIDIUM, DESK}
    public item_type_ item_type;
    public Item item;
    public bool item_move;

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