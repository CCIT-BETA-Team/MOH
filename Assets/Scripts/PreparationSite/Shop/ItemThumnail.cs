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

    public Vector3 start_position;
    public Vector3 end_position;

    public string[] relative_path;

    int in_hashcode = Animator.StringToHash("in_ani");
    int out_hashcode = Animator.StringToHash("out_ani");

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
        if(true)
        {
            //money -= item.price;
            Debug.Log(item.name + " 구매 완료!!");
            PlayerPrefs.SetInt("shop_tool" + item.id, 1);
            ts.tools[item.id].SetActive(true);
        }
    }

    //void Image_Move(Animation anime, Vector3 start_pos, Vector3 end_pos, float time)
    //{
    //    AnimationClip clip = new AnimationClip();
    //    clip.legacy = true;

    //    AnimationCurve curve = AnimationCurve.Linear(0.0f, start_pos.x, time, end_pos.x);
    //    clip.SetCurve(relative_path[3], typeof(Transform), "localPosition.y", curve);

    //    anime.AddClip(clip, clip.name);
    //    Debug.Log(clip.name);
    //    anime.Play(clip.name);
    //}
}
