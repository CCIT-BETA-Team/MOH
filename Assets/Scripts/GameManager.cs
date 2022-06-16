using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
#if UNITY_ANDROID
    public static readonly int Platform = 0;
#endif

#if UNITY_STANDALONE_WIN
    public static readonly int Platform = 1;
#endif

    public Player Player;
    public bool b_selet_mission = false;
    public Mission select_mission;

    void Start()
    {
        //���� �غ�ܰ� (�Ŵ��������׼� �̰����� �ܰ躰���� ���⼭ ��)

        NpcManager.instance.Spawn_Npc(); //NPC ����
    }

    void Update()
    {
        //�׽�Ʈ��
        if (Input.GetKeyDown(KeyCode.Backspace)) { PlayerPrefs.DeleteAll(); }
        if (Input.GetKeyDown(KeyCode.Keypad1)) { PlayerPrefs.SetInt("pMoney", PlayerPrefs.GetInt("pMoney", 0) + 100);}
        if (Input.GetKeyDown(KeyCode.Keypad2)) { PlayerPrefs.SetInt("pMyung_Seong", PlayerPrefs.GetInt("pMyung_Seong", 0) + 100);}
    }
}