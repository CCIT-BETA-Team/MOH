using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighting : MonoBehaviour
{
    Light light;
    private void Start()
    {
     if(GetComponent<Light>()!=null)
     {
            light = GetComponent<Light>();
     }
    }
    public void LightDown()
    {
        light.enabled = false;
    }
    public void LightOn()
    {
        light.enabled = true;
    }
}
