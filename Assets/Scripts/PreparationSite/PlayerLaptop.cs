using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaptop : Item
{
    public Player player;
    public GameObject[] main_ui;
    public FadeInOut fo;

    public override void interaction()
    {
        StartCoroutine(fo.CamearaSwitch(FadeInOut.Fade_State.IN, fo.cam[1], fo.cam[0], fo.fade_image[1], fo.fade_image[0]));
        Cursor.lockState = CursorLockMode.Confined;
        for (int i = 0; i < main_ui.Length; i++)
        {
            main_ui[i].SetActive(false);
        }
        player.freeze = true;
    }
}
