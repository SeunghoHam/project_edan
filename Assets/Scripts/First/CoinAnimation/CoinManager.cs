using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class CoinManager : MonoBehaviour
{
    [SerializeField] Text coinUIText;
    [SerializeField] GameObject animatedCoinPrefab;
    [SerializeField] Transform target;

    [Space]
    [Header ("Avaialbe coins : (coins to pool")]
    [SerializeField] int maxCoins;
    Queue<GameObject> coinsQueue = new Queue<GameObject>();  


    [Space]
    [Header("Animation settings")]
    [SerializeField] [Range(0.5f, 0.9f)] float minAnimDuration;
    [SerializeField] [Range(0.9f, 2f)] float maxAnimDuration;

    [SerializeField] Ease easeType;
    [SerializeField] float spread;
    Vector3 targetPosition;



    private int _c = 0;

    public int Coins
    {
        get { return _c; }
        set
        { 
            _c = value;
            coinUIText.text = Coins.ToString();    
        }
    }
    private void Awake()
    {
        targetPosition = target.position;
    }
    void PrepareCoins()
    {
        GameObject coin;
        for(int i =0; i< maxCoins; i++)
        {
        coin = Instantiate(animatedCoinPrefab);
        coin.transform.parent = transform;
        coin.SetActive(false);
        coinsQueue.Enqueue(coin);
        }
   
    }
    void Animate(Vector3 collectedCoinPosition, int amount)
    {
        for(int i = 0; i < amount; i++)
        {
            // check if there's coins in the pool
            if(coinsQueue.Count> 0)
            {
                GameObject coin = coinsQueue.Dequeue();
                coin.SetActive(true);

                // move coin to the collected coin pos
                coin.transform.position = collectedCoinPosition + new Vector3(Random.Range(-spread, spread), 0f, 0f);


       
                //aniamted coin to target position; 
                float duration = Random.Range(minAnimDuration, maxAnimDuration);
                coin.transform.DOMove(targetPosition, duration);
                //.SetEase(easeType).onComplete( coin.SetActive(false), coinsQueue.Enqueue(coin) );

                Coins++;
                
                
                /*
                .SetEase(easeType)
                .onComplete(() => {
                coin.SetActive(false);
                coinsQueue.Enqueue(coin);
                Coins++;
                
                });*/
            }
        }
    }
    public void AddCoins (Vector3 collectedCoinPosition, int amount)
    {
        Animate(collectedCoinPosition, amount);

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
