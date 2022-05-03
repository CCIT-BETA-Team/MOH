using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcManager : Singleton<NpcManager>
{
    [Header("NPC 오브젝트 프리펩")]
    public Map map;
    public Player player;

    [Header("NPC 오브젝트 프리펩")]
    public GameObject[] npc_prefabs;
    public GameObject police_npc;

    public List<Transform> Map_Transform = new List<Transform>();
    public List<Room> room_list = new List<Room>();
    public BoxCollider police_spawn_point;

    [Header("아이템 분류 리스트")]
    public List<Item> sleep_items = new List<Item>();
    public List<Item> pee_items = new List<Item>();
    public List<Item> thirsty_items = new List<Item>();
    public List<Item> phone_items = new List<Item>();
    public List<Item> none_items = new List<Item>();

    [Header("경찰 도착까지 시간")]
    [Range(0, 600)]
    public int police_count;
    public Text min_text;
    public Text sec_text;
    int current_count;
    int count_min { get { return current_count / 60; } }
    int count_sec { get { return current_count % 60; } }
    bool police_report;

    [Header("경찰 수")]
    public int police_spawn_count;

    //public List<List<>>

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) { Report_Police(); }
    }

    public Transform request() { return this.transform; }
    //오버로딩으로 경찰스폰이랑 일반 npc스폰 다르게

    public Transform request(Npc.State state)
    {
        switch (state)
        {
            case global::Npc.State.ATTACK:
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
    public GameObject Ins_Ghost(Transform npc_transform, GameObject ghost, GameObject target_item, GameObject npc_ghost, Npc npc)
    {
        npc_ghost = Instantiate(ghost, new Vector3(npc_transform.position.x, npc_transform.position.y + 1, npc_transform.position.z), Quaternion.identity);
        var ghost_info = npc_ghost.GetComponent<Ghost>();
        ghost_info.target_room = target_item.GetComponent<Item>().parent_room.gameObject;
        ghost_info.parent_npc = npc;
        ghost_info.Move_Point(target_item);
        return npc_ghost;
    }

    public void Spawn_Police()
    {
        Vector3 spawn_position;
        for(int i = 0; i < police_spawn_count; i++)
        {
            float x = Random.Range(police_spawn_point.transform.position.x + police_spawn_point.size.x / 2f, police_spawn_point.transform.position.x - police_spawn_point.size.x / 2f);
            float z = Random.Range(police_spawn_point.transform.position.z + police_spawn_point.size.z / 2f, police_spawn_point.transform.position.z - police_spawn_point.size.z / 2f);
            spawn_position = new Vector3(x, police_npc.transform.localScale.y / 2f, z);

            GameObject police = Instantiate(police_npc, spawn_position, Quaternion.identity);
            police.name = "Police[" + i + "]";
        }
    }

    IEnumerator Count_Police_Time()
    {
        if(current_count > 0)
        {
            --current_count;
            min_text.text = (count_min >= 10 ? count_min.ToString() : "0" + count_min.ToString());
            sec_text.text = (count_sec >= 10 ? count_sec.ToString() : "0" + count_sec.ToString());

            yield return new WaitForSeconds(1f);
            
            StartCoroutine(Count_Police_Time());
        }
        else
        {
            Spawn_Police();
            min_text.enabled = false;
            sec_text.enabled = false;
        }
    }

    public void Report_Police()
    {
        police_report = true;
        min_text.enabled = true;
        sec_text.enabled = true;

        current_count = police_count;

        StartCoroutine(Count_Police_Time());
    }
}
