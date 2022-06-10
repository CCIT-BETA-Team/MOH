using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupManager : Singleton<PopupManager>
{
    public List<PopupObject> popup_list = new List<PopupObject>();
    public Transform popup_content_transform;
    public float start_y;
    public float time;
    public Slider door_unlock_slider;

    void Start()
    {
        for (int i = 0; i < popup_list.Count; i++)
            AnimetionNArray(i);
    }

    void Update()
    {
        //테스트용
        if (Input.GetKey(KeyCode.Alpha1)) { Popup_On(0); }
        if (Input.GetKey(KeyCode.Alpha2)) { Popup_On(1); }
        if (Input.GetKey(KeyCode.Alpha3)) { Popup_On(2); }
        if (Input.GetKey(KeyCode.Alpha4)) { Popup_On(3); }

        if (Input.GetKey(KeyCode.Alpha5)) { Popup_Off(0); }
        if (Input.GetKey(KeyCode.Alpha6)) { Popup_Off(1); }
        if (Input.GetKey(KeyCode.Alpha7)) { Popup_Off(2); }
        if (Input.GetKey(KeyCode.Alpha8)) { Popup_Off(3); }
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

    public void Popup_Off(int num)
    {
        popup_list[num].anime.Play(popup_list[num].animeList[1]);
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