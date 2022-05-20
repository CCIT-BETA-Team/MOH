using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemThumnail : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //public Animation anime;
    //public float anime_time;
    public ToolShelf ts;
    public Animator anime;
    public Item item;

    public Image panel;
    public Text item_name;
    public Text item_explain;
    public Text price;
    public Button buy_button;
    public Text buy_text;

    public Vector3 start_position;
    public Vector3 end_position;

    public string[] relative_path;

    int in_hashcode = Animator.StringToHash("in_ani");
    int out_hashcode = Animator.StringToHash("out_ani");

    void Start()
    {
        Buy_Check();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Image_Move(anime, start_position, end_position, anime_time);
        item_name.text = item.name;
        price.text = "$ " + item.price.ToString();

        anime.SetTrigger(in_hashcode);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Image_Move(anime, end_position, start_position, anime_time);
        anime.SetTrigger(out_hashcode);
    }

    public void Buy()
    {
        if(PlayerPrefs.GetInt("iBuy_Item[" + item.id + "]") == 0)
        {
            if(item.price <= PlayerPrefs.GetInt("pMoney", 0))
            {
                PlayerPrefs.SetInt("iBuy_Item[" + item.id + "]", 1);
                PlayerPrefs.SetInt("pMoney", PlayerPrefs.GetInt("pMoney") - item.price);
                Debug.Log(item.name + " 구매 완료!!");
                PlayerPrefs.SetInt("shop_tool" + item.id, 1);
                ts.tools[item.id].SetActive(true);
                Buy_Check();
            }
            else
                Debug.Log("돈이 부족해요..."); //차후 돈 부족 팝업으로 교체
        }
    }

    void Buy_Check()
    {
        if (PlayerPrefs.GetInt("iBuy_Item[" + item.id + "]") == 0)
            buy_text.text = "구매";
        else
            buy_text.text = "구매 완료";
    }
}
