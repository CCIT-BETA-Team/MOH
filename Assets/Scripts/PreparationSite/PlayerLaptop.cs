using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaptop : Item
{
    //public GameObject[] main_ui;
    public FadeInOut fo;

    public override void interaction()
    {
        StartCoroutine(fo.CamearaSwitch(FadeInOut.Fade_State.IN, fo.cam[1], GameManager.instance.player_comp.cam, fo.fade_image[1], fo.fade_image[0]));
        Cursor.lockState = CursorLockMode.Confined;
        PopupManager.instance.current_popup.SetActive(false);
        PopupManager.instance.ip_.SetActive(false);
        GameManager.instance.player_comp.freeze = true;
    }
}
