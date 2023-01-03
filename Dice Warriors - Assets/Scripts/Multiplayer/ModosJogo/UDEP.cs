using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UDEP : MonoBehaviour
{
    GameObject energyBar;
    Energy barraDeEnergia;
    GameObject healthBar;
    Health barraDeVida;

    void Start()
    {
        energyBar = GameObject.FindWithTag("EnergyBar");
        barraDeEnergia = energyBar.GetComponent<Energy>();
        healthBar = GameObject.FindWithTag("HealthBar");
        barraDeVida = healthBar.GetComponent<Health>();
    }

    public void Energizando() {
        barraDeEnergia.Energia(10);
        StartCoroutine("EnergiaPraTodos");
    }

    IEnumerator EnergiaPraTodos() {
        yield return new WaitForSeconds(3f);
        while (barraDeVida.vidaatual > 0) {
            barraDeEnergia.Energia(1);
            yield return new WaitForSeconds(3f);
        }
    }
}
