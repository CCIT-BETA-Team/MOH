using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EquipUI : MonoBehaviour , IPointerEnterHandler,IPointerExitHandler,IPointerUpHandler,IPointerClickHandler,IPointerDownHandler
{
    public EquipUImanagement manager;
    public Item current_item= null;
    public Image item_icon= null;
    public Image item_ui = null;
    public Text text = null;
    
    public static Color32 selectedcolor = new Color32(125,125,125,255);
    public static Color32 defalutcolor = new Color32(255,255,255,255);
    public static Color32 hovercolor = new Color32(170,170,170,255);
    public enum equip_num{
    NONE=-1,
    EQUIPMENT_1,
    EQUIPMENT_2,
    EQUIPMENT_3,
    EQUIPMENT_4,
    }
    public p_Player player;
    
    public equip_num current_item_num = equip_num.NONE;
    public void Update_equipUI()
    {
        if(player ==null)
        {
            player = GameManager.instance.Player.GetComponent<p_Player>();
        }
        
        if ((int)current_item_num != -1)
        {
            if (player.itemBag[(int)current_item_num] != null)
            {
                current_item = player.itemBag[(int)current_item_num];
                if (player.itemBag[(int)current_item_num].item_image != null)
                {
                    item_icon.sprite = player.itemBag[(int)current_item_num].item_image;
                }
                else
                {
                    item_icon.sprite = Resources.Load<Sprite>("Itembox");
                }
            }
            else
            {
                item_icon.sprite = null;
            }
        }
       
    }
 
    IEnumerator Point_Out_Ani()
    {
        while(item_ui.color.r!=1&& current_item!=null)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            Debug.Log(item_ui.color.r);
            item_ui.color = new Color((Mathf.Clamp(item_ui.color.r + (1.0f / 254), 0, 1)), (Mathf.Clamp(item_ui.color.r + (1.0f / 254), 0, 1)), (Mathf.Clamp(item_ui.color.r + (1.0f / 254), 0, 1)), 1);

        }

    }
    public void Point_In_Animation()
    {
        if(current_item!=null)
            item_ui.color = hovercolor;
            StopCoroutine(Point_Out_Ani());
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (current_item != null)
            StartCoroutine(Point_Out_Ani());
            text.text = "";

    }
    public void Color_clear()
    {
        if (current_item != null)
            item_ui.color = defalutcolor;

    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (current_item != null)
            manager.selected_obj = current_item;
            Point_In_Animation();
            text.text = current_item.item_name;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        Color_clear();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (current_item != null)
            manager.Selected();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (current_item != null)
            item_ui.color = selectedcolor;
    }
}
