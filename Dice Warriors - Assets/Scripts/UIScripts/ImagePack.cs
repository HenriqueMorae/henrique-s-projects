using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImagePack : MonoBehaviour
{
    public ImagemPlacar imagens;

    public Sprite tessa1;
    public Sprite tessa2;
    public Sprite tessa3;
    public Sprite tessa4;

    public Sprite calebe1;
    public Sprite calebe2;
    public Sprite calebe3;
    public Sprite calebe4;

    public Sprite octavia1;
    public Sprite octavia2;
    public Sprite octavia3;
    public Sprite octavia4;

    public Sprite dominick1;
    public Sprite dominick2;
    public Sprite dominick3;
    public Sprite dominick4;

    public Sprite icaro1;
    public Sprite icaro2;
    public Sprite icaro3;
    public Sprite icaro4;

    void Awake()
    {
        int pack = PlayerPrefs.GetInt("personagem", 0);

        switch (pack)
        {
            case 0:
                break;
            case 4:
                imagens.nada = tessa1;
                imagens.bronze = tessa2;
                imagens.prata = tessa3;
                imagens.ouro = tessa4;
                break;
            case 6:
                imagens.nada = calebe1;
                imagens.bronze = calebe2;
                imagens.prata = calebe3;
                imagens.ouro = calebe4;
                break;
            case 8:
                imagens.nada = octavia1;
                imagens.bronze = octavia2;
                imagens.prata = octavia3;
                imagens.ouro = octavia4;
                break;
            case 12:
                imagens.nada = dominick1;
                imagens.bronze = dominick2;
                imagens.prata = dominick3;
                imagens.ouro = dominick4;
                break;
            case 20:
                imagens.nada = icaro1;
                imagens.bronze = icaro2;
                imagens.prata = icaro3;
                imagens.ouro = icaro4;
                break;
        }
    }
}
