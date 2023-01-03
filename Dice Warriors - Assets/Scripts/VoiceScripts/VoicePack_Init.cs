using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoicePack_Init : MonoBehaviour
{
    public VoiceInit falas;

    public AudioClip tessa1;
    public AudioClip tessa2;
    public AudioClip tessa3;

    public AudioClip calebe1;
    public AudioClip calebe2;
    public AudioClip calebe3;

    public AudioClip octavia1;
    public AudioClip octavia2;
    public AudioClip octavia3;

    public AudioClip dominick1;
    public AudioClip dominick2;
    public AudioClip dominick3;

    public AudioClip icaro1;
    public AudioClip icaro2;
    public AudioClip icaro3;

    void Awake()
    {
        int pack = PlayerPrefs.GetInt("personagem", 0);

        switch (pack)
        {
            case 0:
                break;
            case 4:
                falas.player1 = tessa1;
                falas.player2 = tessa2;
                falas.player3 = tessa3;
                break;
            case 6:
                falas.player1 = calebe1;
                falas.player2 = calebe2;
                falas.player3 = calebe3;
                break;
            case 8:
                falas.player1 = octavia1;
                falas.player2 = octavia2;
                falas.player3 = octavia3;
                break;
            case 12:
                falas.player1 = dominick1;
                falas.player2 = dominick2;
                falas.player3 = dominick3;
                break;
            case 20:
                falas.player1 = icaro1;
                falas.player2 = icaro2;
                falas.player3 = icaro3;
                break;
        }
    }
}
