using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public Rigidbody rig;
    bool activated;
    public bool attach_object;
    bool attached = false;
    private void FixedUpdate()
    {
        if (activated)
        {
              if(!attached)
              {
                rig.AddForce(transform.forward * speed, ForceMode.Force);
              }
        }
    }
    
    public void Burst()
    {
        if (!activated)
        {
            activated = true;
          //  rig.AddForce(transform.forward*1000,ForceMode.Impulse);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {

        if(attach_object)
        {
            Debug.Log("isAttached");
            if (!attached)
            {
                this.transform.parent = collision.collider.transform;
                Debug.Log("Attached");

                rig.freezeRotation = true;
                rig.velocity = new Vector3(0, 0, 0);
                rig.angularVelocity = new Vector3(0, 0, 0);
                
                
                
            }
        }
      
       
    }
}
