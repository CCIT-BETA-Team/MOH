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
    public Text missionText;
    public Text tipText;
    public Text anyKeyText;
    public string[] str;
    AsyncOperation op;

    [SerializeField]
    Image LoadingBar;

    public void Load_Scene(string SceneName)
    {
        nextScene = SceneName;
        SceneManager.LoadScene("LoadingScene");
    }

    void Start()
    {
        StartCoroutine(LoadSceneProcess());
    }

    void Update()
    {
        if (Input.anyKey)
            op.allowSceneActivation = true;
    }

    IEnumerator LoadSceneProcess()
    {
        missionText.text = GameManager.instance.select_mission.mission_name;
        op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;
        float timer = 0f;
        int index = UnityEngine.Random.Range(0, str.Length);
        tipText.text = str[index];
        while(!op.isDone)
        {
            yield return null;

            if (op.progress < 0.001f)
            {
                LoadingBar.fillAmount = op.progress;
                loadingText.text = (Math.Truncate(LoadingBar.fillAmount * 100)).ToString() + "%";
            }
            else
            {
                timer += Time.deltaTime;
                LoadingBar.fillAmount = Mathf.Lerp(0.001f, 1.0f, timer);
                loadingText.text = (Math.Truncate(LoadingBar.fillAmount * 100)).ToString() + "%";
                if (LoadingBar.fillAmount >= 1.0f)
                {
                    anyKeyText.text = "계속 하려면 아무키나 입력하세요.";
                    if (op.allowSceneActivation == true)
                        yield break;
                }
            }
        }
    }
}
