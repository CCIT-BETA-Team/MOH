using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostCamera : MonoBehaviour
{
    public Camera cam;
    public float speed;
    protected float sensitivity = 0.5f;
    public Rigidbody rg;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Control();
    }

    Vector2 turn;

    void Control()
    {
        turn.x += Input.GetAxis("Mouse X") * sensitivity;
        turn.y += Input.GetAxis("Mouse Y") * sensitivity;
        transform.localRotation = Quaternion.Euler(0, turn.x, 0);
        cam.transform.localRotation = Quaternion.Euler(-turn.y, 0, 0);

        if (Input.GetKey(KeyCode.W)) { transform.Translate(Vector3.forward * speed); }
        if (Input.GetKey(KeyCode.A)) { transform.Translate(Vector3.left * speed); }
        if (Input.GetKey(KeyCode.S)) { transform.Translate(Vector3.back * speed); }
        if (Input.GetKey(KeyCode.D)) { transform.Translate(Vector3.right * speed); }
        if (Input.GetKey(KeyCode.Space)) { transform.Translate(Vector3.up * speed); }
        if (Input.GetKey(KeyCode.LeftShift)) { transform.Translate(Vector3.down * speed); }
    }
}
