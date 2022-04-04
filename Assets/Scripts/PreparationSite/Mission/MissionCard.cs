using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionCard : MonoBehaviour
{
    public Mission mission;
    public InfoCard ic;
    public WhiteBoard mi;

    void OnMouseEnter()
    {
        mission.InfoCard_Update(ic);
        ic.gameObject.SetActive(true);
    }

    void OnMouseDown()
    {
        mission.WhiteBoard_Update(mi);
    }

    void OnMouseExit()
    {
        ic.gameObject.SetActive(false);
    }

    //void 
}
