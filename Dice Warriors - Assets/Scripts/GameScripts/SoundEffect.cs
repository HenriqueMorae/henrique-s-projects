using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    public int efeito;

    // Start is called before the first frame update
    void Start()
    {
        switch (efeito)
        {
            case 1: FindObjectOfType<AudioManager>().Play("RankUp"); break;
        }
    }
}
