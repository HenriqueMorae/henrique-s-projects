using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Arma_D12 : MonoBehaviour
{
    public Animator animator;
    public Energy energia;
    public Ativador ativar;
    public PowerIcons icons;
    public Vida vida;
    public PlayerMovement movimento;

    public Transform attackPoint;
    public Transform pushPoint;
    public float attackRange = 1f;
    public float pushRange = 1f;
    public float pushDamageRange = 1f;
    public LayerMask enemyLayer;
    public GameObject tremorPrefab;

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

        icons.mudapowers(12);
        ativar.SetPowerPoints(12);
    }

    // Update is called once per frame
    void Update()
    {
        if(PauseMenu.GameIsPaused || vida.Morreu() || animando)
            return;

        if (Time.time >= nextAttackTime) {
            if (Input.GetKeyDown(keyFire) || Input.GetButtonDown("Attack"))
            {
                if (animator.GetFloat("running") > 0)
                {
                    animator.Play("d12_attackrun");
                } else {
                    animator.Play("d12_attack");
                }
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }

        if (Input.GetKeyDown(keyPower1) || Input.GetButtonDown("Power1"))
        {
            if (energia.HowManyEnergy() >= 3 && !ativar.isOnCooldown(1))
            {
                animando = true;
                energia.UsingEnergy(3);
                ativar.Cooldown(1);

                if (animator.GetFloat("running") > 0)
                {
                    animator.Play("d12_pushrun");
                } else {
                    animator.Play("d12_push");
                }

                FindObjectOfType<VoiceManager>().Power1();
            }
        }

        if (Input.GetKeyDown(keyPower2) || Input.GetButtonDown("Power2"))
        {
            if (energia.HowManyEnergy() >= 6 && !ativar.isOnCooldown(2) && !ativar.isUsando(2))
            {
                StartCoroutine("AtivarEscudos");
            }
        }

        if (ativar.isUsando(2) && !vida.isEscudoOn()) {
            StopCoroutine("AtivarEscudos");
            ativar.UsandoPower(2, false);
            ativar.Cooldown(2);
        }

        if (Input.GetKeyDown(keyPower3) || Input.GetButtonDown("Power3"))
        {
            if (energia.HowManyEnergy() >= 12 && !ativar.isOnCooldown(3) && !ativar.isUsando(3))
            {
                animando = true;
                energia.UsingEnergy(12);
                animator.Play("d12_ult");
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

    public void Tremor() {
        if(vida.Morreu())
            return;
        
        Instantiate(tremorPrefab, pushPoint.position, pushPoint.rotation);
    }

    IEnumerator AtivarEscudos() {
        energia.UsingEnergy(6);
        ativar.UsandoPower(2, true);
        vida.SetEscudo(Random.Range(1, 13));
        FindObjectOfType<VoiceManager>().Power2();
        yield return new WaitForSeconds(5f);
        vida.TerminarEscudo();
        ativar.UsandoPower(2, false);
        ativar.Cooldown(2);
    }

    public void Atacar() {
        if(vida.Morreu())
            return;

        //Detect enemies in range
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        FindObjectOfType<AudioManager>().Play("espada_dominick");

        //Damage
        foreach (Collider2D enemy in hits)
        {
            dano = Random.Range(1, 13);
            enemy.GetComponent<Enemy>().LevarDano(dano+EfeitosDown.danoExtra);
        }
    }

    public void Push() {
        if(vida.Morreu())
            return;

        //Detect enemies in range
        Collider2D[] hits = Physics2D.OverlapCircleAll(pushPoint.position, pushDamageRange, enemyLayer);
        Collider2D[] pushhits = Physics2D.OverlapCircleAll(pushPoint.position, pushRange, enemyLayer);
        FindObjectOfType<AudioManager>().Play("shield_push");

        foreach (Collider2D enemy in pushhits)
        {
            StartCoroutine("Empurrao", enemy);
        }

        foreach (Collider2D enemy in hits)
        {
            dano = Random.Range(1, 13);
            enemy.GetComponent<Enemy>().LevarDano(dano+EfeitosDown.danoExtra);
        }
    }

    IEnumerator Empurrao(Collider2D inimigo) {
        inimigo.GetComponent<AIPath>().enabled = false;
        inimigo.GetComponent<Rigidbody2D>().AddForce(movimento.LadoCerto()*75, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.5f);
        if(inimigo != null)
            inimigo.GetComponent<AIPath>().enabled = true;
    }

    public void UltEmpurrao(Collider2D target) {
        StartCoroutine("EmpurraoTremor", target);
    }

    IEnumerator EmpurraoTremor(Collider2D inimigo) {
        inimigo.GetComponent<AIPath>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        if(inimigo != null)
            inimigo.GetComponent<AIPath>().enabled = true;
    }

    void OnDrawGizmosSelected () {
        if (attackPoint == null)
            return;

        if (pushPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        Gizmos.DrawWireSphere(pushPoint.position, pushRange);
        Gizmos.DrawWireSphere(pushPoint.position, pushDamageRange);
    }
}
