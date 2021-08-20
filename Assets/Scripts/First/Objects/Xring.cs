using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Xring : MonoBehaviour
{
    ScoreManager theScoreManager;
    public ParticleSystem particle_Pass;

    private void Awake()
    {
        theScoreManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<ScoreManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            //particle_Pass.Play();
            //Debug.Log("multiper collision");
            //theScoreManager.multiplerScore += 1;
            
        }
    }
}
