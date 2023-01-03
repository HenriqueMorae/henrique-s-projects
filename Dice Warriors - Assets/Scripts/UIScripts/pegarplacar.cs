using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pegarplacar : MonoBehaviour
{
    public Text placar;
    public Text recorde;
    string qualRecorde; 
    int pontos;
    string modo;
    bool isRecorde;

    public GameObject novorecorde;
    public ChangevoiceGameover vozMusica;

    // Start is called before the first frame update
    void Start()
    {
        isRecorde = false;
        modo = PlayerPrefs.GetString("modo");

        switch (modo)
        {
            case "ranqueado": qualRecorde = "recorde_ranqueado"; break;
            case "zenfacil": qualRecorde = "recorde_zenf"; break;
            case "zenmedio": qualRecorde = "recorde_zenm"; break;
            case "zendificil": qualRecorde = "recorde_zend"; break;
        }


        pontos = PlayerPrefs.GetInt("placar");
        StartCoroutine("ScoreCounter");

        if (pontos > PlayerPrefs.GetInt(qualRecorde, 0)) {
            isRecorde = true;
            PlayerPrefs.SetInt(qualRecorde, pontos);
            PlayerPrefs.SetString("arena_" + qualRecorde, PlayerPrefs.GetString("arena"));
            PlayerPrefs.SetInt("player_" + qualRecorde, PlayerPrefs.GetInt("personagem"));
        }

        string recordedepersonagem = qualRecorde + "_" + PlayerPrefs.GetInt("personagem").ToString();

        if (pontos > PlayerPrefs.GetInt(recordedepersonagem, 0)) {
            PlayerPrefs.SetInt(recordedepersonagem, pontos);
            PlayerPrefs.SetString("arena_" + recordedepersonagem, PlayerPrefs.GetString("arena"));
        }

        recorde.text = PlayerPrefs.GetInt(qualRecorde, 0).ToString();
    }

    IEnumerator ScoreCounter() {
        yield return new WaitForSeconds(2.5f);
        float intervalo = 1f/(pontos+1);
        FindObjectOfType<AudioManager>().Play("placar_counter");

        for (int i = 0; i < pontos; i++)
        {
            placar.text = i.ToString();
            yield return new WaitForSeconds(intervalo);
        }
        placar.text = pontos.ToString();
        FindObjectOfType<AudioManager>().Stop("placar_counter");
        FindObjectOfType<AudioManager>().Play("finish_placar_counter");

        yield return new WaitForSeconds(1f);

        if (isRecorde) {
            novorecorde.SetActive(true);
            yield return new WaitForSeconds(1f);
        }

        vozMusica.Vai();
    }
}
