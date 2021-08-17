using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSpawnManager : MonoBehaviour
{
    public GameObject[] BuildingPrefabs;
    private Transform playerTransform;
    private float spanwZ;
    private float tileLength = 12.0f;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
