using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcManager : MonoBehaviour
{
    public GameObject Npc;//������ ���� Npc

    public List<Transform> Map_Transform = new List<Transform>();

    private void Awake()
    {
        Respawn_Npc();
    }
    public Transform request() { return this.transform; }
    //�����ε����� ���������̶� �Ϲ� npc���� �ٸ���

    public Transform request(Npc.State state)
    {
        switch(state)
        {
            case global::Npc.State.HUNGRY:
                //return Map_Transform[0];
                break;
        }
        return null;
    }
   

    void Respawn_Npc()
    {
        //Npc ������
    }
}
