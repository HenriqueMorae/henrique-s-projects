using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftDown : MonoBehaviour
{
    public RectTransform local1;

    public GameObject subirRanque;
    public GameObject combo;
    public Animator torcida;

    int abates = 0;
    float endCombo = 0f;
    bool comboOn = false;
    GameObject rankAtual;
    GameObject comboAtual;
    Animator comboAnim;
    aumentaCombo comboNum;

    float time;
    int idDelete;
    int posicao;

    void Start() {
        abates = 0;
        endCombo = 0f;
        comboOn = false;
        posicao = 1;
    }

    public void Abate() {
        abates++;
        endCombo = Time.time + 1f;

        if (comboOn == true) {
            comboNum.Aumenta(abates);
        }
    }

    void Update() {

        if (abates >= 3 && comboOn == false) {
            Combo();
        }

        if (Time.time >= endCombo && comboOn == true) {
            FinishCombo();
        }

        if (Time.time >= endCombo && comboOn == false) {
            abates = 0;
        }
    }

    public void RemoveCoisa(int idEfeito, float tempo) {
        time = tempo+0.1f;
        idDelete = idEfeito;
        StartCoroutine("MoverDeVolta");
    }

    IEnumerator MoverDeVolta() {
        int idRemove = idDelete;
        yield return new WaitForSeconds(time);

        foreach (Transform child in transform)
        {
            if (child.GetComponent<idIdentifier>().GetID() < idRemove)
                child.Translate(0f, -1.5f, 0f);
        }

        if (transform.childCount == 0)
            posicao = 1;
    }

    public void RankUp(){

        foreach (Transform child in transform)
        {
            child.Translate(0f, 1.5f, 0f);
        }

        rankAtual = Instantiate(subirRanque, local1.transform);
        rankAtual.GetComponent<idIdentifier>().SetID(posicao);
        posicao++;
    }

    public void Combo(){
        comboOn = true;

        if (abates > PlayerPrefs.GetInt("melhorcombo", 0))
            PlayerPrefs.SetInt("melhorcombo", abates);

        foreach (Transform child in transform)
        {
            child.Translate(0f, 1.5f, 0f);
        }

        comboAtual = Instantiate(combo, local1.transform);
        comboAtual.GetComponent<idIdentifier>().SetID(posicao);
        posicao++;
        comboAnim = comboAtual.GetComponent<Animator>();
        comboNum = comboAtual.GetComponent<aumentaCombo>();
        comboNum.Aumenta(abates);
    }

    public void FinishCombo(){
        comboOn = false;

        if (abates > PlayerPrefs.GetInt("melhorcombo", 0))
            PlayerPrefs.SetInt("melhorcombo", abates);
        
        if (abates > 8)
            Alegria();

        abates = 0;
        comboAnim.Play("combo_end");
    }

    void Alegria() {
        torcida.SetTrigger("alegria");
        FindObjectOfType<AudioManager>().Play("torcida");
    }
}
