using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class botaoAnimado : MonoBehaviour
{
    public Image imagem;
    public Sprite imagemoriginal;
    public Sprite imagemmudada;

    public void Bota()
    {
        imagem.sprite = imagemmudada;
    }

    public void Tira()
    {
        imagem.sprite = imagemoriginal;
    }
}
