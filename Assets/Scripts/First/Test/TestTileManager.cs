using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTileManager : MonoBehaviour
{
    [SerializeField]GameObject[] prefab_Tiles;

    public GameObject Parent;
    //public GameObject Tile;
    public Vector3 createPos;

    private bool DelayOn = false;
    private bool canCreate= false;
    private float createTime;

    private List<GameObject> activeTiles = new List<GameObject>();
    void Start()
    {  
        //createPos = Parent.transform.position; // 굳이 두번 안거치고 createPos위치에 할당시켜도 될듯함

        //GameObject.Instantiate(prefab_Tiles[0], createPos, Quaternion.identity).transform.parent = Parent.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.mode_system2)
        {
            if(canCreate)
               timeElapsed();
            
            if(DelayOn == true)
                return;
            Delay();
        }
            
    }

    public void SpawnTile()
    {
        /*
        GameObject go = Instantiate(prefab_Tiles[tileIndex], transform.position, transform.rotation);
        //GameObject go = Instantiate(Tile, createPos, Quaternion.identity).transform.parent = Parent.transform);
        activeTiles.Add(go);
        go.transform.position += Vector3.back * Time.deltaTime;*/
            createPos = Parent.transform.position;
            int prefabIndex =  Random.Range(0,2); // 0 1 2
            // ↓ 캐릭터 생성할때 쓴거
            //GameObject.Instantiate(prefab_Tiles[prefabIndex], createPos ,moveCharacter.transform.rotation);
            GameObject.Instantiate(prefab_Tiles[prefabIndex], createPos, Quaternion.identity);
       
    }
    void timeElapsed()
    {
         createTime += Time.deltaTime;
        if(createTime >= 1f)
        {
            createTime = 0.0f;
            SpawnTile();
        }
    }
    void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
    void Delay()
    {
        StartCoroutine(StartDelay());
    }
    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(2f);
        canCreate = true;
        DelayOn = true;
    }
}
