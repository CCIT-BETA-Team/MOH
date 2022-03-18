using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public List<Lighting> Lightings = new List<Lighting>();
    bool on = false;
    public Animation switch_ani;


    // Update is called once per frame


    public void interection()
    {
        switch (on)
        {
            case true:
                //animation controll
                for(int i =0; i<Lightings.Count;i++)
                {
                    Lightings[i].on_off=true;
                }
                break;
            case false:
                //animation controll
                for (int i = 0; i < Lightings.Count; i++)
                {
                    Lightings[i].on_off = false;
                }
                break;
        }

    }
    public void Turn_On_Off(bool value)
    {
      
            on = value;
            interection();
    }

    public void Electrocity_On_Off(bool value)
    {
   
                for (int i = 0; i < Lightings.Count; i++)
                {
                    Lightings[i].electricity_property = value;
                }

    
    }

}
