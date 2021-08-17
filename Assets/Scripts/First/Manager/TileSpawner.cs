using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    [SerializeField] Transform Player;
    [SerializeField] GameObject[] prefabTile;
    [SerializeField] Camera MainCamera;

    List<GameObject> TileList = new List<GameObject>();
    List<GameObject> SpawnList = new List<GameObject>();

    Vector3 PlayerPosition;
    Vector3 TargetCameraPosition;


    bool ChangeTile;

    int Main_index, Next_index, Sub_index;
    // Start is called before the first frame update
    void Start()
    {
        Main_index = 0;
        Next_index = 1;
        Sub_index = 2;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void SpawnTile()
    {
        GameObject Tileinstantiate;

        for (int i = 0; i < 5; i++)
        {
            Tileinstantiate = Instantiate(prefabTile[0] as GameObject);
            Tileinstantiate.transform.position = new Vector3(0, 0, 10 + i); // z 값 수정해줘야함

            TileList.Add(Tileinstantiate);
        }
    }

    void TileSystem()
    {
        PlayerPosition = Player.transform.position;

        if (PlayerPosition.z >= TileList[Next_index].transform.position.z - 5)
        {
            int temp;

            temp = Main_index;
            Main_index = Next_index;
            Next_index = Sub_index;
            Sub_index = temp;
        }
    }

    void StartSpawnSystem()
    {
        int ObjectVal;
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                ObjectVal = Random.Range(0, 4); // startval = include/ lastval  = exclusive

                if (ObjectVal == 1)
                {
                    //Debug.Log("")
                }
            }
        }
    }
}
