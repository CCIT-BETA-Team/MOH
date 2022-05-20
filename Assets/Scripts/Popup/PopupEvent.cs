using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupEvent : MonoBehaviour
{
    public GameObject popup;

    void Off_Popup()
    {
        popup.SetActive(false);
    }
}
