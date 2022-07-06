using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Player : p_Player
{
    [Header("플레이어 신체")]
    public Camera cam;
    public ScreenShot ss;
    public GameObject hand;
    public Item emptyhand;
    public Rigidbody rg;
    public float power;
     
    [Space]
    [Header("플레이어 아이템 관련")]
    bool isHoldingItem;
    bool getQuestItem;
    [HideInInspector]
    public bool is_attack;
    bool is_capture;

    public List<Rigidbody> itemRG = new List<Rigidbody>();
    public List<Collider> itemCol = new List<Collider>();
    public int currentItem;
    Item unlockTool;
    public Transform InteractionObject;

    public Animator ani;
    [HideInInspector]
    public int attack_hash = Animator.StringToHash("Attack");

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
  
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1)) { Debug.Log(money); }

        switch (GameManager.Platform)
        {
            case 0: //오큘러스

                break;
            case 1: //PC
                if (!freeze)
                {
                    Control();
                }
                if (health <= 0) { Die(); }
                break;
        }
    }

    void FixedUpdate()
    {

    }

    Vector2 turn;

    void Control()
    {
        if(!move)
        {
            turn.x += Input.GetAxis("Mouse X") * sensitivity;
            turn.y += Input.GetAxis("Mouse Y") * sensitivity;
            transform.localRotation = Quaternion.Euler(0, turn.x, 0);
            cam.transform.localRotation = Quaternion.Euler(-turn.y, 0, 0);

            //이동
            if (Input.GetKey(KeyCode.W)) { transform.Translate(Vector3.forward * Time.deltaTime * (walkingSpeed - itemBag[currentItem].weight * 0.3f)); }
            if (Input.GetKey(KeyCode.A)) { transform.Translate(Vector3.left * Time.deltaTime * (walkingSpeed - itemBag[currentItem].weight * 0.3f)); }
            if (Input.GetKey(KeyCode.S)) { transform.Translate(Vector3.back * Time.deltaTime * (walkingSpeed - itemBag[currentItem].weight * 0.3f)); }
            if (Input.GetKey(KeyCode.D)) { transform.Translate(Vector3.right * Time.deltaTime * (walkingSpeed - itemBag[currentItem].weight * 0.3f)); }
            if (Input.GetKey(KeyCode.LeftShift)) { }
            if (Input.GetKeyDown(KeyCode.Space)) { rg.AddForce(Vector3.up * power); }
            if (Input.GetKey(KeyCode.LeftControl)) { }

            //장비
            if (Input.GetMouseButton(1)) { Zoom(); }
            else if (Input.GetMouseButtonDown(0)) { Use_Item(); }
            if (Input.GetMouseButton(1) && Input.GetMouseButtonDown(0)) { Throw_Item(); }

            if (Input.GetKeyDown(KeyCode.Alpha1)) { ItemSwitch(itemBag[0]); currentItem = 0; }
            if (Input.GetKeyDown(KeyCode.Alpha2)) { ItemSwitch(itemBag[1]); currentItem = 1; }
            if (Input.GetKeyDown(KeyCode.Alpha3)) { ItemSwitch(itemBag[2]); currentItem = 2; }
            if (Input.GetKeyDown(KeyCode.Alpha4)) { ItemSwitch(itemBag[3]); currentItem = 3; }
        }

        //상호작용
        if (Input.GetKeyDown(KeyCode.E))
        {
            InteractionItem = null;
            ItemCheck();
            if (InteractionItem.itemtype == Item.itemType.TOOL || InteractionItem.itemtype == Item.itemType.EQUIPMENT)
            {
                ani.SetTrigger("PickUp");
                //Pickup_Item();
            }
        }
        if(InteractionItem)
        {
            if(InteractionItem.itemtype == Item.itemType.DOOR || InteractionItem.itemtype == Item.itemType.FURNITURE)
            {
                if (Input.GetKey(KeyCode.E)) { Interaction(); }
            }
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            if (InteractionItem)
            {
                switch (InteractionItem.itemtype)
                {
                    case Item.itemType.DOOR:
                        InteractionItem.Door_Unlock_Gauge_Init();
                        break;
                    case Item.itemType.FURNITURE:
                        break;
                    default:
                        break;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.F)) { ani.SetTrigger("FXXk"); }
    }

    public void ItemSwitch(Item item)
    {
        if (itemBag[currentItem].gameObject.layer == LayerMask.NameToLayer("Npc"))
            Throw_Out_Item();
        itemBag[currentItem].gameObject.SetActive(false);
        item.gameObject.SetActive(true);
    }

    Ray ray;
    RaycastHit[] hits;
    RaycastHit hit;

    void ItemCheck()
    {
        ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        hits = Physics.RaycastAll(ray, 10);

        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit_ = hits[i];
            if (hit_.transform.gameObject.layer == LayerMask.NameToLayer("Wall"))
            {
                break;
            }

            if (hit_.transform.gameObject.layer == LayerMask.NameToLayer("NPCBody"))
            {
                Item item = hit_.transform.GetComponent<Item>();
                if (item)
                {
                    InteractionObject = hit_.transform.root;
                    hit = hit_;
                    InteractionItem = item;
                    InteractionItem.player = this;
                    break;
                }
            }
            else if (hit_.transform.gameObject.layer != LayerMask.NameToLayer("NPCBody"))
            {
                Item item = hit_.transform.GetComponent<Item>();
                if (item)
                {
                    InteractionObject = hit_.transform;
                    hit = hit_;
                    InteractionItem = item;
                    InteractionItem.player = this;
                    break;
                }
            }
        }
    }

    public Item InteractionItem;

    void Interaction()
    {
        if(InteractionItem)
        {
            switch (InteractionItem.itemtype)
            {
                case Item.itemType.DOOR:
                    InteractionItem.interaction();
                    break;
                case Item.itemType.FURNITURE:
                    InteractionItem.interaction();
                    break;
                default:
                    Pickup_Item();
                    break;
            }
        }
    }

    RagDollSetter rds;

    void Pickup_Item()
    {
        if(currentItem == 0 || currentItem == 1)
        {
            Throw_Out_Item();

            if (GameManager.instance.select_mission != null && InteractionItem.gameObject.name == GameManager.instance.select_mission.goal_item.name)
            {
                ss.take_screen = true;
                return;
            }

            Pick_Up();
        }
    }

    public void Pick_Up()
    {
        itemBag[currentItem] = InteractionItem;

        if (hit.transform.gameObject.layer == LayerMask.NameToLayer("NPCBody"))
        {
            rds = InteractionItem.GetComponent<RagDollSetter>();
            itemRG[currentItem] = rds.test_r;
            itemCol[currentItem] = rds.test_C;
            foreach (var col in rds.cols)
                col.isTrigger = true;
            rds.test_r.isKinematic = true;
            foreach (var rig in rds.rigs)
            {
                rig.velocity = new Vector3(0, 0, 0);
                rig.useGravity = true;
            }
            rds.test_O.transform.position = hand.transform.position;
        }
        else
        {
            itemRG[currentItem] = InteractionItem.GetComponent<Rigidbody>();
            itemCol[currentItem] = InteractionItem.GetComponent<Collider>();
            InteractionObject.position = hand.transform.position;
        }

        InteractionObject.parent = hand.transform;
        itemCol[currentItem].isTrigger = true;
        itemRG[currentItem].useGravity = false;
        itemRG[currentItem].velocity = Vector3.zero;
    }

    void Throw_Item()
    {
        if(currentItem == 0 ||currentItem == 1)
        {
            if (itemBag[currentItem].gameObject.layer == LayerMask.NameToLayer("Npc"))
            {
                foreach (var col in rds.cols)
                    col.isTrigger = false;
                rds.test_r.isKinematic = false;
            }

            Ray throwRay = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            itemBag[currentItem].transform.parent = null;
            itemRG[currentItem].AddForce(throwRay.direction * 1000);
            itemCol[currentItem].isTrigger = false;
            itemRG[currentItem].useGravity = true;

            itemBag[currentItem] = emptyhand;
            itemCol[currentItem] = null;
            itemRG[currentItem] = null;
        }
    }

    void Use_Item()
    {
        if(InteractionItem.itemtype == Item.itemType.TOOL || InteractionItem.itemtype == Item.itemType.EQUIPMENT)
        {
            InteractionItem.interaction();
        }
    }

    void Throw_Out_Item()
    {
        if (itemBag[currentItem].gameObject.layer == LayerMask.NameToLayer("Npc"))
        {
            foreach (var col in rds.cols)
                col.isTrigger = false;
            rds.test_r.isKinematic = false;
        }

        if(itemBag[currentItem] != emptyhand)
        {
            itemBag[currentItem].transform.parent = null;
            itemCol[currentItem].isTrigger = false;
            itemRG[currentItem].useGravity = true;
        }

        itemBag[currentItem] = emptyhand;
        itemCol[currentItem] = null;
        itemRG[currentItem] = null;
    }

    public void Player_Init()
    {
        for(int i = 0; i < 2; i++)
        {
            Destroy(itemBag[i].gameObject);
            itemBag[i] = emptyhand;
            itemCol[i] = null;
            itemRG[i] = null;
            currentItem = 0;
        }
    }

    void Zoom()
    {
        Debug.Log("ZOOOOOOOOOOOOOOOOOOOOOOM");
    }

    void Die()
    {
        NpcManager.instance.map.result_popup.On_Result_Popup(1);
    }
}
