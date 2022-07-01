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
            GameManager.instance.Player.SetActive(false);
            Mission_Start(GameManager.instance.select_mission);
        }
        else if(GameManager.instance.select_mission == null)
        {
            Debug.Log("미션 선택하세요");
        }
    }
    
    static void Mission_Start(Mission select_mission)
    {
        ScenesManager.instance.Load_Scene(select_mission.mission_scene,ScenesManager.LoadingType.DELAY);
    }
}
