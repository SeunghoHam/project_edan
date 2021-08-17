using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Feather : MonoBehaviour
{
    Manager theManager;
    PositionManager thePositionManager;

    [SerializeField] ParticleSystem p_featherUp;

    void Start()
    {
        theManager = FindObjectOfType<Manager>();
        thePositionManager = FindObjectOfType<PositionManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Destroy(gameObject);
            p_featherUp.Play();
            if(thePositionManager.leadPlayer)
                theManager.player_IncreaseFeather(1);
            else if(thePositionManager.leadEnemy)
                   theManager.player_IncreaseFeather(3);
        }

        if(other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            p_featherUp.Play();
            if (thePositionManager.leadPlayer)
                theManager.enemy_IncreaseFeather(1);
            else if (thePositionManager.leadPlayer)
                theManager.enemy_IncreaseFeather(3);
        }
    }
}
