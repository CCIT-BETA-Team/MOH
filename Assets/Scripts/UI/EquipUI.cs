using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EquipUI : MonoBehaviour , IPointerEnterHandler,IPointerExitHandler,IPointerUpHandler,IPointerClickHandler,IPointerDownHandler
{
    public Item current_item= null;
    public Image item_icon= null;
    public Image item_ui = null;
    public static Color32 selectedcolor = new Color32(125,125,125,255);
    public static Color32 defalutcolor = new Color32(255,255,255,255);
    public static Color32 hovercolor = new Color32(170,170,170,255);
    public float test_speed = 0.02f;
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
        if(item_ui==null)
        {
            item_ui = GetComponent<Image>();
        }
        // 기본 4개의 이미지 연결 선행 필요
        if (item_icon!=null)
        {
            Update_equipUI();
        }
       
    }
    public void Point_Out_Animation()
    {

    }
    IEnumerator Point_Out_Ani()
    {
        if(item_ui.color.r==1)
        {
            Debug.Log("Stop Coroutine");
            StopCoroutine(Point_Out_Ani());
        }
        yield return new WaitForSeconds(Time.deltaTime);
        item_ui.color = new Color((item_ui.color.r + (0.1f)), (item_ui.color.r+(0.1f)), (item_ui.color.r + (0.1f)), 1);
        StartCoroutine(Point_Out_Ani());
    }
 
    public void Point_In_Animation()
    {
        StopCoroutine(Point_Out_Ani());
        item_ui.color = hovercolor;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        
        StartCoroutine(Point_Out_Ani());
        Debug.Log("Exit");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Point_In_Animation();
        Debug.Log("Enter");
    }

 
    public void OnPointerUp(PointerEventData eventData)
    {
        item_ui.color = defalutcolor;
        Debug.Log("Click");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
      
        Debug.Log("Click2"); 
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        item_ui.color = selectedcolor;
        Debug.Log("Click2");
    }
}
