using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public event Action<float> OnPlayerHealthChanged;

    [SerializeField] SpriteRenderer sr;
    [SerializeField] int hitPoints;
    [SerializeField] GameObject escudo;
    [SerializeField] PlayerInput pi;

    Color corOriginal;
    Color corOriginalEscudo;
    int escudoHitPoints;
    bool escudoLigado;
    bool morreu;
    int startingHp;

    public bool IsDead => morreu;

    public float CurrentPercent => (float)hitPoints / startingHp;

    void Start() {
        startingHp = hitPoints;
        corOriginal = sr.color;
        morreu = false;
        escudoLigado = false;
        if (escudo != null) corOriginalEscudo = escudo.GetComponent<SpriteRenderer>().color;
    }

    public void Dano(int damage) {
        if (morreu) return;

        if (escudoLigado) {
            StartCoroutine(HitEscudo());
            FindObjectOfType<AudioManager>().Play("char_turtle_def");
            escudoHitPoints -= damage;

            if (escudoHitPoints <= 0) {
                EscudoOff();
            }

        } else {
            StartCoroutine(Hit());
            hitPoints -= damage;
            OnPlayerHealthChanged?.Invoke(CurrentPercent);

            if (hitPoints <= 0) {
                StopAllCoroutines();
                sr.color = Color.red;
                morreu = true;
                pi.enabled = false;
                SceneManager.LoadScene("GameScene");
            }
        }
    }

    IEnumerator HitEscudo() {
        escudo.GetComponent<SpriteRenderer>().color = Color.blue;
        yield return new WaitForSeconds(0.25f);
        escudo.GetComponent<SpriteRenderer>().color = corOriginalEscudo;
    }

    IEnumerator Hit() {
        sr.color = Color.red;
        yield return new WaitForSeconds(0.25f);
        sr.color = corOriginal;
    }

    public void EscudoOn(int vidaDoEscudo) {
        escudo.GetComponent<SpriteRenderer>().color = corOriginalEscudo;
        escudoHitPoints = vidaDoEscudo;
        escudoLigado = true;
        escudo.SetActive(true);
    }

    public void EscudoOff() {
        escudoLigado = false;
        escudo.SetActive(false);
    }
}
