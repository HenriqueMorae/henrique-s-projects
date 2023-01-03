using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Botoes : MonoBehaviour
{
    public bool ultimaFalaDoJogo;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;

        if(ultimaFalaDoJogo) FindObjectOfType<AudioManager>().Play("oficina4-1");
    }

    public void ParaFala() {
        FindObjectOfType<AudioManager>().Stop("oficina4-1");
    }

    public void MenuPrincipal () {
        FindObjectOfType<GameManager>().NovoJogo();
        Time.timeScale = 1f;
        AudioListener.pause = false;
        SceneManager.LoadScene("Menu");
    }

    public void Easy() {
        FindObjectOfType<GameManager>().Facil();
        SceneManager.LoadScene("OficinaScene1");
    }

    public void Medio() {
        FindObjectOfType<GameManager>().Medio();
        SceneManager.LoadScene("OficinaScene1");
    }

    public void Hard() {
        FindObjectOfType<GameManager>().Dificil();
        SceneManager.LoadScene("OficinaScene1");
    }

    public void Pro() {
        FindObjectOfType<GameManager>().Pro();
        SceneManager.LoadScene("OficinaScene1");
    }

    public void SairDoJogo() {
        Application.Quit();
    }
}
