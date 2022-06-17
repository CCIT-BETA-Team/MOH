using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapePoint : MonoBehaviour
{
    public ResultPopup result_popup;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(2);
        //어디서 받아올지 몰라서 임시로 npcmanager         수정 예정
        if (other.gameObject == GameManager.instance.Player)
        {
            Debug.Log(0);
            Player p = GameManager.instance.Player.GetComponent<Player>();
            for(int i = 0; i < 2; i++)
            {
                Debug.Log(p.itemBag[i].name);
                Debug.Log(1);
                if(p.itemBag[i].name == GameManager.instance.select_mission.goal_item.name)
                {
                    Debug.Log(3);
                    result_popup.On_Result_Popup(0);
                    break;
                }
            }
        }
    }
}
