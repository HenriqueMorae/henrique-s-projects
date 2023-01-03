using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    int superchargers;
    int vida;
    float velocidadeAcelera;
    float velocidadeDirecao;
    bool turboDobrado;

    string dif;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        superchargers = 0;
        vida = 75;
        velocidadeAcelera = 1;
        velocidadeDirecao = 4;
        turboDobrado = false;

        dif = " ";
    }

    public void Facil() {
        dif = "f";
    }

    public void Medio() {
        dif = "m";
    }

    public void Dificil() {
        dif = "d";
    }

    public void Pro() {
        dif = "p";
    }

    public string Dificuldade() {
        return dif;
    }

    public int GetSuperchargers() {
        int sc = superchargers;
        superchargers = 0;
        return sc;
    }

    public void AddSuperchargers(int n) {
        superchargers = n;
    }

    public int GetVida() {
        return vida;
    }

    public void SetVida(int n) {
        vida = n;
    }

    public void AddVida(int n) {
        vida += n;
        if (vida > 100) vida = 100;
    }

    public float GetVelocidadeAcelera() {
        return velocidadeAcelera;
    }

    public void AddVelocidadeAcelera() {
        velocidadeAcelera = 2f;
    }

    public float GetVelocidadeDirecao() {
        return velocidadeDirecao;
    }

    public void AddVelocidadeDirecao() {
        velocidadeDirecao = 7;
    }

    public bool GetTurboDobrado() {
        return turboDobrado;
    }

    public void AddTurboDobrado() {
        turboDobrado = true;
    }

    public void NovoJogo() {
        superchargers = 0;
        vida = 75;
        velocidadeAcelera = 1;
        velocidadeDirecao = 4;
        turboDobrado = false;

        dif = " ";
    }
}
