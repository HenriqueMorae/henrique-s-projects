using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pegarMelhorCombo : MonoBehaviour
{
    public Text qtd;

    // Start is called before the first frame update
    void Start()
    {
        qtd.text = PlayerPrefs.GetInt("melhorcombo", 0).ToString();
    }
}
