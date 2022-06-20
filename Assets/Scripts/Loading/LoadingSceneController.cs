using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class LoadingSceneController : MonoBehaviour
{
    static int nextScene;

    [SerializeField]
    Image LoadingBar;

    public static void LoadScene(int sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadSceneProcess());
    }

    IEnumerator LoadSceneProcess()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;

        float timer = 0f;
        while(!op.isDone)
        {
            yield return null;

            if (op.progress < 0.01f)
                LoadingBar.fillAmount = op.progress;
            else
            {
                timer += Time.deltaTime;
                LoadingBar.fillAmount = Mathf.Lerp(0.01f, 1.0f, timer);
                if (LoadingBar.fillAmount >= 1.0f)
                {       
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}
