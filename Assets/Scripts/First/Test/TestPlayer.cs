using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    [SerializeField] private float horizonVel = 0f;
    [SerializeField] private int laneNum = 1;
    [SerializeField] private string controlLocked = "n";

    SwipeManager  theSwipeManager;

    Rigidbody myRigid;
    // Start is called before the first frame update
    void Start()
    {


        theSwipeManager = FindObjectOfType<SwipeManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Swipe();
        //directionSetting();
        if(Input.GetKeyDown(KeyCode.A))
        {
            transform.localPosition += Vector3.left * 0.5f;
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            transform.localPosition += Vector3.right * 0.5f;
        }
    }
    void directionSetting()
    {

        Vector3 side_Left = new Vector3(-0.5f, transform.position.y, transform.position.z);
        Vector3 side_Middle = new Vector3(0f, transform.position.y, transform.position.z);
        Vector3 side_Right = new Vector3(0.5f, transform.position.y, transform.position.z);
        if (laneNum == 0)
            transform.position = Vector3.Lerp(transform.position, side_Left, 0.1f);
        else if (laneNum == 1)
            transform.position = Vector3.Lerp(transform.position, side_Middle, 0.1f);
        else if (laneNum == 2)
            transform.position = Vector3.Lerp(transform.position, side_Right, 0.1f);   
    }
    void Swipe()
    {
        
        if ((SwipeManager.swipeLeft) && (laneNum > 0) && (controlLocked == "n"))
        {
            horizonVel = -0.5f;
            StartCoroutine(stopSlide());
            laneNum -= 1;
            controlLocked = "y";
        }

        if ((SwipeManager.swipeRight) && (laneNum < 2) && (controlLocked == "n"))
        {
            horizonVel = 0.5f;
            StartCoroutine(stopSlide());
            laneNum += 1;
            controlLocked = "y";
        }
    }


    IEnumerator stopSlide()
    {
        yield return new WaitForSeconds(0.5f);
        horizonVel = 0;
        controlLocked = "n";
    }
}
