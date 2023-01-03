using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Placar : MonoBehaviour
{
    public Text pontos;
    int placar = 0;

    public EnemySpawn spawn;
    public float delay;
    public float minusDelay;

    public GameObject rankBar;
    Slider medidor;
    public Text texto;
    public Image cor;

    public GameObject changeRankShield;
    Animator mudarShield;

    bool isRanqueado;
    bool guerreiro = false;
    bool primeiravez = true;

    // Start is called before the first frame update
    void Start()
    {
        pontos.text = "0";

        string modo = PlayerPrefs.GetString("modo");

        if (modo == "ranqueado") {
            isRanqueado = true;
            rankBar.SetActive(true);
            medidor = rankBar.GetComponent<Slider>();
            mudarShield = changeRankShield.GetComponent<Animator>();
        } else
            isRanqueado = false;
    }

    public void Maispontos(int add) {
        placar += add;
        pontos.text = placar.ToString();

        int sobra;
        int resultado = Math.DivRem(placar, 40, out sobra);
        
        if (sobra == 0){
            FindObjectOfType<VoiceManager>().FalaFala();
        }

        if (isRanqueado && !guerreiro) {
            resultado = Math.DivRem(placar, 50, out sobra);
            medidor.value = sobra;

            if (sobra == 0 && delay > 0) {
                FindObjectOfType<LeftDown>().RankUp();

                if (primeiravez) {
                    mudarShield.Play("put_rankshield");
                    primeiravez = false;
                } else {
                    mudarShield.Play("change_rankshield");
                }

                delay -= minusDelay;

                if (delay == 0) {
                    medidor.value = 50;
                    cor.color = Color.yellow;
                    texto.text = "Guerreiro";
                    guerreiro = true;
                    return;
                }

                spawn.NewSpawn(delay);
            }
        }
    }

    void OnDisable()
    {
        PlayerPrefs.SetInt("placar", placar);
    }
}
