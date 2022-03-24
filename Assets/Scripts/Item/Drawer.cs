using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawer : Item
{
    float value;
    public float sensitivity = 10f;
    float max_z, min_z;

    void Start()
    {
        min_z = transform.position.z;
        max_z = min_z + 0.3f;
    }

    public override void interaction()
    {
        value += Input.GetAxis("Mouse Y") * sensitivity;
        Debug.Log(value);
        transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Clamp(transform.position.z - value, min_z, max_z));
    }
}
