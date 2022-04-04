using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaptop : Item
{
    public Player player;
    public FadeInOut fo;

    public override void interaction()
    {
        StartCoroutine(fo.CamearaSwitch(FadeInOut.Fade_State.IN, fo.cam[1], fo.cam[0], fo.fade_image[1], fo.fade_image[0]));
        Cursor.lockState = CursorLockMode.Confined;
        player.freeze = true;
    }
}
