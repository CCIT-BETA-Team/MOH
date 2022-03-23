using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grandma : Npc
{
    public override void Select_Personality()
    {
        int a = Random.Range(0, 2);
        if (a == 0)
        {
            this.personality = Npc_Personality.AGGESSIVE;
        }
        else if (a == 1)
        {
            this.personality = Npc_Personality.Defensive;
        }
        //생성하고 Npc가 공격적인지 방어적인지 정해줄거임
        //Start에서 한번만 돌려주자고~
    }


    // Start is called before the first frame update
    void Start()
    {
        Select_Personality();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
