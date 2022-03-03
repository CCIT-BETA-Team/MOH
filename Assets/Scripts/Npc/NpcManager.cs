using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcManager : MonoBehaviour
{
    public GameObject Npc;//리스폰 해줄 Npc

    public List<Transform> Map_Transform = new List<Transform>();

    private void Awake()
    {
        Respawn_Npc();
    }
    public Transform request() { return this.transform; }
    //오버로딩으로 경찰스폰이랑 일반 npc스폰 다르게

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
        //Npc 리스폰
    }
}
