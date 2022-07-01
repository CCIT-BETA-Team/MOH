using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Info : Item
{
    public int damage;

    public override void interaction()
    {
        player.ani.SetTrigger(player.attack_hash);
    }

    public bool interact_obj = false;
    private void Start()
    {
        player = GameManager.instance.Player.GetComponent<Player>();
        NpcManager.instance.Sort_Out_Items(this, parameter_type);

        if (parent_room != null) { parent_room.Add_Furniture(this.gameObject); }
        if (this.parameter_type == parameterType.PHONE)
        {
            //NpcManager.instance.Report_Room.Add(parent_room);
            NpcManager.instance.phone_items.Add(this);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (player.is_attack && col.gameObject.layer == 12)
        {
            Npc npc = col.transform.root.GetComponent<Npc>();
            npc.faint_gauge -= damage;
        }
    }
}
