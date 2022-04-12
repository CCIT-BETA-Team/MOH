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
        Debug.Log(select_mission.name + " Start!");
        //문 열리는 애니메이션 이후 씬전환 굳굳

        SceneManager.LoadScene(select_mission.mission_scene);
    }
}
