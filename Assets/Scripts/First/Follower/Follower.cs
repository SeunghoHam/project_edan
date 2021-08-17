using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Follower : MonoBehaviour
{
    public PathCreator pathCreator;
    public float moveSpeed;
    
    float distanceTravelled;
    private bool move;
    // path key
    // create - shift leftClick
    // delete - ctrl leftClick

    private float decreaseTime = 0.0f;

    FollowerTrack theFollowerTrack;
    void Start()
    {
        //theFollowerTrack = FindObjectOfType<FollowerTrack>();
        move = false;
        moveSpeed = GameManager.Instance.normalSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.mode_system2)
        {
                distanceTravelled += moveSpeed * Time.deltaTime;
                transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
                transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled);
        }



    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RaceFinish"))
        {
            Debug.Log("카메라 앵글이 변하면 됩니다");
        }
    }
    //WaitForSeconds delay = new WaitForSeconds(2f);
    IEnumerator FollowerMove()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("Follower Move = true");
        move = true;
    }

}
