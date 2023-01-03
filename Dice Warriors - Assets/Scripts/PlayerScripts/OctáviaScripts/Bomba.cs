using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomba : MonoBehaviour
{
    public GameObject explosao;
    public Transform center;

    public void Boom() {
        Instantiate(explosao, center.position, center.rotation);
    }
}
