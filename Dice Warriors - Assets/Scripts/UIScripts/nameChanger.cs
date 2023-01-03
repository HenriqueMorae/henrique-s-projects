using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class nameChanger : MonoBehaviour
{
    public Text nome;
    public Text descricao;
    
    public Image d4b;
    public Image d6b;
    public Image d8b;
    public Image d12b;
    public Image d20b;

    public Sprite d4;
    public Sprite d4atv;
    public Sprite d6;
    public Sprite d6atv;
    public Sprite d8;
    public Sprite d8atv;
    public Sprite d12;
    public Sprite d12atv;
    public Sprite d20;
    public Sprite d20atv;

    // Start is called before the first frame update
    void Start()
    {
        nome.text = "";
        descricao.text = "";
    }

    public void Tessa() {
        PlayerPrefs.SetInt("personagem", 4);

        nome.text = "Tessa";
        d4b.sprite = d4atv;
        d6b.sprite = d6;
        d8b.sprite = d8;
        d12b.sprite = d12;
        d20b.sprite = d20;
        descricao.text = "Tessa é uma arqueira ágil que atira flechas com muita destreza. Sua precisão e habilidade lhe permitem atirar várias flechas ao mesmo tempo e para direções diferentes, fazendo com que seus inimigos estejam sempre à procura de proteção quando ela está na arena.";
    }

    public void Calebe() {
        PlayerPrefs.SetInt("personagem", 6);

        nome.text = "Calebe";
        d4b.sprite = d4;
        d6b.sprite = d6atv;
        d8b.sprite = d8;
        d12b.sprite = d12;
        d20b.sprite = d20;
        descricao.text = "A personalidade descolada e frenética de Calebe reflete exatamente seu estilo de luta na arena. Com seus golpes rápidos e constantes, poucos serão capazes de sair ilesos após chegar perto dele. E, se forem fugir, cuidado! Calebe sempre tem um truque quente na manga...";
    }

    public void Octavia() {
        PlayerPrefs.SetInt("personagem", 8);

        nome.text = "Octávia";
        d4b.sprite = d4;
        d6b.sprite = d6;
        d8b.sprite = d8atv;
        d12b.sprite = d12;
        d20b.sprite = d20;
        descricao.text = "Muitos podem ver Octávia como uma ameaça pequena ao vê-la apenas com seu escudo. Porém, não se engane. A mente por trás daquele escudo tecnológico conta com esse pensamento para surpreender os inimigos com abalos e bombas que os deixarão de cabelo em pé.";
    }

    public void Dominick() {
        PlayerPrefs.SetInt("personagem", 12);

        nome.text = "Dominick";
        d4b.sprite = d4;
        d6b.sprite = d6;
        d8b.sprite = d8;
        d12b.sprite = d12atv;
        d20b.sprite = d20;
        descricao.text = "Com sua espada e seu escudo, Dominick está sempre pronto para ser uma fortaleza na arena. Com seus golpes perfurantes e empurrões, não há quem consiga realizar uma investida simples para cima dele. Principalmente se ele ficar com raiva e estremecer a arena toda...";
    }

    public void Icaro() {
        PlayerPrefs.SetInt("personagem", 20);

        nome.text = "Ícaro";
        d4b.sprite = d4;
        d6b.sprite = d6;
        d8b.sprite = d8;
        d12b.sprite = d12;
        d20b.sprite = d20atv;
        descricao.text = "Ícaro é o mais pesado e lento dos guerreiros, mas seu ataque é de longe o mais letal. Sua espada, a Criadora de Tempestades, lhe dá poderes elétricos que podem torrar qualquer um em seu caminho. Além disso, ele pode se abastecer de uma fúria que o torna imbatível.";
    }
}
