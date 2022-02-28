using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controll : MonoBehaviour
{



    
    public GameObject Lighting;
    public Animator LeftHandAni;
    public Animator RightHandAni;
    void Start()
    {
       
    var inputDevices = new List<UnityEngine.XR.InputDevice>();
    UnityEngine.XR.InputDevices.GetDevices(inputDevices);

    foreach (var device in inputDevices)
    {
        Debug.Log(string.Format("Device found with name '{0}' and role '{1}'", device.name, device.role.ToString()));
    }
}

    // Update is called once per frame
    void Update()
    {
      if(OVRInput.GetDown(OVRInput.Button.One))
      {
            Debug.Log("Click one button");
      }
        if (OVRInput.GetDown(OVRInput.Button.Three))
        {
            if(Lighting.activeSelf)
            {
                Lighting.SetActive(false);

            }
            else
            {
                Lighting.SetActive(true);
            }
            Debug.Log("Click one button");
        }
        if(OVRInput.GetDown(OVRInput.Button.Two))
        {
            RightHandAni.SetTrigger("FY");
        }
    }
}
