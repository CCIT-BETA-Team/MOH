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
    public string[] sub_mission;

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

    public void InfoCard_Update(InfoCard ic)
    {
        //goal = goal_item.GetComponent<Item>();

        ic.mission_name.text = mission_name;
        ic.client_name.text = client_name;
        ic.goal_name.text = goal.item_name;
        ic.mission_image.sprite = mission_image;
        ic.item_image.sprite = goal.item_image;
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
                    sub_mission_text.transform.position = new Vector3(ip.main_mission_text.transform.position.x, ip.main_mission_text.transform.position.y - 44.12f, ip.main_mission_text.transform.position.z);
                    ((RectTransform)ip.transform).sizeDelta = new Vector2(ip.only_main_transform.x, ip.only_main_transform.y + ip.sub_transform);
                    ((RectTransform)ip.transform).anchoredPosition = new Vector2(-((RectTransform)ip.transform).rect.width / 2, -((RectTransform)ip.transform).rect.height / 2);
                }
                else if (i > 0)
                {
                    sub_mission_text.transform.position = new Vector3(ip.sub_mission_text[0].transform.position.x, ip.sub_mission_text[0].transform.position.y - 16.8f * i, ip.sub_mission_text[0].transform.position.z);
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
}