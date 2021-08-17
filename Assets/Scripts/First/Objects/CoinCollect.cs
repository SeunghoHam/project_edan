using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CoinCollect : MonoBehaviour
{
    public float speed;

    public Transform target;
    //public Transform intial;
    public GameObject CoinPrefab;
    public Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        if(cam == null)
        {
            cam = Camera.main;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartCoinMove(Vector3 _intial, Action onComplete)
    {
        //Vector3 intialpos = cam.ScreenToWorldPoint(new Vector3(intial.position.x, intial.position.y, cam.transform.position.z * -1));
        Vector3 targetpos = cam.ScreenToWorldPoint(new Vector3(target.position.x, target.position.y, cam.transform.position.z * -1));
        GameObject _coin = Instantiate(CoinPrefab, transform);

        StartCoroutine(MoveCoin (_coin.transform, _intial, targetpos, onComplete));
    }
    IEnumerator MoveCoin(Transform obj, Vector3 startPos, Vector3 endPos, Action onComplete)
    {
        float  time = 0;

        while(time < 1)
        {
            time += speed * Time.deltaTime;
            obj.position = Vector3.Lerp(startPos, endPos, time);

            yield return new WaitForEndOfFrame();
        }
        onComplete.Invoke();
        Destroy(obj.gameObject);
    }
}
