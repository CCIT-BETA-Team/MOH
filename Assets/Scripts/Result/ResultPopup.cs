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

    public void On_Result_Popup(int result)
    {
        Cursor.lockState = CursorLockMode.Confined;
        result_text.text = Results[result].result;
        result_popup.enabled = true;
    }

    public void Go_PreparationSite()
    {
        ScenesManager.instance.Load_Sence("PreparationSite");
    }
}

[System.Serializable]
public class Result
{
    public string result;
    //결과별 이미지같은거 넣으면 좋을듯
}
