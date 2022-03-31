using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteManager : Singleton<RemoteManager>
{
   public List<RemotedObject> remoted_objects = new List<RemotedObject>();
   public List<RemoteControl> remocons = new List<RemoteControl>();
   

    public void Remocon_Setting()
    {
     foreach(RemotedObject r in remoted_objects)
     {
            remocons[Random.Range(0, remocons.Count)].Regist_Target(r);
     }
    }
    private void Start()
    {
        Remocon_Setting();   
    }
}
