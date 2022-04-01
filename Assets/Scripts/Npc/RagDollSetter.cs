using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System;
public class RagDollSetter : MonoBehaviour
{
   Animator animator;

    Rigidbody[] rigs;
    // Start is called before the first frame update
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rigs = transform.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody r in rigs)
        {
            if (r.GetComponent<XRGrabInteractable>() == null)
            {
                XRGrabInteractable i_Grab = r.gameObject.AddComponent<XRGrabInteractable>();

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
            GetComponent<Animator>().enabled = false;
            foreach (Rigidbody r in rigs)
           {
                r.isKinematic = false;
                r.gameObject.GetComponent<XRGrabInteractable>().enabled = true;
           }
     }
     else
     {
            GetComponent<Animator>().enabled=true;
            foreach (Rigidbody r in rigs)
            {

                r.isKinematic = true;
                r.gameObject.GetComponent<XRGrabInteractable>().enabled = false;
            }
        }
    }
  
}
