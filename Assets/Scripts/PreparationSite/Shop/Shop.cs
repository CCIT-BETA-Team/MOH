using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public GameObject[] Categories;

    public Image[] lock_image;

    void Start()
    {
        if (PlayerPrefs.GetInt("pMyung_Seong") >= 0)
        {
            lock_image[0].enabled = false;
        }
        if (PlayerPrefs.GetInt("pMyung_Seong") >= 100)
        {
            lock_image[1].enabled = false;
        }
        if (PlayerPrefs.GetInt("pMyung_Seong") >= 200)
        {
            lock_image[2].enabled = false;
        }
    }

    public void Load_Category_0()
    {
        Categories[0].SetActive(true);
        Categories[1].SetActive(false);
        Categories[2].SetActive(false);
    }
    public void Load_Category_1()
    {
        if(PlayerPrefs.GetInt("pMyung_Seong") >= 100)
        {
            Categories[0].SetActive(false);
            Categories[1].SetActive(true);
            Categories[2].SetActive(false);
        }
    }
    public void Load_Category_2()
    {
        if(PlayerPrefs.GetInt("pMyung_Seong") >= 200)
        {
            Categories[0].SetActive(false);
            Categories[1].SetActive(false);
            Categories[2].SetActive(true);
        }
    }
}
