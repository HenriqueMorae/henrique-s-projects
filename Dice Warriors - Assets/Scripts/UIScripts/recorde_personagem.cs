using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class recorde_personagem : MonoBehaviour
{
    public string personagem;

    public Text recordeRank;
    public Text recordeZenF;
    public Text recordeZenM;
    public Text recordeZenD;
    public Text arenaRank;
    public Text arenaZenF;
    public Text arenaZenM;
    public Text arenaZenD;

    public Sprite bronze;    
    public Sprite prata;
    public Sprite ouro;
    public Sprite platina;
    public Sprite diamante;
    public Sprite guerreiro;

    public SpriteRenderer ranque;

    // Start is called before the first frame update
    void Start()
    {
        recordeRank.text = PlayerPrefs.GetInt("recorde_ranqueado_" + personagem, 0).ToString();
        recordeZenF.text = PlayerPrefs.GetInt("recorde_zenf_" + personagem, 0).ToString();
        recordeZenM.text = PlayerPrefs.GetInt("recorde_zenm_" + personagem, 0).ToString();
        recordeZenD.text = PlayerPrefs.GetInt("recorde_zend_" + personagem, 0).ToString();
        
        arenaRank.text = GetArena(PlayerPrefs.GetString("arena_recorde_ranqueado_" + personagem));
        arenaZenF.text = GetArena(PlayerPrefs.GetString("arena_recorde_zenf_" + personagem));
        arenaZenM.text = GetArena(PlayerPrefs.GetString("arena_recorde_zenm_" + personagem));
        arenaZenD.text = GetArena(PlayerPrefs.GetString("arena_recorde_zend_" + personagem));

        int pontos = PlayerPrefs.GetInt("recorde_ranqueado_" + personagem, 0);

        if (pontos < 50) {
            ranque.sprite = null;
        } else if (pontos >= 50 && pontos < 100) {
            ranque.sprite = bronze;
        } else if (pontos >= 100 && pontos < 150) {
            ranque.sprite = prata;
        } else if (pontos >= 150 && pontos < 200) {
            ranque.sprite = ouro;
        } else if (pontos >= 200 && pontos < 250) {
            ranque.sprite = platina;
        } else if (pontos >= 250 && pontos < 300) {
            ranque.sprite = diamante;
        } else if (pontos >= 300) {
            ranque.sprite = guerreiro;
        }
    }

    string GetArena(string nome) {
        string essenome = "";
        
        switch (nome)
        {
            case "tradicional": essenome = "Arena Tradicional"; break;
            case "caixas": essenome = "Arena Floresta"; break;
            case "cercas": essenome = "Arena Coliseu"; break;
        }

        return essenome;
    }
}
