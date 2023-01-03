using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class idIdentifier : MonoBehaviour
{
    int id;

    public void SetID(int i){
        id = i;
    }

    public int GetID(){
        return id;
    }
}
