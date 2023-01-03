using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class checkInternet : MonoBehaviour
{
    bool tentativa = false;
    bool enviado;
    public Text texto;
    public Image internet;

    public Animator enviarPlacarAnimator;
    public GameObject aviso;
    public GameObject aviso2;

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<internetManager>().RestartScore();
        enviado = false;

        if (PlayerPrefs.GetString("modooffline") == "on") {
            OfflineUI();
            return;
        }

        string response = FindObjectOfType<internetManager>().InternetOn();

        if (response == "yes") {
            OnlineUI();

            if (!enviado) {
                if (FindObjectOfType<internetManager>().RankingOn()) {
                    GameObject.FindWithTag("EnviarPlacarText").GetComponent<Text>().text = "Enviar pontuação para o Ranking";
                    GameObject.FindWithTag("EnviarPlacar").GetComponent<Button>().enabled = true;
                    GameObject.FindWithTag("EnviarPlacar").GetComponent<Image>().color = new Color(0.75f, 0.75f, 1f, 1f);
                    GameObject.FindWithTag("EnviarPlacar").GetComponent<EventTrigger>().enabled = true;
                } else {
                    GameObject.FindWithTag("EnviarPlacarText").GetComponent<Text>().text = "Ranking indisponível";
                    GameObject.FindWithTag("EnviarPlacar").GetComponent<Button>().enabled = false;
                    GameObject.FindWithTag("EnviarPlacar").GetComponent<Image>().color = new Color(1f, 0.75f, 0.75f, 1f);
                    GameObject.FindWithTag("EnviarPlacar").GetComponent<EventTrigger>().enabled = false;
                }
            }
            
        } else if (response == "noVersion") {
            OfflineUI();

            if (!enviado) {
                GameObject.FindWithTag("EnviarPlacarText").GetComponent<Text>().text = "Ranking indisponível";
                GameObject.FindWithTag("EnviarPlacar").GetComponent<Button>().enabled = false;
                GameObject.FindWithTag("EnviarPlacar").GetComponent<Image>().color = new Color(1f, 0.75f, 0.75f, 1f);
                GameObject.FindWithTag("EnviarPlacar").GetComponent<EventTrigger>().enabled = false;
            }
        } else if (response == "noInternet") {
            OfflineUI();
            
            if (!enviado) {
                GameObject.FindWithTag("EnviarPlacarText").GetComponent<Text>().text = "Ranking indisponível";
                GameObject.FindWithTag("EnviarPlacar").GetComponent<Button>().enabled = false;
                GameObject.FindWithTag("EnviarPlacar").GetComponent<Image>().color = new Color(1f, 0.75f, 0.75f, 1f);
                GameObject.FindWithTag("EnviarPlacar").GetComponent<EventTrigger>().enabled = false;
            }
        }

        enviarPlacarAnimator.Play("mandarplacar_in");
    }

    public void AtivarAviso(int qual, bool como) {
        switch (qual)
        {
            case 1: aviso.SetActive(como); break;
            case 2: aviso2.SetActive(como); break;
        }
    }

    public void OnlineUI() {
        texto.text = "Online";
        internet.color = Color.green;
    }

    public void OfflineUI() {
        texto.text = "Offline";
        internet.color = Color.red;
    }

    public void Reconectar() {
        if (PlayerPrefs.GetString("modooffline") == "on" || tentativa == true)
            return;

        internet.color = Color.white;
        texto.text = "Reconectando...";

        tentativa = true;
        StartCoroutine("Reconnect");
    }

    public void PontosEnviados() {
        enviado = true;
    }

    IEnumerator Reconnect() {
        yield return new WaitForSeconds(0.5f);
        string response = FindObjectOfType<internetManager>().InternetOn();

        if (response == "yes") {
            OnlineUI();

            if (!enviado) {
                if (FindObjectOfType<internetManager>().RankingOn()) {
                    GameObject.FindWithTag("EnviarPlacarText").GetComponent<Text>().text = "Enviar pontuação para o Ranking";
                    GameObject.FindWithTag("EnviarPlacar").GetComponent<Button>().enabled = true;
                    GameObject.FindWithTag("EnviarPlacar").GetComponent<Image>().color = new Color(0.75f, 0.75f, 1f, 1f);
                    GameObject.FindWithTag("EnviarPlacar").GetComponent<EventTrigger>().enabled = true;
                } else {
                    GameObject.FindWithTag("EnviarPlacarText").GetComponent<Text>().text = "Ranking indisponível";
                    GameObject.FindWithTag("EnviarPlacar").GetComponent<Button>().enabled = false;
                    GameObject.FindWithTag("EnviarPlacar").GetComponent<Image>().color = new Color(1f, 0.75f, 0.75f, 1f);
                    GameObject.FindWithTag("EnviarPlacar").GetComponent<EventTrigger>().enabled = false;
                }
            }
            
        } else if (response == "noVersion") {
            OfflineUI();

            if (!enviado) {
                GameObject.FindWithTag("EnviarPlacarText").GetComponent<Text>().text = "Ranking indisponível";
                GameObject.FindWithTag("EnviarPlacar").GetComponent<Button>().enabled = false;
                GameObject.FindWithTag("EnviarPlacar").GetComponent<Image>().color = new Color(1f, 0.75f, 0.75f, 1f);
                GameObject.FindWithTag("EnviarPlacar").GetComponent<EventTrigger>().enabled = false;
            }
        } else if (response == "noInternet") {
            OfflineUI();
            
            if (!enviado) {
                GameObject.FindWithTag("EnviarPlacarText").GetComponent<Text>().text = "Ranking indisponível";
                GameObject.FindWithTag("EnviarPlacar").GetComponent<Button>().enabled = false;
                GameObject.FindWithTag("EnviarPlacar").GetComponent<Image>().color = new Color(1f, 0.75f, 0.75f, 1f);
                GameObject.FindWithTag("EnviarPlacar").GetComponent<EventTrigger>().enabled = false;
            }
        }

        tentativa = false;
    }
}
