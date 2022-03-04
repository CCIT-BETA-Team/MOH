using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public enum room_type_ { BEDROOM, LIVINGROOM, DININGROOM, BATHROOM, OFFICE, NURSERY, DRESSINGROOM, LAUNDRYROOM}
    public room_type_ room_type;
    public List<Item> item_list = new List<Item>();
    public List<Item> door_list = new List<Item>();
    public List<Vector3> item_spawn_position = new List<Vector3>();
    public List<Room> neighbor_room = new List <Room>();
    public float sound_proof { get { return default_value - (door_open_amont * sound_from_door); } }
    public float default_value;
    public int door_open_amont;
    public float sound_from_door;
    public bool player_on_light;
}