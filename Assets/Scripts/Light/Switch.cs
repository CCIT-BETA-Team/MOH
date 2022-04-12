using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : Item
{
    public List<Lighting> Lightings = new List<Lighting>();
    bool on = false;
    public GameObject lever;

    public Animation switch_ani;

    public AudioSource switch_sound;

    public Vector3 on_rotation = new Vector3(15,0,0);
    public Vector3 off_rotation = new Vector3(-15, 0, 0);

    // Update is called once per frame

    private void Awake()
    {
        Lightmanager.instance.Resist_Light(this);
    }
   

    public void Electrocity_On_Off(bool value)
    {
   
                for (int i = 0; i < Lightings.Count; i++)
                {
                    Lightings[i].electricity_property = value;
                }

    
    }
    private void Update()
    {

        #if UNITY_EDITOR
       // if (Input.anyKey)
        {
        //    interaction();
        }
        #endif
    }

    public override void interaction()
    {
        if (switch_sound != null)
        {
            switch_sound.PlayOneShot(switch_sound.clip);
        }

        if (on)
        {
            on = false;

        }
        else
        {
            on = true;
        }
        if (Lightings != null)
        {
            switch (on)
            {
                case true:
                    //animation controll

                    for (int i = 0; i < Lightings.Count; i++)
                    {
                        Lightings[i].on_off = true;
                        lever.transform.rotation = Quaternion.Euler(on_rotation);
                    }
                    break;
                case false:
                    //animation controll
                    for (int i = 0; i < Lightings.Count; i++)
                    {
                        Lightings[i].on_off = false;
                        lever.transform.rotation = Quaternion.Euler(off_rotation);
                    }
                    break;
            }
        }
    }
}
