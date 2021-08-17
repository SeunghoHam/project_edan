using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionPlayer : MonoBehaviour
{
    PositionManager positionManager;
    CoinCollect coinCollect;
    Manager theManager;

    public ParticleSystem Steal;
    public ParticleSystem GetFeatherItem;
    public ParticleSystem GetCoinItem;


    WaitForSeconds stealWait = new WaitForSeconds(1f);

    // Start is called before the first frame update
    void Start()
    {
        positionManager = FindObjectOfType<PositionManager>();
        coinCollect = FindObjectOfType<CoinCollect>();
        theManager = FindObjectOfType<Manager>();
        Steal.Stop();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {

        if (GameManager.Instance.mode_system2 && GameManager.Instance.canSteal)
        {
            //Debug.Log("e백과 충돌");
            if (positionManager.leadEnemy)
            {
                if (other.CompareTag("e_Back"))
                {
                    Steal.Play();
                    theManager.player_IncreaseFeather(2);
                    theManager.enemy_DecreaseFeather(2);
                    GameManager.Instance.canSteal = false;
                    StartCoroutine(stealDelay());
                }

            }
        }


        if (other.gameObject.CompareTag("RaceFinish"))
        {
            GameManager.Instance.mode_system4 = true;
            GameManager.Instance.mode_system2 = false;
        }

        if (other.gameObject.CompareTag("Coin"))
        {
            GetCoinItem.Play();
            coinCollect.StartCoinMove(other.transform.position, () =>
            {
                //theManager.GetCoin(1);
                //Destroy(other.gameObject);
            });
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
