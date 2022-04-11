using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("플레이어 프리즈")]
    public bool freeze;

    [Header("플레이어 신체")]
    public Camera cam;
    public GameObject hand;
    public Item emptyhand;

    [Header("플레이어 스테이터스")]
    public int health;
    public int money;
    public float defaultSpped;
    public float walkingSpeed;
    public float runningSpeed;
    public float unlockSpeed;
    public float noiseValue;
     
    [Space]
    [Header("플레이어 아이템 관련")]
    bool isHoldingItem;
    bool getQuestItem;
    public List<Item> itemBag = new List<Item>();
    public List<Rigidbody> itemRG = new List<Rigidbody>();
    public List<Collider> itemCol = new List<Collider>();
    int currentItem;
    Item unlockTool;
    public Transform InteractionObject;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        switch (GameManager.Platform)
        {
            case 0: //오큘러스
                Debug.Log("Android Hi~");
                break;
            case 1: //PC
                Debug.Log("Window Hi~");
                break;
        }
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
    }

    Vector2 turn;
    float sensitivity = 0.5f;

    void Control()
    {
        turn.x += Input.GetAxis("Mouse X") * sensitivity;
        turn.y += Input.GetAxis("Mouse Y") * sensitivity;
        transform.localRotation = Quaternion.Euler(0, turn.x, 0);
        cam.transform.localRotation = Quaternion.Euler(-turn.y, 0, 0);

        //이동
        if (Input.GetKey(KeyCode.W)) { transform.Translate(Vector3.forward * Time.deltaTime * walkingSpeed); }
        if (Input.GetKey(KeyCode.A)) { transform.Translate(Vector3.left * Time.deltaTime * walkingSpeed); }
        if (Input.GetKey(KeyCode.S)) { transform.Translate(Vector3.back * Time.deltaTime * walkingSpeed); }
        if (Input.GetKey(KeyCode.D)) { transform.Translate(Vector3.right * Time.deltaTime * walkingSpeed); }
        if (Input.GetKey(KeyCode.Space)) { }

        //상호작용
        if (Input.GetKeyDown(KeyCode.E)) { ItemCheck(); }
        if (Input.GetKey(KeyCode.E)) { Interaction(); }

        //장비
        if (Input.GetMouseButton(1)) { Zoom(); }
        else if (Input.GetMouseButton(0)) { Use_Item(); }
        if (Input.GetMouseButton(1) && Input.GetMouseButtonDown(0)) { Throw_Item(); }

        if (Input.GetKeyDown(KeyCode.Alpha1)) { ItemSwitch(itemBag[0]); currentItem = 0; }
        if (Input.GetKeyDown(KeyCode.Alpha2)) { ItemSwitch(itemBag[1]); currentItem = 1; }
        if (Input.GetKeyDown(KeyCode.Alpha3)) { ItemSwitch(itemBag[2]); currentItem = 2; }
        if (Input.GetKeyDown(KeyCode.Alpha4)) { ItemSwitch(itemBag[3]); currentItem = 3; }
    }

    void ItemSwitch(Item item)
    {
        itemBag[currentItem].gameObject.SetActive(false);
        item.gameObject.SetActive(true);
    }

    Ray ray;
    RaycastHit hit;

    void ItemCheck()
    {
        ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        hit = new RaycastHit();

        Debug.DrawRay(ray.origin, ray.direction * 10, Color.red);

        if(Physics.Raycast(ray, out hit)) { InteractionObject = hit.transform; }
    }

    Item InteractionItem;

    void Interaction()
    {
        InteractionItem = InteractionObject.GetComponent<Item>();

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

    void Pickup_Item()
    {
        itemBag[currentItem] = InteractionItem;
        itemRG[currentItem] = InteractionItem.GetComponent<Rigidbody>();
        itemCol[currentItem] = InteractionItem.GetComponent<Collider>();

        itemRG[currentItem].velocity = Vector3.zero;

        InteractionObject.parent = hand.transform;
        itemCol[currentItem].isTrigger = true;
        itemRG[currentItem].useGravity = false;
        InteractionObject.position = hand.transform.position;
    }

    void Throw_Item()
    {
        Ray throwRay = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        itemBag[currentItem].transform.parent = null;
        itemRG[currentItem].AddForce(throwRay.direction * 1000);
        itemCol[currentItem].isTrigger = false;
        itemRG[currentItem].useGravity = true;

        itemBag[currentItem] = emptyhand;
        itemCol[currentItem] = null;
        itemRG[currentItem] = null;
    }

     void Use_Item()
    {
        if(InteractionItem.itemtype == Item.itemType.TOOL || InteractionItem.itemtype == Item.itemType.EQUIPMENT)
        {
            InteractionItem.interaction();
        }
    }

    void Zoom()
    {
        Debug.Log("ZOOOOOOOOOOOOOOOOOOOOOOM");
    }

    void Die()
    {
        Debug.Log("으앙쥬금");
    }
}
