using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemotedObject : Item
{
    // Start is called before the first frame update
    public enum RemoteType
    {
    LIGHT,
    ANIMATION
    }
    public AnimationClip ani;
    public Animation animation;
    float animationspeed = -0.04f;
    public RemoteType object_remotetype;


    Lighting light;
    public void Regist()
    {
        if(RemoteManager.instance != null)
        {
            RemoteManager.instance.remoted_objects.Add(this);
        }
    }
     void Awake()
    {
        Regist();
        if(GetComponent<Animation>()!=null)
        {
            animation = GetComponent<Animation>();
            ani = animation.clip;
        }    
        if(GetComponent<Lighting>()!=null)
        {
            light = GetComponent<Lighting>();
        }
    }
    private void Update()
    {
    
        #if UNITY_EDITOR
        if(Input.GetKeyDown(KeyCode.M))
        {
            interaction();
        }
        #endif
        if(animation[ani.name].normalizedTime>0.95f && animation[ani.name].speed != 0)
        {
            animation[ani.name].speed = 0;
            animation[ani.name].normalizedTime = 0.94f;


        }
    }
    
    public override void interaction()
    {
        switch (object_remotetype)
        {
            case RemoteType.LIGHT:
               
                if(light.on_off)
                {
                    light.on_off = false;
                    light.Light_Update();
                }
                else
                {
                    light.on_off = true;
                    light.Light_Update();
                }
                break;
            case RemoteType.ANIMATION:
                animation[ani.name].speed = -animationspeed;
            
                animationspeed = -animationspeed;
                animation.Play(ani.name);
                break;
        }

    }
}
