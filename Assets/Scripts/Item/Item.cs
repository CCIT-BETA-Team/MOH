using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public int id;
    public enum itemType
    { DOOR, EQUIPMENT, FURNITURE, TOOL, EMPTYHAND }
    public itemType itemtype;
    public float price, weight;
    public bool bIsMoving, bIsQuestItem, bIsGrab;
    public Vector3 GrabRotation;
    public bool bIsLightOnPlayer;

    public abstract void interaction();
}
