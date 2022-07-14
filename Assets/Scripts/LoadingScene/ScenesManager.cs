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

    public void Load_Scene(string SceneName,LoadingType type)
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
        StartCoroutine(LoadSceneProcess());
    }
        
    IEnumerator LoadSceneProcess()
    {
        RandomLoadingImage();
        yield return null;
        op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;
        missionText.text = GameManager.instance.select_mission.mission_name;
        int index = UnityEngine.Random.Range(0, str.Length);
        tipText.text = str[index];
        float timer = 0f;
        while(!op.isDone)
        {
            yield return null;
            if (op.progress < 0.9f)
            {
                LoadingBar.fillAmount = op.progress;
                loadingText.text = (Math.Truncate(LoadingBar.fillAmount * 100)).ToString() + "%";
                if (LoadingBar.fillAmount >= op.progress) { timer = 0f; }
            }
            else
            {
                timer += Time.deltaTime;
                LoadingBar.fillAmount = Mathf.Lerp(LoadingBar.fillAmount, 1.0f, timer);
                loadingText.text = (Math.Truncate(LoadingBar.fillAmount * 100)).ToString() + "%";
                if (LoadingBar.fillAmount == 1.0f)
                {
                    anyKeyText.text = "��� �Ϸ��� �ƹ�Ű�� �Է��ϼ���.";
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