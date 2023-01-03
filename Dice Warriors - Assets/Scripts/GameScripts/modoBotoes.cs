using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class modoBotoes : MonoBehaviour
{
    int modo;
    public Text qualModo;
    public Image movimento;
    public Image poderes;

    public Sprite setas;
    public Sprite wasd;
    public Sprite ijkl;
    public Sprite cvb;
    public Sprite bnm;
    public Sprite qwer;
    public Sprite uiop;

    // Start is called before the first frame update
    void Start()
    {
        modo = PlayerPrefs.GetInt("modobotoes", 1);

        switch (modo)
        {
            case 1: qualModo.text = "Modo 1"; break;
            case 2: qualModo.text = "Modo 2"; break;
            case 3: qualModo.text = "Modo 3"; break;
            case 4: qualModo.text = "Modo 4"; break;
            case 5: qualModo.text = "Modo 5"; break;
            case 6: qualModo.text = "Modo 6"; break;
            case 7: qualModo.text = "Modo 7"; break;
            default: qualModo.text = "Modo X"; break;
        }

        UpdateImagens();
    }

    public void Resetou() {
        PlayerPrefs.SetInt("modobotoes", 1);
        qualModo.text = "Modo 1";

        UpdateImagens();
    }

    public void UpdateModoNext()
    {
        modo = PlayerPrefs.GetInt("modobotoes", 1);

        switch (modo)
        {
            case 1: qualModo.text = "Modo 2"; PlayerPrefs.SetInt("modobotoes", 2); break;
            case 2: qualModo.text = "Modo 3"; PlayerPrefs.SetInt("modobotoes", 3); break;
            case 3: qualModo.text = "Modo 4"; PlayerPrefs.SetInt("modobotoes", 4); break;
            case 4: qualModo.text = "Modo 5"; PlayerPrefs.SetInt("modobotoes", 5); break;
            case 5: qualModo.text = "Modo 6"; PlayerPrefs.SetInt("modobotoes", 6); break;
            case 6: qualModo.text = "Modo 7"; PlayerPrefs.SetInt("modobotoes", 7); break;
            case 7: qualModo.text = "Modo 1"; PlayerPrefs.SetInt("modobotoes", 1); break;
            default: qualModo.text = "Modo 1"; PlayerPrefs.SetInt("modobotoes", 1); break;
        }

        UpdateImagens();
    }

    public void UpdateModoPrevious()
    {
        modo = PlayerPrefs.GetInt("modobotoes", 1);

        switch (modo)
        {
            case 1: qualModo.text = "Modo 7"; PlayerPrefs.SetInt("modobotoes", 7); break;
            case 2: qualModo.text = "Modo 1"; PlayerPrefs.SetInt("modobotoes", 1); break;
            case 3: qualModo.text = "Modo 2"; PlayerPrefs.SetInt("modobotoes", 2); break;
            case 4: qualModo.text = "Modo 3"; PlayerPrefs.SetInt("modobotoes", 3); break;
            case 5: qualModo.text = "Modo 4"; PlayerPrefs.SetInt("modobotoes", 4); break;
            case 6: qualModo.text = "Modo 5"; PlayerPrefs.SetInt("modobotoes", 5); break;
            case 7: qualModo.text = "Modo 6"; PlayerPrefs.SetInt("modobotoes", 6); break;
            default: qualModo.text = "Modo 1"; PlayerPrefs.SetInt("modobotoes", 1); break;
        }

        UpdateImagens();
    }

    void UpdateImagens()
    {
        modo = PlayerPrefs.GetInt("modobotoes", 1);

        switch (modo)
        {
            case 1: movimento.sprite = setas; poderes.sprite = cvb; break;
            case 2: movimento.sprite = setas; poderes.sprite = qwer; break;
            case 3: movimento.sprite = wasd; poderes.sprite = bnm; break;
            case 4: movimento.sprite = wasd; poderes.sprite = uiop; break;
            case 5: movimento.sprite = ijkl; poderes.sprite = cvb; break;
            case 6: movimento.sprite = ijkl; poderes.sprite = qwer; break;
            case 7: movimento.sprite = null; poderes.sprite = null; break;
            default: break;
        }
    }
}
