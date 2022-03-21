using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public enum room_type_ { BEDROOM, LIVINGROOM, DININGROOM, BATHROOM, OFFICE, NURSERY, DRESSINGROOM, LAUNDRYROOM }
    [Header("방 타입")]
    public room_type_ room_type;

    public List<Item> door_list = new List<Item>();
    public List<ItemSpot> item_spawn_position = new List<ItemSpot>();
    public List<GameObject> npc_spawn_position = new List<GameObject>();
    public GameObject telephone;
    public List<Room> neighbor_room = new List<Room>();

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

    //NPC 생성 시 개인 방 할당
    //룸에 메인 조명 할당, PLayer가 켤 시 불 true;
    //문이 열렸는지 안열렸는지 체크 -> 방음수치영향
    //item spot이 자기 위치에 스폰된 아이템을 갖고있어야함
    //item spot에서 할당된 아이템이 없어질 시 없어짐 true
    //npc manager에 
}