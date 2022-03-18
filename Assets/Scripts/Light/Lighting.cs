using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighting : MonoBehaviour
{
    Light light;

    bool electricity =true;
    bool broken = false;
   public bool on_off = false;

    public List<Animation> light_ani = new List<Animation>();
    public bool broke_property
    { 
        get 
        { return broken; }
        set 
        { broken = value; Light_Update();} 
    }
    public bool electricity_property { get { return electricity; } set { electricity=value; Light_Update(); } }
    private void Start()
    {
        if(GetComponent<Light>()!=null)
        {
            light = GetComponent<Light>();
        }
    }
  
    public void Light_Update(bool generate)
    {
        if (broke_property)
        {
            light.enabled = false;
        }
        else
        {
            if (generate)
            {
                if(on_off)
                {
                    light.enabled = true;
                }
                else
                {
                    light.enabled = false;
                }
            }
            else
            {
                light.enabled = false;
            } 
        }
    }
    public void Light_Update()
    {
        if (broke_property)
        {
         light.enabled = false;
        }
        else
        {
            if (on_off)
            {
                light.enabled = true;
            }
            else
            {
                light.enabled = false;
            }
        }
    }
    
}
