using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class RunningRandom : MonoBehaviour
{
    // Start is called before the first frame update
    int valorNumerico = 0;

    System.Random aleatorio = new System.Random();

    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        valorNumerico = aleatorio.Next(1,7); 

        // print(valorNumerico);

        if(Input.GetKeyUp(KeyCode.F12))
        {

            print("here in code");
        }
    }
}
