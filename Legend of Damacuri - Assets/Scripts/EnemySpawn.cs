using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] Transform wave1;
    [SerializeField] Transform wave2;
    [SerializeField] Transform wave3;
    [SerializeField] GameObject ending;

    bool wave2Ativada;
    bool wave3Ativada;
    bool endingAtivado;

    void Start()
    {
        wave1.gameObject.SetActive(true);
        FindObjectOfType<AudioManager>().Play("enemy_appear");
    }

    void Update()
    {
        if (wave1.childCount == 0 && !wave2Ativada) {
            wave2.gameObject.SetActive(true);
            FindObjectOfType<AudioManager>().Play("enemy_appear");
            wave2Ativada = true;
        }

        if (wave2.childCount == 0 && !wave3Ativada) {
            wave3.gameObject.SetActive(true);
            FindObjectOfType<AudioManager>().Play("enemy_appear");
            wave3Ativada = true;
        }

        if (wave3.childCount == 0 && !endingAtivado) {
            endingAtivado = true;
            ending.SetActive(true);
        }

        if (endingAtivado && Input.anyKeyDown) {
            SceneManager.LoadScene("MenuScene");
        } 
    }
}
