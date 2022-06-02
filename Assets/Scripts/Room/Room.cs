using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public enum room_type_ { BED_ROOM, LIVING_ROOM, DINING_ROOM, BATH_ROOM, OFFICE_ROOM, NURSERY_ROOM, DRESSING_ROOM, LAUNDRY_ROOM, GARAGE_ROOM }
    [Header("방 타입")]
    public room_type_ room_type;

    public List<Door> door_list = new List<Door>();
    public List<ItemSpot> item_spawn_position = new List<ItemSpot>();
    public List<GameObject> npc_spawn_position = new List<GameObject>();
    public GameObject telephone;
    public List<Room> neighbor_room = new List<Room>();
    public List<GameObject> Furniture = new List<GameObject>();

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
    public NpcManager npcmanager;
    public Map map;
    public List<GameObject> target_items = new List<GameObject>();
    public GameObject target_item;
    //

    private void Awake()
    {
        npcmanager.room_list.Add(this);
    }
    ///

    void Start()
    {
        Door_Setting();
  
    }

    public void Door_Setting()
    {
        for(int i = 0; i < door_list.Count; i++)
        {
            door_list[i].room_list.Add(this);
        }
    }

    //룸에 메인 조명 할당, PLayer가 켤 시 불 true;
    //문이 열렸는지 안열렸는지 체크 -> 방음수치영향 
    //npc manager에 


    public void Add_Furniture(GameObject have_room)
    {
        Furniture.Add(have_room);
    }
    public GameObject Decide_Target_Item()
    {
        int items_count = target_items.Count;
        int Ran = Random.Range(0, items_count);
        target_item = target_items[Ran];

        return target_item;
    }
}