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
        //게임 준비단계 (매니저들한테서 이것저것 단계별설정 여기서 ㄱ)
    }

    void Update()
    {
        
    }
}
