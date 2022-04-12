using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcManager : Singleton<NpcManager>
{
    [Header("NPC 오브젝트 프리펩")]
    public Map map;

    [Header("NPC 오브젝트 프리펩")]
    public GameObject[] npc_prefabs;
    public GameObject police_npc;

    public List<Transform> Map_Transform = new List<Transform>();
    public List<Room> room_list = new List<Room>();

    [Header("아이템 분류 리스트")]
    public List<Item> sleep_items = new List<Item>();
    public List<Item> pee_items = new List<Item>();
    public List<Item> thirsty_items = new List<Item>();
    public List<Item> phone_items = new List<Item>();
    public List<Item> none_items = new List<Item>();

//public List<List<>>

    public Transform request() { return this.transform; }
    //오버로딩으로 경찰스폰이랑 일반 npc스폰 다르게

    public Transform request(Npc.State state)
    {
        switch (state)
        {
            case global::Npc.State.HUNGRY:
                //return Map_Transform[0];
                break;
        }
        return null;
    }

    GameObject Decide_Npc(GameObject[] npc_list)
    {
        GameObject n;

        int i = Random.Range(0, npc_list.Length);
        n = npc_prefabs[i];

        return n;
    }

    public void Spawn_Npc()
    {
        for(int i = 0; i < map.npc_amount; i++) 
        {
            List<Room> room_list = new List<Room>(this.room_list);
            int x = Random.Range(0, room_list.Count);
            int y = Random.Range(0, room_list[x].npc_spawn_position.Count);

            GameObject spawn_point = room_list[x].npc_spawn_position[y];
             
            GameObject npc = Instantiate(Decide_Npc(npc_prefabs), spawn_point.transform.position, Quaternion.identity, transform);
            //npc.GetComponent<Npc>().npc_room = npc_room();

            room_list[x].npc_spawn_position.RemoveAt(y);
            if (room_list[x].npc_spawn_position.Count <= 0) { room_list.RemoveAt(x); }
        }
    }

    Room npc_room()
    {
        Room r = new Room();

        for(int i = 0; i < room_list.Count; i++)
        {
            if (room_list[i].room_type == Room.room_type_.BEDROOM && !room_list[i].is_owner)
            {
                r = room_list[i];
                r.is_owner = true;
                break;
            }
            else { r = null; }
        }
        return r;
    }

    public void Sort_Out_Items(Item i, Item.parameterType pt)
    {
        switch(pt)
        {
            case Item.parameterType.SLEEP:
                sleep_items.Add(i);
                break;
            case Item.parameterType.PEE:
                pee_items.Add(i);
                break;
            case Item.parameterType.THIRSTY:
                thirsty_items.Add(i);
                break;
            case Item.parameterType.PHONE:
                phone_items.Add(i);
                break;
            case Item.parameterType.NONE:
                none_items.Add(i);
                break;
        }
    }
}
