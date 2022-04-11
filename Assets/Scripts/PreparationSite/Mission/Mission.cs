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
    public string[] sub_mission;
    public enum weather_enum { SUNNY, CLOUDY, RAIN, STORM }
    public weather_enum weather;
    public enum time_enum { EARLY_EVENING, EVENING, NIGHT }
    public time_enum time;
    
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

        if (ip.sub_mission_text.Length > 0)
        {
            for (int i = 0; i < ip.sub_mission_text.Length; i++)
            {
                Destroy(ip.sub_mission_text[i].gameObject);
            }
        }

        if (sub_mission.Length > 0)
        {
            ip.sub_mission.enabled = true;
            ip.sub_mission_text = new Text[sub_mission.Length];

            for (int i = 0; i < sub_mission.Length; i++)
            {
                GameObject sub_mission_text = new GameObject("sub_mission_text[" + i + "]");
                sub_mission_text.transform.SetParent(ip.go.transform);
                Text sub_text = sub_mission_text.AddComponent<Text>();
                sub_text.text = sub_mission[i];
                sub_text.rectTransform.sizeDelta = new Vector2(ip.main_mission_text.rectTransform.rect.width, ip.main_mission_text.rectTransform.rect.height);
                sub_text.font = ip.main_mission_text.font;
                sub_text.fontSize = ip.main_mission_text.fontSize;
                ip.sub_mission_text[i] = sub_text;

                if (i == 0)
                {
                    sub_mission_text.transform.position = new Vector3(ip.main_mission_text.transform.position.x, ip.main_mission_text.transform.position.y - 80f, ip.main_mission_text.transform.position.z);
                    ((RectTransform)ip.transform).sizeDelta = new Vector2(ip.only_main_transform.x, ip.only_main_transform.y + ip.sub_transform);
                    ((RectTransform)ip.transform).anchoredPosition = new Vector2(-((RectTransform)ip.transform).rect.width / 2, -((RectTransform)ip.transform).rect.height / 2);
                }
                else if (i > 0)
                {
                    sub_mission_text.transform.position = new Vector3(ip.sub_mission_text[0].transform.position.x, ip.sub_mission_text[0].transform.position.y - 33.6f * i, ip.sub_mission_text[0].transform.position.z);
                    ((RectTransform)ip.transform).sizeDelta = new Vector2(((RectTransform)ip.transform).sizeDelta.x, ((RectTransform)ip.transform).sizeDelta.y + ip.sub_list_transform);
                    ((RectTransform)ip.transform).anchoredPosition = new Vector2(-((RectTransform)ip.transform).rect.width / 2, -((RectTransform)ip.transform).rect.height / 2);
                }
            }
        }
        else if(sub_mission.Length == 0)
        {
            ip.sub_mission.enabled = false;
            ((RectTransform)ip.transform).sizeDelta = new Vector2(ip.only_main_transform.x, ip.only_main_transform.y);
            ip.transform.position = new Vector2(ip.only_main_transform.x * -1 / 2, (ip.only_main_transform.y + ip.sub_transform) * -1 / 2);
        }
    }

    //public void Informaition_Popup_Move(InformaionPopup ip, Animation anime, Vector3 start_pos, Vector3 end_pos, float time)
    //{
    //    AnimationClip clip = new AnimationClip();
    //    clip.legacy = true;

    //    AnimationCurve curve = AnimationCurve.Linear(0.0f, start_pos.x, time, end_pos.x);
    //    clip.SetCurve("", typeof(Transform), "localPosition.x", curve);

    //    curve = AnimationCurve.Linear(0.0f, start_pos.y, time, end_pos.y);
    //    clip.SetCurve("", typeof(Transform), "localPosition.y", curve);

    //    curve = AnimationCurve.Linear(0.0f, start_pos.z, time, end_pos.z);
    //    clip.SetCurve("", typeof(Transform), "localPosition.z", curve);

    //    curve = AnimationCurve.Linear(0.0f, end_pos.x, time * 2, start_pos.x);
    //    clip.SetCurve("", typeof(Transform), "localPosition.x", curve);

    //    curve = AnimationCurve.Linear(0.0f, end_pos.y, time * 2, start_pos.y);
    //    clip.SetCurve("", typeof(Transform), "localPosition.y", curve);

    //    curve = AnimationCurve.Linear(0.0f, end_pos.z, time * 2, start_pos.z);
    //    clip.SetCurve("", typeof(Transform), "localPosition.z", curve);

    //    AnimationEvent evt = new AnimationEvent();
    //    evt.time = time;
    //    evt.functionName = "InfoPopup_Update";
    //    evt.objectReferenceParameter = ip;

    //    clip.AddEvent(evt);

    //    anime.AddClip(clip, clip.name);
    //    anime.Play(clip.name);
    //}

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