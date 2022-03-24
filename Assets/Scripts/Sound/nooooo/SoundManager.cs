using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound  // 컴포넌트 추가 불가능.  MonoBehaviour 상속 안 받아서. 그냥 C# 클래스.
{
    public string name;  // 곡 이름
    public AudioClip clip;  // 곡
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

    public string[] play_sound_name;//문자열 배열,재생 중인 '효과음'들의 이름이 게임 중 실시간으로 원소로 들어감

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
                //Debug.Log("모든 가용 AudioSource가 사용 중입니다.");
                //return;
            }
        }
        Debug.Log(_name + "사운드가 SoundManager에 등록되지 않았습니다.");
    }//효과음 재생 함수

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
        Debug.Log(_name + "사운드가 SoundManager에 등록되지 않았습니다.");
    }//BGM 재생 함수

    public void StopAllSE()
    {
        for (int i = 0; i < audiosources_play.Length; i++)
        {
            //audiosources_play[i].Stop();
        }
    }//모든 효과음 재생을멈춤

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
        //Debug.Log("재생 중인" + _name + "사운드가 없습니다. ");
    }//특정 효과음의 재생을 멈춤

}
