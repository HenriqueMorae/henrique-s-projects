using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changevolume : MonoBehaviour
{
    public AudioSource musica;
    
    void Awake () {
        Time.timeScale = 1f;
        AudioListener.pause = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        musica.volume = PlayerPrefs.GetFloat("music", 0.5f);
    }

    public void MudaAgora()
    {
        musica.volume = PlayerPrefs.GetFloat("music");
    }
}
