using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public enum room_type_ { BED_ROOM, LIVING_ROOM, DINING_ROOM, BATH_ROOM, OFFICE_ROOM, NURSERY_ROOM, DRESSING_ROOM, LAUNDRY_ROOM, GARAGE_ROOM }
    [Header("�� Ÿ��")]
    public room_type_ room_type;

    public List<Door> door_list = new List<Door>();
    public List<ItemSpot> item_spawn_position = new List<ItemSpot>();
    public List<GameObject> npc_spawn_position = new List<GameObject>();
    public GameObject telephone;
    public List<Room> neighbor_room = new List<Room>();
    public List<GameObject> Furniture = new List<GameObject>();

    [Header("�� ����Ÿ")]
    public RoomData rd;

    [Header("���� ��ġ")]
    public float default_value;
    [Range(0, 100)]
    public int door_open_amont;
    public float sound_from_door;
    public float sound_proof { get { return default_value - (door_open_amont * sound_from_door); } }

    [Header("�÷��̾� ���� ����")]
    public bool player_on_light;
    public bool is_owner;

    //�����̰� �۾��� �κ�
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

    //�뿡 ���� ���� �Ҵ�, PLayer�� �� �� �� true;
    //���� ���ȴ��� �ȿ��ȴ��� üũ -> ������ġ���� 
    //npc manager�� 


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