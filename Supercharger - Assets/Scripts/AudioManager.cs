using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    [Range(0f, 1f)]
    [SerializeField] float effectsVolume;
    
    [Range(0f, 1f)]
    [SerializeField] float musicVolume;

    public Sound[] sounds;

    public static AudioManager instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            if (s.music) {
                s.source.volume = musicVolume;
            } else {
                s.source.volume = effectsVolume;
            }

            s.source.loop = s.loop;
            s.source.mute = s.mute;
            s.source.ignoreListenerPause = s.ignorepause;

            if (s.tresD) {
                s.source.spatialBlend = 1f;
            } else {
                s.source.spatialBlend = 0f;
            }
        }
    }

    public void AtualizarEfeitos(float novoVolume) {
        effectsVolume = novoVolume;

        foreach (Sound s in sounds)
        {
            if (!s.music) {
                s.source.volume = novoVolume;
            }
        }
    }

    public void AtualizarMusicas(float novoVolume) {
        musicVolume = novoVolume;

        foreach (Sound s in sounds)
        {
            if (s.music) {
                s.source.volume = novoVolume;
            }
        }
    }

    public float VolumeEfeitosAtual() {
        return effectsVolume;
    }

    public float VolumeMusicasAtual() {
        return musicVolume;
    }

    public float Length (string name)
    {
        if (name == "") return 0f;

        Sound s = Array.Find(sounds, sound => sound.name == name);
        
        if (s == null) {
            Debug.LogWarning("Sound: " + name + " not found!");
            return 0f;
        }
        
        return s.source.clip.length;
    }

    public void Play (string name)
    {
        if (name == "") return;

        Sound s = Array.Find(sounds, sound => sound.name == name);
        
        if (s == null) {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        
        s.source.Play();
    }

    public void Play3D (string name, Vector3 position)
    {
        if (name == "") return;

        Sound s = Array.Find(sounds, sound => sound.name == name);
        
        if (s == null) {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        
        AudioSource.PlayClipAtPoint(s.clip, position, effectsVolume);
    }

    public void SetVolume (string name, float volume) {
        if (name == "") return;

        Sound s = Array.Find(sounds, sound => sound.name == name);
        
        if (s == null) {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.volume = effectsVolume * volume;
    }

    public void Stop (string name)
    {
        if (name == "") return;

        Sound s = Array.Find(sounds, sound => sound.name == name);
        
        if (s == null) {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        
        s.source.Stop();
    }

    public void StopAll() {
        foreach (Sound s in sounds)
        {
            s.source.Stop();
        }
    }
}