using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class escolherModoRanking : MonoBehaviour
{
    int pagina = 1;

    public GameObject telaRanqueado;
    public GameObject telaFacil;
    public GameObject telaMedio;
    public GameObject telaDificil;

    public Text modo;

    public void Next() {
        switch (pagina)
        {
            case 1: pagina++; telaRanqueado.SetActive(false); telaFacil.SetActive(true); modo.text = "Zen Fácil"; break;
            case 2: pagina++; telaFacil.SetActive(false); telaMedio.SetActive(true); modo.text = "Zen Médio"; break;
            case 3: pagina++; telaMedio.SetActive(false); telaDificil.SetActive(true); modo.text = "Zen Difícil"; break;
            case 4: pagina = 1; telaDificil.SetActive(false); telaRanqueado.SetActive(true); modo.text = "Ranqueado"; break;
        }
    }

    public void Previous() {
        switch (pagina)
        {
            case 1: pagina = 4; telaRanqueado.SetActive(false); telaDificil.SetActive(true); modo.text = "Zen Difícil"; break;
            case 2: pagina--; telaFacil.SetActive(false); telaRanqueado.SetActive(true); modo.text = "Ranqueado"; break;
            case 3: pagina--; telaMedio.SetActive(false); telaFacil.SetActive(true); modo.text = "Zen Fácil"; break;
            case 4: pagina--; telaDificil.SetActive(false); telaMedio.SetActive(true); modo.text = "Zen Médio"; break;
        }
    }
}
