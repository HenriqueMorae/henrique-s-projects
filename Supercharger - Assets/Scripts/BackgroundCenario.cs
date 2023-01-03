using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundCenario : MonoBehaviour
{
    public GameObject chegada;
    public Transform localChegada;

    public GameObject linha;
    public Transform localLinha;

    public GameObject fundoLonge;
    public Transform localFundoLonge;

    public GameObject[] cenarios;
    public Transform[] locais;
    public float cenarioDelay;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Linha());
        StartCoroutine(FundoLonge());
        StartCoroutine(Cenario1());
        StartCoroutine(Cenario2());
    }

    public void BotaAChegada() {
        Instantiate(chegada, localChegada.position, Quaternion.identity);
    }

    public void ParaTudo() {
        StopAllCoroutines();
    }

    IEnumerator Linha() {
        while (true)
        {
            Instantiate(linha, localLinha.position, Quaternion.identity);
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator FundoLonge() {
        while (true)
        {
            Instantiate(fundoLonge, localFundoLonge.position, Quaternion.identity);
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator Cenario1() {
        while (true)
        {
            Instantiate(cenarios[Random.Range(0, cenarios.Length)], locais[0].position, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(cenarioDelay-1, cenarioDelay+1));
        }
    }

    IEnumerator Cenario2() {
        while (true)
        {
            Instantiate(cenarios[Random.Range(0, cenarios.Length)], locais[1].position, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(cenarioDelay-1, cenarioDelay+1));
        }
    }
}
