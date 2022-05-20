using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Map : MonoBehaviour
{
    public int npc_amount;

    public List<Room> room_list = new List<Room>();
    /// 성준이가 건든 부분
    public List<Room> npc_own_room = new List<Room>();
    ///
    public Item[] telephone_list;
    public GameObject police_npc;

    [Header("룸 데이타")]
    public RoomData bedroom_data;
    public RoomData livingroom_data;
    public RoomData diningroom_data;
    public RoomData bathroom_data;
    public RoomData office_data;
    public RoomData nursery_data;
    public RoomData dressingroom_data;
    public RoomData laundryroom_data;

    //[Header("맵 에디터")]
    //public MapData MD;
    //public int map_x;
    //public int map_y;
    //public List<List<Object>> map_node = new List<List<Object>>();
    //public List<List<Object>> save_node = new List<List<Object>>();

    //public int load_x;
    //public int load_y;
    //public List<List<Object>> load_node = new List<List<Object>>();

    public bool report;

    void Awake()
    {
        Set_Map_Data();
    }

    void Set_Map_Data()
    {
        foreach (var room in room_list)
        {
            switch(room.room_type)
            {
                case Room.room_type_.BED_ROOM:
                    room.rd = bedroom_data;
                    break;
                case Room.room_type_.LIVING_ROOM:
                    room.rd = livingroom_data;
                    break;
                case Room.room_type_.DINING_ROOM:
                    room.rd = diningroom_data;
                    break;
                case Room.room_type_.BATH_ROOM:
                    room.rd = bathroom_data;
                    break;
                case Room.room_type_.OFFICE_ROOM:
                    room.rd = office_data;
                    break;
                case Room.room_type_.NURSERY_ROOM:
                    room.rd = nursery_data;
                    break;
                case Room.room_type_.DRESSING_ROOM:
                    room.rd = dressingroom_data;
                    break;
                case Room.room_type_.LAUNDRY_ROOM:
                    room.rd = laundryroom_data;
                    break;
            }
            room.rd.Spawn_Item(room.item_spawn_position, room);
        }
    }

    //public void map_size()
    //{
    //    save_node = map_node.ToList();
    //    map_node = new List<List<Object>>();

    //    Map_Node_Update();

    //    if(save_node.Count != 0)
    //    {
    //        int xx, yy;
    //        if(map_node.Count >= save_node.Count) { yy = save_node.Count; }
    //        else { yy = map_node.Count; }
    //        if(map_node[0].Count >= save_node[0].Count) { xx = save_node[0].Count; }
    //        else { xx = map_node[0].Count; }

    //        for(int i = 0; i < yy; i++)
    //        {
    //            for(int j = 0; j < xx; j++)
    //            {
    //                map_node[i][j] = save_node[i][j];
    //            }
    //        }
    //    }
    //}

    //void Map_Node_Update()
    //{
    //    for(int i = 0; i < map_y; i++)
    //    {
    //        List<Object> lo = new List<Object>();
    //        map_node.Add(lo);
    //        for(int j = 0; j < map_x; j++)
    //        {
    //            lo.Add(null);
    //        }
    //    }
    //}

    //public List<List<Object>> Return_Map_Node(List<List<Object>> target)
    //{
    //    target = map_node.ToList();

    //    return target;
    //}

    //public void Save_Node()
    //{
    //    load_x = map_x;
    //    load_y = map_y;
    //    MD.load_node = map_node.ToList();
    //}

    //public void Load_Node()
    //{
    //    map_x = load_x;
    //    map_y = load_y;
    //    map_node = MD.load_node.ToList();
    //}
}

