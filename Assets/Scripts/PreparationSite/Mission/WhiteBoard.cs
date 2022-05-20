using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WhiteBoard : MonoBehaviour
{
    public Canvas canvas;
    public Text mission_name;
    public Item goal;
    //보상
    //의뢰주 이름?
    public Image mission_image;
    public Image item_image;
    public Text coment;
    public Text scenario;
    public Text main_mission;
    public Text sub_mission;

    void Update()
    {
        if(!GameManager.instance.b_selet_mission && canvas.enabled)
            canvas.enabled = false;
        else if(GameManager.instance.b_selet_mission && !canvas.enabled)
            canvas.enabled = true;
    }
}
