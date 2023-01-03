using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactoDamage : MonoBehaviour
{
    public Transform center;
    public float range = 1f;
    public LayerMask layers;

    int dano;

    public void Ataque () {
        //Detect enemies in range
        Collider2D[] hits = Physics2D.OverlapCircleAll(center.position, range, layers);

        //Damage
        foreach (Collider2D enemy in hits)
        {
            dano = Random.Range(1, 9);
            enemy.GetComponent<Enemy>().LevarDano(dano+EfeitosDown.danoExtra);
        }
    }

    void OnDrawGizmosSelected () {
        if (center == null)
            return;

        Gizmos.DrawWireSphere(center.position, range);
    }
}
