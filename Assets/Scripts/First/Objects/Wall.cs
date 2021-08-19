using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    Manager theManager;

    BoxCollider collider;
    private void Awake()
    {
        theManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>();
        collider= GetComponent<BoxCollider>();
    }

    private void Update()
    {
        if(theManager.currentPlayerFeather >= theManager.maxFeather)
        {
            collider.isTrigger = true;
        }
        else
        {
            collider.isTrigger = false;
        }
    }
}
