using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class FollowerTrack : MonoBehaviour
{
    public PathCreator pathCreator;
    public float speed = 20f;
    float distanceTravelled;

    bool canCreate = false;
    bool move = false;

    private Vector3 createPos;

    private float createTime = 0f;

    Follower theFollower;
    private void Start() 
    {
        createPos = transform.position; 
        
        theFollower = FindObjectOfType<Follower>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //speed = theFollower.moveSpeed;
        if (GameManager.Instance.mode_system2)
        {
            distanceTravelled += speed * Time.deltaTime;
            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
            transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RaceFinish"))
            Destroy(gameObject);
    }
}
