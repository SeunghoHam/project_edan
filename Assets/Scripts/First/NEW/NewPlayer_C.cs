using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayer_C : MonoBehaviour
{
    PositionManager thePositionManager;
    CoinCollect theCoinCollect;
    Manager theManager;

    public ParticleSystem Steal;
    public ParticleSystem GetFeatherItem;
    public ParticleSystem GetCoinItem;

    WaitForSeconds stealWait = new WaitForSeconds(1f);

    CoinManager theCoinManager;
    // Start is called before the first frame update
    void Start()
    {
        thePositionManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<PositionManager>();
        theCoinCollect = GameObject.FindGameObjectWithTag("Manager").GetComponent<CoinCollect>();
        theManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>();
        theCoinManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<CoinManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Coin"))
        {
            theCoinManager.AddCoins(other.transform.position, 1);
        }
        
    }
}
