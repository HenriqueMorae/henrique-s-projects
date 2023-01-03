using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject inimigoprefab1;
    public GameObject inimigoprefab2;
    public GameObject inimigoprefab3;
    public GameObject inimigoprefab4;
    GameObject inimigoprefab;

    public Transform portaol1;
    public Transform portaol2;
    public Transform portaor1;
    public Transform portaor2;
    public Transform portaoup1;
    public Transform portaoup2;
    public Transform portaoupgg;

    public float spawnTime;
    public float spawnDelay;
    public int numPortoes;

    int portaoescolhido;
    int inimigo;

    void Awake() {
        string modo = PlayerPrefs.GetString("modo");

        if (modo == "zenmedio") {
            spawnTime = 4f;
            spawnDelay = 2f;
        } else if (modo == "zendificil") {
            spawnTime = 4f;
            spawnDelay = 1f;
        } else {
            spawnTime = 4f;
            spawnDelay = 3f;
        }
    }

    void Start()
    {
        InvokeRepeating("Spawn", spawnTime, spawnDelay);
    }

    public void NewSpawn (float newDelay) {
        CancelInvoke("Spawn");
        InvokeRepeating("Spawn", 0f, newDelay);
    }

    void Spawn ()
    {
        inimigo = Random.Range(1,8);
        switch (inimigo)
        {
            case 1: inimigoprefab = inimigoprefab1; break;
            case 2: inimigoprefab = inimigoprefab2; break;
            case 3: inimigoprefab = inimigoprefab3; break;
            case 4: inimigoprefab = inimigoprefab4; break;
            case 5: inimigoprefab = inimigoprefab1; break;
            case 6: inimigoprefab = inimigoprefab2; break;
            case 7: inimigoprefab = inimigoprefab3; break;
        }

        portaoescolhido = Random.Range(1,numPortoes);
        switch (portaoescolhido)
        {
            case 1: Instantiate(inimigoprefab, portaol1.position, portaol1.rotation); break;
            case 2: Instantiate(inimigoprefab, portaol2.position, portaol2.rotation); break;
            case 3: Instantiate(inimigoprefab, portaor1.position, portaor1.rotation); break;
            case 4: Instantiate(inimigoprefab, portaor2.position, portaor2.rotation); break;
            case 5: Instantiate(inimigoprefab, portaoup1.position, portaoup1.rotation); break;
            case 6: Instantiate(inimigoprefab, portaoup2.position, portaoup2.rotation); break;
            case 7: Instantiate(inimigoprefab, portaoupgg.position, portaoupgg.rotation); break;
        }
    }
}
