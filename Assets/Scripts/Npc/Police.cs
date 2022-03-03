using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Police : Npc
{
   

    public override void Move()
    {
        this.state = State.Move;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Choose(asd);
    }
}
