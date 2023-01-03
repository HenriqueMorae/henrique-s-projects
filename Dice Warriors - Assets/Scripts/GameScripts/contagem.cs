using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class contagem : MonoBehaviour
{
    public Text texto;
    public Animator efeito;

    void Start()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        PauseMenu.GameIsPaused = false;

        PlayerPrefs.SetInt("melhorcombo", 0);
        
        texto.text = "3";
        FindObjectOfType<AudioManager>().Play("countdown");
        StartCoroutine("contar");
    }

    IEnumerator contar() {
        yield return new WaitForSeconds(1f);
        texto.text = "2";
        yield return new WaitForSeconds(1f);
        texto.text = "1";
        yield return new WaitForSeconds(1f);
        texto.text = "LUTE!";
        efeito.Play("contagem_1");
    }
}
