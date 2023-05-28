using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[System.Serializable]
public class Sound
{
    public enum AudioTypes { soundEffect, music}
    public AudioTypes audioType;

    public AudioSource source;
    public AudioClip clip;
    public string clipName;

    public bool playOnAwake;
    public bool isLoop;
    public float volume = 1.0f;

}
