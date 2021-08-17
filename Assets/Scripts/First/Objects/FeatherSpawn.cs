using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatherSpawn : MonoBehaviour
{
    //private int featherSpawnCount = 15;
    private int featherSpawnCount = 30;

    [SerializeField] private GameObject prefab_Feather;
    [SerializeField] private Transform[] spawnPointArray;
    private int currentObjectCount = 0; // 현재까지 생성한 오브젝트 개수
    private float objectSpawnTime = 0.0f;

    Player thePlayer;

     private void Awake() 
    {
        CreateandSpwan();

        for(int i=0; i< featherSpawnCount; ++i)
        {
            //int index= Random.Range(0, prefab_Feather.Length);
            float x = Random.Range(-9f, 9.1f); 
            float z = Random.Range(-21f, -3f);
            Vector3 position = new Vector3(x,24,z);

            Instantiate(prefab_Feather, position, Quaternion.identity);
        }
    }
    private void Start()
    {
        thePlayer = FindObjectOfType<Player>();
        //SpawnerSpwan();
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(thePlayer.mode_Race || thePlayer.mode_Wait)
        {
            return;
        }
        if (thePlayer.mode_Normal)
        {
            if (currentObjectCount + 1 > featherSpawnCount)
            {
                return;
            }

            objectSpawnTime += Time.deltaTime;

            if (objectSpawnTime >= 2f)
            {
                int spawnIndex = Random.Range(0, spawnPointArray.Lengtsh);

                Vector3 position = spawnPointArray[spawnIndex].position;
                GameObject clone = Instantiate(prefab_Feather, position, Quaternion.identity);

                currentObjectCount++;
                objectSpawnTime = 0.0f;
            }
        }
    */
    }
    void SpawnerSpwan()
    {
        Transform Parent = GameObject.Find("FeatherSpawner").GetComponent<Transform>();
        for(int i=0; i< featherSpawnCount; i++)
        {
            Vector3 position = spawnPointArray[i].position;
            //GameObject clone = Instantiate(prefab_Feather, position, Quaternion.identity);
            //clone.transform.parent = Parent; 
        }
    }

    void CreateandSpwan()
    {
            for(int i=0; i < featherSpawnCount; ++ i)
        {
            //int index = Random.Range(0, prefab_Feather);
            float x = Random.Range(-15f, 15f); // x위치
            float z = Random.Range(-30f, 00f);
            Vector3 position = new Vector3(x ,0 , z );

            //Instantiate(prefab_Feather, position, Quaternion.identity);
        }  
    }
}
