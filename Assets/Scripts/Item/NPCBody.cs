using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_ANDROID
using UnityEngine.XR.Interaction.Toolkit;
#endif

public class NPCBody : Item
{
    private void Awake()
    {
        if(GetComponent<FixedJoint>()!=null)
        {
            gameObject.AddComponent<FixedJoint>();
        }
#if UNITY_ANDROID
        if(GetComponent<XRGrabInteractable>()!=null)
        {
         gameObject.AddComponent<XRGrabInteractable>();
        }
#endif
    }
    public override void interaction()
    {
        //Grab Ragdoll
        throw new System.NotImplementedException();
    }
}
