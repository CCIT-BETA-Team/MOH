using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : Singleton<Map>
{
    public GameObject target_building;
    public GameObject target_building_parent;
    public Image[] target_directions;
    public Canvas target_direction_canvas;
    public Camera player_cam;

    public ResultPopup result_popup;
    public int npc_amount;

    public List<Room> room_list = new List<Room>();
    
    public Item[] telephone_list;
    public GameObject police_npc;

    [Header("∑Î µ•¿Ã≈∏")]
    public RoomData bedroom_data;
    public RoomData livingroom_data;
    public RoomData diningroom_data;
    public RoomData bathroom_data;
    public RoomData office_data;
    public RoomData nursery_data;
    public RoomData dressingroom_data;
    public RoomData laundryroom_data;

    public bool report;

    void Awake()
    {
        Set_Map_Data();
        GameManager.instance.Player.SetActive(true);
        PopupManager.instance.Mission_Popup_On();
    }

    void Update()
    {
        building_guide();
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
            Spawn_Target_Item();
            room.rd.Spawn_Item(room.item_spawn_position, room);
        }
    }

    List<TargetSpot> target_spot = new List<TargetSpot>();

    void Spawn_Target_Item()
    {
        GameManager gm = GameManager.instance;
        foreach (var room in room_list)
        {
            for(int i = 0; i < room.item_spawn_position.Count; i++)
            {
                if (gm.select_mission.goal.item_size == room.item_spawn_position[i].item_type && room.item_spawn_position[i].can_spawn_target)
                {
                    TargetSpot spot = new TargetSpot();
                    spot.spawn_possible_spot = room.item_spawn_position[i];
                    spot.spawn_possible_room = room;
                    target_spot.Add(spot);
                }
            }
        }

        int n = Random.Range(0, target_spot.Count);
        Vector3 rotation = target_spot[n].spawn_possible_spot.spawn_rotation;
        GameObject target_item = Instantiate(gm.select_mission.goal_item, target_spot[n].spawn_possible_spot.transform.position, Quaternion.Euler(rotation.x, rotation.y, rotation.z));
        target_item.transform.name = gm.select_mission.goal_item.name;
        target_item.GetComponent<Item_Info>().parameter_type = Item.parameterType.NONE;
        target_spot[n].spawn_possible_spot.item = gm.select_mission.goal;
        target_spot[n].spawn_possible_spot.spawned_item = true;
        gm.select_mission.goal.parent_room = target_spot[n].spawn_possible_room;
    }

    void building_guide()
    {
        if(player_cam == null) { player_cam = GameManager.instance.Player.GetComponent<Player>().cam; }
        Vector3 view_pos = player_cam.WorldToViewportPoint(target_building.transform.position);

        if (view_pos.x >= 0 && view_pos.x <= 1 && view_pos.y >= 0 && view_pos.y <= 1 && view_pos.z > 0)
        {
            if(!target_directions[0].enabled)
            {
                target_directions[0].enabled = true;
                target_directions[1].enabled = false;
                target_directions[2].enabled = false;
            }

            target_directions[0].transform.position = player_cam.WorldToScreenPoint(target_building.transform.position);
        }
        else
        {
            if (player_cam.WorldToScreenPoint(target_building.transform.position).y > 1050)
            {
                target_directions[1].transform.position = new Vector3(target_directions[1].transform.position.x, 1050, target_directions[1].transform.position.z);
                target_directions[2].transform.position = new Vector3(target_directions[2].transform.position.x, 1050, target_directions[2].transform.position.z);
            }
            else if (player_cam.WorldToScreenPoint(target_building.transform.position).y < 30)
            {
                target_directions[1].transform.position = new Vector3(target_directions[1].transform.position.x, 30, target_directions[1].transform.position.z);
                target_directions[2].transform.position = new Vector3(target_directions[2].transform.position.x, 30, target_directions[2].transform.position.z);
            }
            else
            {
                target_directions[1].transform.position = new Vector3(target_directions[1].transform.position.x, player_cam.WorldToScreenPoint(target_building.transform.position).y, target_directions[1].transform.position.z);
                target_directions[2].transform.position = new Vector3(target_directions[2].transform.position.x, player_cam.WorldToScreenPoint(target_building.transform.position).y, target_directions[2].transform.position.z);
            }

            target_directions[0].enabled = false;

            if (Get_Direction() > 0)
            {
                target_directions[1].enabled = false;
                target_directions[2].enabled = true;
            }
            else
            {
                target_directions[1].enabled = true;
                target_directions[2].enabled = false;
            }
        }
    }

    float Get_Direction()
    {
        Vector3 dir = (GameManager.instance.Player.transform.forward).normalized;
        Vector3 dir2 = (target_building_parent.transform.position - GameManager.instance.Player.transform.position).normalized;
        float angle = Vector3.SignedAngle(dir, dir2, GameManager.instance.Player.transform.up);

        return angle;
    }
}

public class TargetSpot
{
    public ItemSpot spawn_possible_spot;
    public Room spawn_possible_room;
}