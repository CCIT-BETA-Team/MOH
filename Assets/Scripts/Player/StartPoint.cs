using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    void Awake()
    {
        GameManager.instance.Player.transform.position = transform.position;
        GameManager.instance.Player.transform.rotation = transform.rotation;
    }
}
