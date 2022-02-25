using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
#if UNITY_ANDROID
    public static readonly int Platform = 0;
#endif

#if UNITY_STANDALONE_WIN
    public static readonly int Platform = 1;
#endif

    void Start()
    {
        switch(Platform)
        {
            case 0:
                Debug.Log("Android Hi~");
                break;
            case 1:
                Debug.Log("Window Hi~");
                break;
        }
    }

    void Update()
    {
        
    }
}
