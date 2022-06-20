using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : p_Player
{
    [Header("플레이어 신체")]
    public Camera cam;
    public GameObject hand;
    public Item emptyhand;
    public Rigidbody rg;
    public float power;
     
    [Space]
    [Header("플레이어 아이템 관련")]
    bool isHoldingItem;
    bool getQuestItem;
    
    public List<Rigidbody> itemRG = new List<Rigidbody>();
    public List<Collider> itemCol = new List<Collider>();
    public int currentItem;
    Item unlockTool;
    public Transform InteractionObject;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
  
    }

    void Update()
    {
        switch (GameManager.Platform)
        {
            case 0: //오큘러스

                break;
            case 1: //PC
                if(!freeze)
                {
                    Control();
                }
                if (health <= 0) { Die(); }
                break;
        }

        if (Input.GetKeyDown(KeyCode.Keypad1)) { Debug.Log(money); }
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
            else if (Input.GetMouseButton(0)) { Use_Item(); }
            if (Input.GetMouseButton(1) && Input.GetMouseButtonDown(0)) { Throw_Item(); }

            if (Input.GetKeyDown(KeyCode.Alpha1)) { ItemSwitch(itemBag[0]); currentItem = 0; }
            if (Input.GetKeyDown(KeyCode.Alpha2)) { ItemSwitch(itemBag[1]); currentItem = 1; }
            if (Input.GetKeyDown(KeyCode.Alpha3)) { ItemSwitch(itemBag[2]); currentItem = 2; }
            if (Input.GetKeyDown(KeyCode.Alpha4)) { ItemSwitch(itemBag[3]); currentItem = 3; }
        }

        //상호작용
        if (Input.GetKeyDown(KeyCode.E)) { ItemCheck(); }
        if (Input.GetKey(KeyCode.E)) { Interaction(); }
        if (Input.GetKeyUp(KeyCode.E))
        {
            if (InteractionItem != null)
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
    }

    void ItemSwitch(Item item)
    {
        if (itemBag[currentItem].gameObject.layer == LayerMask.NameToLayer("Npc"))
            Throw_Out_Item();
        itemBag[currentItem].gameObject.SetActive(false);
        item.gameObject.SetActive(true);
    }

    Ray ray;
    RaycastHit hit;

    void ItemCheck()
    {
        ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        hit = new RaycastHit();

        if(Physics.Raycast(ray, out hit))
        {
            if(hit.transform.gameObject.layer == LayerMask.NameToLayer("NPCBody"))
            {
                InteractionObject = hit.transform.root;
            }
            else
            {
                InteractionObject = hit.transform;
            }
            InteractionItem = InteractionObject.GetComponent<Item>();
            InteractionItem.player = this;
        }
    }

    Item InteractionItem;

    void Interaction()
    {
        if(InteractionItem != null)
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
        if(currentItem == 0 ||currentItem == 1)
        {
            Throw_Out_Item();

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

            itemRG[currentItem].velocity = Vector3.zero;

            InteractionObject.parent = hand.transform;
            itemCol[currentItem].isTrigger = true;
            itemRG[currentItem].useGravity = false;
        }
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

    void Zoom()
    {
        Debug.Log("ZOOOOOOOOOOOOOOOOOOOOOOM");
    }

    void Die()
    {
        Debug.Log("으앙쥬금");
        NpcManager.instance.map.result_popup.On_Result_Popup(1);
    }
}
