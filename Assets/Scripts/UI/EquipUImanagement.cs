using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EquipUImanagement : Singleton<EquipUImanagement>
{
    // Start is called before the first frame update
    public bool available = true;
    public KeyCode key= KeyCode.Mouse2;
    public GameObject[] UIs;
    public EquipUI[] UI_comps;
    public Image[] select_images;
    public Item selected_obj;
    public EquipUI.equip_num equip_num;

    private void Update()
    {
        if(available)
        {
            if (Input.GetKeyDown(key))
            {
                GameManager.instance.Player.GetComponent<p_Player>().Freezing();
                EnableUI();
                Time.timeScale = 0.1f;
                Cursor.lockState = CursorLockMode.None;
                PopupManager.instance.cross_head.enabled = false;
            }
            if (Input.GetKeyUp(key))
            {
                GameManager.instance.Player.GetComponent<p_Player>().Melting();
                DisableUI();
                Time.timeScale = 1f;
                Cursor.lockState = CursorLockMode.Locked;
                PopupManager.instance.cross_head.enabled = true;
            }
        } 
    }
    public void Selected()
    {
        //selected_obj ·Î ¿ä¸®
        Player p = GameManager.instance.player_comp;
        switch (equip_num)
        {
            case EquipUI.equip_num.EQUIPMENT_1:
                p.ItemSwitch(p.itemBag[0]);
                p.currentItem = 0;
                break;
            case EquipUI.equip_num.EQUIPMENT_2:
                p.ItemSwitch(p.itemBag[1]);
                p.currentItem = 1;
                break;
            case EquipUI.equip_num.EQUIPMENT_3:
                p.ItemSwitch(p.itemBag[2]);
                p.currentItem = 2;
                break;
            case EquipUI.equip_num.EQUIPMENT_4:
                p.ItemSwitch(p.itemBag[3]);
                p.currentItem = 3;
                break;
        }
    }

    private void DisableUI()
    {
      foreach(GameObject i in UIs)
      {
           
            if(i.GetComponent<EquipUI>()!=null)
            {
                i.GetComponent<EquipUI>().Color_clear();
            }
            else if(i.GetComponentInChildren<Text>()!=null)
            {
                i.GetComponentInChildren<Text>().text = "";
            }
            i.SetActive(false);
      }
      for(int i = 0; i < UI_comps.Length; i++)
            UI_comps[i].selet_image.color = UI_comps[i].selet_image_colors[0];
    }
    private void EnableUI()
    {
        foreach (GameObject i in UIs)
        {
            i.SetActive(true);
            if (i.GetComponent<EquipUI>()!=null)
            {
                i.GetComponent<EquipUI>().Update_equipUI();
            }
            
        }
        UI_comps[GameManager.instance.player_comp.currentItem].is_select = true;
        UI_comps[GameManager.instance.player_comp.currentItem].selet_image.color = UI_comps[GameManager.instance.player_comp.currentItem].selet_image_colors[2];
    }
 
}
