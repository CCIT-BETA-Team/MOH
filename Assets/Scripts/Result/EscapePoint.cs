using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapePoint : MonoBehaviour
{
    public ResultPopup result_popup;

    void OnTriggerEnter(Collider other)
    {
        //��� �޾ƿ��� ���� �ӽ÷� npcmanager         ���� ����
        if (other.gameObject == GameManager.instance.Player)
        {
            Player p = GameManager.instance.Player.GetComponent<Player>();
            for(int i = 0; i < 2; i++)
            {
                Debug.Log(p.itemBag[i].name);
                if(p.itemBag[i].name == GameManager.instance.select_mission.goal_item.name)
                {
                    result_popup.On_Result_Popup(0);
                    break;
                }
            }
        }
    }
}
