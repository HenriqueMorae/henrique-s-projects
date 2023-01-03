using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowRanking : MonoBehaviour
{
    public Sprite nada;
    public Sprite d4;
    public Sprite d6;
    public Sprite d8;
    public Sprite d12;
    public Sprite d20;

    public Text primeiroNome;
    public Text primeiroScore;
    public Text primeiroArena;
    public Image primeiroImage;
    public Text segundoNome;
    public Text segundoScore;
    public Text segundoArena;
    public Image segundoImage;
    public Text terceiroNome;
    public Text terceiroScore;
    public Text terceiroArena;
    public Image terceiroImage;
    public Text quartoNome;
    public Text quartoScore;
    public Text quartoArena;
    public Image quartoImage;
    public Text quintoNome;
    public Text quintoScore;
    public Text quintoArena;
    public Image quintoImage;

    public Sprite bronze;    
    public Sprite prata;
    public Sprite ouro;
    public Sprite platina;
    public Sprite diamante;
    public Sprite guerreiro;

    public Image primeiroRank;
    public Image segundoRank;
    public Image terceiroRank;
    public Image quartoRank;
    public Image quintoRank;

    public void ShowPlacares (Dictionary<int, ScoreRanking> scoresInfo, bool isRanqueado) {

        if (scoresInfo.ContainsKey(1)) {
            primeiroNome.text = scoresInfo[1].username;
            primeiroScore.text = scoresInfo[1].pontos;
            primeiroArena.text = Arena(scoresInfo[1].arena);
            primeiroImage.sprite = ImagemdoPersonagem(scoresInfo[1].personagem);

            if (isRanqueado) {
                if (int.Parse(scoresInfo[1].pontos) >= 50)
                    primeiroRank.color = Color.white;

                primeiroRank.sprite = Rank(int.Parse(scoresInfo[1].pontos));
            }
        }

        if (scoresInfo.ContainsKey(2)) {
            segundoNome.text = scoresInfo[2].username;
            segundoScore.text = scoresInfo[2].pontos;
            segundoArena.text = Arena(scoresInfo[2].arena);
            segundoImage.sprite = ImagemdoPersonagem(scoresInfo[2].personagem);

            if (isRanqueado) {
                if (int.Parse(scoresInfo[2].pontos) >= 50)
                    segundoRank.color = Color.white;

                segundoRank.sprite = Rank(int.Parse(scoresInfo[2].pontos));
            }
        }

        if (scoresInfo.ContainsKey(3)) {
            terceiroNome.text = scoresInfo[3].username;
            terceiroScore.text = scoresInfo[3].pontos;
            terceiroArena.text = Arena(scoresInfo[3].arena);
            terceiroImage.sprite = ImagemdoPersonagem(scoresInfo[3].personagem);

            if (isRanqueado) {
                if (int.Parse(scoresInfo[3].pontos) >= 50)
                    terceiroRank.color = Color.white;

                terceiroRank.sprite = Rank(int.Parse(scoresInfo[3].pontos));
            }
        }

        if (scoresInfo.ContainsKey(4)) {
            quartoNome.text = scoresInfo[4].username;
            quartoScore.text = scoresInfo[4].pontos;
            quartoArena.text = Arena(scoresInfo[4].arena);
            quartoImage.sprite = ImagemdoPersonagem(scoresInfo[4].personagem);

            if (isRanqueado) {
                if (int.Parse(scoresInfo[4].pontos) >= 50)
                    quartoRank.color = Color.white;

                quartoRank.sprite = Rank(int.Parse(scoresInfo[4].pontos));
            }
        }

        if (scoresInfo.ContainsKey(5)) {
            quintoNome.text = scoresInfo[5].username;
            quintoScore.text = scoresInfo[5].pontos;
            quintoArena.text = Arena(scoresInfo[5].arena);
            quintoImage.sprite = ImagemdoPersonagem(scoresInfo[5].personagem);

            if (isRanqueado) {
                if (int.Parse(scoresInfo[5].pontos) >= 50)
                    quintoRank.color = Color.white;

                quintoRank.sprite = Rank(int.Parse(scoresInfo[5].pontos));
            }
        }

    }

    Sprite ImagemdoPersonagem (string nome) {
        switch (nome)
        {
            case "tessa": return d4;            
            case "calebe": return d6;
            case "octavia": return d8;
            case "dominick": return d12;
            case "icaro": return d20;
            default: return nada;
        }
    }

    string Arena (string nome) {
        switch (nome)
        {
            case "tradicional": return "Arena Tradicional";            
            case "cercas": return "Arena Coliseu";
            case "caixas": return "Arena Floresta";
            default: return "";
        }
    }

    Sprite Rank(int pontos) {
        if (pontos < 50) {
            return null;
        } else if (pontos >= 50 && pontos < 100) {
            return bronze;
        } else if (pontos >= 100 && pontos < 150) {
            return prata;
        } else if (pontos >= 150 && pontos < 200) {
            return ouro;
        } else if (pontos >= 200 && pontos < 250) {
            return platina;
        } else if (pontos >= 250 && pontos < 300) {
            return diamante;
        } else if (pontos >= 300) {
            return guerreiro;
        } else {
            return null;
        }
    }
}
