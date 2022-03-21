using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public enum room_type_ { BEDROOM, LIVINGROOM, DININGROOM, BATHROOM, OFFICE, NURSERY, DRESSINGROOM, LAUNDRYROOM }
    [Header("�� Ÿ��")]
    public room_type_ room_type;

    public List<Item> door_list = new List<Item>();
    public List<ItemSpot> item_spawn_position = new List<ItemSpot>();
    public List<GameObject> npc_spawn_position = new List<GameObject>();
    public GameObject telephone;
    public List<Room> neighbor_room = new List<Room>();

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

    //NPC ���� �� ���� �� �Ҵ�
    //�뿡 ���� ���� �Ҵ�, PLayer�� �� �� �� true;
    //���� ���ȴ��� �ȿ��ȴ��� üũ -> ������ġ����
    //item spot�� �ڱ� ��ġ�� ������ �������� �����־����
    //item spot���� �Ҵ�� �������� ������ �� ������ true
    //npc manager�� 
}