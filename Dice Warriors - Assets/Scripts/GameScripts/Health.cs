using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Slider slider;
    public int vidaatual = 0;

    public void SetMaxHealth (int health) {
        slider.maxValue = health;
        slider.value = health;
        vidaatual = health;
    }

    public void SetHealth (int health) {
        slider.value = health;
        vidaatual = health;
    }
}
