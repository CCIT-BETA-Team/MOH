using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultPopup : MonoBehaviour
{
    public Result[] Results = new Result[2];
    [Space]
    public Canvas result_popup;
    public Text result_text;
    public Text time_text;
    public Text mission_name_text;
    public Text pay_text;
    public Image screen_shot;
    public int time = 0;
    int sec { get { return time % 60; } }
    int min { get { return time / 60 % 60; } }
    int hour { get { return time / 60 / 60; } }

    void Start()
    {
        StartCoroutine(Timer());
    }

    public void On_Result_Popup(int result)
    {
        Cursor.lockState = CursorLockMode.Confined;
        result_text.text = Results[result].result;
        mission_name_text.text = GameManager.instance.select_mission.mission_name;
        result_popup.enabled = true;
        screen_shot.sprite = GameManager.instance.screen_sprite;
        StopCoroutine(Timer());
        time_text.text = string.Format("{0:D2}", hour) + ":" + string.Format("{0:D2}", min) + ":" + string.Format("{0:D2}", sec);
    }

    public void Go_PreparationSite()
    {
        //ScenesManager.instance.Load_Sence("PreparationSite");
        ScenesManager.instance.Load_Scene("PreparationSite");
        //ScenesManager.instance.missionText.text = "";
    }

    IEnumerator Timer()
    {
        time += 1;
        yield return new WaitForSeconds(1f);
        StartCoroutine(Timer());
    }
}

[System.Serializable]
public class Result
{
    public string result;
    //결과별 이미지같은거 넣으면 좋을듯
}
