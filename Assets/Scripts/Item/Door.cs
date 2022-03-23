using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Item
{
    public bool isLock;
    Rigidbody rg;
    float value;
    public float sensitivity = 10f;
    public List<Room> room_list = new List<Room>();
    public enum DoorDir { FRONT, BACK, LEFT ,RIGHT,UPandDOWN}
    public DoorDir doorDir;
    public GameObject door;
    public Door another_handle;
    [Header("Front and BACK type use")]
    [Range(0,180)]
    public float maximum_y_angle=130;// �������� ������ ����
    [Header("LEFT and RIGHT type use")]
    public float maximum_x_position;
    [Header("LEFT and RIGHT type use")]
    public float maximum_y_position;

    void Start()
    {
        rg = GetComponent<Rigidbody>();
    }

    public override void interaction()
    {
        if(isLock)
        {
            Debug.Log("����־��");
        }
        else if(!isLock)
        {
            switch(doorDir)
            {
                case DoorDir.FRONT:
                    value += Input.GetAxis("Mouse Y") * sensitivity;
                    door.transform.rotation = Quaternion.Euler(0, Mathf.Clamp(door.transform.rotation.y - value, -maximum_y_angle, maximum_y_angle), 0);
                    break;
                case DoorDir.BACK:
                    value -= Input.GetAxis("Mouse Y") * sensitivity;
                    door.transform.rotation = Quaternion.Euler(0,Mathf.Clamp(door.transform.rotation.y - value,-maximum_y_angle,maximum_y_angle), 0);
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

    //test code �ȵ���̵� ���� ������ ���Ŀ� (VR�� �� ������)
    public  void interaction(Vector3 vrinput)
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
                    door.transform.rotation = Quaternion.Euler(0, Mathf.Clamp(door.transform.rotation.y - value, -maximum_y_angle, maximum_y_angle), 0);
                    break;
                case DoorDir.BACK:
                    value -= vrinput.z * sensitivity;
                    door.transform.rotation = Quaternion.Euler(0, Mathf.Clamp(door.transform.rotation.y - value, -maximum_y_angle, maximum_y_angle), 0);
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
}
