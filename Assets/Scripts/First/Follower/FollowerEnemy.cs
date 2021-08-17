using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class FollowerEnemy : MonoBehaviour
{
    public PathCreator pathCreator;
    public float moveSpeed;
    float distanceTravelled;

    Enemy theEnemy;
    // Start is called before the first frame update
    void Start()
    {
        theEnemy = FindObjectOfType<Enemy>();
        moveSpeed = GameManager.Instance.normalSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if(theEnemy.canMove && GameManager.Instance.raceFinish==false)
        {
                distanceTravelled += moveSpeed * Time.deltaTime;
                transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
                transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled);
        }
    }
}
