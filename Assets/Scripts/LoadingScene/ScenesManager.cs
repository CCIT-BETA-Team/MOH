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
    
    [SerializeField]
    Image LoadingBar;

    public void Load_Scene(string SceneName)
    {
        nextScene = SceneName;
        SceneManager.LoadScene("LoadingScene");
    }

    void Start()
    {
        LoadingBar.fillAmount=0;
        StartCoroutine(LoadSceneProcess());
    }

    IEnumerator LoadSceneProcess()
    {
        missionText.text = GameManager.instance.select_mission.mission_name;
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;
        float timer = 0f;
        int index = UnityEngine.Random.Range(0, 6);
        string[] str = { "�˰� ��̳���?  ������ �������� �ٽ�Ÿ�� ������ ų�� ���� ����Դϴ�.",
                         "���� ������ �ڽ��� �ڿ� �ִ� è�Ǿ𿡰� �ξ� �� ū ���ظ� �ݴϴ�.",
                         "�˰� ��̳���? Ÿ���� ���� �����þ� �����̾���, ������ Ÿ�� ���� ��ȣ�ڰ� ��� ���� �Ǿ����ϴ�.",
                         "�˰� ��̳���? ������ ���� ģ�� �̸��� �������Դϴ�.",
                         "�˰� ��̳���? �巡���� óġ�� �� 6�� �Ŀ� ������˴ϴ�.",
                         "�߱��� �� ���� ���������� ���� ���� �������� ��ũ�� ���� ������ ������ ���� �ִٰ� �մϴ�."};
        tipText.text = str[index];
        while(!op.isDone)
        {
            yield return null;

            if(op.progress<0.0001f)
            {
                LoadingBar.fillAmount = op.progress;
                loadingText.text = (Math.Truncate(LoadingBar.fillAmount * 100)).ToString() + "%";
            }
            else
            {
                timer += Time.deltaTime;
                LoadingBar.fillAmount = Mathf.Lerp(0.0001f, 1.0f, timer);
                loadingText.text = (Math.Truncate(LoadingBar.fillAmount * 100)).ToString() + "%";
                if(LoadingBar.fillAmount>=1.0f)
                {
                    op.allowSceneActivation = true;
                    yield return new WaitForSeconds(4.0f);
                }
            }
        }
    }
}
