using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Energy : MonoBehaviour
{
    public Slider slider;
    public Text energytexto;

    public int energia = 0;
    public int maxenergia = 20;

    void Start() {
        energytexto.text = energia.ToString() + "/20";
        SetEnergy(energia);
        SetMaxEnergy(maxenergia);
    }

    public void Energia (int energy) {
        energia += energy;

        if (energia > 20)
            energia = 20;

        energytexto.text = energia.ToString() + "/20";
        SetEnergy(energia);
    }

    public void QuantidadeCerta (int qtd) {
        energia = qtd;

        if (energia > 20)
            energia = 20;

        energytexto.text = energia.ToString() + "/20";
        SetEnergy(energia);
    }

    public void SetMaxEnergy (int energy) {
        slider.maxValue = energy;
    }

    public void SetEnergy (int energy) {
        slider.value = energy;
    }

    public int HowManyEnergy () {
        return energia;
    }

    public void UsingEnergy (int energy) {
        energia -= energy;
        energytexto.text = energia.ToString() + "/20";
        SetEnergy(energia);
    }
}
