using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
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
        Time.timeScale = 1f;
        AudioListener.pause = false;
        GameIsPaused = false;
    }

    public void Pause () {
        pauseMenuUI.SetActive(true);
        configMenuUI.SetActive(false);
        sairMenuUI.SetActive(false);
        sair2MenuUI.SetActive(false);
        Time.timeScale = 0f;
        AudioListener.pause = true;
        GameIsPaused = true;
    }
}
