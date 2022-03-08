using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controll : MonoBehaviour
{




    public GameObject Lighting;
    public GameObject head;

    public Animator leftHandAni;
    public Animator rightHandAni;
    public Animator f_leftHandAni;
    public Animator f_rightHandAni;


    public GameObject rotation_root;

    public Rigidbody rig;

    public GameObject l_hand;
    public GameObject l_fakehand;
    public GameObject r_hand;
    public GameObject r_fakehand;


    public float door_sensitivity;
    public float speed;
    public float angle_speed;

    private CharacterController chacontroll;
    private GameObject l_door = null;
    private GameObject r_door = null;
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
       if(l_hand.transform.position!=L_preposition)
       {
            Door_moving();
       }
        if (r_hand.transform.position != R_preposition)
        {
            Door_moving();
        }
        if (l_door != null)
        {
            l_fakehand.transform.position = l_door.transform.position;
        }
        if (r_door != null)
        {
            r_fakehand.transform.position = r_door.transform.position;
        }
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


        Hand_controll();

        if (OVRInput.Get(OVRInput.Touch.PrimaryThumbstick))
        {
            Vector2 PrimaryThumbstick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);

            Vector3 Dir = transform.forward * PrimaryThumbstick.y*speed +transform.right*PrimaryThumbstick.x *speed;
            chacontroll.Move(Dir);  
           
            
        }

        var SecondaryThumbstick = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick, OVRInput.Controller.RTouch);
        if(SecondaryThumbstick.x>0.8f)
        {
            rig.transform.RotateAround(head.transform.position, head.transform.up,angle_speed*0.1f);
        }
        else if (SecondaryThumbstick.x < -0.8f)
        {
            rig.transform.RotateAround(head.transform.position, head.transform.up, angle_speed * -0.1f);
        }

    }
    
   public void Hand_controll()
   {
        if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) != 0)
        {
            RaycastHit hit;
            Physics.Raycast(l_hand.transform.position,l_hand.transform.forward, out hit, 20);
            if (hit.transform.GetComponent<Door>()!=null)
            {
                l_door = hit.transform.gameObject; 
                l_fakehand.transform.position = hit.transform.position;
                l_hand.SetActive(false);
            }
        }
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            if (l_door != null)
            {
                l_fakehand.transform.position = l_door.transform.position;
                Vector2 PrimaryThumbstick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
                l_door.GetComponent<Door>().interaction(PrimaryThumbstick);
            }
        }
        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger))
        {
            l_door = null;
            l_fakehand.transform.localPosition = new Vector3(0, 0, 0);
            l_hand.SetActive(true);
        }
        if (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) != 0)
        {
            RaycastHit hit;
            Physics.Raycast(r_hand.transform.position, r_hand.transform.forward, out hit, 20);
            if (hit.transform.GetComponent<Door>() != null)
            {
                r_door = hit.transform.gameObject;
                Debug.Log("Object is : " + hit.transform.name);
                r_fakehand.transform.position = hit.transform.position;
                r_hand.SetActive(false);
            }
        }
        if(OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            if (r_door != null)
            {
                r_fakehand.transform.position = r_door.transform.position;
                Vector2 SecondaryThumbstick = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
               
            }
        }
        if (OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger))
        {
            r_door = null;
            r_fakehand.transform.localPosition = new Vector3(0, 0, 0);
            r_hand.SetActive(true);
        }
    }

    private Vector3 L_preposition = new Vector3(0, 0, 0);
    private Vector3 R_preposition = new Vector3(0, 0, 0);
    private void Door_moving()
    {
         if(r_door!=null)
         {
            if (r_hand.transform.position != R_preposition)
            {
                r_door.GetComponent<Door>().interaction(new Vector2((r_hand.transform.position.x - R_preposition.x) * door_sensitivity, (r_hand.transform.position.y - R_preposition.y) * door_sensitivity));
            }
         }
        if (l_door != null)
        {
            if (l_hand.transform.position != L_preposition)
            {
                l_door.GetComponent<Door>().interaction(new Vector2((l_hand.transform.position.x - L_preposition.x) * door_sensitivity, (l_hand.transform.position.y - L_preposition.y) * door_sensitivity));
            }
        }
        R_preposition = r_hand.transform.position;
        L_preposition = l_hand.transform.position;
    }
}
