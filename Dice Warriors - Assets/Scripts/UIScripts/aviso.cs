using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aviso : MonoBehaviour
{
    public GameObject avisoNaTela;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetString("modooffline") == "on")
            avisoNaTela.SetActive(true);
    }

}
