using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerIcons : MonoBehaviour
{
    SpriteRenderer imagem;
    public Sprite d4;
    public Sprite d6;
    public Sprite d8;
    public Sprite d12;
    public Sprite d20;

    public void mudapowers(int dado)
    {
        imagem = GetComponent<SpriteRenderer>();
        switch (dado)
        {
            case 4:
                imagem.sprite = d4;
                break;
            case 6:
                imagem.sprite = d6;
                break;
            case 8:
                imagem.sprite = d8;
                break;
            case 12:
                imagem.sprite = d12;
                break;
            case 20:
                imagem.sprite = d20;
                break;
        }
        var tempColor = imagem.color;
        tempColor.a = 255f;
        imagem.color = tempColor;
    }
}
