using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//������Ʈ�� ��ȣ�ۿ��ϴ� ���常 ���⼭ �����Ұ���

[System.Serializable]
public class Sounds  
{
    public AudioClip object_touch_sound;//���� ������ �Ҹ�
    public AudioClip throwing_sound;//���� ������ �Ҹ�
    public AudioClip light_switch_sound;//�� Ű�� ���� �Ҹ�
    public AudioClip clashing_sound; // �ε����� �Ҹ�
    public AudioClip crackling_sound; // �����¼Ҹ�


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
