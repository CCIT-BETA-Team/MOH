using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//오브젝트랑 상호작용하는 사운드만 여기서 관리할거임

[System.Serializable]
public class Sounds  
{
    public AudioClip object_touch_sound;//물건 만지는 소리
    public AudioClip throwing_sound;//물건 던지는 소리
    public AudioClip light_switch_sound;//불 키고 끄는 소리
    public AudioClip clashing_sound; // 부딪히는 소리
    public AudioClip crackling_sound; // 깨지는소리


    [Range(0, 1.0f)]
    public float volume = 1.0f;
    [Range(0, 0.4f)]
    public float pitchRandom = 0.2f;
}
public class ObjectSound : MonoBehaviour
{
    AudioSource sound_play_obj;
    public Sounds sound = new Sounds();
    private void Start()
    {
        AddAudioSource();
    }

    private void Update()
    {
        if (sound_play_obj.isPlaying) { sound_play_obj.Stop(); }
    }
    void AddAudioSource()
    {
        GameObject sound_source = new GameObject("sound_play_obj");
        sound_source.transform.position = transform.position;
        sound_source.transform.rotation = transform.rotation;
        sound_source.transform.parent = transform;
        sound_play_obj = sound_source.AddComponent<AudioSource>();
        sound_play_obj.volume = sound.volume;
        sound_play_obj.spatialBlend = 1;
        sound_play_obj.playOnAwake = false;
        //sound_play_obj.clip = sound.open;
    }
}
