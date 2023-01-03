using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class RaceManager : MonoBehaviour
{
    [Header("Telas Finais")]
    public GameObject completo;
    public GameObject incompleto;
    public GameObject aviso;

    [Header("Próxima Oficina")]
    public bool oficina2;
    public bool oficina3;
    public bool oficina4;

    [Header("Contagem")]
    public TextMeshProUGUI numero;

    [Header("Carro")]
    public Transform carro;

    [Header("Corrida")]
    public Slider medidor;
    public float duracao;
    public float duracaoFinal;

    [Header("Vida")]
    public Image barraDeVida;

    [Header("Supercharger")]
    public Image charge;
    public float offset;
    public float multiplicador = 1f;
    public GameObject supercharger;
    public Transform localDosSuperchargers;

    [Header("Radio")]
    public GameObject radio;

    float tempoAtual;
    float barraDeCharge;
    float tempoDesdeUltimoSC;
    int numeroSuperchargers;
    int vida;
    bool final;
    Coroutine sc;
    Coroutine av;
    Coroutine rd;
    bool falandoRadio;
    bool avisomeio;
    string cr;

    void Awake() {
        Time.timeScale = 1f;
    }

    // Start is called before the first frame update
    void Start()
    {
        if(oficina2) cr = "1";
        if(oficina3) cr = "2";
        if(oficina4) cr = "3";

        falandoRadio = false;
        avisomeio = false;
        tempoDesdeUltimoSC = 0f;
        vida = FindObjectOfType<GameManager>().GetVida();
        barraDeVida.fillAmount = vida/100f;
        final = false;
        tempoAtual = 0;
        barraDeCharge = 0;
        numeroSuperchargers = FindObjectOfType<GameManager>().GetSuperchargers();

        if (numeroSuperchargers == 1) {
            Instantiate(supercharger, localDosSuperchargers);
        }

        if (numeroSuperchargers == 2) {
            Instantiate(supercharger, localDosSuperchargers);
            Instantiate(supercharger, localDosSuperchargers);
        }

        StartCoroutine(Largada());
    }

    IEnumerator Largada() {
        yield return new WaitForSeconds(1f);
        numero.text = "2";
        yield return new WaitForSeconds(1f);
        numero.text = "1";
        yield return new WaitForSeconds(1f);
        numero.text = "GO";
        carro.GetComponent<CarMovement>().enabled = true;
        foreach (CenarioMovement cm in FindObjectsOfType<CenarioMovement>()) { cm.enabled = true; }
        foreach (Adversario ad in FindObjectsOfType<Adversario>()) { ad.enabled = true; }
        FindObjectOfType<ObstacleSpawn>().enabled = true;
        FindObjectOfType<BackgroundCenario>().enabled = true;
        FindObjectOfType<PlayerInput>().enabled = true;
        StartCoroutine(Corrida());
        sc = StartCoroutine(Charge());
        yield return new WaitForSeconds(1f);
        numero.text = "";
        Radio("inicio" + cr);
    }

    IEnumerator Corrida() {
        while (tempoAtual < duracao) {
            tempoAtual += 0.05f;
            tempoDesdeUltimoSC += 0.05f;
            medidor.value = tempoAtual/duracao;

            if (tempoAtual > duracao/2 && !avisomeio) {
                avisomeio = true;
                Radio("meio" + cr);
            }

            if (tempoDesdeUltimoSC > 25f) {
                Radio("aviso" + Random.Range(1,3).ToString());
                tempoDesdeUltimoSC = 0f;
            }

            if (tempoAtual+3.5f >= duracao && !final) {
                final = true;
                StartCoroutine(FinishLine());
            }
            yield return new WaitForSeconds(0.05f);
        }
        StopCoroutine(sc);
        barraDeCharge = 0;
        charge.fillAmount = 0;
    }

    IEnumerator FinishLine() {
        FindObjectOfType<ObstacleSpawn>().Parar();
        yield return new WaitForSeconds(3.5f);
        carro.GetComponent<CarMovement>().Finish();
        Radio("final" + cr);
        av = StartCoroutine(Pisca());
        foreach (Adversario ad in FindObjectsOfType<Adversario>()) { ad.Final(); }
        yield return new WaitForSeconds(duracaoFinal);
        FindObjectOfType<BackgroundCenario>().BotaAChegada();
    }

    public void Fim() {
        StopCoroutine(av);
        aviso.SetActive(false);
    }

    IEnumerator Pisca() {
        while (true)
        {
            aviso.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            aviso.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator Charge() {
        while (true)
        {
            barraDeCharge += multiplicador * (carro.position.x + offset)/50;
            if (barraDeCharge < 0) barraDeCharge = 0;
            if (barraDeCharge >= 100 && numeroSuperchargers < 10) {
                Instantiate(supercharger, localDosSuperchargers);
                FindObjectOfType<AudioManager>().Play("supercharger");
                numeroSuperchargers++;
                tempoDesdeUltimoSC = 0f;
                if (Random.Range(1,3) == 1) Radio("super" + Random.Range(1,5).ToString());
                barraDeCharge = 0;
            }
            if (numeroSuperchargers == 10) barraDeCharge = 0;
            charge.fillAmount = barraDeCharge/100;
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void Dano(int dano) {
        vida -= dano;
        barraDeCharge -= dano/2;
        if (barraDeCharge < 0) barraDeCharge = 0;
        if (vida <= 0) {
            vida = 0;
            Time.timeScale = 0f;
            Falha();
        } else {
            if (Random.Range(1,3) == 1) Radio("dano" + Random.Range(1,5).ToString());
        }
        barraDeVida.fillAmount = vida/100f;
    }

    public float Boost() {
        float boost = barraDeCharge;
        barraDeCharge = 0;
        charge.fillAmount = 0;
        return boost;
    }

    public int NumeroSC() {
        return numeroSuperchargers;
    }

    public void Reiniciar() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ProximaOficina() {
        FindObjectOfType<GameManager>().SetVida(vida);
        if(oficina2) SceneManager.LoadScene("OficinaScene2");
        if(oficina3) SceneManager.LoadScene("OficinaScene3");
        if(oficina4) SceneManager.LoadScene("OficinaScene4");
    }

    public void Completou() {
        completo.SetActive(true);
    }

    public void Falha() {
        incompleto.SetActive(true);
    }

    void Radio(string fala) {
        if (falandoRadio) return;
        rd = StartCoroutine(FalaRadio(fala));
    }

    IEnumerator FalaRadio(string fala) {
        falandoRadio = true;
        if (fala.Substring(0,5) == "super" || fala.Substring(0,4) == "dano") yield return new WaitForSeconds(1f);
        radio.SetActive(true);
        FindObjectOfType<AudioManager>().Play(fala);
        yield return new WaitForSeconds(FindObjectOfType<AudioManager>().Length(fala));
        radio.SetActive(false);
        falandoRadio = false;
    }
}
