using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cooldownTimer : MonoBehaviour
{
    int seg = 0;
    public Text num;

    public void Seconds (int s) {
        seg = s;
        StartCoroutine("Contagem");
    }

    IEnumerator Contagem() {
        for (int i = seg; i > 0; i--)
        {
            num.text = i.ToString();
            yield return new WaitForSeconds(1);
        }
        Destroy(gameObject);
    }
}
