using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NewCodeRunning : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        List<string> listString = new List<string>(){ "AGNOSTA", "PERCIATE", "DEFINITIONE", "CRESCIUTO", "MODELATO"};


        List<string> shuffled_list = new List<string>(listString);



        for (int i = 0; i < 50; i++)
        {
            
            System.Random aleatorio = new System.Random(i);
            System.Random aleatorio2 = new System.Random(i + 5175);
    
            int randomIndex = aleatorio.Next(0, listString.Count);
            int randomIndex2 = aleatorio2.Next(0, listString.Count);

            string variableString = listString[randomIndex2];
            listString[randomIndex2] = listString[randomIndex];
            listString[randomIndex] = variableString;

        }


        for(int i = 0 ; i < shuffled_list.Count; ++i)
        {

            Debug.Log(shuffled_list[i] + "      " + listString[i]);

        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
