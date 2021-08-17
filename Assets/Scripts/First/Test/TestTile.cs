using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTile : MonoBehaviour
{
    private float tileSpeed = 10f;


    private void Start() 
    {
        StartCoroutine(DestroyTile());
    }

    IEnumerator DestroyTile()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }
}
