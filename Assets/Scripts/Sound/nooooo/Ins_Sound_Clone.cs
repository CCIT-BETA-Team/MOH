using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ins_Sound_Clone : MonoBehaviour
{
    public GameObject audio_clone;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ObjectPoolManager.Instance.pool.Pop().transform.position = audio_clone.transform.position;
        }
    }

}
