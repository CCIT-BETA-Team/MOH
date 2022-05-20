using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionCard : MonoBehaviour
{
    public Mission mission;
    public InfoCard ic;
    public MissionSpawn ms;
    public WhiteBoard mi;
    public InformaionPopup ip;

    public void Setting()
    {
        mission = ms.get_mission();
        mission.setting_environment();
    }

    void OnMouseEnter()
    {
        mission.InfoCard_Update(ic, this);
        ic.gameObject.SetActive(true);
    }

    void OnMouseDown()
    {
        mission.WhiteBoard_Update(mi);
        mission.InfoPopup_Update(ip);
        GameManager.instance.b_selet_mission = true;
    }

    void OnMouseExit()
    {
        ic.gameObject.SetActive(false);
    }

    //void 
}
