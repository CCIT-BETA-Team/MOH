using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformaionPopup : MonoBehaviour
{
    public RectTransform rt;
    public RectTransform cb_rt;

    public Text main_mission_text;
    public Text sub_mission;
    public Text[] sub_mission_text;
    public GameObject go;

    public Vector2 only_main_transform;
    public float sub_transform;
    public float sub_list_transform;

    float color_bar_position { get { return (rt.rect.height / 2 * -1) - (cb_rt.rect.height / 2); } }

    void Update()
    {
        if (cb_rt.position.y != color_bar_position)
            cb_rt.localPosition = new Vector3(0, color_bar_position, 0);
    }
}
