using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arma_D20 : MonoBehaviour
{
    public Animator animator;
    public Energy energia;
    public Ativador ativar;
    public PowerIcons icons;
    public Vida vida;

    public Transform attackPoint;
    public Transform raioPoint;
    public Transform raioUpPoint;
    public Transform raioDownPoint;
    public float attackRange = 1f;
    public LayerMask enemyLayer;
    public GameObject raioPrefab;
    public GameObject raio2Prefab;

    public D20Ult ultimate;

    public float attackRate = 1f;
    float nextAttackTime = 0f;
    int dano;
    bool ultimateLigada = false;

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

        icons.mudapowers(20);
        ativar.SetPowerPoints(20);
    }

    // Update is called once per frame
    void Update()
    {
        if(PauseMenu.GameIsPaused || vida.Morreu() || animando)
            return;

        if (Time.time >= nextAttackTime) {
            if (Input.GetKeyDown(keyFire) || Input.GetButtonDown("Attack"))
            {
                if (ultimateLigada) {
                    animator.Play("d20_attackult");
                } else if (animator.GetFloat("running") > 0)
                {
                    animator.Play("d20_attackrun");
                } else {
                    animator.Play("d20_attack");
                }
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }

        if (Input.GetKeyDown(keyPower1) || Input.GetButtonDown("Power1"))
        {
            if (energia.HowManyEnergy() >= 5 && !ativar.isOnCooldown(1))
            {
                animando = true;
                energia.UsingEnergy(5);
                ativar.Cooldown(1);

                if (animator.GetFloat("running") > 0)
                {
                    animator.Play("d20_raiorun");
                } else {
                    animator.Play("d20_raio");
                }

                FindObjectOfType<VoiceManager>().Power1();
            }
        }

        if (Input.GetKeyDown(keyPower2) || Input.GetButtonDown("Power2"))
        {
            if (energia.HowManyEnergy() >= 10 && !ativar.isOnCooldown(2))
            {
                animando = true;
                energia.UsingEnergy(10);
                ativar.Cooldown(2);

                if (animator.GetFloat("running") > 0)
                {
                    animator.Play("d20_raiorun2");
                } else {
                    animator.Play("d20_raio2");
                }

                FindObjectOfType<VoiceManager>().Power2();
            }
        }

        if (Input.GetKeyDown(keyPower3) || Input.GetButtonDown("Power3"))
        {
            if (energia.HowManyEnergy() >= 20 && !ativar.isOnCooldown(3) && !ativar.isUsando(3))
            {
                animando = true;
                energia.UsingEnergy(20);
                //ULT
                animator.Play("d20_ult");
                FindObjectOfType<VoiceManager>().Power3();
            }
        }
    }

    public void AcabouAnimation() {
        animando = false;
    }

    public void Furia() {
        if(vida.Morreu())
            return;

        StartCoroutine("FuriaEletrica");
    }

    IEnumerator FuriaEletrica() {
        ativar.UsandoPower(3, true);
        FindObjectOfType<AudioManager>().Play("raio");
        yield return new WaitForSeconds(0.1f);
        ultimate.IcaroBranco();
        yield return new WaitForSeconds(0.1f);
        ultimate.IcaroFuria();
        yield return new WaitForSeconds(5f);
        ultimate.IcaroBranco();
        yield return new WaitForSeconds(0.1f);
        ultimate.IcaroNormal();
        ativar.UsandoPower(3, false);
        ativar.Cooldown(3);
    }

    public void Atacar() {
        if(vida.Morreu())
            return;

        //Detect enemies in range
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        FindObjectOfType<AudioManager>().Play("espada_icaro");

        //Damage
        foreach (Collider2D enemy in hits)
        {
            dano = Random.Range(1, 21);
            enemy.GetComponent<Enemy>().LevarDano(dano+EfeitosDown.danoExtra);
        }
    }

    public void AtacarFuria() {
        if(vida.Morreu())
            return;

        //Detect enemies in range
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        FindObjectOfType<AudioManager>().Play("espada_icaro");

        //Damage
        foreach (Collider2D enemy in hits)
        {
            enemy.GetComponent<Enemy>().LevarDano(20+EfeitosDown.danoExtra);
        }
    }

    public void UltOn(bool ultimato) {
        ultimateLigada = ultimato;
    }

    public void NextAttack() {
        nextAttackTime = 0f;
    }

    public void Raio() {
        if(vida.Morreu())
            return;
        
        Instantiate(raioPrefab, raioPoint.position, raioPoint.rotation);
    }

    public void Raio2() {
        if(vida.Morreu())
            return;

        Instantiate(raio2Prefab, raioUpPoint.position, raioUpPoint.rotation);
        Instantiate(raio2Prefab, raioDownPoint.position, raioDownPoint.rotation);
    }

    void OnDrawGizmosSelected () {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
