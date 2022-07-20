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

    #region For Loading Image
    public Sprite[] pictures;
    public Image loadingimage;
    #endregion

    [SerializeField]
    Image LoadingBar;

    public enum LoadingType
    {
    DELAY=0,
    DIRECT
    }

    public static void Load_Scene(string SceneName,LoadingType type)
    {
        switch (type)
        {
            case (LoadingType)0:
                nextScene = SceneName;
                SceneManager.LoadScene("LoadingScene");
                break;
            case (LoadingType)1:
                SceneManager.LoadScene(SceneName);
                break;
        }
    }

    void Start()
    {
        RandomLoadingImage();
        StartCoroutine(LoadSceneProcess());
        missionText.text = GameManager.instance.select_mission.mission_name;
        int index = UnityEngine.Random.Range(0, str.Length);
        tipText.text = str[index];
    }

    IEnumerator LoadSceneProcess()
    {
        yield return null;
        op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;
        float timer = 0f;
        float _timer = 0f;
        while (!op.isDone)
        {
            yield return null;
            if (op.progress < 0.9f || LoadingBar.fillAmount < 0.9f)
            {
                _timer += Time.deltaTime * 0.7f;
                LoadingBar.fillAmount = Mathf.Lerp(0.001f, 0.9f, _timer);
                loadingText.text = (Math.Truncate(LoadingBar.fillAmount * 100)).ToString() + "%";
            }

            else
            {
                timer += Time.deltaTime * 0.3f;
                LoadingBar.fillAmount = Mathf.Lerp(0.9f, 1.0f, timer);
                loadingText.text = (Math.Truncate(LoadingBar.fillAmount * 100)).ToString() + "%";
                if (LoadingBar.fillAmount >= 1.0f)
                {
                    anyKeyText.text = "계속 하려면 아무키나 입력하세요.";
                    if (Input.anyKey)
                    {
                        op.allowSceneActivation = true;
                        anyKeyText.text = null;
                        yield break;
                    }
                }
            }
        }
    }
    public void RandomLoadingImage()
    {
        loadingimage.sprite = pictures[UnityEngine.Random.Range(0, pictures.Length)];
    }
}