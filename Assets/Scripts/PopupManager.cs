using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupManager : Singleton<PopupManager>
{
    public List<PopupObject> popup_list = new List<PopupObject>();
    public float start_y;
    public float time;

    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1)) { popup_list[0].popup.SetActive(true); Popup_Ani(popup_list[0].anime, time); }
        if (Input.GetKey(KeyCode.Alpha2)) { popup_list[1].popup.SetActive(true); Popup_Ani(popup_list[1].anime, time); }
        if (Input.GetKey(KeyCode.Alpha3)) { popup_list[2].popup.SetActive(true); Popup_Ani(popup_list[2].anime, time); }
        if (Input.GetKey(KeyCode.Alpha4)) { popup_list[3].popup.SetActive(true); Popup_Ani(popup_list[3].anime, time); }

        if (Input.GetKey(KeyCode.Alpha5)) { popup_list[0].popup.SetActive(false); }
        if (Input.GetKey(KeyCode.Alpha6)) { popup_list[1].popup.SetActive(false); }
        if (Input.GetKey(KeyCode.Alpha7)) { popup_list[2].popup.SetActive(false); }
        if (Input.GetKey(KeyCode.Alpha8)) { popup_list[3].popup.SetActive(false); }
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