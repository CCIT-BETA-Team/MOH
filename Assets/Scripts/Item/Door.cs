using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Item
{
    public bool isLock =false;
    public float unlock_gauge;
    public float gauge_max;
    Rigidbody rg;
    float value;
    public float sensitivity = 10f;
    public List<Room> room_list = new List<Room>();
    public enum DoorDir { FRONT, BACK, LEFT ,RIGHT,UPandDOWN}
    public DoorDir doorDir;
    public GameObject door;
    public GameObject door_parent;
    public Door another_handle;
    [Header("Front and BACK type use")]
    [Range(0,180)]
    public float maximum_y_angle=130;// 정문기준 멀쩡한 각도
    [Header("LEFT and RIGHT type use")]
    public float maximum_x_position;
    [Header("LEFT and RIGHT type use")]
    public float maximum_y_position;

    public float door_rotation { get { return door.transform.localRotation.y; } }
    public bool is_open
    {
        get
        {
            if(door_rotation != 0) { return true; }
            else { return false; }
        }
    }
    public bool test;

    void Start()
    {
        rg = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(test != is_open) { test = is_open; }
    }

    public override void interaction()
    {
        if(isLock)
        {
            Unlocking(player.unlock_speed);
        }
        else if(!isLock)
        {
            switch(doorDir)
            {
                case DoorDir.FRONT:
                    value += Input.GetAxis("Mouse Y") * sensitivity;
                    door.transform.localRotation = Quaternion.Euler(0, Mathf.Clamp(door_parent.transform.rotation.y + door.transform.rotation.y - value, -maximum_y_angle, maximum_y_angle), 0);
                    break;
                case DoorDir.BACK:
                    value -= Input.GetAxis("Mouse Y") * sensitivity;
                    door.transform.localRotation = Quaternion.Euler(0,Mathf.Clamp(door_parent.transform.rotation.y + door.transform.rotation.y - value,-maximum_y_angle,maximum_y_angle), 0);
                    break;
                case DoorDir.LEFT:
                    value -= Input.GetAxis("Mouse X") * sensitivity;
                    door.transform.position = new Vector3(door.transform.position.x + value, door.transform.position.y, door.transform.position.z);
                    break;
                case DoorDir.RIGHT:
                    value += Input.GetAxis("Mouse X") * sensitivity;
                    door.transform.position = new Vector3(door.transform.position.x + value, door.transform.position.y, door.transform.position.z);
                    break;
            }
           
            if (another_handle != null)
            {
                Connecting();
            }
        }
    }

    //test code 안드로이드 빌드 제한은 추후에 (VR외 로 사용금지)
    public void interaction(Vector3 vrinput)
    {
        if (isLock)
        {
            Debug.Log("Lock");
        }
        else if (!isLock)
        {
            switch (doorDir)
            {
                case DoorDir.FRONT:
                    value += vrinput.z * sensitivity;
                    door.transform.localRotation = Quaternion.Euler(0, Mathf.Clamp(door.transform.localRotation.y - value, -maximum_y_angle, maximum_y_angle), 0);
                    break;
                case DoorDir.BACK:
                    value -= vrinput.z * sensitivity;
                    door.transform.localRotation = Quaternion.Euler(0, Mathf.Clamp(door.transform.localRotation.y - value, -maximum_y_angle, maximum_y_angle), 0);
                    break;
                case DoorDir.LEFT:
                    value -= vrinput.x * sensitivity;
                    door.transform.position = new Vector3(door.transform.position.x +value , door.transform.position.y, door.transform.position.z);

                    break;
                case DoorDir.RIGHT:
                    value += vrinput.x * sensitivity;
                    door.transform.position = new Vector3(door.transform.position.x + value , door.transform.position.y, door.transform.position.z);
                    break;
                case DoorDir.UPandDOWN:
                    break;
            }
            
            if(another_handle != null)
            {
                Connecting();
            }
        }
    }

    public void Connecting()
    {
        another_handle.value = value;
    }

    public void Unlocking(float unlock_speed)
    {
        if (unlock_gauge > 0)
        {
            if (!PopupManager.instance.door_unlock_slider.gameObject.activeSelf) PopupManager.instance.door_unlock_slider.gameObject.SetActive(true);
            if (PopupManager.instance.door_unlock_slider.maxValue != gauge_max) PopupManager.instance.door_unlock_slider.maxValue = gauge_max;
            if (PopupManager.instance.door_unlock_slider.value != unlock_gauge) PopupManager.instance.door_unlock_slider.value = unlock_gauge;
            if (!player.move) { player.move = true; }
            unlock_gauge -= Time.deltaTime * unlock_speed;
        }
        else
        {
            if (PopupManager.instance.door_unlock_slider.gameObject.activeSelf) { PopupManager.instance.door_unlock_slider.gameObject.SetActive(false); }
            isLock = false;
            unlock_gauge = 0;
            another_handle.unlock_gauge = 0;
        }
    }

    public override void Door_Unlock_Gauge_Init()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            if (player.move) { player.move = false; }
            PopupManager.instance.door_unlock_slider.gameObject.SetActive(false);
            unlock_gauge = gauge_max;
        }
    }
}
