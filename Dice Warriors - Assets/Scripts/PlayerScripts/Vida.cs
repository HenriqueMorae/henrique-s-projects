using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Vida : MonoBehaviour
{
    public bool multiplayer = false;

    public int vida = 20;
    public Animator animator;
    public Health healthBar;
    public Text vidatexto;
    public Animator damage;
    public Animator efeitoescudo;
    public Text textoescudo;
    public string deathAnim;
    public SpriteRenderer cara;
    public Sprite dor;
    public Sprite normal;
    public Sprite morri;
    
    PlayerMovement movimento;
    bool morreu;
    bool escudo;
    int qtdEscudo;
    int estimulos;

    GameObject barradevida;
    GameObject textodevida;
    GameObject efeitodano;
    GameObject escudonatela;
    GameObject textodoescudonatela;
    AudioSource passos;

    GameObject carregandoDeath;

    PhotonView view;

    void Start()
    {
        view = GetComponent<PhotonView>();

        if (view != null) {
            if (!view.IsMine) {
                return;
            }
        }

        barradevida = GameObject.FindWithTag("HealthBar");
        healthBar = barradevida.GetComponent<Health>();
        textodevida = GameObject.FindWithTag("TextVida");
        vidatexto = textodevida.GetComponent<Text>();
        efeitodano = GameObject.FindWithTag("Damage");
        damage = efeitodano.GetComponent<Animator>();
        escudonatela = GameObject.FindWithTag("Shield");
        efeitoescudo = escudonatela.GetComponent<Animator>();
        textodoescudonatela = GameObject.FindWithTag("TextEscudo");
        textoescudo = textodoescudonatela.GetComponent<Text>();

        carregandoDeath = GameObject.FindWithTag("Canvas").GetComponent<botoes>().CarregandoScreenDeath();

        morreu = false;
        escudo = false;
        qtdEscudo = 0;
        estimulos = 0;
        healthBar.SetMaxHealth(vida);
        vidatexto.text = vida.ToString() + "/20";
    }

    public void SetEscudo (int shield) {
        escudo = true;
        estimulos++;
        qtdEscudo = qtdEscudo + shield;
        textoescudo.text = qtdEscudo.ToString();
        efeitoescudo.Play("escudo_on");
    }

    public void TerminarEscudo() {
        if (estimulos == 1) {
            escudo = false;
            qtdEscudo = 0;
            textoescudo.text = "0";
            efeitoescudo.Play("escudo_off");
        }
        estimulos--;
    }

    public bool isEscudoOn() {
        return escudo;
    }

    public void LevarDano (int dano) {
        if (morreu == false) {

            if (escudo) {
                damage.SetTrigger("dano_shield");
                qtdEscudo -= dano;

                if (qtdEscudo > 0) {
                    textoescudo.text = qtdEscudo.ToString();
                } else if (qtdEscudo == 0) {
                    escudo = false;
                    qtdEscudo = 0;
                    estimulos = 0;
                    textoescudo.text = "0";
                    efeitoescudo.Play("escudo_off");
                } else if (qtdEscudo < 0) {
                    escudo = false;
                    textoescudo.text = "0";
                    efeitoescudo.Play("escudo_off");
                    LevarDano(-1*qtdEscudo);
                    qtdEscudo = 0;
                    estimulos = 0;
                }

            } else {
                FindObjectOfType<VoiceManager>().Dano();
                damage.SetTrigger("dano");
                vida -= dano;

                StartCoroutine("SentindoDor");
                if (multiplayer)
                    view.RPC("AiDoeu", RpcTarget.Others);

                vidatexto.text = vida.ToString() + "/20";
                healthBar.SetHealth(vida);
            }
        
        }

        if (vida <= 0 && morreu == false) {
            Morte();
        }
    }

    [PunRPC]
    void AiDoeu() {
        StartCoroutine("SentindoDor");
    }

    public void LevarDanoMultiplayer (int danomulti) {
        view.RPC("DanoNesseJogador", RpcTarget.All, danomulti);
    }

    [PunRPC]
    void DanoNesseJogador (int quantidade) {
        if (view.IsMine) {
            LevarDano(quantidade);
        }
    }

    public void Cura (int heal) {
        if (morreu)
            return;

        damage.SetTrigger("cura");
        vida += heal;

        if (vida > 20)
            vida = 20;

        FindObjectOfType<VoiceManager>().Cura();
        vidatexto.text = vida.ToString() + "/20";
        healthBar.SetHealth(vida);
    }

    IEnumerator SentindoDor() {
        cara.sprite = dor;
        yield return new WaitForSeconds(0.25f);
        cara.sprite = normal;
    }

    void Morte () {
        StopCoroutine("SentindoDor");
        morreu = true;
        cara.sprite = morri;
        passos = GetComponent<AudioSource>();
        passos.Stop();
        
        if (multiplayer)
            view.RPC("AiMorri", RpcTarget.All);
        
        FindObjectOfType<VoiceManager>().ParaFala();
        vidatexto.text = "0/20";
        movimento = gameObject.GetComponent<PlayerMovement>();
        movimento.enabled = false;
        //TiraArma();
        transform.Rotate(0f, 0f, 80f);
        
        animator.Play(deathAnim);
        //animator.SetTrigger("morreu");

        if (!multiplayer)
            StartCoroutine("PeraUmPoco");
        //Destroy(gameObject);
    }

    [PunRPC]
    void AiMorri() {
        StopCoroutine("SentindoDor");
        morreu = true;
        cara.sprite = morri;
        passos = GetComponent<AudioSource>();
        passos.Stop();
    }

    IEnumerator PeraUmPoco () {
        yield return new WaitForSeconds(2f);
        carregandoDeath.SetActive(true);
        Loader.Load(Loader.Scene.GameOver);
    }

    public bool Morreu() {
        return morreu;
    }

    void TiraArma() {
        int dado = PlayerPrefs.GetInt("personagem");

        switch (dado)
        {
            case 4: gameObject.GetComponent<Arma>().enabled = false; break;
            case 6: gameObject.GetComponent<Arma_D6>().enabled = false; break;
            case 8: gameObject.GetComponent<Arma_D8>().enabled = false; break;
            case 12: gameObject.GetComponent<Arma_D12>().enabled = false; break;
            case 20: gameObject.GetComponent<Arma_D20>().enabled = false; break;
        }
    }
}
