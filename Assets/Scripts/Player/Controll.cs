using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controll : MonoBehaviour
{




    public GameObject Lighting;
    public Animator LeftHandAni;
    public Animator RightHandAni;

    public GameObject l_hand;
    public GameObject r_hand;
    public float speed;

    private CharacterController chacontroll;

    void Start()
    {
        chacontroll = GetComponent<CharacterController>();
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
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            Debug.Log("Click one button");
        }
        if (OVRInput.GetDown(OVRInput.Button.Three))
        {
            if (Lighting.activeSelf)
            {
                Lighting.SetActive(false);

            }
            else
            {
                Lighting.SetActive(true);
            }
            Debug.Log("Click one button");
        }
        if (OVRInput.GetDown(OVRInput.Button.Two))
        {
           
        }
        Vector3 prevector = new Vector3(0,0,0);
        GameObject door=null;
        if(OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger)!=0)
        {
            RaycastHit hit;
            Physics.Raycast(l_hand.transform.position, l_hand.transform.forward, out hit, 20);
            if (hit.transform.GetComponent<Door>())
            {
                door = hit.transform.gameObject;
                prevector = l_hand.transform.position;
                Debug.Log("Object is : " + hit.transform.name);
                l_hand.transform.position = hit.transform.position;
            }
        }
        if(OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
          if(door!=null)
          {
                l_hand.transform.position = door.transform.position;
            }
        }
        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger))
        {
            door = null;
            l_hand.transform.position = prevector;
          
        }
        if (OVRInput.Get(OVRInput.Touch.PrimaryThumbstick))
        {
            Vector2 PrimaryThumbstick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);

            Vector3 Dir = transform.forward * PrimaryThumbstick.y*speed +transform.right*PrimaryThumbstick.x *speed;
            chacontroll.Move(Dir);  
           
            
        }
    }
}
