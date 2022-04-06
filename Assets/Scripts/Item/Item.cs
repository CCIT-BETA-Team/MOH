using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Item : MonoBehaviour
{
    public int id;
    public string item_name;
    public enum itemType
    { DOOR, EQUIPMENT, FURNITURE, TOOL, EMPTYHAND }
    public itemType itemtype;
    public float price, weight;
    public bool bIsMoving, bIsQuestItem, bIsGrab;
    public Vector3 GrabRotation;
    public bool bIsLightOnPlayer;
    public Sprite item_image;

    public abstract void interaction();
}
