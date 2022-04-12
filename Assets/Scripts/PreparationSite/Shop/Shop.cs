using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject[] Categories;

    public void Load_Category_0()
    {
        Categories[0].SetActive(true);
        Categories[1].SetActive(false);
        Categories[2].SetActive(false);
    }
    public void Load_Category_1()
    {
        Categories[0].SetActive(false);
        Categories[1].SetActive(true);
        Categories[2].SetActive(false);
    }
    public void Load_Category_2()
    {
        Categories[0].SetActive(false);
        Categories[1].SetActive(false);
        Categories[2].SetActive(true);
    }
}
