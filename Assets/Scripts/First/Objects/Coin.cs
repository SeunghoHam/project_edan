using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    Manager theManager;

    [SerializeField] GameObject text_Coin;

    private void Start()
    {
        theManager = FindObjectOfType<Manager>();
        text_Coin.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            //StartCoroutine(Destroy()); 
            text_Coin.SetActive(true);
            theManager.GetCoin(1);
            StartCoroutine(GetCoin());
        }
        if (other.CompareTag("Enemy"))
        {
            StartCoroutine(GetCoin());
        }


    }
    IEnumerator GetCoin()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }

}
