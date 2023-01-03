using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changeRankShield : MonoBehaviour
{
    public Image shield;

    public Sprite bronze;
    public Sprite prata;
    public Sprite ouro;
    public Sprite platina;
    public Sprite diamante;
    public Sprite guerreiro;

    int numeroAtual = 0;

    public void atualizarRank() {
        switch (numeroAtual)
        {
            case 0: numeroAtual++; shield.sprite = bronze; break;
            case 1: numeroAtual++; shield.sprite = prata; break;
            case 2: numeroAtual++; shield.sprite = ouro; break;
            case 3: numeroAtual++; shield.sprite = platina; break;
            case 4: numeroAtual++; shield.sprite = diamante; break;
            case 5: numeroAtual++; shield.sprite = guerreiro; break;
            case 6: break;
            default: break;
        }
    }
}
