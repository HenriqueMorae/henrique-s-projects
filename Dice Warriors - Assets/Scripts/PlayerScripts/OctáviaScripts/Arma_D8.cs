using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arma_D8 : MonoBehaviour
{
    public Animator animator;
    public Energy energia;
    public Ativador ativar;
    public PowerIcons icons;
    public Vida vida;

    public Transform attackPoint;
    public Transform bombPoint;
    public GameObject impactoPrefab;
    public GameObject impactoUltPrefab;
    public GameObject bombaPrefab;

    public float attackRate = 1f;
    float nextAttackTime = 0f;
    int dano;

    GameObject energyBar;
    GameObject poderes1;
    GameObject poderes2;

    KeyCode keyFire;
    KeyCode keyPower1;
    KeyCode keyPower2;
    KeyCode keyPower3;
    bool animando = false;

    // Start is called before the first frame update
    void Start()
    {
        switch (PlayerPrefs.GetInt("modobotoes", 1))
        {
            case 1: keyFire = KeyCode.Space; keyPower1 = KeyCode.C; keyPower2 = KeyCode.V; keyPower3 = KeyCode.B; break;
            case 2: keyFire = KeyCode.Q; keyPower1 = KeyCode.W; keyPower2 = KeyCode.E; keyPower3 = KeyCode.R; break;
            case 3: keyFire = KeyCode.Space; keyPower1 = KeyCode.B; keyPower2 = KeyCode.N; keyPower3 = KeyCode.M; break;
            case 4: keyFire = KeyCode.U; keyPower1 = KeyCode.I; keyPower2 = KeyCode.O; keyPower3 = KeyCode.P; break;
            case 5: keyFire = KeyCode.Space; keyPower1 = KeyCode.C; keyPower2 = KeyCode.V; keyPower3 = KeyCode.B; break;
            case 6: keyFire = KeyCode.Q; keyPower1 = KeyCode.W; keyPower2 = KeyCode.E; keyPower3 = KeyCode.R; break;
            default: keyFire = KeyCode.Space; keyPower1 = KeyCode.C; keyPower2 = KeyCode.V; keyPower3 = KeyCode.B; break;
        }

        energyBar = GameObject.FindWithTag("EnergyBar");
        energia = energyBar.GetComponent<Energy>();
        poderes1 = GameObject.FindWithTag("Ativador");
        ativar = poderes1.GetComponent<Ativador>();
        poderes2 = GameObject.FindWithTag("PowerIcons");
        icons = poderes2.GetComponent<PowerIcons>();

        icons.mudapowers(8);
        ativar.SetPowerPoints(8);
    }

    // Update is called once per frame
    void Update()
    {
        if(PauseMenu.GameIsPaused || vida.Morreu() || animando)
            return;

        if (Time.time >= nextAttackTime) {
            if (Input.GetKeyDown(keyFire) || Input.GetButtonDown("Attack"))
            {
                animator.Play("d8_attack");
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }

        if (Input.GetKeyDown(keyPower1) || Input.GetButtonDown("Power1"))
        {
            if (energia.HowManyEnergy() >= 2 && !ativar.isOnCooldown(1) && !ativar.isUsando(1))
            {
                StartCoroutine("AtivarEscudos");
                StartCoroutine("DepoisEscudo");
            }
        }

        if (ativar.isUsando(1) && !vida.isEscudoOn()) {
            StopCoroutine("AtivarEscudos");
            ativar.UsandoPower(1, false);
            ativar.Cooldown(1);
        }

        if (Input.GetKeyDown(keyPower2) || Input.GetButtonDown("Power2"))
        {
            if (energia.HowManyEnergy() >= 4 && !ativar.isOnCooldown(2) && !ativar.isUsando(2))
            {
                energia.UsingEnergy(4);
                ativar.UsandoPower(2, true);
                //Poder 2
                Instantiate(bombaPrefab, bombPoint.position, bombPoint.rotation);
                StartCoroutine("DepoisBomba");
            }
        }

        if (Input.GetKeyDown(keyPower3) || Input.GetButtonDown("Power3"))
        {
            if (energia.HowManyEnergy() >= 8 && !ativar.isOnCooldown(3) && !ativar.isUsando(3))
            {
                animando = true;
                energia.UsingEnergy(8);
                animator.Play("d8_ult");
                ativar.UsandoPower(3, true);
                FindObjectOfType<VoiceManager>().Power3();
            }
        }
    }

    public void AcabouAnimation() {
        animando = false;
    }

    public void AcabouUlt() {
        ativar.UsandoPower(3, false);
        ativar.Cooldown(3);
    }

    IEnumerator DepoisEscudo() {
        yield return new WaitForSeconds(1f);
        FindObjectOfType<VoiceManager>().Power1();
    }

    IEnumerator DepoisBomba() {
        yield return new WaitForSeconds(3f);
        ativar.UsandoPower(2, false);
        ativar.Cooldown(2);
        yield return new WaitForSeconds(1f);
        FindObjectOfType<VoiceManager>().Power2();
    }

    IEnumerator AtivarEscudos() {            
        energia.UsingEnergy(2);
        ativar.UsandoPower(1, true);
        vida.SetEscudo(Random.Range(1, 9));
        yield return new WaitForSeconds(5f);
        vida.TerminarEscudo();
        ativar.UsandoPower(1, false);
        ativar.Cooldown(1);
    }

    public void Atacar() {
        if(vida.Morreu())
            return;

        Instantiate(impactoPrefab, attackPoint.position, attackPoint.rotation);
    }

    public void AtacarUlt() {
        if(vida.Morreu())
            return;

        Instantiate(impactoUltPrefab, attackPoint.position, attackPoint.rotation);
    }

    public void AtacarUltSom() {
        if(vida.Morreu())
            return;

        FindObjectOfType<AudioManager>().Play("Impacto_ult");
    }
}
