using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Config : MonoBehaviour
{
    public Slider music;
    public Slider effects;
    public Slider voice;

    public Text modoInternet;
    public checarVersion versao;
    public bool checarModoOffline;

    // Start is called before the first frame update
    void Start()
    {
        music.value = PlayerPrefs.GetFloat("music", 0.5f);
        effects.value = PlayerPrefs.GetFloat("effects", 0.8f);
        voice.value = PlayerPrefs.GetFloat("voice", 0.8f);

        if (checarModoOffline) {
            if (PlayerPrefs.GetString("modooffline", "off") == "off")
                modoInternet.text = "Desligado";
            else
                modoInternet.text = "Ligado";
        }
    }

    public void MudarInternet() {
        if (PlayerPrefs.GetString("modooffline") == "off") {
            modoInternet.text = "Ligado";
            PlayerPrefs.SetString("modooffline", "on");
            versao.OfflineUI();
        } else {
            modoInternet.text = "Desligado";
            PlayerPrefs.SetString("modooffline", "off");
        }
    }

    public void SalvarMusic() {
        PlayerPrefs.SetFloat("music", music.value);
    }

    public void SalvarEffect() {
        PlayerPrefs.SetFloat("effects", effects.value);
        FindObjectOfType<AudioManager>().Atualizar();
    }

    public void SalvarVoice() {
        PlayerPrefs.SetFloat("voice", voice.value);
    }

    public void AtualizarValores()
    {
        music.value = PlayerPrefs.GetFloat("music", 0.5f);
        effects.value = PlayerPrefs.GetFloat("effects", 0.8f);
        voice.value = PlayerPrefs.GetFloat("voice", 0.8f);
    }
}
