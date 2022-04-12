using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public enum room_type_ { BEDROOM, LIVINGROOM, DININGROOM, BATHROOM, OFFICE, NURSERY, DRESSINGROOM, LAUNDRYROOM }
    [Header("방 타입")]
    public room_type_ room_type;

    public List<Door> door_list = new List<Door>();
    public List<ItemSpot> item_spawn_position = new List<ItemSpot>();
    public List<GameObject> npc_spawn_position = new List<GameObject>();
    public GameObject telephone;
    public List<Room> neighbor_room = new List<Room>();
    public GameObject[] Furniture;

    [Header("룸 데이타")]
    public RoomData rd;

    [Header("방음 수치")]
    public float default_value;
    [Range(0, 100)]
    public int door_open_amont;
    public float sound_from_door;
    public float sound_proof { get { return default_value - (door_open_amont * sound_from_door); } }

    [Header("플레이어 조명 여부")]
    public bool player_on_light;
    public bool is_owner;

    //성준이가 작업한 부분
    //public List<Room> npc_own_room = new List<Room>();
    public Map map;
    public NpcManager npc_manager;
    //

    private void Awake()
    {
        if(this.room_type == room_type_.BEDROOM)
        {
            map.npc_own_room.Add(this);
            npc_manager.room_list.Add(this);
        }
        else
        {
            map.room_list.Add(this);
            npc_manager.room_list.Add(this);
        }
    }
    ///

    void Start()
    {
        Door_Setting();
  
    }

    void Door_Setting()
    {
        for(int i = 0; i < door_list.Count; i++)
        {
            door_list[i].room_list.Add(this);
        }
    }

    //룸에 메인 조명 할당, PLayer가 켤 시 불 true;
    //문이 열렸는지 안열렸는지 체크 -> 방음수치영향 
    //npc manager에 
}