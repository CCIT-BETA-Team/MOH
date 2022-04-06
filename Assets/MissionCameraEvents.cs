using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionCameraEvents : MonoBehaviour
{
    public delegate void Anime_Event();
    public Anime_Event AE;

    void Camera_Event()
    {
        AE();
    }
}
