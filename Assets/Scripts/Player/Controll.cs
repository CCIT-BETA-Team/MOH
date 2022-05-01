using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.InputSystem;
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

    public Transform fake_hand_p;

    public GameObject l_hand;
    public GameObject l_fakehand;
    public GameObject l_mesh;
    public GameObject r_hand;
    public GameObject r_fakehand;
    public GameObject r_mesh;

    int doorlayer;
    int objectlayer;
    public XRNode Lhand;
    public XRNode Rhand;

    public InputActionReference l_grip_click;
    public InputActionReference r_grip_click;

    public float door_sensitivity;
    public float speed;
    public float angle_speed;



    private CharacterController chacontroll;
    private GameObject l_door = null;
    private GameObject r_door = null;



    #region Animator_p
    private int flexid = -1;
    private int poseid = -1;
    private int pinch = -1;
    #endregion

    #region XR

    private XRNode LNode = XRNode.LeftHand;
    private XRNode RNode = XRNode.RightHand;


    private List<UnityEngine.XR.InputDevice> l_devices = new List<UnityEngine.XR.InputDevice>();
    private List<UnityEngine.XR.InputDevice> r_devices = new List<UnityEngine.XR.InputDevice>();

    private UnityEngine.XR.InputDevice l_device;
    private UnityEngine.XR.InputDevice r_device;
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
        doorlayer = 1<< LayerMask.NameToLayer("Door");
        objectlayer= 1 << LayerMask.NameToLayer("Object");
        Debug.Log("Door code" + doorlayer);
        chacontroll = GetComponent<CharacterController>();
        var inputDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevices(inputDevices);

        foreach (var device in inputDevices)
        {
            Debug.Log(string.Format("Device found with name '{0}' and role '{1}'", device.name, device.role.ToString()));
        }
    }
     private void Click(InputAction.CallbackContext coontext)
     {
        Debug.Log("Clcik");
        RaycastHit hit2;
        Physics.Raycast(l_hand.transform.position, l_hand.transform.forward, out hit2, 20, objectlayer);
        if (hit2.transform != null)
        {
            if (hit2.transform.GetComponent<Item>() != null)
            {
                hit2.transform.GetComponent<Item>().interaction();
            }
        }
    }
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

        l_grip_click.action.started += Click;

        if (l_device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton,out l_tirggerButtonAction) && l_tirggerButtonAction)
        {
    
            RaycastHit hit;
            /*
            RaycastHit hit2;
            Physics.Raycast(l_hand.transform.position, l_hand.transform.forward, out hit2, 20, objectlayer);
            if (hit2.transform != null)
            {
                if (hit2.transform.GetComponent<Item>() != null)
                {
                    hit2.transform.GetComponent<Item>().interaction();
                }
            }*/
            if (l_door == null)
            {
                Physics.Raycast(l_hand.transform.position, l_hand.transform.forward, out hit, 20, doorlayer);

                if (hit.transform!=null)
                {
                    if (hit.transform.GetComponent<Door>() != null)
                    {
                        L_preposition = l_hand.transform.localPosition;
                        l_door = hit.transform.gameObject;
                        l_fakehand.transform.position = hit.transform.position;
                        l_mesh.SetActive(false);
                        l_fakehand.transform.SetParent(hit.transform);
                    }
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
            l_fakehand.transform.SetParent(fake_hand_p);
        }

        bool l_primarybutton = false;
        InputFeatureUsage<bool> l_uses1 = UnityEngine.XR.CommonUsages.primaryButton;
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
        InputFeatureUsage<bool> l_uses2 = UnityEngine.XR.CommonUsages.secondaryButton;
        if (l_device.TryGetFeatureValue(l_uses2, out l_secondarybutton) && l_secondarybutton)
        {
            Debug.Log(" Trigger    :   " + l_secondarybutton);
        }

        bool r_tirggerButtonAction = false;
        if (r_device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out r_tirggerButtonAction) && r_tirggerButtonAction)
        {
            
            RaycastHit hit;
            RaycastHit hit2;
            if (r_door ==null)
            {

                Physics.Raycast(r_hand.transform.position, r_hand.transform.forward, out hit, 20, doorlayer);
                if (hit.transform != null)
                {
                    if (hit.transform.GetComponent<Door>() != null)
                    {
                        Debug.Log(" right");
                        R_preposition = r_hand.transform.localPosition;
                        r_door = hit.transform.gameObject;
                        r_fakehand.transform.position = hit.transform.position;
                        r_mesh.SetActive(false);
                        r_fakehand.transform.SetParent(hit.transform);
                    }
                }
            }
            else
            {
                r_fakehand.transform.position = r_door.transform.position;
         
            }
            Physics.Raycast(r_hand.transform.position, r_hand.transform.forward, out hit2, 20, objectlayer);
            if (hit2.transform != null)
            {
                if (hit2.transform.GetComponent<Item>() != null)
                {
                    hit2.transform.GetComponent<Item>().interaction();
                }
            }
            Debug.Log(" Trigger    :   " + r_tirggerButtonAction);
        }
        else
        {
            r_door = null;
            r_fakehand.transform.localPosition = new Vector3(0, 0, 0);
            r_mesh.SetActive(true);
            r_fakehand.transform.SetParent(fake_hand_p);
        }
        bool r_primarybutton = false;
        InputFeatureUsage<bool> r_uses1 = UnityEngine.XR.CommonUsages.primaryButton;
        if (r_device.TryGetFeatureValue(r_uses1, out r_primarybutton) && r_primarybutton)
        {
            Debug.Log(" Trigger    :   " + r_primarybutton);
        }
        bool r_secondarybutton = false;
        InputFeatureUsage<bool> r_uses2 = UnityEngine.XR.CommonUsages.secondaryButton;
        if (r_device.TryGetFeatureValue(r_uses2, out r_secondarybutton) && r_secondarybutton)
        {
            Debug.Log(" Trigger    :   " + r_secondarybutton);
        }


        if (l_door!=null || r_door!=null)
       {
            Door_moving();
       }
        
        Vector2 l_2daxis = Vector2.zero;
        Vector2 r_2daxis = Vector2.zero;
        InputFeatureUsage<Vector2> l_stick = UnityEngine.XR.CommonUsages.primary2DAxis;
        InputFeatureUsage<Vector2> r_stick = UnityEngine.XR.CommonUsages.secondary2DAxis;
        

         
        if(l_device.TryGetFeatureValue(l_stick,out l_2daxis)&& l_2daxis!=Vector2.zero)
        {
            Vector3 Dir = transform.forward * l_2daxis.y * speed + transform.right * l_2daxis.x * speed;
            //chacontroll.Move(Dir);
        }



        if (r_device.TryGetFeatureValue(r_stick, out r_2daxis) && r_2daxis != Vector2.zero)
        {
            rig.transform.RotateAround(head.transform.position, head.transform.up, angle_speed * 0.1f);
        }
     

    }

    private void Action_started(InputAction.CallbackContext obj)
    {
        throw new System.NotImplementedException();
    }

    public void Fake_hand_controll()
   {
     
    }

    private Vector3 L_preposition = new Vector3(0, 0, 0);
    private Vector3 R_preposition = new Vector3(0, 0, 0);
    private void Door_moving()
    {
        
            if (r_hand.transform.localPosition != R_preposition && r_door != null)
            {
                r_door.GetComponent<Door>().interaction(new Vector3((r_hand.transform.localPosition.z - R_preposition.z) * door_sensitivity, (r_hand.transform.localPosition.y - R_preposition.y) * door_sensitivity, (r_hand.transform.localPosition.z - R_preposition.z) * door_sensitivity));
        
            }
         
                
            if (l_hand.transform.localPosition != L_preposition&&l_door!=null)
            {
                l_door.GetComponent<Door>().interaction(new Vector3((l_hand.transform.localPosition.x - L_preposition.x) * door_sensitivity, (l_hand.transform.localPosition.y - L_preposition.y) * door_sensitivity, (l_hand.transform.localPosition.z - L_preposition.z) * door_sensitivity));
         
            }
        
        R_preposition = r_hand.transform.localPosition;
        L_preposition = l_hand.transform.localPosition;
     
    }
}
