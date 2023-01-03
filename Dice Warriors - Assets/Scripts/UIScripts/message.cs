using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class message : MonoBehaviour
{
    public GameObject mensagem;

    // Start is called before the first frame update
    void Start()
    {
        int abrir = PlayerPrefs.GetInt("mensagem", 0);

        if (abrir == 0)
            mensagem.SetActive(true);
    }

    public void AbrirMensagem()
    {
        mensagem.SetActive(true);
    }

    public void Fechar() {
        PlayerPrefs.SetInt("mensagem", 1);
    }
}
