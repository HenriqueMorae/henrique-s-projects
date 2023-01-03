using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSounds : MonoBehaviour
{
    [SerializeField] string somInicial;

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().StopAll();
        FindObjectOfType<AudioManager>().Play(somInicial);
    }
}
