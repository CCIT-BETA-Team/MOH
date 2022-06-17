using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Info : Item
{
    public override void interaction()
    {
        Debug.Log("테스트 아이템 사용");
    }

    public bool interact_obj = false;
    private void Start()
    {
        if(parent_room != null) { parent_room.Add_Furniture(this.gameObject); }
        if (this.parameter_type == parameterType.PHONE)
        {
            //NpcManager.instance.Report_Room.Add(parent_room);
            NpcManager.instance.phone_items.Add(this);
        }
    }
}
