using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbSpawn : MonoBehaviour
{
    public GameObject orbPrefab;
    public int numLocais;

    int localescolhido;

    public Transform p1;
    public Transform p2;
    public Transform p3;
    public Transform p4;
    public Transform p5;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnOrb", 10f, 30f);
    }

    void SpawnOrb()
    {
        localescolhido = Random.Range(1,numLocais);
        
        switch (localescolhido)
        {
            case 1: Instantiate(orbPrefab, p1.position, p1.rotation); break;
            case 2: Instantiate(orbPrefab, p2.position, p2.rotation); break;
            case 3: Instantiate(orbPrefab, p3.position, p3.rotation); break;
            case 4: Instantiate(orbPrefab, p4.position, p4.rotation); break;
            case 5: Instantiate(orbPrefab, p5.position, p5.rotation); break;
        }
    }
}
