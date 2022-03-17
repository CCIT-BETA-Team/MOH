using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightmanager : Singleton<Lightmanager>
{
    List<Lighting> lights = new List<Lighting>();
    //
    //Generator에 붙일지 매니저에 붙일지 생각
    //
     bool generator_power = false;
     public bool generator_on_off
    { 
        get {return generator_power; } 
        set 
        {
            if (value == false) { All_Power_Down(); }
            else { All_Power_Up(); }
        }
     }

    public void All_Power_Down()
    {
         foreach(Lighting  v in lights)
         {
            v.electro_property = false;
         }
    }
    public void All_Power_Up()
    {
        foreach (Lighting v in lights)
        {
            v.electro_property = true;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //
        //RoomManger.GetAllRoom();
        //
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
