using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ativador : MonoBehaviour
{
    public Sprite desativado;
    public Sprite ativado;
    public Sprite emCooldown;
    public Sprite usando;
    public Energy energia;
    public Image power1;
    public Image power2;
    public Image power3;
    public Text espaco;
    public Text textp1;
    public Text textp2;
    public Text textp3;

    public RectTransform local1;
    public RectTransform local2;
    public RectTransform local3;
    public GameObject cooldownTime;

    public GameObject playStation;

    int qtd;
    int p1 = 0;
    int p2 = 0;
    int ult = 0;

    bool p1on = false;
    bool p2on = false;
    bool p3on = false;

    bool p1cd = false;
    bool p2cd = false;
    bool p3cd = false;

    bool usandop1 = false;
    bool usandop2 = false;
    bool usandop3 = false;

    // Start is called before the first frame update
    void Start()
    {
        power1.sprite = desativado;
        power2.sprite = desativado;
        power3.sprite = desativado;

        int modo = PlayerPrefs.GetInt("modobotoes", 1);

        switch (modo)
        {
            case 1:
                espaco.text = "ESPAÇO";
                textp1.text = "C";
                textp2.text = "V";
                textp3.text = "B";
                break;
            case 2:
                espaco.text = "Q";
                textp1.text = "W";
                textp2.text = "E";
                textp3.text = "R";
                break;
            case 3:
                espaco.text = "ESPAÇO";
                textp1.text = "B";
                textp2.text = "N";
                textp3.text = "M";
                break;
            case 4:
                espaco.text = "U";
                textp1.text = "I";
                textp2.text = "O";
                textp3.text = "P";
                break;
            case 5:
                espaco.text = "ESPAÇO";
                textp1.text = "C";
                textp2.text = "V";
                textp3.text = "B";
                break;
            case 6:
                espaco.text = "Q";
                textp1.text = "W";
                textp2.text = "E";
                textp3.text = "R";
                break;
            case 7:
                espaco.text = " ";
                textp1.text = " ";
                textp2.text = " ";
                textp3.text = " ";
                playStation.SetActive(true);
                break;
            default:
                espaco.text = "X";
                textp1.text = "X";
                textp2.text = "X";
                textp3.text = "X";
                break;
        }
    }

    public void SetPowerPoints (int dado) {
        switch (dado)
        {
            case 4: p1 = 2; p2 = 3; ult = 4; break;
            case 6: p1 = 2; p2 = 4; ult = 6; break;
            case 8: p1 = 2; p2 = 4; ult = 8; break;
            case 12: p1 = 3; p2 = 6; ult = 12; break;
            case 20: p1 = 5; p2 = 10; ult = 20; break;
        }
    }

    public bool isOnCooldown (int power) {
        switch (power)
        {
            case 1: return p1cd;
            case 2: return p2cd;
            case 3: return p3cd;
            default: return false;
        }
    }

    public bool isUsando (int power) {
        switch (power)
        {
            case 1: return usandop1;
            case 2: return usandop2;
            case 3: return usandop3;
            default: return false;
        }
    }

    public void UsandoPower (int power, bool uso) {
        switch (power)
        {
            case 1: usandop1 = uso; break;
            case 2: usandop2 = uso; break;
            case 3: usandop3 = uso; break;
        }
    }

    public void Cooldown (int power) {
        GameObject timer;
        cooldownTimer cdt;
        switch (power)
        {
            case 1:
                p1cd = true;
                timer = Instantiate(cooldownTime, local1.transform);
                cdt = timer.GetComponent<cooldownTimer>();
                cdt.Seconds(p1);
                StartCoroutine("ContagemP1");
                break;
            case 2:
                p2cd = true;
                timer = Instantiate(cooldownTime, local2.transform);
                cdt = timer.GetComponent<cooldownTimer>();
                cdt.Seconds(p2);
                StartCoroutine("ContagemP2");
                break;
            case 3:
                p3cd = true;
                timer = Instantiate(cooldownTime, local3.transform);
                cdt = timer.GetComponent<cooldownTimer>();
                cdt.Seconds(ult);
                StartCoroutine("ContagemP3");
                break;
        }
    }

    IEnumerator ContagemP1() {
        yield return new WaitForSeconds(p1);
        p1cd = false;
    }
    
    IEnumerator ContagemP2() {
        yield return new WaitForSeconds(p2);
        p2cd = false;
    }
    
    IEnumerator ContagemP3() {
        yield return new WaitForSeconds(ult);
        p3cd = false;
    }

    public void ReiniciaTudo() {
        FindObjectOfType<Energy>().QuantidadeCerta(0);

        foreach (cooldownTimer cdTimer in FindObjectsOfType<cooldownTimer>())
        {
            Destroy(cdTimer.gameObject);
        }

        StopCoroutine("ContagemP1");
        StopCoroutine("ContagemP2");
        StopCoroutine("ContagemP3");
        p1cd = false;
        p2cd = false;
        p3cd = false;
        p1on = false;
        p2on = false;
        p3on = false;
        usandop1 = false;
        usandop2 = false;
        usandop3 = false;
        power1.sprite = desativado;
        power2.sprite = desativado;
        power3.sprite = desativado;
    }

    // Update is called once per frame
    void Update()
    {
        qtd = energia.HowManyEnergy();
        
        if (usandop1) {
            power1.sprite = usando;
        } else if (qtd >= p1 && !p1cd) {
            power1.sprite = ativado;
            
            if (!p1on) {
                FindObjectOfType<AudioManager>().Play("power1");
                p1on = true;
            }

        } else {

            if (p1cd) {
                power1.sprite = emCooldown;
            } else {
                power1.sprite = desativado;
            }
            p1on = false;
        }

        if (usandop2) {
            power2.sprite = usando;
        } else if (qtd >= p2 && !p2cd) {
            power2.sprite = ativado;
            
            if (!p2on) {
                FindObjectOfType<AudioManager>().Play("power2");
                p2on = true;
            }

        } else {
            
            if (p2cd) {
                power2.sprite = emCooldown;
            } else {
                power2.sprite = desativado;
            }
            p2on = false;
        }

        if (usandop3) {
            power3.sprite = usando;
        } else if (qtd >= ult && !p3cd) {
            power3.sprite = ativado;
            
            if (!p3on) {
                FindObjectOfType<VoiceManager>().UltPronta();
                FindObjectOfType<AudioManager>().Play("power3");
                p3on = true;
            }

        } else {
            
            if (p3cd) {
                power3.sprite = emCooldown;
            } else {
                power3.sprite = desativado;
            }
            p3on = false;
        }
    }
}
