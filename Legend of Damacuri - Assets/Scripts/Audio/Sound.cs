using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound {

    public string name;

    public AudioClip clip;

    public bool music;
    public bool tresD;
    public bool mute;
    public bool loop;
    public bool ignorepause;

    [HideInInspector]
    public AudioSource source;
}
