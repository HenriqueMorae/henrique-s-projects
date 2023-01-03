using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DarRanque : MonoBehaviour
{
    public SpriteRenderer ranque;
    public Text nomeRanque;

    public Sprite bronze;    
    public Sprite prata;
    public Sprite ouro;
    public Sprite platina;
    public Sprite diamante;
    public Sprite guerreiro;

    // Start is called before the first frame update
    void Start()
    {
        string modo = PlayerPrefs.GetString("modo");

        if (modo != "ranqueado")
            return;

        int pontos = PlayerPrefs.GetInt("placar");

        if (pontos < 50) {
            nomeRanque.text = "Nada";
        } else if (pontos >= 50 && pontos < 100) {
            nomeRanque.text = "Bronze";
            ranque.sprite = bronze;
        } else if (pontos >= 100 && pontos < 150) {
            nomeRanque.text = "Prata";
            ranque.sprite = prata;
        } else if (pontos >= 150 && pontos < 200) {
            nomeRanque.text = "Ouro";
            ranque.sprite = ouro;
        } else if (pontos >= 200 && pontos < 250) {
            nomeRanque.text = "Platina";
            ranque.sprite = platina;
        } else if (pontos >= 250 && pontos < 300) {
            nomeRanque.text = "Diamante";
            ranque.sprite = diamante;
        } else if (pontos >= 300) {
            nomeRanque.text = "Guerreiro";
            ranque.sprite = guerreiro;
        }
    }
}
