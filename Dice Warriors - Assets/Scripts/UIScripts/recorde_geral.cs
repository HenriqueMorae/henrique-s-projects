using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class recorde_geral : MonoBehaviour
{
    public Text recordeRank;
    public Text recordeZenF;
    public Text recordeZenM;
    public Text recordeZenD;
    public Text arenaRank;
    public Text arenaZenF;
    public Text arenaZenM;
    public Text arenaZenD;

    public Sprite dado4;
    public Sprite dado6;
    public Sprite dado8;
    public Sprite dado12;
    public Sprite dado20;

    public SpriteRenderer playerRank;
    public SpriteRenderer playerZenF;
    public SpriteRenderer playerZenM;
    public SpriteRenderer playerZenD;

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
        recordeRank.text = PlayerPrefs.GetInt("recorde_ranqueado", 0).ToString();
        recordeZenF.text = PlayerPrefs.GetInt("recorde_zenf", 0).ToString();
        recordeZenM.text = PlayerPrefs.GetInt("recorde_zenm", 0).ToString();
        recordeZenD.text = PlayerPrefs.GetInt("recorde_zend", 0).ToString();
        
        arenaRank.text = GetArena(PlayerPrefs.GetString("arena_recorde_ranqueado"));
        arenaZenF.text = GetArena(PlayerPrefs.GetString("arena_recorde_zenf"));
        arenaZenM.text = GetArena(PlayerPrefs.GetString("arena_recorde_zenm"));
        arenaZenD.text = GetArena(PlayerPrefs.GetString("arena_recorde_zend"));

        playerRank.sprite = GetPhoto(PlayerPrefs.GetInt("player_recorde_ranqueado"));
        playerZenF.sprite = GetPhoto(PlayerPrefs.GetInt("player_recorde_zenf"));
        playerZenM.sprite = GetPhoto(PlayerPrefs.GetInt("player_recorde_zenm"));
        playerZenD.sprite = GetPhoto(PlayerPrefs.GetInt("player_recorde_zend"));

        int pontos = PlayerPrefs.GetInt("recorde_ranqueado", 0);

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

    Sprite GetPhoto(int dado) {
        Sprite imagem = null;

        switch (dado)
        {
            case 4: imagem = dado4; break;
            case 6: imagem = dado6; break;
            case 8: imagem = dado8; break;
            case 12: imagem = dado12; break;
            case 20: imagem = dado20; break;
        }

        return imagem;
    }

}
