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
    
    static void Mission_Start(Mission select_mission)
    {
        //select_mission.goal_item;
        ScenesManager.instance.Load_Scene(select_mission.mission_scene);
    }
}
