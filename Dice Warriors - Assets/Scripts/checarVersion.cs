using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class checarVersion : MonoBehaviour
{
    public GameObject versaoDesatualizada;
    public GameObject versaoSemInternet;
    public GameObject questionario;
    public GameObject ranking;
    public GameObject aviso;
    public GameObject noticiaSemRanking;
    public GameObject modoOnlineOk;
    public GameObject modoOnlineNo;
    public Text texto;
    public Text botaoConfig;
    public Image internet;
    
    bool tentativa = false;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetString("modooffline", "off") == "on") {
            OfflineUI();
            return;
        }

        string response = FindObjectOfType<internetManager>().InternetOn();

        if (response == "yes") {
            OnlineUI();
            PlayerPrefs.SetString("modooffline", "off");
        } else if (response == "noVersion") {
            versaoDesatualizada.SetActive(true);
            OfflineUI();
        } else if (response == "noInternet") {
            versaoSemInternet.SetActive(true);
            OfflineUI();
        }
    }

    public void OnlineUI() {
        texto.text = "Online";
        internet.color = Color.green;
        questionario.SetActive(true);
        aviso.SetActive(false);
        modoOnlineOk.SetActive(true);
        modoOnlineNo.SetActive(false);
        botaoConfig.text = "Desligado";
        IsRankingOn();
    }

    void IsRankingOn() {
        if (FindObjectOfType<internetManager>().RankingOn()) {
            ranking.SetActive(true);
            noticiaSemRanking.SetActive(false);
        } else {
            noticiaSemRanking.SetActive(true);
            ranking.SetActive(false);
        }
    }

    public void OfflineUI() {
        texto.text = "Offline";
        internet.color = Color.red;
        questionario.SetActive(false);
        ranking.SetActive(false);
        noticiaSemRanking.SetActive(false);
        aviso.SetActive(true);
        modoOnlineOk.SetActive(false);
        modoOnlineNo.SetActive(true);
    }

    public void ModoOfflineOff() {
        PlayerPrefs.SetString("modooffline", "off");
        botaoConfig.text = "Desligado";
    }

    public void ModoOfflineOn() {
        PlayerPrefs.SetString("modooffline", "on");
        botaoConfig.text = "Ligado";
    }

    public void Reconectar() {
        if (PlayerPrefs.GetString("modooffline", "off") == "on" || tentativa == true)
            return;

        internet.color = Color.white;
        texto.text = "Reconectando...";

        tentativa = true;
        StartCoroutine("Reconnect");
    }

    IEnumerator Reconnect() {
        yield return new WaitForSeconds(0.5f);
        string response = FindObjectOfType<internetManager>().InternetOn();

        if (response == "yes") {
            OnlineUI();
        } else if (response == "noVersion") {
            versaoDesatualizada.SetActive(true);
            OfflineUI();
        } else if (response == "noInternet") {
            versaoSemInternet.SetActive(true);
            OfflineUI();
        }

        tentativa = false;
    }
}
