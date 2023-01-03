using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkRanking : MonoBehaviour
{
    public GameObject avisoModo;
    public GameObject avisoVersao;
    public GameObject avisoInternet;
    public GameObject avisoRanking;
    public GameObject avisoCarregar;
    public GameObject avisoCarregando;
    public GameObject escolherModo;
    public GameObject ranqueadoTela;

    public ShowRanking rankingRanqueado;
    public ShowRanking rankingFacil;
    public ShowRanking rankingMedio;
    public ShowRanking rankingDificil;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetString("modooffline") == "on") {
            avisoModo.SetActive(true);
            return;
        }

        string response = FindObjectOfType<internetManager>().InternetOn();

        if (response == "noVersion") {
            avisoVersao.SetActive(true);
            return;
        } else if (response == "noInternet") {
            avisoInternet.SetActive(true);
            return;
        }

        if (!FindObjectOfType<internetManager>().RankingOn()) {
            avisoRanking.SetActive(true);
            return;
        }

        // tentar carregar o ranking
        avisoCarregando.SetActive(true);
        FindObjectOfType<internetManager>().RetrieveScores();
    }

    public void AvisoErroCarregar() {
        avisoCarregando.SetActive(false);
        avisoCarregar.SetActive(true);
    }

    public void ReceberPlacares(Dictionary<int, ScoreRanking> scores, int modo) {
        switch (modo)
        {
            case 1: rankingRanqueado.ShowPlacares(scores, true); break;
            case 2: rankingFacil.ShowPlacares(scores, false); break;
            case 3: rankingMedio.ShowPlacares(scores, false); break;
            case 4: rankingDificil.ShowPlacares(scores, false); MostrarRanking(); break;
        }
    }

    void MostrarRanking() {
        avisoCarregando.SetActive(false);
        ranqueadoTela.SetActive(true);
        escolherModo.SetActive(true);
    }
}
