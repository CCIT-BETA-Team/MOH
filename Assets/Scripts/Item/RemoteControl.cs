using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteControl: Item
{

    public bool universal_remocon;
    public float ray_distance = 100;
    [Tooltip("직접 넣지말고 매니저에서 따로 지정필요.//이 리모콘에 만 반응하는 오브젝트들임 오해 금지 // 현재는 테스트용으로 public 선언함")]
    public List<RemotedObject> TargetObjects;
    
    public void Resist_Target(RemotedObject target)
    {
        TargetObjects.Add(target);
    }
    private void Update()
    {
        #if UNITY_EDITOR
        Debug.DrawRay(transform.position, transform.forward, Color.red);

        if(Input.GetKeyDown(KeyCode.F))
        {
            interaction();
        }
        #endif
    }
    public override void interaction()
    {
        RaycastHit target;
   
        Physics.Raycast(transform.position, Vector3.forward, out target, ray_distance);

        if(universal_remocon)
        {
            if (target.transform != null)
            {
                target.transform.GetComponent<Item>().interaction();
            }
        }
        else
        {
            foreach (RemotedObject a in TargetObjects)
            {
                if (a == target.transform.GetComponent<RemotedObject>())
                {
                    a.interaction();
                }
            }
        }
      
       
    }
}
