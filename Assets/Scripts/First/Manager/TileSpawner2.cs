using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner2 : MonoBehaviour
{

    public float moveSpeed;
    [SerializeField] GameObject[] prefabTiles;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i=0; i < 4; i++)
        {
            Vector3 curPos = prefabTiles[i].transform.position;
            Vector3 nextPos = Vector3.back * moveSpeed * Time.deltaTime;
            //transform.position = curPos+ new
        }
        
    }
}
