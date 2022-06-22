using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScenesManager : Singleton<ScenesManager>
{
    static string nextScene;
    public Text loadingText;

    [SerializeField]
    Image LoadingBar;


    public void Load_Sence(string SceneName)
    {
        //loadingText = this.gameObject.transform.GetChild(3).gameObject.GetComponent<Text>();
        //LoadingBar = this.gameObject.transform.GetChild(2).gameObject.GetComponent<Image>();
        nextScene = SceneName;
        SceneManager.LoadScene(SceneName);
    }
    int scene_num;
    private void Awake()
    {
        scene_num = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("456456456 +" + scene_num);
    }
    void Start()
    {
        StartCoroutine(LoadSceneProcess());
    }

    IEnumerator LoadSceneProcess()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(scene_num + 1);
        op.allowSceneActivation = false;
        float timer = 0f;
        while(!op.isDone)
        {
            yield return null;

            if(op.progress<0.001f)
            {
                LoadingBar.fillAmount = op.progress;
                loadingText.text = (Math.Truncate(LoadingBar.fillAmount * 100)).ToString() + "%";
            }
            else
            {
                timer += Time.deltaTime;
                LoadingBar.fillAmount = Mathf.Lerp(0.001f, 1.0f, timer);
                loadingText.text = (Math.Truncate(LoadingBar.fillAmount * 100)).ToString() + "%";
                if(LoadingBar.fillAmount>=1.0f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}
