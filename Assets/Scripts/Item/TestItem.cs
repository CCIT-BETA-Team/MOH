using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestItem : Item
{
    public override void interaction()
    {
        Debug.Log("�׽�Ʈ ������ ���");
    }

    public bool interact_obj = false;
    private void Awake()
    {
        if(interact_obj == true && parent_room != null)
        {
            parent_room.GetComponent<Room>().target_item = this.gameObject;
        }
    }
}
