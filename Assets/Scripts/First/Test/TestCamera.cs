using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCamera : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;

    private Transform camTr;
    

    void LateUpdate() 
    {
        //Vector3 desiredPosition = target.localPosition;
        //Vector3 smoothedPosition = Vector3.Lerp(transform.localPosition, desiredPosition, smoothSpeed);
        //transform.localPosition=  smoothedPosition;

        //transform.position = new Vector3(target.position.x, target.position.y+10.0f, target.position.z -5f);
        transform.LookAt(target);
    }
}
