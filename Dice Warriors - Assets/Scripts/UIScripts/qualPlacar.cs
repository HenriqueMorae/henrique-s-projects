using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class qualPlacar : MonoBehaviour
{
    public GameObject ranque;
    public GameObject zen;

    // Start is called before the first frame update
    void Start()
    {
        string modo = PlayerPrefs.GetString("modo");

        if (modo == "ranqueado")
            ranque.SetActive(true);
        else
            zen.SetActive(true);

    }
}
