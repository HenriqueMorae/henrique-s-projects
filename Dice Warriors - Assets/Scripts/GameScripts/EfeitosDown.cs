using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfeitosDown : MonoBehaviour
{
    public RectTransform local1;

    public GameObject vida;
    public GameObject escudo;
    public GameObject energia;
    public GameObject velocidade;
    public GameObject ataque;
    public GameObject maisUm;
    public GameObject maisDois;
    public GameObject maisTres;

    public static int danoExtra;
    GameObject escudoEffect;
    GameObject efeito;
    GameObject jogador;
    Vida vida2;
    public static bool orbEscudo;
    int posicao;
    float time;
    int idDelete;
    bool orbMaisAtaque;

    void Start() {
        danoExtra = 0;
        orbEscudo = false;
        orbMaisAtaque = false;
        jogador = GameObject.FindWithTag("Player");
        posicao = 1;
    }

    void Update() {
        if(!orbEscudo)
            return;

        if (!vida2.isEscudoOn()) {
            StopCoroutine("MaisEscudo");
            escudoEffect.GetComponent<Animator>().Play("efeito_off");
            orbEscudo = false;
        }
    }

    public void AddEfeito(int tipo){
        if ((tipo == 2 && orbEscudo) || (tipo == 8 && orbMaisAtaque))
            return;

        foreach (Transform child in transform)
        {
            child.Translate(0f, 0.5f, 0f);
        }

        switch (tipo)
        {
            case 1: efeito = Instantiate(vida, local1.transform); efeito.GetComponent<Efeito>().SetID(posicao); MaisVida(); break;
            case 2: escudoEffect = Instantiate(escudo, local1.transform); escudoEffect.GetComponent<Efeito>().SetID(posicao); StartCoroutine("MaisEscudo"); break;
            case 3: efeito = Instantiate(energia, local1.transform); efeito.GetComponent<Efeito>().SetID(posicao); MaisEnergia(); break;
            case 4: efeito = Instantiate(velocidade, local1.transform); efeito.GetComponent<Efeito>().SetID(posicao); StartCoroutine("MaisVelocidade"); break;
            case 5: efeito = Instantiate(maisUm, local1.transform); efeito.GetComponent<Efeito>().SetID(posicao); StartCoroutine("MaisUm"); break;
            case 6: efeito = Instantiate(maisDois, local1.transform); efeito.GetComponent<Efeito>().SetID(posicao); StartCoroutine("MaisDois"); break;
            case 7: efeito = Instantiate(maisTres, local1.transform); efeito.GetComponent<Efeito>().SetID(posicao); StartCoroutine("MaisTres"); break;
            case 8: efeito = Instantiate(ataque, local1.transform); efeito.GetComponent<Efeito>().SetID(posicao); StartCoroutine("MaisAtaque"); break;
        }

        posicao++;
    }

    public void RemoveEfeito (int idEfeito, float tempo) {
        time = tempo+0.1f;
        idDelete = idEfeito;
        StartCoroutine("MoverDeVolta");
    }

    IEnumerator MoverDeVolta() {
        int idRemove = idDelete;
        yield return new WaitForSeconds(time);

        foreach (Transform child in transform)
        {
            if (child.GetComponent<Efeito>().GetID() < idRemove)
                child.Translate(0f, -0.5f, 0f);
        }

        if (transform.childCount == 0)
            posicao = 1;
    }

    void MaisVida() {
        jogador.GetComponent<Vida>().Cura(Random.Range(1, 11));
    }

    IEnumerator MaisEscudo() {
        jogador.GetComponent<Vida>().SetEscudo(Random.Range(1, 11));
        vida2 = jogador.GetComponent<Vida>();
        orbEscudo = true;
        yield return new WaitForSeconds(10f);
        orbEscudo = false;
        jogador.GetComponent<Vida>().TerminarEscudo();
    }

    void MaisEnergia() {
        GameObject.FindWithTag("EnergyBar").GetComponent<Energy>().Energia(Random.Range(1, 11));
    }

    IEnumerator MaisVelocidade() {
        jogador.GetComponent<PlayerMovement>().ModificarVelocidade(1f);
        yield return new WaitForSeconds(10f);
        jogador.GetComponent<PlayerMovement>().ModificarVelocidade(-1f);
    }

    IEnumerator MaisUm() {
        danoExtra += 1;
        yield return new WaitForSeconds(10f);
        danoExtra -= 1;
    }

    IEnumerator MaisDois() {
        danoExtra += 2;
        yield return new WaitForSeconds(10f);
        danoExtra -= 2;
    }

    IEnumerator MaisTres() {
        danoExtra += 3;
        yield return new WaitForSeconds(10f);
        danoExtra -= 3;
    }

    IEnumerator MaisAtaque() {
        orbMaisAtaque = true;
        int personagem = PlayerPrefs.GetInt("personagem");

        switch (personagem)
        {
            case 4: jogador.GetComponent<Arma>().attackRate += 1f; break;
            case 6: jogador.GetComponent<Arma_D6>().attackRate += 1f; break;
            case 8: jogador.GetComponent<Arma_D8>().attackRate += 1f; break;
            case 12: jogador.GetComponent<Arma_D12>().attackRate += 1f; break;
            case 20: jogador.GetComponent<Arma_D20>().attackRate += 1f; break;
        }

        jogador.GetComponent<Animator>().SetFloat("attackMultiplier", 1.5f);

        yield return new WaitForSeconds(10f);

        switch (personagem)
        {
            case 4: jogador.GetComponent<Arma>().attackRate -= 1f; break;
            case 6: jogador.GetComponent<Arma_D6>().attackRate -= 1f; break;
            case 8: jogador.GetComponent<Arma_D8>().attackRate -= 1f; break;
            case 12: jogador.GetComponent<Arma_D12>().attackRate -= 1f; break;
            case 20: jogador.GetComponent<Arma_D20>().attackRate -= 1f; break;
        }

        jogador.GetComponent<Animator>().SetFloat("attackMultiplier", 1f);
        orbMaisAtaque = false;
    }

}
