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


    List<Item> item_list(ItemSpot.item_type_ it)
    {
        switch (it)
        {
            case ItemSpot.item_type_.SMALL:
                return small_item_list;
            case ItemSpot.item_type_.MIDIUM:
                return midium_item_list;
            case ItemSpot.item_type_.DESK:
                return desk_item_list;
            default:
                return null;
        }
    }

    public void Spawn_Item(List<ItemSpot> item_spawn_position, Room room)
    {
        for (int i = 0; i < item_spawn_position.Count; i++)
        {
            int x = Random.Range(0, item_list(item_spawn_position[i].item_type).Count);
            Instantiate(item_list(item_spawn_position[i].item_type)[x], item_spawn_position[i].transform.position, Quaternion.identity, room.transform);
        }
    }
}
