using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR;
public class Controll : MonoBehaviour
{


 
    public const string ANIM_LAYER_NAME_THUMB = "Pinch";
    public const string ANIM_PARAM_NAME_FLEX = "Flex";
    public const string ANIM_PARAM_NAME_POSE = "Pose";

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
    public GameObject l_mesh;
    public GameObject r_hand;
    public GameObject r_fakehand;
    public GameObject r_mesh;


    public XRNode Lhand;
    public XRNode Rhand;

    public float door_sensitivity;
    public float speed;
    public float angle_speed;

    private CharacterController chacontroll;
    private GameObject l_door = null;
    private GameObject r_door = null;




    private int flexid = -1;
    private int poseid = -1;
    private int pinch = -1;


    #region XR

    private XRNode LNode = XRNode.LeftHand;
    private XRNode RNode = XRNode.RightHand;


    private List<InputDevice> l_devices = new List<InputDevice>();
    private List<InputDevice> r_devices = new List<InputDevice>();

    private InputDevice l_device;
    private InputDevice r_device;
    void GetDevice()
    {
        InputDevices.GetDevicesAtXRNode(LNode, l_devices);
        InputDevices.GetDevicesAtXRNode(RNode, r_devices);
        l_device = l_devices.FirstOrDefault();
        r_device = r_devices.FirstOrDefault();


    }

    #endregion
    private void OnEnable()
    {
        if(!l_device.isValid|| !r_device.isValid)
        {
            GetDevice();
        }
    }

    public void Animationid()
    {
        flexid= Animator.StringToHash(ANIM_PARAM_NAME_FLEX);
        poseid = Animator.StringToHash(ANIM_PARAM_NAME_POSE);
        pinch = Animator.StringToHash(ANIM_LAYER_NAME_THUMB);
    }
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
    public 
    // Update is called once per frame
    void Update()
    {
        if (!l_device.isValid || !r_device.isValid)
        {
            GetDevice();
        }
        Debug.Log("L hand : "+ l_hand.transform.localPosition);
        Debug.Log("R hand : " + r_hand.transform.localPosition);
        bool l_tirggerButtonAction = false;
        if(l_device.TryGetFeatureValue(CommonUsages.triggerButton,out l_tirggerButtonAction) && l_tirggerButtonAction)
        {
            
            RaycastHit hit;
            if (l_door == null)
            {
                Physics.Raycast(l_hand.transform.position, l_hand.transform.forward, out hit, 20);
                if (hit.transform.GetComponent<Door>() != null)
                {
                    l_door = hit.transform.gameObject;
                    l_fakehand.transform.position = hit.transform.position;
                    l_mesh.SetActive(false);
                }
            }
            else
            {
                l_fakehand.transform.position = l_door.transform.position;

            }



        }
        else
        {
            l_door = null;
            l_fakehand.transform.localPosition = new Vector3(0, 0, 0);
            l_mesh.SetActive(true);
        }

        bool l_primarybutton = false;
        InputFeatureUsage<bool> l_uses1 = CommonUsages.primaryButton;
        if (l_device.TryGetFeatureValue(l_uses1, out l_primarybutton) && l_primarybutton)
        {
            Debug.Log(" Trigger    :   " + l_primarybutton);
            if (Lighting.activeSelf)
            {
                Lighting.SetActive(false);

            }
            else
            {
                Lighting.SetActive(true);
            }
        }
        bool l_secondarybutton = false;
        InputFeatureUsage<bool> l_uses2 = CommonUsages.secondaryButton;
        if (l_device.TryGetFeatureValue(l_uses2, out l_secondarybutton) && l_secondarybutton)
        {
            Debug.Log(" Trigger    :   " + l_secondarybutton);
        }






        bool r_tirggerButtonAction = false;
        if (r_device.TryGetFeatureValue(CommonUsages.triggerButton, out r_tirggerButtonAction) && r_tirggerButtonAction)
        {
            RaycastHit hit;
            if(r_door ==null)
            {
                Physics.Raycast(r_hand.transform.position, r_hand.transform.forward, out hit, 20);
                if (hit.transform.GetComponent<Door>() != null)
                {
                    r_door = hit.transform.gameObject;
                    r_fakehand.transform.position = hit.transform.position;
                    r_mesh.SetActive(false);
                }
            }
            else
            {
                r_fakehand.transform.position = r_door.transform.position;
         
            }

            Debug.Log(" Trigger    :   " + r_tirggerButtonAction);
        }
        else
        {
            r_door = null;
            r_fakehand.transform.localPosition = new Vector3(0, 0, 0);
            r_mesh.SetActive(true);
        }
        bool r_primarybutton = false;
        InputFeatureUsage<bool> r_uses1 = CommonUsages.primaryButton;
        if (r_device.TryGetFeatureValue(r_uses1, out r_primarybutton) && r_primarybutton)
        {
            Debug.Log(" Trigger    :   " + r_primarybutton);
        }
        bool r_secondarybutton = false;
        InputFeatureUsage<bool> r_uses2 = CommonUsages.secondaryButton;
        if (r_device.TryGetFeatureValue(r_uses2, out r_secondarybutton) && r_secondarybutton)
        {
            Debug.Log(" Trigger    :   " + r_secondarybutton);
        }


        if (l_door!=null || r_door!=null)
       {
            Door_moving();
       }
       
       /*
        
        if (l_door != null)
        {
            l_fakehand.transform.position = l_door.transform.position;
        }
        if (r_door != null)
        {
            r_fakehand.transform.position = r_door.transform.position;
        }
        */
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            Debug.Log("Click one button");
        }
        if (OVRInput.GetDown(OVRInput.Button.Three))
        {
          
            Debug.Log("Click one button");
        }
        if (OVRInput.GetDown(OVRInput.Button.Two))
        {
           
        }
        Vector3 prevector = new Vector3(0,0,0);


       

        if (OVRInput.Get(OVRInput.Touch.PrimaryThumbstick))
        {
            Vector2 PrimaryThumbstick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);

            Vector3 Dir = transform.forward * PrimaryThumbstick.y*speed +transform.right*PrimaryThumbstick.x *speed;
            chacontroll.Move(Dir);  
           
            
        }

        var SecondaryThumbstick = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
        if(SecondaryThumbstick.x > 0.8f)
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
     
    }

    private Vector3 L_preposition = new Vector3(0, 0, 0);
    private Vector3 R_preposition = new Vector3(0, 0, 0);
    private void Door_moving()
    {
        
            if (r_hand.transform.localPosition != R_preposition && r_door != null)
            {
                r_door.GetComponent<Door>().interaction(new Vector2((r_hand.transform.localPosition.z - R_preposition.z) * door_sensitivity, (r_hand.transform.localPosition.y - R_preposition.y) * door_sensitivity));
            }
         
                
            if (l_hand.transform.localPosition != L_preposition&&l_door!=null)
            {
                l_door.GetComponent<Door>().interaction(new Vector2((l_hand.transform.localPosition.z - L_preposition.z) * door_sensitivity, (l_hand.transform.localPosition.y - L_preposition.y) * door_sensitivity));
            }
        
        R_preposition = r_hand.transform.localPosition;
        L_preposition = l_hand.transform.localPosition;
     
    }
}
