using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }
}
