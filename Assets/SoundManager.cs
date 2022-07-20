using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public AudioSource AS;

    public void Start()
    {
        AS.loop = true;
        AS.Play();
    }
}
