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
    
    //int scene_index;

    [SerializeField]
    Image LoadingBar;


    public void Load_Scene(string SceneName)
    {
        nextScene = SceneName;
        SceneManager.LoadScene("LoadingScene");
    }

    //private void Awake()
    //{
    //    //scene_index = SceneManager.GetActiveScene().buildIndex;
    //}
    void Start()
    {
        LoadingBar.fillAmount=0;
        StartCoroutine(LoadSceneProcess());
    }

    IEnumerator LoadSceneProcess()
    {
        missionText.text = GameManager.instance.select_mission.mission_name;
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene/*scene_index+1*/);
        op.allowSceneActivation = false;
        float timer = 0f;
        int index = UnityEngine.Random.Range(0, 6);
        string[] str = { "알고 계셨나요?  렝가는 슈리마의 바스타야 종족인 킬라쉬 부족 출신입니다.",
                         "내셔 남작은 자신의 뒤에 있는 챔피언에게 훨씬 더 큰 피해를 줍니다.",
                         "알고 계셨나요? 타릭은 원래 데마시아 군인이었고, 지금은 타곤 성위 수호자가 깃든 몸이 되었습니다",
                         "알고 계셨나요? 누누의 설인 친구 이름은 윌럼프입니다.",
                         "알고 계셨나요? 드래곤은 처치된 지 6분 후에 재생성됩니다.",
                         "중국의 한 스시 전문점에서 리그 오브 레전드의 랭크에 따라 할인을 제공한 적이 있다고 합니다."};
        tipText.text = str[index];
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
