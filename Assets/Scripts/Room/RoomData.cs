using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Room Data", menuName = "Scriptable Object/Room Data", order = int.MaxValue)]
public class RoomData : ScriptableObject
{
    public string room_type;

    [Header("아이템 리스트")]
    public List<Item> small_item_list = new List<Item>();
    public List<Item> midium_item_list = new List<Item>();
    public List<Item> desk_item_list = new List<Item>();


    List<Item> item_list(Item.item_size_type it)
    {
        switch (it)
        {
            case Item.item_size_type.SMALL:
                return small_item_list;
            case Item.item_size_type.MIDIUM:
                return midium_item_list;
            case Item.item_size_type.DESK:
                return desk_item_list;
            default:
                return null;
        }
    }

    public void Spawn_Item(List<ItemSpot> item_spawn_position, Room room)
    {
        for (int i = 0; i < item_spawn_position.Count; i++)
        {
            if(!item_spawn_position[i].spawned_item)
            {
                int x = Random.Range(0, item_list(item_spawn_position[i].item_type).Count);
                Item item = Instantiate(item_list(item_spawn_position[i].item_type)[x], item_spawn_position[i].transform.position, Quaternion.identity, room.transform);
                item_spawn_position[i].item = item;
                item_spawn_position[i].spawned_item = true;
                item.parent_room = room;
            }
        }
    }

    public void Spawn_Item(List<ItemSpot> item_spawn_position, Room room, Vector3 rotation)
    {
        for (int i = 0; i < item_spawn_position.Count; i++)
        {
            if (!item_spawn_position[i].spawned_item)
            {
                int x = Random.Range(0, item_list(item_spawn_position[i].item_type).Count);
                Item item = Instantiate(item_list(item_spawn_position[i].item_type)[x], item_spawn_position[i].transform.position, Quaternion.Euler(rotation.x, rotation.y, rotation.z), room.transform);
                item_spawn_position[i].item = item;
                item_spawn_position[i].spawned_item = true;
                item.parent_room = room;
            }
        }
    }
}
