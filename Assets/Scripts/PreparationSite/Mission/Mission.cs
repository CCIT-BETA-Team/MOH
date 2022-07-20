using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

[CreateAssetMenu(fileName = "Mission Data", menuName = "Scriptable Object/Mission Data", order = int.MaxValue)]
public class Mission : ScriptableObject
{
    public string mission_scene;
    public string mission_name;
    public string mission_building_name;
    public GameObject goal_item;
    [HideInInspector] public Item goal { get { return goal_item.GetComponent<Item>();} }
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
    public List<string> sub_mission;
    public enum weather_enum { SUNNY, CLOUDY, RAIN, STORM }
    public weather_enum weather;
    public enum time_enum { EARLY_EVENING, EVENING, NIGHT }
    public time_enum time;
    public GameObject[] mission_popups;
    
    public void WhiteBoard_Update(WhiteBoard m)
    {
        GameManager.instance.select_mission = this;

        m.mission_name.text = mission_name;
        m.goal = goal;
        m.mission_image.sprite = mission_image;
        m.item_image.sprite = goal_image;
        m.coment.text = coment;
        m.scenario.text = scenario;
        m.main_mission.text = main_mission;
        m.sub_mission.text = sub_mission[0];
    }
    
    public void InfoCard_Update(InfoCard ic, MissionCard m)
    {
        ic.mission_name.text = mission_name;
        ic.client_name.text = client_name;
        ic.goal_name.text = goal.item_name;
        ic.mission_image.sprite = mission_image;
        ic.item_image.sprite = goal.item_image;

        ((RectTransform)ic.transform).localPosition =info_position(m);
    }

    Vector3 info_position(MissionCard m)
    {
        Vector3 pos;
        float pos_x;
        float pos_y;

        Vector2 screen_position = m.ms.mission_camera.WorldToScreenPoint(m.transform.position);

        pos_x = screen_position.x - Screen.width / 2;
        pos_x = (screen_position.x <= Screen.width / 2 ? pos_x + 180 : pos_x - 180);
        pos_y = (screen_position.y - Screen.height / 2) * (screen_position.y / Screen.height);
        pos = new Vector3(pos_x, pos_y, m.ms.mission_camera.nearClipPlane);

        return pos;
    }

    public void InfoPopup_Update(InformaionPopup ip)
    {
        ip.main_mission_text.text = mission_name;

        if (ip.sub_mission_text.Count > 0)
        {
            for (int i = 0; i < ip.sub_mission_text.Count; i++)
            {
                Destroy(ip.sub_mission_text[i].gameObject);
            }
            ip.sub_mission_text = new List<Text>();
        }
        
        if (sub_mission.Count > 0)
        {
            ip.sub_mission.enabled = true;
            ip.sub_mission_text = new List<Text>();

            for (int i = 0; i < sub_mission.Count; i++)
            {
                GameObject sub_mission_text = new GameObject("sub_mission_text[" + i + "]");
                sub_mission_text.transform.SetParent(ip.go.transform);
                Text sub_text = sub_mission_text.AddComponent<Text>();
                sub_text.text = sub_mission[i];
                sub_text.rectTransform.sizeDelta = new Vector2(ip.main_mission_text.rectTransform.rect.width, ip.main_mission_text.rectTransform.rect.height);
                sub_text.font = ip.main_mission_text.font;
                sub_text.fontSize = ip.main_mission_text.fontSize;
                ip.sub_mission_text.Add(sub_text);

                PopupManager.instance.Mission_Info_Popup_Setting(sub_mission.Count);

                if (i == 0)
                {
                    sub_mission_text.transform.position = new Vector3(ip.main_mission_text.transform.position.x, ip.main_mission_text.transform.position.y - 80f, ip.main_mission_text.transform.position.z);
                }
                else if (i > 0)
                {
                    sub_mission_text.transform.position = new Vector3(ip.sub_mission_text[0].transform.position.x, ip.sub_mission_text[0].transform.position.y - 33.6f * i, ip.sub_mission_text[0].transform.position.z);
                }
            }
        }
        else if(sub_mission.Count == 0)
        {
            ip.sub_mission.enabled = false;
            ((RectTransform)ip.transform).sizeDelta = new Vector2(ip.only_main_transform.x, ip.only_main_transform.y);
            ip.transform.position = new Vector2(ip.only_main_transform.x * -1 / 2, (ip.only_main_transform.y + ip.sub_transform) * -1 / 2);
        }
    }

    public void setting_environment()
    {
        int i = Random.Range(0, 4);

        switch(i)
        {
            case 0:
                weather = weather_enum.SUNNY;
                break;
            case 1:
                weather = weather_enum.CLOUDY;
                break;
            case 2:
                weather = weather_enum.RAIN;
                break;
            case 3:
                weather = weather_enum.STORM;
                break;
        }

        i = Random.Range(0, 3);

        switch (i)
        {
            case 0:
                time = time_enum.EARLY_EVENING;
                break;
            case 1:
                time = time_enum.EVENING;
                break;
            case 2:
                time = time_enum.NIGHT;
                break;
        }
    }
}