using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class botoes : MonoBehaviour
{
    public GameObject carregandoScreen;
    public bool recordesScreen;

    public GameObject desligar1;
    public GameObject desligar2;
    public GameObject desligar3;
    public GameObject desligar4;
    public GameObject desligar5;
    public GameObject desligar6;

    void Awake() {
        FindObjectOfType<AudioManager>().StopAll();
    }

    public void Sair() {
        Application.Quit();
    }

    public void PostPlacar() {
        FindObjectOfType<internetManager>().PostScore(false);
    }

    public void PostRanking() {
        FindObjectOfType<internetManager>().PostScore(true);
    }

    public void Questionario() {
        Application.OpenURL("https://forms.gle/WNoBdVD6DLsaxavf9");
    }

    public void Atualizar() {
        Application.OpenURL("https://playdicewarriors.blogspot.com/p/download.html");
    }

    public void EscolherPersonagem() {
        Loader.Load(Loader.Scene.CharacterSelect);
    }

    public void Guerreiros() {
        Loader.Load(Loader.Scene.Guerreiros);
    }

    public void Ranking() {
        carregandoScreen.SetActive(true);
        Loader.Load(Loader.Scene.Ranking);
    }

    public void Recordes() {
        Loader.Load(Loader.Scene.Recordes);
    }

    public void ModoArena() {
        string arena = PlayerPrefs.GetString("arena");

        switch (arena)
        {
            case "tradicional": Loader.Load(Loader.Scene.Arena); break;
            case "caixas": Loader.Load(Loader.Scene.Arena_Caixas); break;
            case "cercas": Loader.Load(Loader.Scene.Arena_Cercas); break;
        }
            
    }

    public void GameOver() {
        carregandoScreen.SetActive(true);
        Loader.Load(Loader.Scene.GameOver);
    }

    public void MenuPrincipal() {
        carregandoScreen.SetActive(true);

        if (recordesScreen) {
            desligar1.SetActive(false);
            desligar2.SetActive(false);
            desligar3.SetActive(false);
            desligar4.SetActive(false);
            desligar5.SetActive(false);
            desligar6.SetActive(false);
        }

        Loader.Load(Loader.Scene.MainMenu);
    }

    public void MenuPrincipalParaSom() {
        carregandoScreen.SetActive(true);
        FindObjectOfType<AudioManager>().StopAll();
        Loader.Load(Loader.Scene.MainMenu);
    }

    public void MenuPrincipalParaSomMultiplayer() {
        carregandoScreen.SetActive(true);
        PhotonNetwork.Disconnect();
        FindObjectOfType<AudioManager>().StopAll();
        Loader.Load(Loader.Scene.MainMenu);
    }

    public void PassarMouse() {
        FindObjectOfType<AudioManager>().Play("botao2");
    }

    public void ApertarMouse() {
        FindObjectOfType<AudioManager>().Play("botao1");
    }

    public void ResetaroJogo() {
        PlayerPrefs.DeleteAll();
    }

    public GameObject CarregandoScreenDeath() {
        return carregandoScreen;
    }
}
