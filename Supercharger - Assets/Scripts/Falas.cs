using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Falas : MonoBehaviour
{
    [Header("Opções")]
    public GameObject dialog;
    public GameObject escolhas;
    public string[] falas;
    public TextMeshProUGUI falaAtual;
    public string falaFinal;

    [Header("Próxima Corrida")]
    public bool corrida1;
    public bool corrida2;
    public bool corrida3;

    int numeroDaFalaAtual;
    bool falaFinalAtiva;
    string oficina;

    void Start()
    {
        if(corrida1) oficina = "oficina1-";
        if(corrida2) oficina = "oficina2-";
        if(corrida3) oficina = "oficina3-";

        falaFinalAtiva = false;
        numeroDaFalaAtual = 0;
        falaAtual.text = falas[numeroDaFalaAtual];
        FindObjectOfType<AudioManager>().Play(oficina + (numeroDaFalaAtual+1).ToString());
    }

    public void Proximo()
    {
        FindObjectOfType<AudioManager>().Stop(oficina + (numeroDaFalaAtual+1).ToString());
        if (falaFinalAtiva) {
            ProximaCorrida();
        } else {
            numeroDaFalaAtual++;

            if (numeroDaFalaAtual < falas.Length) {
                falaAtual.text = falas[numeroDaFalaAtual];
                FindObjectOfType<AudioManager>().Play(oficina + (numeroDaFalaAtual+1).ToString());
            } else {
                dialog.SetActive(false);
                escolhas.SetActive(true);
            }
        }
    }

    void FalaFinal() {
        falaAtual.text = falaFinal;
        falaFinalAtiva = true;
        dialog.SetActive(true);
        escolhas.SetActive(false);
        FindObjectOfType<AudioManager>().Play(oficina + (numeroDaFalaAtual+1).ToString());
    }

    void ProximaCorrida() {
        if(corrida1) SceneManager.LoadScene("RaceScene1");
        if(corrida2) SceneManager.LoadScene("RaceScene2");
        if(corrida3) SceneManager.LoadScene("RaceScene3");
    }

    public void EscolhaSuperchargers(int n) {
        FindObjectOfType<GameManager>().AddSuperchargers(n);
        FalaFinal();
    }

    public void EscolhaVida(int n) {
        FindObjectOfType<GameManager>().AddVida(n);
        FalaFinal();
    }

    public void EscolhaVelocidadeAcelera() {
        FindObjectOfType<GameManager>().AddVelocidadeAcelera();
        FalaFinal();
    }

    public void EscolhaVelocidadeDirecao() {
        FindObjectOfType<GameManager>().AddVelocidadeDirecao();
        FalaFinal();
    }

    public void EscolhaTurbo() {
        FindObjectOfType<GameManager>().AddTurboDobrado();
        FalaFinal();
    }
}
