using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    public float zSpawn = 0f;

    [Header("tiles's length")]
    public float tileLength = 30f;


    [Header("default tiles's count")]
    public int numberofTiles = 5;

    private List<GameObject> activeTiles = new List<GameObject>();

    public Transform playerTransform; // 플레이어 위치 가져오기
    Player thePlayer;

    void Start()
    {
        thePlayer = FindObjectOfType<Player>();
        
        if (GameManager.Instance.mode_system2)
        {
            Debug.Log("tilecreate");
            for (int i = 0; i < numberofTiles; i++)
            {
                if (i == 0)
                    SpanwTile(0);
                else
                    SpanwTile(Random.Range(0, tilePrefabs.Length)); // 0 ~ 타일트리팹( 게임오브젝트배열 )의 크기만큼
            }
        }

    }


    void Update()
    {       
        if(GameManager.Instance.mode_system1 ==true || thePlayer.isDead == true)
            return;
        if (GameManager.Instance.mode_system2)
        {
            if (playerTransform.position.z - 20f > zSpawn - (numberofTiles * tileLength))
            //if(playerTransform.position.z > zSpawn - (numberofTiles * tileLength))
            {
                SpanwTile(Random.Range(0, tilePrefabs.Length));
                //DeleteTile();
            }
        }

    }

    public void SpanwTile(int tileIndex)
    {
        GameObject go = Instantiate(tilePrefabs[tileIndex], transform.forward * zSpawn, transform.rotation);
        // Instantiate = 게임 실행중 오브젝트 생성 
        // transform.forward 로 z축 + 방향으로 타일 생성
        activeTiles.Add(go);
        zSpawn += tileLength; // z 스폰 위치를 타일의 길이만큼 더한다.
        //Debug.Log(activeTiles.Count);
    }
 
    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
