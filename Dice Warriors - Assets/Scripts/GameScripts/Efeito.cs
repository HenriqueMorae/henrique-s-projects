using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Efeito : MonoBehaviour
{
    public int segundos;
    public Animator animator;
    int id;

    // Start is called before the first frame update
    void Start()
    {
        switch (segundos)
        {
            case 3: animator.SetBool("time3", true); break;
            case 10: animator.SetBool("time10", true); break;
        }
    }

    public void SetID(int i){
        id = i;
    }

    public int GetID(){
        return id;
    }
}
