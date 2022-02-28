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

    [Header("�÷��̾� �������ͽ�")]
    int health;
    float walkingSpeed;
    float runningSpeed;
    float unlockSpeed;
    float noiseValue;

    [Space]
    [Header("�÷��̾� ������ ����")]
    bool isHoldingItem;
    bool getQuestItem;
    enum currentHands { rightHand, leftHand}
    currentHands currentHand;

    void Start()
    {
        switch(Platform)
        {
            case 0: //��ŧ����
                Debug.Log("Android Hi~");
                break;
            case 1: //PC
                Debug.Log("Window Hi~");
                break;
        }
    }

    void Update()
    {
        
    }
}
