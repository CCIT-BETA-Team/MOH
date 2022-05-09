using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : Singleton<ScenesManager>
{
    public void Load_Sence(string s)
    {
        SceneManager.LoadScene(s);
    }
}
