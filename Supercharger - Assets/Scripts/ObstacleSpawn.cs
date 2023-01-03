using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawn : MonoBehaviour
{
    public GameObject[] obstaculos;
    public Transform[] locais;
    float spawnDelay;
    Coroutine obs;

    // Start is called before the first frame update
    void Start()
    {
        switch (FindObjectOfType<GameManager>().Dificuldade())
        {
            case "f": spawnDelay = 1.25f; break;
            case "m": spawnDelay = 1f; break;
            case "d": spawnDelay = 0.75f; break;
            case "p": spawnDelay = 0.5f; break;
            default: spawnDelay = 1f; break;
        }

        obs = StartCoroutine(Spawn());
    }

    IEnumerator Spawn() {
        while (true)
        {
            Instantiate(obstaculos[Random.Range(0, obstaculos.Length)], locais[Random.Range(0, locais.Length)].position, Quaternion.identity);
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    public void Parar() {
        StopCoroutine(obs);
    }
}
