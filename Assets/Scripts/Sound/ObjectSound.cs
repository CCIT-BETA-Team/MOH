using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//오브젝트랑 상호작용하는 사운드만 여기서 관리할거임

[System.Serializable]
public class Sounds  
{
    public AudioClip interaction_clip;
    public AudioClip continuous_clip; 
    public AudioClip crackling_clips; 


    [Range(0, 1.0f)]
    public float volume = 0.5f;
}
public class ObjectSound : MonoBehaviour
{
    GameObject sound_source;
    AudioSource sound;
    public Sounds sounds = new Sounds();

    public bool is_continuous;//지속 사운드 체크
    public bool is_glass;     //깨지는 물건 체크

    float sound_effect_range;
    int layermask = 1 << 7;


    private void Start()
    {
        AddAudioSource();
        
    }
    float sound_range = 5;
    /// <summary>
    /// aduiosource 추가 및 사운드 clip 할당
    /// </summary>
    void AddAudioSource()
    {
        sound_source = new GameObject("sound_play_obj");
        sound_source.transform.position = transform.position;
        sound_source.transform.rotation = transform.rotation;
        sound_source.transform.parent = transform;

        sound = sound_source.AddComponent<AudioSource>();
        sound.spatialBlend = 1;
        sound.volume = sounds.volume;
        sound.playOnAwake = false;

        sound.rolloffMode = AudioRolloffMode.Linear;
        sound.minDistance = 1;

        if (is_glass) { sound.maxDistance = sound_range + 2; }//유리 재질인 경우 소리 전달 범위를 올려줌
        else if (is_glass == false) { sound.maxDistance = sound_range; }

        switch (is_continuous) 
        {
            case true:
                sound.clip = sounds.continuous_clip;
                sound.loop = true;
                break;
            case false:
                if (is_glass) { sound.clip = sounds.crackling_clips; }
                else { sound.clip = sounds.interaction_clip; }
                break;
        }
    }

    /// <summary>
    /// volume Up
    /// </summary>
    /// <param name="value"></param>
    void VolumeUp(float value)
    {
        sound.volume += value; 
    }
    /// <summary>
    /// volume 값에 따른 범위 할당
    /// </summary>
    void Volume_Check()
    {
        sound_effect_range = sound.volume * 10;
    }


    /// <summary>
    /// 즉발 사운드
    /// </summary>
    public void Interaction_sound()
    {
        sound.Play();
        Volume_Check();
        Collider[] col = Physics.OverlapBox(transform.position, new Vector3(2 + sound_effect_range, sound_effect_range / 2,
            2 + sound_effect_range), Quaternion.identity, layermask);

        if (col.Length != 0)
        {
            for (int i = 0; i < col.Length; i++)
            {
                col[i].gameObject.GetComponent<Npc>().fear_percent += 10;
            }

        }
    }
    public void Interaction_sound(float volume)
    {
        sound.Play();
        VolumeUp(volume);
        Volume_Check();
        Collider[] col = Physics.OverlapBox(transform.position, new Vector3(2 + sound_effect_range, sound_effect_range / 2,
            2 + sound_effect_range),Quaternion.identity,layermask);

        if(col.Length != 0)
        {
            for(int i = 0; i < col.Length; i++)
            {
                col[i].gameObject.GetComponent<Npc>().fear_percent += 10;
            }
            
        }
    }
    public void Interaction_sound(Npc npc_script)
    {
        if (npc_script)
        {
            sound.Play();
            Volume_Check();
            Collider[] col = Physics.OverlapBox(transform.position, new Vector3(2 + sound_effect_range, sound_effect_range / 2,
                2 + sound_effect_range), Quaternion.identity, layermask);

            if (col.Length != 0)
            {
                for (int i = 0; i < col.Length; i++)
                {
                    col[i].gameObject.GetComponent<Npc>().fear_percent += 10;
                }
            }
        }
    }
    public void Interaction_sound(Npc npc_script,float volume)
    {
        if (npc_script)
        {
            sound.Play();
            VolumeUp(volume);
            Volume_Check();
            Collider[] col = Physics.OverlapBox(transform.position, new Vector3(2 + sound_effect_range, sound_effect_range / 2,
                2 + sound_effect_range), Quaternion.identity, layermask);

            if (col.Length != 0)
            {
                for (int i = 0; i < col.Length; i++)
                {
                    col[i].gameObject.GetComponent<Npc>().fear_percent += 10;
                }
            }
        }
    }
    public void Interaction_sound(Player player_script)
    {
        if (player_script)
        {
            sound.Play();
            Volume_Check();
            Collider[] col = Physics.OverlapBox(transform.position, new Vector3(2 + sound_effect_range, sound_effect_range / 2,
                2 + sound_effect_range), Quaternion.identity, layermask);

            if (col.Length != 0)
            {
                for (int i = 0; i < col.Length; i++)
                {
                    col[i].gameObject.GetComponent<Npc>().fear_percent += 10;
                }
            }
        }
    }
    public void Interaction_sound(Player player_script, float volume)
    {
        if (player_script)
        {
            sound.Play();
            VolumeUp(volume);
            Volume_Check();
            Collider[] col = Physics.OverlapBox(transform.position, new Vector3(2 + sound_effect_range, sound_effect_range / 2,
                2 + sound_effect_range), Quaternion.identity, layermask);

            if (col.Length != 0)
            {
                for (int i = 0; i < col.Length; i++)
                {
                    col[i].gameObject.GetComponent<Npc>().fear_percent += 10;
                }
            }
        }
    }

    /// <summary>
    /// 지속 사운드
    /// </summary>
    public void Continuous_sound()
    {
        sound.Play();
        Volume_Check();
        Collider[] col = Physics.OverlapBox(transform.position, new Vector3(2 + sound_effect_range, sound_effect_range / 2,
            2 + sound_effect_range), Quaternion.identity, layermask);

        if (col.Length != 0)
        {
            for (int i = 0; i < col.Length; i++)
            {
                col[i].gameObject.GetComponent<Npc>().fear_percent += 10;
            }
        }
    }
    public void Continuous_sound(float volume)
    {
        sound.Play();
        VolumeUp(volume);
        Volume_Check();
        Collider[] col = Physics.OverlapBox(transform.position, new Vector3(2 + sound_effect_range, sound_effect_range / 2,
            2 + sound_effect_range), Quaternion.identity, layermask);

        if (col.Length != 0)
        {
            for (int i = 0; i < col.Length; i++)
            {
                col[i].gameObject.GetComponent<Npc>().fear_percent += 10;
            }
        }
    }
    public void Continuous_sound(Npc npc_script)
    {
        if (npc_script)
        {
            sound.Play();
            Volume_Check();
            Collider[] col = Physics.OverlapBox(transform.position, new Vector3(2 + sound_effect_range, sound_effect_range / 2,
                2 + sound_effect_range), Quaternion.identity, layermask);

            if (col.Length != 0)
            {
                for (int i = 0; i < col.Length; i++)
                {
                    col[i].gameObject.GetComponent<Npc>().fear_percent += 10;
                }
            }
        }
    }
    public void Continuous_sound(Npc npc_script, float volume)
    {
        if (npc_script)
        {
            sound.Play();
            VolumeUp(volume);
            Volume_Check();
            Collider[] col = Physics.OverlapBox(transform.position, new Vector3(2 + sound_effect_range, sound_effect_range / 2,
                2 + sound_effect_range), Quaternion.identity, layermask);

            if (col.Length != 0)
            {
                for (int i = 0; i < col.Length; i++)
                {
                    col[i].gameObject.GetComponent<Npc>().fear_percent += 10;
                }
            }
        }
    }
    public void Continuous_sound(Player player_script)
    {
        if (player_script)
        {
            sound.Play();
            Volume_Check();
            Collider[] col = Physics.OverlapBox(transform.position, new Vector3(2 + sound_effect_range, sound_effect_range / 2,
                2 + sound_effect_range), Quaternion.identity, layermask);

            if (col.Length != 0)
            {
                for (int i = 0; i < col.Length; i++)
                {
                    col[i].gameObject.GetComponent<Npc>().fear_percent += 10;
                }
            }
        }
    }
    public void Continuous_sound(Player player_script,float volume)
    {
        if (player_script)
        {
            sound.Play();
            VolumeUp(volume);
            Volume_Check();
            Collider[] col = Physics.OverlapBox(transform.position, new Vector3(2 + sound_effect_range, sound_effect_range / 2,
                2 + sound_effect_range), Quaternion.identity, layermask);

            if (col.Length != 0)
            {
                for (int i = 0; i < col.Length; i++)
                {
                    col[i].gameObject.GetComponent<Npc>().fear_percent += 10;
                }
            }
        }
    }

    

}
