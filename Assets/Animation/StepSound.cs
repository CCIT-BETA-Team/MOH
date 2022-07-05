using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepSound : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField]
    AudioClip[] groundMaterial;
    Vector2 turn;
    public Camera cam;
    Animator anim;
    public float speed = 5.0f;
    public float sensitivity = 0.5f;
    bool isMoving;
    RaycastHit hit;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }
    
    void FixedUpdate()
    {
        turn.x += Input.GetAxis("Mouse X") * sensitivity;
        turn.y += Input.GetAxis("Mouse Y") * sensitivity;
        transform.localRotation = Quaternion.Euler(0, turn.x, 0);
        cam.transform.localRotation = Quaternion.Euler(-turn.y, 0, 0);

        //¿Ãµø
        if (Input.GetKey(KeyCode.LeftShift)) 
        { 
            speed = 7.0f;
            anim.SetBool("IsRun",true);
        }
        else 
        { 
            speed = 5.0f;
            anim.SetBool("IsRun",false);
        }
        if (Input.GetKey(KeyCode.W)) { transform.Translate(Vector3.forward * Time.deltaTime * speed); }
        if (Input.GetKey(KeyCode.A)) { transform.Translate(Vector3.left * Time.deltaTime * speed); }
        if (Input.GetKey(KeyCode.S)) { transform.Translate(Vector3.back * Time.deltaTime * speed); }
        if (Input.GetKey(KeyCode.D)) { transform.Translate(Vector3.right * Time.deltaTime * speed); }
    }

    void Update()
    {
        Debug.DrawRay(transform.position, new Vector3(0, -1, 0) * 3.0f, Color.green);
        if (Physics.Raycast(transform.position, new Vector3(0, -1, 0), out hit))
        {
            if (hit.collider.CompareTag("grass"))
            {
                audioSource.clip = groundMaterial[1];
                Debug.Log("grass");
            }
            else if (hit.collider.CompareTag("wood"))
            {
                audioSource.clip = groundMaterial[2];
                Debug.Log("wood");
            }
            else if (hit.collider.CompareTag("ground"))
            {
                audioSource.clip = groundMaterial[0];
                Debug.Log("ground");
            }
            
        }
    }

    public void stepsound()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            isMoving = true;
        else
            isMoving = false;

        if (isMoving == true)
            audioSource.Play();

        else
            audioSource.Pause();
    }
    //private void OnTriggerStay(Collider other)
    //{
    //    if(other.gameObject.tag=="grass")
    //        audioSource.clip = groundMaterial[1];
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.tag == "grass")
    //        audioSource.clip = groundMaterial[0];
    //}
}
