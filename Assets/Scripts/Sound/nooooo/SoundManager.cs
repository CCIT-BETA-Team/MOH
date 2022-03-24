using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound  // ������Ʈ �߰� �Ұ���.  MonoBehaviour ��� �� �޾Ƽ�. �׳� C# Ŭ����.
{
    public string name;  // �� �̸�
    public AudioClip clip;  // ��
}
public class SoundManager : MonoBehaviour
{
    #region singleton;
    private static SoundManager instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion singleton;

    public GameObject Sound_ball;



    [Header("Object Sounds")]
    public Sound[] player_sounds;
    public Sound[] npc_sounds;
    public Sound[] interior_sounds;
    public Sound[] nature_sounds;
    public Sound[] effect_sounds;
    public Sound[] bgm_sounds;
    public AudioSource audiosourceBGM;
    public AudioSource[] audiosources_play;

    public string[] play_sound_name;//���ڿ� �迭,��� ���� 'ȿ����'���� �̸��� ���� �� �ǽð����� ���ҷ� ��

    private void Start()
    {
        play_sound_name = new string[audiosources_play.Length];
    }




    public void PlaySE(string _name,Sound[] sound_list)
    {
        for (int i = 0; i < sound_list.Length; i++)
        {
            if (_name == sound_list[i].name)
            {

                //for (int j = 0; j < audiosources_play.Length; j++)
                //{
                //    if (!audiosources_play[j].isPlaying)
                //    {
                //        audiosources_play[j].clip = sound_list[i].clip;
                //        audiosources_play[j].Play();
                //        play_sound_name[j] = sound_list[i].name;
                //        return;
                //    }
                //}
                //Debug.Log("��� ���� AudioSource�� ��� ���Դϴ�.");
                //return;
            }
        }
        Debug.Log(_name + "���尡 SoundManager�� ��ϵ��� �ʾҽ��ϴ�.");
    }//ȿ���� ��� �Լ�

    public void PlayBGM(string _name)
    {
        for (int i = 0; i < bgm_sounds.Length; i++)
        {
            if (_name == bgm_sounds[i].name)
            {
                audiosourceBGM.clip = bgm_sounds[i].clip;
                audiosourceBGM.Play();
                return;
            }
        }
        Debug.Log(_name + "���尡 SoundManager�� ��ϵ��� �ʾҽ��ϴ�.");
    }//BGM ��� �Լ�

    public void StopAllSE()
    {
        for (int i = 0; i < audiosources_play.Length; i++)
        {
            //audiosources_play[i].Stop();
        }
    }//��� ȿ���� ���������

    public void StopSE(string _name)
    {
        //for (int i = 0; i < audiosources_play.Length; i++)
        //{
        //    if (play_sound_name[i] == _name)
        //    {
        //        audiosources_play[i].Stop();
        //        break;
        //    }
        //}
        //Debug.Log("��� ����" + _name + "���尡 �����ϴ�. ");
    }//Ư�� ȿ������ ����� ����

}
