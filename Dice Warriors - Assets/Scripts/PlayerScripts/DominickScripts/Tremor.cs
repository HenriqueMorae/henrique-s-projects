using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tremor : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D target)
    {       
        if (target.tag == "Player")
        {

        }
        else if (target.tag == "Enemy")
        {
            Enemy inimigo = target.GetComponent<Enemy>();
            
            if (inimigo != null) {
                inimigo.LevarDano(12+EfeitosDown.danoExtra);
            }
        }
        else
        {
            
        }
    }
}
