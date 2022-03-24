using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    private Poolable poolobj;
    [SerializeField]
    private int allocateCount;

    private Stack<Poolable> poolstack = new Stack<Poolable>();

    private void Start()
    {
        Allocate();
    }

    public void Allocate()
    {
        for(int i = 0;i < allocateCount; i++)
        {
            Poolable allocateObj = Instantiate(poolobj, this.gameObject.transform);
            allocateObj.Create(this);
            poolstack.Push(allocateObj);
        }
    }

    public GameObject Pop()
    {
        Poolable obj;
        if (poolstack.Count != 0)
        {
            obj = poolstack.Pop();
            obj.gameObject.SetActive(true);
        }
        else
        {
            Poolable allocateObj = Instantiate(poolobj, this.gameObject.transform);
            allocateObj.Create(this);
            poolstack.Push(allocateObj);
            obj = poolstack.Pop();
            obj.gameObject.SetActive(true);
        }
        return obj.gameObject;
    }

    public void Push(Poolable obj)
    {
        obj.gameObject.SetActive(false);
        poolstack.Push(obj);
    }
}
