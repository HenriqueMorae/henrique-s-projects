using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoicePack_GameOver : MonoBehaviour
{

    public ChangevoiceGameover falas;

    public AudioClip tessa1;
    public AudioClip tessa2;
    public AudioClip tessa3;
    public AudioClip tessa4;
    public AudioClip tessa5;
    public AudioClip tessa6;
    public AudioClip tessa7;
    public AudioClip tessa8;

    public AudioClip calebe1;
    public AudioClip calebe2;
    public AudioClip calebe3;
    public AudioClip calebe4;
    public AudioClip calebe5;
    public AudioClip calebe6;
    public AudioClip calebe7;
    public AudioClip calebe8;

    public AudioClip octavia1;
    public AudioClip octavia2;
    public AudioClip octavia3;
    public AudioClip octavia4;
    public AudioClip octavia5;
    public AudioClip octavia6;
    public AudioClip octavia7;
    public AudioClip octavia8;

    public AudioClip dominick1;
    public AudioClip dominick2;
    public AudioClip dominick3;
    public AudioClip dominick4;
    public AudioClip dominick5;
    public AudioClip dominick6;
    public AudioClip dominick7;
    public AudioClip dominick8;

    public AudioClip icaro1;
    public AudioClip icaro2;
    public AudioClip icaro3;
    public AudioClip icaro4;
    public AudioClip icaro5;
    public AudioClip icaro6;
    public AudioClip icaro7;
    public AudioClip icaro8;

    void Awake()
    {
        int pack = PlayerPrefs.GetInt("personagem", 0);

        switch (pack)
        {
            case 0:
                break;
            case 4:
                falas.nada2 = tessa1;
                falas.nada3 = tessa2;
                falas.bronze3 = tessa3;
                falas.bronze4 = tessa4;
                falas.prata2 = tessa5;
                falas.prata3 = tessa6;
                falas.ouro3 = tessa7;
                falas.ouro4 = tessa8;
                break;
            case 6:
                falas.nada2 = calebe1;
                falas.nada3 = calebe2;
                falas.bronze3 = calebe3;
                falas.bronze4 = calebe4;
                falas.prata2 = calebe5;
                falas.prata3 = calebe6;
                falas.ouro3 = calebe7;
                falas.ouro4 = calebe8;
                break;
            case 8:
                falas.nada2 = octavia1;
                falas.nada3 = octavia2;
                falas.bronze3 = octavia3;
                falas.bronze4 = octavia4;
                falas.prata2 = octavia5;
                falas.prata3 = octavia6;
                falas.ouro3 = octavia7;
                falas.ouro4 = octavia8;
                break;
            case 12:
                falas.nada2 = dominick1;
                falas.nada3 = dominick2;
                falas.bronze3 = dominick3;
                falas.bronze4 = dominick4;
                falas.prata2 = dominick5;
                falas.prata3 = dominick6;
                falas.ouro3 = dominick7;
                falas.ouro4 = dominick8;
                break;
            case 20:
                falas.nada2 = icaro1;
                falas.nada3 = icaro2;
                falas.bronze3 = icaro3;
                falas.bronze4 = icaro4;
                falas.prata2 = icaro5;
                falas.prata3 = icaro6;
                falas.ouro3 = icaro7;
                falas.ouro4 = icaro8;
                break;
        }
    }
}
