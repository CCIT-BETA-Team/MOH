using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighting : MonoBehaviour
{
    Light light;
    bool electricity =true;
    bool broken = false;
    public bool broke_property
    { 
        get 
        { return broken; }
        set 
        { if (value) { electro_property = false; }; broken = value; } 
    }
    public bool electro_property { get {return electricity; }set { Light_Update(); electricity=value; } }
    private void Start()
    {
     if(GetComponent<Light>()!=null)
     {
            light = GetComponent<Light>();
     }
    }
    public void Light_Down()
    {
        if (broke_property)
        {
            light.enabled = false;
        }
    }
    public void Light_On()
    {
        if(!broke_property)
        {
            light.enabled = true;
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
            light.enabled = true;
        }
    }
}
