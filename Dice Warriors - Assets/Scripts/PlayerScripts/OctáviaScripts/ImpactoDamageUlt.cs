using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactoDamageUlt : MonoBehaviour
{
    public Transform center;
    public float range = 1f;
    public LayerMask layers;

    public void Ataque () {
        //Detect enemies in range
        Collider2D[] hits = Physics2D.OverlapCircleAll(center.position, range, layers);

        //Damage
        foreach (Collider2D enemy in hits)
        {
            enemy.GetComponent<Enemy>().LevarDano(8+EfeitosDown.danoExtra);
        }
    }

    void OnDrawGizmosSelected () {
        if (center == null)
            return;

        Gizmos.DrawWireSphere(center.position, range);
    }
}
