using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System;
public class RagDollSetter : Item
{
  public Animator animator;

    public Rigidbody[] rigs;
    public Collider[] cols;

    public Rigidbody test_r;
    public Collider test_C;
    public GameObject test_O;

    // Start is called before the first frame update
    private void Awake()
    {
     //   animator = GetComponent<Animator>();
        rigs = transform.GetComponentsInChildren<Rigidbody>();
        cols = transform.GetComponentsInChildren<Collider>();

        foreach (Rigidbody r in rigs)
        {
            if (r.GetComponent<XRGrabInteractable>() == null)
            {
                XRGrabInteractable i_Grab = r.gameObject.AddComponent<XRGrabInteractable>();
                RagDollBody ragbody =r.gameObject.AddComponent<RagDollBody>();
                i_Grab.colliders.RemoveRange(1,i_Grab.colliders.Count-1);
               
                i_Grab.attachTransform = r.transform;
                ragbody.p_ragdoll = this;
                i_Grab.smoothPosition = true;
                i_Grab.smoothRotation = true;
            }
        }
        RagdollOnOff(false);
      
    }

    private void Update()
    {
#if UNITY_EDITOR
        if(Input.GetKeyDown(KeyCode.G))
        {
        
            RagdollOnOff(true);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {

            RagdollOnOff(false);
            animator.SetTrigger("Up");
        }
#endif
    }
    public void RagdollOnOff(bool v)
    {
     if(v)
     {
            animator.enabled = false;
            foreach (Rigidbody r in rigs)
           {
                r.isKinematic = false;
#if UNITY_ADROID
                r.gameObject.GetComponent<XRGrabInteractable>().enabled = true;
#endif
           }
     }
     else
     { 
            animator.enabled=true;
            foreach (Rigidbody r in rigs)
            {

                r.isKinematic = true;
#if UNITY_ADROID
                r.gameObject.GetComponent<XRGrabInteractable>().enabled = false;
#endif
            }
        }
    }

    public override void interaction()
    {
        
    }
}
