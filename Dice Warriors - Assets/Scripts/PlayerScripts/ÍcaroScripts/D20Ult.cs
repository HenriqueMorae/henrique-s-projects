using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D20Ult : MonoBehaviour
{
    public SpriteRenderer espada;
    public SpriteRenderer perna1;
    public SpriteRenderer perna2;
    public SpriteRenderer braco;
    public SpriteRenderer corpo;

    public Sprite espadaUlt;
    public Sprite perna1Ult;
    public Sprite perna2Ult;
    public Sprite bracoUlt;
    public Sprite corpoUlt;

    public Sprite espadaNormal;
    public Sprite perna1Normal;
    public Sprite perna2Normal;
    public Sprite bracoNormal;
    public Sprite corpoNormal;

    public Sprite espadaBranco;
    public Sprite perna1Branco;
    public Sprite perna2Branco;
    public Sprite bracoBranco;
    public Sprite corpoBranco;

    public Sprite corpoMorto;
    public Sprite corpoDor;
    public Vida vida;
    public PlayerMovement movimento;

    public Arma_D20 arma;
    public GameObject raios;

    public void IcaroBranco() {
        espada.sprite = espadaBranco;
        perna1.sprite = perna1Branco;
        perna2.sprite = perna2Branco;
        braco.sprite = bracoBranco;
        corpo.sprite = corpoBranco;

        vida.dor = corpoBranco;
        vida.normal = corpoBranco;
        vida.morri = corpoBranco;
    }

    public void IcaroNormal() {
        raios.SetActive(false);
        arma.UltOn(false);
        arma.attackRate -= 4.5f;
        arma.NextAttack();
        movimento.moveSpeed = movimento.moveSpeed - 1.1f;

        espada.sprite = espadaNormal;
        perna1.sprite = perna1Normal;
        perna2.sprite = perna2Normal;
        braco.sprite = bracoNormal;
        
        if (vida.Morreu())
            corpo.sprite = corpoMorto;
        else
            corpo.sprite = corpoNormal;

        vida.dor = corpoDor;
        vida.normal = corpoNormal;
        vida.morri = corpoMorto;
    }

    public void IcaroFuria() {
        raios.SetActive(true);
        arma.UltOn(true);
        arma.attackRate += 4.5f;
        arma.NextAttack();
        movimento.moveSpeed = movimento.moveSpeed + 1.1f;

        espada.sprite = espadaUlt;
        perna1.sprite = perna1Ult;
        perna2.sprite = perna2Ult;
        braco.sprite = bracoUlt;

        if (vida.Morreu())
            corpo.sprite = corpoMorto;
        else
            corpo.sprite = corpoUlt;

        vida.dor = corpoUlt;
        vida.normal = corpoUlt;
        vida.morri = corpoMorto;
    }
}
