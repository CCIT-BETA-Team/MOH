using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EquipUI : MonoBehaviour , IPointerEnterHandler,IPointerExitHandler,IPointerUpHandler,IPointerClickHandler
{
    public Item current_item= null;
    public Image item_icon= null;
    public enum equip_num{
    NONE,
    EQUIPMENT_1,
    EQUIPMENT_2,
    EQUIPMENT_3,
    EQUIPMENT_4,
    }
    public equip_num current_item_num = equip_num.NONE;
    public void Update_equipUI()
    {
        if(current_item_num !=0)
        {
            current_item = GameManager.instance.Player.GetComponent<p_Player>().itemBag[(int)current_item_num];
            item_icon.sprite = GameManager.instance.Player.GetComponent<p_Player>().itemBag[(int)current_item_num].item_image;
        }
        else
        {
            //default
        }
    }
    private void Start()
    {
        if (item_icon == null)
        {
            item_icon=GetComponent<Image>();
        }
        Update_equipUI();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Exit");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Enter");
    }

 
    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Click");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Click2"); 
    }
}
