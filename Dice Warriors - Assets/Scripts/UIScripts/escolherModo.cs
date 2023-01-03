using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class escolherModo : MonoBehaviour
{
    public void ZenFacil() {
        PlayerPrefs.SetString("modo", "zenfacil");
    }

    public void ZenMedio() {
        PlayerPrefs.SetString("modo", "zenmedio");
    }

    public void ZenDificil() {
        PlayerPrefs.SetString("modo", "zendificil");
    }

    public void Ranqueado() {
        PlayerPrefs.SetString("modo", "ranqueado");
    }
}
