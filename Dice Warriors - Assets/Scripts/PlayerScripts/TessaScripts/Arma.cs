using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Arma : MonoBehaviourPunCallbacks
{
    public bool multiplayer = false;

    public Transform firePoint;
    public GameObject flechaPrefab;
    public Animator animator;
    public Energy energia;
    public Ativador ativar;
    public PowerIcons icons;
    public Vida vida;

    public float attackRate = 1f;
    float nextAttackTime = 0f;

    public Transform firePoint2;
    public Transform firePoint3;
    public GameObject explosivoPrefab;

    public Transform firePointUp;
    public Transform firePointDown;
    public Transform firePointBack;
    public GameObject ultPrefab;

    GameObject energyBar;
    GameObject poderes1;
    GameObject poderes2;

    KeyCode keyFire;
    KeyCode keyPower1;
    KeyCode keyPower2;
    KeyCode keyPower3;  
    bool animando = false;

    PhotonView view;
    ExitGames.Client.Photon.Hashtable playerProperties = new ExitGames.Client.Photon.Hashtable();

    void Brota (string nomedotroco, Object prefab, Vector3 posicao, Quaternion rotacao) {
        if (multiplayer) {
            PhotonNetwork.Instantiate(nomedotroco, posicao, rotacao);
        } else {
            Instantiate(prefab, posicao, rotacao);
        }
    }

    void Start()
    {
        view = GetComponent<PhotonView>();

        if (view != null) {
            if (!view.IsMine) {
                return;
            }
        }

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

        icons.mudapowers(4);
        ativar.SetPowerPoints(4);
    }

    // Update is called once per frame
    void Update()
    {
        if (view != null) {
            if (!view.IsMine) {
                return;
            }
        }

        if(PauseMenu.GameIsPaused || vida.Morreu() || animando)
            return;

        if (Time.time >= nextAttackTime) {
            if (Input.GetKeyDown(keyFire) || Input.GetButtonDown("Attack"))
            {
                Atirar();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }

        if (Input.GetKeyDown(keyPower1) || Input.GetButtonDown("Power1"))
        {
            if (energia.HowManyEnergy() >= 2 && !ativar.isOnCooldown(1))
            {
                animando = true;
                energia.UsingEnergy(2);
                ativar.Cooldown(1);
                //Poder 1
                if (animator.GetFloat("running") > 0)
                {
                    animator.SetTrigger("shootrun_p1");
                } else {
                    animator.SetTrigger("shoot_p1");
                }
                Brota("FlechaMulti", flechaPrefab, firePoint.position, firePoint.rotation);
                Brota("FlechaMulti", flechaPrefab, firePoint2.position, firePoint2.rotation);
                Brota("FlechaMulti", flechaPrefab, firePoint3.position, firePoint3.rotation);
                FindObjectOfType<VoiceManager>().Power1();
            }
        }

        if (Input.GetKeyDown(keyPower2) || Input.GetButtonDown("Power2"))
        {
            if (energia.HowManyEnergy() >= 3 && !ativar.isOnCooldown(2))
            {
                animando = true;
                energia.UsingEnergy(3);
                ativar.Cooldown(2);
                if (animator.GetFloat("running") > 0)
                {
                    animator.SetTrigger("shootrun_p2");
                } else {
                    animator.SetTrigger("shoot_p2");
                }
                Brota("Flecha_ExplosivaMulti", explosivoPrefab, firePoint.position, firePoint.rotation);
                FindObjectOfType<VoiceManager>().Power2();
            }
        }

        if (Input.GetKeyDown(keyPower3) || Input.GetButtonDown("Power3"))
        {
            if (energia.HowManyEnergy() >= 4 && !ativar.isOnCooldown(3) && !ativar.isUsando(3))
            {
                animando = true;
                energia.UsingEnergy(4);
                ativar.UsandoPower(3, true);
                StartCoroutine("Ult");
                Som(2);

                if (multiplayer) {
                    playerProperties["ultTessa"] = true;
                    PhotonNetwork.SetPlayerCustomProperties(playerProperties);
                    StartCoroutine("Backtofalse");
                }

                FindObjectOfType<VoiceManager>().Power3();
            }
        }
    }

    IEnumerator Backtofalse() {
        yield return new WaitForSeconds(2f);
        playerProperties["ultTessa"] = false;
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
    }

    public override void OnPlayerPropertiesUpdate (Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps) {
        if (targetPlayer.CustomProperties.ContainsKey("ultTessa") && changedProps.ContainsKey("ultTessa")) {
            if ((bool)targetPlayer.CustomProperties["ultTessa"])
                Som(2);
        }
    }

    public void AcabouAnimation() {
        animando = false;
    }

    public void Som (int i) {
        switch (i)
        {
            case 1: FindObjectOfType<AudioManager>().Play("Flecha"); break;           
            case 2: FindObjectOfType<AudioManager>().Play("hit_ult"); break;
            case 3: FindObjectOfType<AudioManager>().Play("Flechatripla"); break;
            default: break;
        }
    }

    void Atirar()
    {
        if(vida.Morreu())
            return;

        // Shooting Logic
        if (animator.GetFloat("running") > 0)
        {
            animator.SetTrigger("shootrun");
        } else {
            animator.SetTrigger("shoot");
        }
        Brota("FlechaMulti", flechaPrefab, firePoint.position, firePoint.rotation);
    }

    IEnumerator Ult()
    {
        if(!vida.Morreu()) {

            for (int i=0; i<4; i++) {

                if(!vida.Morreu()) {
                    if (animator.GetFloat("running") > 0)
                    {
                        animator.SetTrigger("shootrunult");
                    } else {
                        animator.SetTrigger("shootult");
                    }
                    Brota("Flecha_ULTMulti", ultPrefab, firePoint.position, firePoint.rotation);
                    Brota("Flecha_ULTMulti", ultPrefab, firePointUp.position, firePointUp.rotation);
                    Brota("Flecha_ULTMulti", ultPrefab, firePointDown.position, firePointDown.rotation);
                    Brota("Flecha_ULTMulti", ultPrefab, firePointBack.position, firePointBack.rotation);
                    yield return new WaitForSeconds(0.5f);
                }
            }
            ativar.UsandoPower(3, false);
            ativar.Cooldown(3);
            animando = false;
        }
    }
}
