using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapePoint : MonoBehaviour
{
    public ResultPopup result_popup;

    void OnTriggerEnter(Collider other)
    {
        //��� �޾ƿ��� ���� �ӽ÷� npcmanager         ���� ����
        if(other.gameObject == GameManager.instance.Player)
        {
            Debug.Log(0);
            Player p = GameManager.instance.Player.GetComponent<Player>();
            for(int i = 0; i < 2; i++)
            {
                Debug.Log(1);
                if(p.itemBag[i] == GameManager.instance.select_mission.goal_item.GetComponent<Item>())
                {
                    result_popup.On_Result_Popup(0);
                    break;
                }
            }
        }
    }
}
