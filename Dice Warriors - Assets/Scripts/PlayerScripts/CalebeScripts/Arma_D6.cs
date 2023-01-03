using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arma_D6 : MonoBehaviour
{

    public Animator animator;
    public Energy energia;
    public Ativador ativar;
    public PowerIcons icons;
    public Vida vida;

    public Transform attackPoint;
    public Transform redemoinhoPoint;
    public Transform ultPoint;
    public float attackRange = 1f;
    public float redemoinhoRange = 1f;
    public LayerMask enemyLayer;

    public float attackRate = 1f;
    float nextAttackTime = 0f;
    int dano;

    public GameObject bolaDeFogoPrefab;
    public GameObject particulas;

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

        icons.mudapowers(6);
        ativar.SetPowerPoints(6);
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
                    animator.Play("d6_attackrun");
                } else {
                    animator.Play("d6_attack");
                }
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }

        if (Input.GetKeyDown(keyPower1) || Input.GetButtonDown("Power1"))
        {
            if (energia.HowManyEnergy() >= 2 && !ativar.isOnCooldown(1) && !ativar.isUsando(1))
            {
                animando = true;
                energia.UsingEnergy(2);
                animator.Play("d6_spin");
                ativar.UsandoPower(1, true);
                FindObjectOfType<VoiceManager>().Power1();
            }
        }

        if (Input.GetKeyDown(keyPower2) || Input.GetButtonDown("Power2"))
        {
            if (energia.HowManyEnergy() >= 4 && !ativar.isOnCooldown(2))
            {
                animando = true;
                energia.UsingEnergy(4);
                ativar.Cooldown(2);
                //Poder 2
                animator.Play("d6_triple");
                FindObjectOfType<VoiceManager>().Power2();
            }
        }

        if (Input.GetKeyDown(keyPower3) || Input.GetButtonDown("Power3"))
        {
            if (energia.HowManyEnergy() >= 6 && !ativar.isOnCooldown(3) && !ativar.isUsando(3))
            {
                animando = true;
                energia.UsingEnergy(6);
                //ULT
                animator.Play("d6_ult");
                ativar.UsandoPower(3, true);
                FindObjectOfType<VoiceManager>().Power3();
            }
        }
    }

    public void AcabouAnimation() {
        animando = false;
    }

    public void AcabouRedemoinho() {
        ativar.UsandoPower(1, false);
        ativar.Cooldown(1);
    }

    public void AcabouUlt() {
        ativar.UsandoPower(3, false);
        ativar.Cooldown(3);
    }

    public void Atacar() {
        if(vida.Morreu())
            return;

        //Detect enemies in range
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        FindObjectOfType<AudioManager>().Play("espada_calebe");

        //Damage
        foreach (Collider2D enemy in hits)
        {
            dano = Random.Range(1, 7);
            enemy.GetComponent<Enemy>().LevarDano(dano+EfeitosDown.danoExtra);
        }
    }

    public void Redemoinho() {
        if(vida.Morreu())
            return;

        //Detect enemies in range
        Collider2D[] hits = Physics2D.OverlapCircleAll(redemoinhoPoint.position, redemoinhoRange, enemyLayer);

        //Damage
        foreach (Collider2D enemy in hits)
        {
            dano = Random.Range(1, 7);
            enemy.GetComponent<Enemy>().LevarDano(dano+EfeitosDown.danoExtra);
        }
    }

    public void RedemoinhoSounds() {
        if(vida.Morreu())
            return;

        StartCoroutine("Foguinho");
        FindObjectOfType<AudioManager>().Play("espada_calebe");
        FindObjectOfType<AudioManager>().Play("redemoinho");
    }

    IEnumerator Foguinho() {
        particulas.SetActive(true);
        yield return new WaitForSeconds(1.1f);
        particulas.SetActive(false);
    }

    public void BolaDeFogo() {
        if(vida.Morreu())
            return;
            
        FindObjectOfType<AudioManager>().Play("espada_calebe");
        Instantiate(bolaDeFogoPrefab, ultPoint.position, ultPoint.rotation);
    }

    void OnDrawGizmosSelected () {
        if (attackPoint == null)
            return;
        
        if (redemoinhoPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        Gizmos.DrawWireSphere(redemoinhoPoint.position, redemoinhoRange);
    }
}
