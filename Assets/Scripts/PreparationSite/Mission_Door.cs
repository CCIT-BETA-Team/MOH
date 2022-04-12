using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mission_Door : Item
{
    public override void interaction()
    {
        if(GameManager.instance.select_mission != null)
        {
            Mission_Start(GameManager.instance.select_mission);
        }
        else if(GameManager.instance.select_mission == null)
        {
            Debug.Log("�̼� �����ϼ���");
        }
    }
    
    void Mission_Start(Mission select_mission)
    {
        Debug.Log(select_mission.name + " Start!");
        //�� ������ �ִϸ��̼� ���� ����ȯ ����

        SceneManager.LoadScene(select_mission.mission_scene);
    }
}
