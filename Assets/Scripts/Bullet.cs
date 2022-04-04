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
     
        
    }
    public void Bust()
    {
        if (!activated)
        {
            rig.velocity = transform.forward * speed;
            activated = true;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {

        if(attach_object)
        {
            if (!attached)
            {
                rig.velocity = new Vector3(0, 0, 0);
                rig.angularVelocity = new Vector3(0, 0, 0);
                this.transform.parent = collision.collider.transform;
                rig.freezeRotation = true;
                
            }
        }
      
       
    }
}
