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
    public List<GameObject> mission_popups = new List<GameObject>();
    public InformaionPopup ip;
    public GameObject ip_;
    [HideInInspector]
    public GameObject current_popup;
    public Image cross_head;

    void Start()
    {
        current_popup = mission_popups[0];
        for (int i = 0; i < popup_list.Count; i++)
            AnimetionNArray(i);
        Anime_Setting();
    }

    void Update()
    {
        //테스트용
        if (Input.GetKey(KeyCode.Alpha3)) { Popup_On(2); }
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

    public void Mission_Popup_On()
    {
        mission_info_popup.popup_object.SetActive(true);
        mission_info_popup.anime.Play(mission_info_popup.animeList[0]);
    }

    public void Mission_Info_Text()
    {
        PlayerPrefs.SetInt("pMissionCount", PlayerPrefs.GetInt("pMissionCount", 0) + 1);

        mission_info_text[0].text = PlayerPrefs.GetInt("pMissionCount", 1).ToString() + "번째 임무";
        mission_info_text[1].text = GameManager.instance.select_mission.mission_building_name;
        mission_info_text[2].text = GameManager.instance.select_mission.mission_name;
    }

    public void Mission_Info_Popup_Setting(int SubMissionIndex)
    {
        Off_Mission_Info();
        current_popup = mission_popups[SubMissionIndex];
        switch (SubMissionIndex)
        {
            case 0:
                mission_popups[0].SetActive(true);
                break;
            case 1:
                mission_popups[1].SetActive(true);
                break;
            case 2:
                mission_popups[2].SetActive(true);
                break;
            case 3:
                mission_popups[3].SetActive(true);
                break;
        }
    }

    public void Off_Mission_Info()
    {
        for (int i = 0; i < mission_popups.Count; i++)
        {
            mission_popups[i].SetActive(false);
        }
    }

    public void Mission_Popup_Init()
    {
        Mission_Info_Popup_Setting(0);
        ip.main_mission.text = "▪ 메인 임무";
        ip.main_mission_text.text = "미션을 선택하세요";
        ip.sub_mission.enabled = false;
        for(int i = 0; i < ip.sub_mission_text.Count; i++)
        {
            Destroy(ip.sub_mission_text[i]);
        }
        ip.sub_mission_text = new List<Text>();
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