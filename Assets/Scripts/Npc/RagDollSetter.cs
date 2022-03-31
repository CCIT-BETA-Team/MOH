using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagDollSetter : MonoBehaviour
{
   public Animator animator;

    Rigidbody[] rigs;
    // Start is called before the first frame update
    private void Awake()
    {
        rigs = transform.GetComponentsInChildren<Rigidbody>();
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
           }
     }
     else
     {
            GetComponent<Animator>().enabled=true;
            foreach (Rigidbody r in rigs)
            {
                r.isKinematic = true;
            }
        }
    }
  
}
