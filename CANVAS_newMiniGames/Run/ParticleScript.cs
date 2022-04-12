using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScript : MonoBehaviour
{

    ParticleSystem ps;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    void LateUpdate()
    {

        ParticleSystem.Particle[] particles =
            new ParticleSystem.Particle[ps.particleCount];


        if(Input.GetKeyUp(KeyCode.F7))
        {
        
             ParticleSystem.EmissionModule emissionVar = ps.emission;

             emissionVar.rateOverTime = 4; 

        }


        if(Input.GetKeyUp(KeyCode.F6))
        {
        
            ps.Stop();

        }

        ps.GetParticles(particles);

    }

}
