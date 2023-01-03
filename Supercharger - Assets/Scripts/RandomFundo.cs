using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomFundo : MonoBehaviour
{
    public Sprite[] fundos;
    public SpriteRenderer imagem;

    // Start is called before the first frame update
    void Start()
    {
        imagem.sprite = fundos[Random.Range(0, fundos.Length)];
    }
}
