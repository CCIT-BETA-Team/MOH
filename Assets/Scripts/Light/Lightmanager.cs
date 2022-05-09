using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightmanager : Singleton<Lightmanager>
{
    public  List<Switch> Switch = new List<Switch>();

   
    //
    //Generator에 붙일지 매니저에 붙일지 생각
    //
     bool generator_power = false;
     public bool generator_on_off
    { 
        get {return generator_power; } 
        set 
        {
            if (value == false) { All_Electricity_Off(); }
            else { All_Electricity_On(); }
        }
     }
    public void Resist_Light(Switch light_switch)
    {
        Switch.Add(light_switch);
    }

    public void All_Power_Down()
    {
         foreach(Switch v in Switch)
         {
          //  v.Turn_On_Off(false);
        }
    }
    public void All_Power_Up()
    {
        foreach (Switch v in Switch)
        {
           // v.Turn_On_Off(true);
        }
    }
    public void All_Electricity_On()
    {
        foreach (Switch v in Switch)
        {
            v.Electrocity_On_Off(true);
        }
    }
    public void All_Electricity_Off()
    {
        foreach (Switch v in Switch)
        {
            v.Electrocity_On_Off(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        generator_on_off = true;
#if UNITY_EDITOR
        All_Power_Up();
        All_Power_Down();
#endif
    }

    // Update is called once per frame
    void Update()
    {
   
    }
}
