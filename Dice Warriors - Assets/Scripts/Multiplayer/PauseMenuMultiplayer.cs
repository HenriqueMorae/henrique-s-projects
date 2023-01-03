using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PauseMenuMultiplayer : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject configMenuUI;
    public GameObject sairMenuUI;
    public GameObject sair2MenuUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (GameIsPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume () {
        pauseMenuUI.SetActive(false);
        configMenuUI.SetActive(false);
        sairMenuUI.SetActive(false);
        sair2MenuUI.SetActive(false);
        GameIsPaused = false;
    }

    public void Pause () {
        pauseMenuUI.SetActive(true);
        configMenuUI.SetActive(false);
        sairMenuUI.SetActive(false);
        sair2MenuUI.SetActive(false);
        GameIsPaused = true;
    }

    public void Desconectar() {
        PhotonNetwork.Disconnect();
    }
}
