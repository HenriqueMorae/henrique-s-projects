using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changevolumeeffect : MonoBehaviour
{
    public AudioSource efeito;

    // Start is called before the first frame update
    void Start()
    {
        efeito.volume = PlayerPrefs.GetFloat("effects", 0.8f);
    }
}
