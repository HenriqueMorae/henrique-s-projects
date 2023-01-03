using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbManager : MonoBehaviour
{
    public Animator animator;

    public SpriteRenderer orbImagem;
    public SpriteRenderer orbFundo;

    public Sprite fundo1;
    public Sprite fundo2;
    public Sprite fundo3;
    public Sprite fundo4;
    public Sprite fundo5;
    public Sprite fundo6;
    public Sprite fundo7;
    public Sprite fundo8;

    public Sprite vida;
    public Sprite escudo;
    public Sprite energia;
    public Sprite velocidade;
    public Sprite maisUm;
    public Sprite maisDois;
    public Sprite maisTres;
    public Sprite ataque;    

    int tipo;
    bool pegou = false;

    // Start is called before the first frame update
    void Start()
    {
        tipo = Random.Range(1, 9);

        switch (tipo)
        {            
            case 1: orbFundo.sprite = fundo1; orbImagem.sprite = vida; break;
            case 2: orbFundo.sprite = fundo2; orbImagem.sprite = escudo; break;
            case 3: orbFundo.sprite = fundo3; orbImagem.sprite = energia; break;
            case 4: orbFundo.sprite = fundo4; orbImagem.sprite = velocidade; break;
            case 5: orbFundo.sprite = fundo5; orbImagem.sprite = maisUm; break;
            case 6: orbFundo.sprite = fundo6; orbImagem.sprite = maisDois; break;
            case 7: orbFundo.sprite = fundo7; orbImagem.sprite = maisTres; break;
            case 8: orbFundo.sprite = fundo8; orbImagem.sprite = ataque; break;
        }

        StartCoroutine("Tempo");
    }

    IEnumerator Tempo() {
        yield return new WaitForSeconds(10f);

        if (!pegou) {
            animator.SetBool("tempo", true);
            FindObjectOfType<AudioManager>().Play("OrbOff");
        }
    }

    void OnTriggerEnter2D(Collider2D target)
    {       
        if (target.tag != "Player" || pegou == true)
            return;
        
        pegou = true;
        StopCoroutine("Tempo");

        FindObjectOfType<EfeitosDown>().AddEfeito(tipo);

        animator.SetBool("pegou", true);
        FindObjectOfType<AudioManager>().Play("OrbPegou");
    }
}
