using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Xring : MonoBehaviour
{
    public ParticleSystem particle_Pass;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            particle_Pass.Play();
        }
    }
}
