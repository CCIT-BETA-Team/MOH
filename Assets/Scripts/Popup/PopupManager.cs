using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupManager : Singleton<PopupManager>
{
    public List<PopupObject> popup_list = new List<PopupObject>();
    public PopupObject mission_info_popup;
    public Text[] mission_info_text;
    public Transform popup_content_transform;
    public float start_y;
    public float time;
    public Slider door_unlock_slider;

    void Start()
    {
        for (int i = 0; i < popup_list.Count; i++)
            AnimetionNArray(i);
        Anime_Setting();
    }

    void Update()
    {
        //�׽�Ʈ��
        if (Input.GetKey(KeyCode.Alpha1)) { Popup_On(0); }
        if (Input.GetKey(KeyCode.Alpha2)) { Popup_On(1); }
        if (Input.GetKey(KeyCode.Alpha3)) { Popup_On(2); }
        if (Input.GetKey(KeyCode.Alpha4)) { Popup_On(3); }

        if (Input.GetKey(KeyCode.Alpha5)) { Popup_Off(0); }
        if (Input.GetKey(KeyCode.Alpha6)) { Popup_Off(1); }
        if (Input.GetKey(KeyCode.Alpha7)) { Popup_Off(2); }
        if (Input.GetKey(KeyCode.Alpha8)) { Popup_Off(3); }
        if (Input.GetKey(KeyCode.Alpha9)) { Mission_Popup_On(); }
    }

    public void Popup_On(int num)
    {
        popup_list[num].popup_object.SetActive(true);
        popup_list[num].popup_object.transform.SetSiblingIndex(popup_content_transform.childCount);

        popup_list[num].anime.Play(popup_list[num].animeList[0]);
    }

    void AnimetionNArray(int num)
    {
        foreach(AnimationState state in popup_list[num].anime)
        {
            popup_list[num].animeList.Add(state.name);
        }
    }

    void Anime_Setting()
    {
        foreach (AnimationState state in mission_info_popup.anime)
        {
            mission_info_popup.animeList.Add(state.name);
        }
    }

    public void Popup_Off(int num)
    {
        popup_list[num].anime.Play(popup_list[num].animeList[1]);
    }

    public void Mission_Popup_On()
    {
        mission_info_popup.popup_object.SetActive(true);
        mission_info_popup.anime.Play(mission_info_popup.animeList[0]);
    }

    public void Mission_Popup_Off()
    {
        mission_info_popup.anime.Play(mission_info_popup.animeList[1]);
        mission_info_popup.popup_object.SetActive(false);
    }

    public void Mission_Info_Text()
    {
        mission_info_text[2].text = GameManager.instance.select_mission.mission_name;
    }
}

[System.Serializable]
public class PopupObject
{
    public string popup_name;
    public GameObject popup_object;
    public Animation anime;
    public List<string> animeList = new List<string>();
}