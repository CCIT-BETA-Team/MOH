using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furniture : Item
{
    [Space]
    public Transform spot;
    public Vector3 anim_view_dir;
    private void Awake()
    {
        //enter_spot = spot;
    }
    public override void interaction()
    {

    }
}
