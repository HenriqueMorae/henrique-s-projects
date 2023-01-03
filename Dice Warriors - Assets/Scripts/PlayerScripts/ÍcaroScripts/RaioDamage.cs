using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaioDamage : MonoBehaviour
{
    public int raioNumero;

    // Start is called before the first frame update
    void Start()
    {
        if (raioNumero == 2)
            FindObjectOfType<AudioManager>().Play("raio3");
        else
            FindObjectOfType<AudioManager>().Play("raio2");
    }

    void OnTriggerEnter2D(Collider2D target)
    {       
        if (target.tag == "Player")
        {

        }
        else if (target.tag == "Enemy")
        {
            Enemy inimigo = target.GetComponent<Enemy>();
            
            if (inimigo != null) {
                inimigo.LevarDano(Random.Range(1,21)+EfeitosDown.danoExtra);
            }
        }
        else
        {

        }
    }
}
