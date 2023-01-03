using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class aumentaCombo : MonoBehaviour
{
    public Text numero;
    public AudioSource effect;

    public AudioClip combo3;
    public AudioClip combo4;
    public AudioClip combo5;
    public AudioClip combo6;
    public AudioClip combo7;
    public AudioClip combo8;
    public AudioClip combo9;
    public AudioClip combo10; 

    void Awake()
    {
        effect.volume = PlayerPrefs.GetFloat("effects", 0.8f);
    }

    public void Aumenta(int combo)
    {
        numero.text = combo.ToString();

        switch (combo)
        {
            case 3: effect.clip = combo3; break;
            case 4: effect.clip = combo4; break;
            case 5: effect.clip = combo5; break;
            case 6: effect.clip = combo6; break;
            case 7: effect.clip = combo7; break;
            case 8: effect.clip = combo8; break;
            case 9: effect.clip = combo9; break;
        }

        if (combo >= 10)
            effect.clip = combo10;

        effect.Play();
    }
}
