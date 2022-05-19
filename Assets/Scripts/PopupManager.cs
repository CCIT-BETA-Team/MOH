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

    void Update()
    {
        //테스트용
        if (Input.GetKey(KeyCode.Alpha1)) { Popup_On(0); }
        if (Input.GetKey(KeyCode.Alpha2)) { Popup_On(1); }
        if (Input.GetKey(KeyCode.Alpha3)) { Popup_On(2); }
        if (Input.GetKey(KeyCode.Alpha4)) { Popup_On(3); }

        if (Input.GetKey(KeyCode.Alpha5)) { popup_list[0].popup.SetActive(false); }
        if (Input.GetKey(KeyCode.Alpha6)) { popup_list[1].popup.SetActive(false); }
        if (Input.GetKey(KeyCode.Alpha7)) { popup_list[2].popup.SetActive(false); }
        if (Input.GetKey(KeyCode.Alpha8)) { popup_list[3].popup.SetActive(false); }
    }

    void Popup_On(int num)
    {
        popup_list[num].popup.SetActive(true);
        Popup_Ani(popup_list[num].anime, time);
        popup_list[num].popup.transform.SetSiblingIndex(popup_content_transform.childCount);
    }

    void Popup_Ani(Animation anime, float time)
    {
        AnimationClip clip = new AnimationClip();
        clip.legacy = true;

        AnimationCurve curve = AnimationCurve.Linear(0.0f, -50.0f, time, -50.0f);
        clip.SetCurve("", typeof(Transform), "localPosition.x", curve);

        curve = AnimationCurve.Linear(0.0f, start_y + 50.0f, time, 50.0f);
        clip.SetCurve("", typeof(Transform), "localPosition.y", curve);

        curve = AnimationCurve.Linear(0.0f, 0.0f, time, 1.0f);
        clip.SetCurve("", typeof(Image), "m_Color.a", curve);

        anime.AddClip(clip, clip.name);
        anime.Play(clip.name);
    }
}

[System.Serializable]
public class PopupObject
{
    public Animation anime;
    public GameObject popup;
}