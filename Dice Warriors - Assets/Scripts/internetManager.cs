using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using UnityEngine.Networking;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WebClientTimeout: WebClient
{
    protected override WebRequest GetWebRequest (Uri address)
    {
        WebRequest wr = base.GetWebRequest(address);
        wr.Timeout = 5000; // ms
        return wr;
    }
}

public class internetManager : MonoBehaviour
{
    public static internetManager instance;
    private const string scoreURL = "https://dicewarriors.000webhostapp.com/ranking_database.php";
    bool newScore = true;

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
    }

    public string InternetOn() {
        string reply;
        
        try
        {
            WebClient client = new WebClientTimeout();
            reply = client.DownloadString("https://dicewarriors.000webhostapp.com/version.txt");
        }
        catch (Exception ex)
        {
            Debug.Log("Problema na Internet 1");
            Debug.Log(ex);

            try
            {
                WebClient client2 = new WebClientTimeout();
                reply = client2.DownloadString("https://drive.google.com/uc?export=download&id=1B368uDeiXnkdoFmLjpp8S6MQLD8bLe5E");
            }
            catch (Exception ex2)
            {
                Debug.Log("Problema na Internet 2");
                Debug.Log(ex2);
                return "noInternet";
            }
        }

        //Debug.Log(reply);

        if (reply == "A.1.3")
            return "yes";
        else
            return "noVersion";
    }

    public bool RankingOn() {
        string reply;

        try
        {
            WebClient client = new WebClientTimeout();
            reply = client.DownloadString("https://dicewarriors.000webhostapp.com/ranking.txt");
        }
        catch (Exception ex)
        {
            Debug.Log("Problema no Ranking");
            Debug.Log(ex);
            return false;
        }

        if (reply == "ON")
            return true;
        else
            return false;
    }

    public void PostScore(bool comp){
        if (newScore) {
            if (comp) {
                GameObject.FindWithTag("Enviar").GetComponent<Button>().enabled = false;
                GameObject.FindWithTag("Fechar").GetComponent<Button>().enabled = false;
                GameObject.FindWithTag("EnviarText").GetComponent<Text>().text = "Enviando...";
                GameObject.FindWithTag("Enviar").GetComponent<EventTrigger>().enabled = false;
                GameObject.FindWithTag("Fechar").GetComponent<EventTrigger>().enabled = false;
                GameObject.FindWithTag("InputName").GetComponent<InputField>().interactable = false;
                GameObject.FindWithTag("CheckInternet").GetComponent<checkInternet>().AtivarAviso(1, false);
                GameObject.FindWithTag("CheckInternet").GetComponent<checkInternet>().AtivarAviso(2, false);

                StartCoroutine("PostScoreCompetitivo");
                StartCoroutine("TimerComp");
            } else {
                StartCoroutine("PostScoreNormal");
                StartCoroutine("Timer");
            }
        }
    }

    public void RestartScore() {
        newScore = true;
    }

    IEnumerator Timer() {
        yield return new WaitForSeconds(10f);
        StopCoroutine("PostScoreNormal");
    }

    IEnumerator TimerComp() {
        yield return new WaitForSeconds(10f);
        StopCoroutine("PostScoreCompetitivo");
        GameObject.FindWithTag("EnviarText").GetComponent<Text>().text = "Enviar";
        GameObject.FindWithTag("Enviar").GetComponent<Button>().enabled = true;
        GameObject.FindWithTag("Fechar").GetComponent<Button>().enabled = true;
        GameObject.FindWithTag("Enviar").GetComponent<EventTrigger>().enabled = true;
        GameObject.FindWithTag("Fechar").GetComponent<EventTrigger>().enabled = true;
        GameObject.FindWithTag("CheckInternet").GetComponent<checkInternet>().AtivarAviso(1, true);
        GameObject.FindWithTag("InputName").GetComponent<InputField>().interactable = true;
    }

    IEnumerator PostScoreCompetitivo()
    {
        yield return new WaitForSeconds(0.5f);
        string inputName = GameObject.FindWithTag("InputName").GetComponent<InputField>().text;

        if (String.IsNullOrWhiteSpace(inputName)) {
            GameObject.FindWithTag("EnviarText").GetComponent<Text>().text = "Enviar";
            GameObject.FindWithTag("Enviar").GetComponent<Button>().enabled = true;
            GameObject.FindWithTag("Fechar").GetComponent<Button>().enabled = true;
            GameObject.FindWithTag("Enviar").GetComponent<EventTrigger>().enabled = true;
            GameObject.FindWithTag("Fechar").GetComponent<EventTrigger>().enabled = true;
            GameObject.FindWithTag("CheckInternet").GetComponent<checkInternet>().AtivarAviso(2, true);
            GameObject.FindWithTag("InputName").GetComponent<InputField>().interactable = true;
        } else {
            WWWForm form = new WWWForm();
            form.AddField("post_partida", "true");
            form.AddField("personagem", Personagem());
            form.AddField("score", PlayerPrefs.GetInt("placar"));
            form.AddField("modo", PlayerPrefs.GetString("modo"));
            form.AddField("arena", PlayerPrefs.GetString("arena"));
            form.AddField("competitivo", 1);
            form.AddField("username", inputName);

            using (UnityWebRequest www = UnityWebRequest.Post(scoreURL, form))
            {
                yield return www.SendWebRequest();

                if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
                {
                    Debug.Log(www.error);
                    StopCoroutine("TimerComp");
                    GameObject.FindWithTag("EnviarText").GetComponent<Text>().text = "Enviar";
                    GameObject.FindWithTag("Enviar").GetComponent<Button>().enabled = true;
                    GameObject.FindWithTag("Fechar").GetComponent<Button>().enabled = true;
                    GameObject.FindWithTag("Enviar").GetComponent<EventTrigger>().enabled = true;
                    GameObject.FindWithTag("Fechar").GetComponent<EventTrigger>().enabled = true;
                    GameObject.FindWithTag("CheckInternet").GetComponent<checkInternet>().AtivarAviso(1, true);
                    GameObject.FindWithTag("InputName").GetComponent<InputField>().interactable = true;
                }
                else
                {
                    Debug.Log("Successfully posted score!");
                    StopCoroutine("TimerComp");
                    newScore = false;
                    GameObject.FindWithTag("EnviarPlacarText").GetComponent<Text>().text = "Enviado com sucesso!";
                    GameObject.FindWithTag("EnviarPlacar").GetComponent<Button>().enabled = false;
                    GameObject.FindWithTag("EnviarPlacar").GetComponent<Image>().color = new Color(0.75f, 1f, 0.75f, 1f);
                    GameObject.FindWithTag("EnviarPlacar").GetComponent<EventTrigger>().enabled = false;
                    GameObject.FindWithTag("TelaEnviar").SetActive(false);
                    GameObject.FindWithTag("CheckInternet").GetComponent<checkInternet>().PontosEnviados();
                }
            }
        }
    }

    IEnumerator PostScoreNormal()
    {
        WWWForm form = new WWWForm();
        form.AddField("post_partida", "true");
        form.AddField("personagem", Personagem());
        form.AddField("score", PlayerPrefs.GetInt("placar"));
        form.AddField("modo", PlayerPrefs.GetString("modo"));
        form.AddField("arena", PlayerPrefs.GetString("arena"));
        form.AddField("competitivo", 0);
        form.AddField("username", "");

        using (UnityWebRequest www = UnityWebRequest.Post(scoreURL, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(www.error);
                StopCoroutine("Timer");
            }
            else
            {
                Debug.Log("Successfully posted score!");
                StopCoroutine("Timer");
            }
        }
    }

    string Personagem() {
        switch (PlayerPrefs.GetInt("personagem"))
        {
            case 4: return "tessa";
            case 6: return "calebe";
            case 8: return "octavia";
            case 12: return "dominick";
            case 20: return "icaro";
            default: return "ERRO";
        }
    }

    public void RetrieveScores() {
        StartCoroutine("RetrieveRanking");
        StartCoroutine("TimerRetrieve");
    }

    IEnumerator TimerRetrieve() {
        yield return new WaitForSeconds(10f);
        StopCoroutine("RetrieveRanking");
        GameObject.FindWithTag("CheckRanking").GetComponent<checkRanking>().AvisoErroCarregar();
    }

    IEnumerator RetrieveRanking() {
        WWWForm form = new WWWForm();
        form.AddField("retrieve_ranking", "true");

        using (UnityWebRequest www = UnityWebRequest.Post(scoreURL, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                StopCoroutine("TimerRetrieve");
                GameObject.FindWithTag("CheckRanking").GetComponent<checkRanking>().AvisoErroCarregar();
                Debug.Log(www.error);
            }
            else
            {
                StopCoroutine("TimerRetrieve");
                Debug.Log("Successfully retrieved scores!");
                string contents = www.downloadHandler.text;
                Debug.Log(contents);

                string[] valores = contents.Split('\n');
                Dictionary<int, ScoreRanking> placares = new Dictionary<int, ScoreRanking>();
                ScoreRanking scoreAtual;
                int posicao = 1;

                for (int i = 0; i < valores.Length; i++)
                {
                    if (valores[i] == "RANQUEADO") {
                        continue;
                    } else if (valores[i] == "ZENFACIL") {
                        GameObject.FindWithTag("CheckRanking").GetComponent<checkRanking>().ReceberPlacares(placares, 1);
                        placares.Clear();
                        posicao = 1;
                        continue;
                    } else if (valores[i] == "ZENMEDIO") {
                        GameObject.FindWithTag("CheckRanking").GetComponent<checkRanking>().ReceberPlacares(placares, 2);
                        placares.Clear();
                        posicao = 1;
                        continue;
                    } else if (valores[i] == "ZENDIFICIL") {
                        GameObject.FindWithTag("CheckRanking").GetComponent<checkRanking>().ReceberPlacares(placares, 3);
                        placares.Clear();
                        posicao = 1;
                        continue;
                    } else if (valores[i] == "FIM") {
                        GameObject.FindWithTag("CheckRanking").GetComponent<checkRanking>().ReceberPlacares(placares, 4);
                    } else {
                        scoreAtual.pontos = valores[i];
                        scoreAtual.personagem = valores[i+1];
                        scoreAtual.username = valores[i+2];
                        scoreAtual.arena = valores[i+3];
                        i = i+3;

                        placares.Add(posicao, scoreAtual);
                        posicao++;
                    }
                }
            }
        }
    }
}

public struct ScoreRanking
{
    public string pontos;
    public string personagem;
    public string username;
    public string arena;
}