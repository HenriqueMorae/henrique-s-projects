using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{

    public Transform spawnpoint;

    public GameObject tessa;
    public GameObject calebe;
    public GameObject octavia;
    public GameObject dominick;
    public GameObject icaro;

    void Awake()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        int player = PlayerPrefs.GetInt("personagem", 0);

        switch (player)
        {
            case 0: break;
            case 4: Instantiate(tessa, spawnpoint.position, spawnpoint.rotation); break;
            case 6: Instantiate(calebe, spawnpoint.position, spawnpoint.rotation); break;
            case 8: Instantiate(octavia, spawnpoint.position, spawnpoint.rotation); break;
            case 12: Instantiate(dominick, spawnpoint.position, spawnpoint.rotation); break;
            case 20: Instantiate(icaro, spawnpoint.position, spawnpoint.rotation); break;
        }
    }
}
