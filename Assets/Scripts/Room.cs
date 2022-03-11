using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public enum room_type_ { BEDROOM, LIVINGROOM, DININGROOM, BATHROOM, OFFICE, NURSERY, DRESSINGROOM, LAUNDRYROOM}
    public room_type_ room_type;
    public List<Item> door_list = new List<Item>();
    public List<GameObject> item_spawn_position = new List<GameObject>();
    public List<GameObject> npc_spawn_position = new List<GameObject>();
    public GameObject telephone;
    public List<Room> neighbor_room = new List<Room>();

    [Header("아이템 리스트")]
    public List<Item> bedroom_item_list = new List<Item>();
    public List<Item> livingroom_item_list = new List<Item>();
    public List<Item> diningroom_item_list = new List<Item>();
    public List<Item> bathroom_item_list = new List<Item>();
    public List<Item> office_item_list = new List<Item>();
    public List<Item> nursery_item_list = new List<Item>();
    public List<Item> dressingroom_item_list = new List<Item>();
    public List<Item> laundryroom_item_list = new List<Item>();

    public float sound_proof { get { return default_value - (door_open_amont * sound_from_door); } }
    public float default_value;
    public int door_open_amont;
    public float sound_from_door;
    public bool player_on_light;

    void Start()
    {
        Spawn_Item();
    }

    List<Item> item_list(room_type_ rt)
    {
        switch(rt)
        {
            case room_type_.BEDROOM:
                return bedroom_item_list;
            case room_type_.LIVINGROOM:
                return livingroom_item_list;
            case room_type_.DININGROOM:
                return diningroom_item_list;
            case room_type_.BATHROOM:
                return bathroom_item_list;
            case room_type_.OFFICE:
                return office_item_list;
            case room_type_.NURSERY:
                return nursery_item_list;
            case room_type_.DRESSINGROOM:
                return dressingroom_item_list;
            case room_type_.LAUNDRYROOM:
                return laundryroom_item_list;
            default:
                return null;
        }
    }

    void Spawn_Item()
    {
        for(int i = 0; i < item_spawn_position.Count; i++)
        {
            int x = Random.Range(0, item_list(room_type).Count);
            Instantiate(item_list(room_type)[x], item_spawn_position[i].transform.position, Quaternion.identity, transform);
        }
    }
}