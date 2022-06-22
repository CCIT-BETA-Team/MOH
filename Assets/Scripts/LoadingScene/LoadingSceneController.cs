using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;



public class LoadingSceneController : MonoBehaviour
{

    static int nextSceneIndex;

    public Text loadingText;

    [SerializeField]
    Image LoadingBar;

    public static void LoadScene(int SceneIndex)
    {
        nextSceneIndex = SceneIndex;
        SceneManager.LoadScene("LoadingScene");
    }

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(LoadSceneProcess());
    }

    IEnumerator LoadSceneProcess()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(nextSceneIndex);
        op.allowSceneActivation = false;

        float timer = 0f;
        while (!op.isDone)
        {
            yield return null;

            if (op.progress < 0.01f)
            {
                LoadingBar.fillAmount = op.progress;
                loadingText.text = (Math.Truncate(LoadingBar.fillAmount * 100)).ToString() + "%";
            }
            else
            {

                timer += Time.deltaTime;
                LoadingBar.fillAmount = Mathf.Lerp(0.01f, 1.0f, timer);
                loadingText.text = (Math.Truncate(LoadingBar.fillAmount * 100)).ToString() + "%";
                if (LoadingBar.fillAmount >= 1.0f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}
