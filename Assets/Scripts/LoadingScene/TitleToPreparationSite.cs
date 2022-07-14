using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleToPreparationSite : MonoBehaviour
{
    public GameObject button;

    public void gotoPreparationSite()
    {
        ScenesManager.instance.Load_Scene("PreparationSite", ScenesManager.LoadingType.DIRECT);
    }
}
