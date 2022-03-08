using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Item
{
    public bool isLock;
    Rigidbody rg;
    float value;
    public float sensitivity = 10f;
    public enum DoorDir { FRONT, BACK, LEFT ,RIGHT}
    public DoorDir doorDir;
    public GameObject door;
    public Door another_handle;
    void Start()
    {
        rg = GetComponent<Rigidbody>();
    }

    public override void interaction()
    {
        if(isLock)
        {
            Debug.Log("잠겨있어요");
        }
        else if(!isLock)
        {
            switch(doorDir)
            {
                case DoorDir.FRONT:
                    value += Input.GetAxis("Mouse Y") * sensitivity;
                    break;
                case DoorDir.BACK:
                    value -= Input.GetAxis("Mouse Y") * sensitivity;
                    break;
                case DoorDir.LEFT:
                    break;
                case DoorDir.RIGHT:
                    break;
            }
            door.transform.rotation = Quaternion.Euler(0, transform.rotation.y - value, 0);
            if (another_handle != null)
            {
                Connecting();
            }
        }
    }

    //test code 안드로이드 빌드 제한은 추후에 (VR외 로 사용금지)
    public  void interaction(Vector2 vrinput)
    {
        if (isLock)
        {
            Debug.Log("잠겨있어요");
        }
        else if (!isLock)
        {
            switch (doorDir)
            {
                case DoorDir.FRONT:
                    value += vrinput.x * sensitivity;
                    break;
                case DoorDir.BACK:
                    value -= vrinput.x * sensitivity;
                    break;
                case DoorDir.LEFT:
                    break;
                case DoorDir.RIGHT:
                    break;
            }
            door.transform.rotation = Quaternion.Euler(0, transform.rotation.y - value, 0);
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
}
