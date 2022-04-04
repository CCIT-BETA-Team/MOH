using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformaionPopup : MonoBehaviour
{
    public Text main_mission_text;
    public Text sub_mission;
    public Text[] sub_mission_text;
    public GameObject go;

    public Vector2 only_main_transform;
    public float sub_transform;
    public float sub_list_transform;

    void Start()
    {
        main_mission_text.text = "미션을 선택하세요.";
    }
}
