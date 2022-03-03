using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestItem : Item
{
    public override void interaction()
    {
        Debug.Log("테스트 아이템 사용");
    }
}
