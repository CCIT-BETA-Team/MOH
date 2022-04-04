using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
#if UNITY_ANDROID
    public static readonly int Platform = 0;
#endif

#if UNITY_STANDALONE_WIN
    public static readonly int Platform = 1;
#endif

    public Mission select_mission;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
