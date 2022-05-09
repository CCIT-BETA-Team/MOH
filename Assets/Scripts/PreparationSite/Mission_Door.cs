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
            Debug.Log("미션 선택하세요");
        }
    }
    
    void Mission_Start(Mission select_mission)
    {
        ScenesManager.instance.Load_Sence(select_mission.mission_scene);
    }
}
