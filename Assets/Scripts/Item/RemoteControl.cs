using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteControl: Item
{

    public bool universal_remocon;
    public float ray_distance = 100;
    [Tooltip("���� �������� �Ŵ������� ���� �����ʿ�.//�� �����ܿ� �� �����ϴ� ������Ʈ���� ���� ���� // ����� �׽�Ʈ������ public ������")]
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
