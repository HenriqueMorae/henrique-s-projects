using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImagemPlacar : MonoBehaviour
{
    public SpriteRenderer imagem;
    int pontos;

    public Sprite nada;
    public Sprite bronze;
    public Sprite prata;
    public Sprite ouro;

    // Start is called before the first frame update
    void Start()
    {
        pontos = PlayerPrefs.GetInt("placar");

        if (pontos < 50) {
            imagem.sprite = nada;
        } else if (pontos >= 50 && pontos < 100) {
            imagem.sprite = bronze;
        } else if (pontos >= 100 && pontos < 150) {
            imagem.sprite = prata;
        } else if (pontos >= 150) {
            imagem.sprite = ouro;
        }   
    }
}
