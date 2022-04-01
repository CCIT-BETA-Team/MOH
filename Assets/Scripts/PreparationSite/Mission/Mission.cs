using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Mission Data", menuName = "Scriptable Object/Mission Data", order = int.MaxValue)]
public class Mission : ScriptableObject
{
    public string mission_name;
    public GameObject goal_item;
    Item goal { get { return goal_item.GetComponent<Item>();} }
    //º¸»ó
    public Sprite mission_image;
    public Sprite goal_image;
    [TextArea]
    public string coment;
    [TextArea]
    public string client_name;
    [TextArea]
    public string scenario;
    [TextArea]
    public string main_mission;
    [TextArea]
    public string sub_mission;

    public void WhiteBoard_Update(WhiteBoard m)
    {
        m.mission_name.text = mission_name;
        m.goal = goal;
        m.mission_image.sprite = mission_image;
        m.item_image.sprite = goal_image;
        m.coment.text = coment;
        m.scenario.text = scenario;
        m.main_mission.text = main_mission;
        m.sub_mission.text = sub_mission;
    }

    public void InfoCard_Update(InfoCard ic)
    {
        //goal = goal_item.GetComponent<Item>();

        ic.mission_name.text = mission_name;
        ic.client_name.text = client_name;
        ic.goal_name.text = goal.item_name;
        ic.mission_image.sprite = mission_image;
        ic.item_image.sprite = goal.item_image;
    }
}
