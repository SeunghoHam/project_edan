using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionEnemy : MonoBehaviour
{
    PositionManager positionManager;
    CoinCollect coinCollect;
    Manager theManager;
    Enemy theEnemy;
    public ParticleSystem Steal;
    public ParticleSystem GetFeatherItem;
    public ParticleSystem GetCoinItem;

    WaitForSeconds stealWait = new WaitForSeconds(1f);

    void Start()
    {
        positionManager = FindObjectOfType<PositionManager>();
        coinCollect = FindObjectOfType<CoinCollect>();
        theManager = FindObjectOfType<Manager>();
        theEnemy = FindObjectOfType<Enemy>();
        Steal.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.Instance.canSteal == true)
        {
            if (theEnemy.canMove)
            {
                if (positionManager.leadPlayer)
                {
                    if (other.CompareTag("p_Back"))
                    {

                        Steal.Play();
                        theManager.enemy_IncreaseFeather(2);
                        theManager.player_DecreaseFeather(2);
                        GameManager.Instance.canSteal = false;
                        StartCoroutine(stealDelay());
                    }

                }
            }
        }
        if (other.CompareTag("RaceFinish"))
        {
            theEnemy.canMove = false;
            GameManager.Instance.raceFinish = true;
            GameManager.Instance.mode_system5 = true;
            GameManager.Instance.mode_system4 = false;
        }
        if (other.gameObject.CompareTag("Coin"))
        {
            GetCoinItem.Play();
        }
        if (other.gameObject.CompareTag("FeatherItem"))
        {
            GetFeatherItem.Play();
        }
    }
    IEnumerator stealDelay()
    {
        yield return stealWait;
        GameManager.Instance.canSteal = true;
    }
}
