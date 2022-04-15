using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagDollBody : MonoBehaviour
{
    RagDollSetter ragdoll;
    public RagDollSetter p_ragdoll
    {
        get { return ragdoll; }
        set {ragdoll = value; }
    }
    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.GetComponent<Item>() != null)
        {
            if (collision.transform.GetComponent<Item>().weight > 50)
            {
                ragdoll.RagdollOnOff(true);

            }
        }
    }
}
