using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform target; // follwing target's transform
    //public Transform race_Target; // if Racint folliwing target
    public Transform SwipeCharacter;
    public GameObject Camera;
    private Transform tr; // camera's transform
    [SerializeField] float speed;
    PlayerController thePlayer;
    
    Quaternion system2Camrot = Quaternion.identity;

    private bool setCam2 = false;
    
    Vector3 Camset1; 
    Vector3 Camset2;
    Vector3 changeCamset2;
    Vector3 Camset3;

    WaitForSeconds waitCam = new WaitForSeconds(2f);
    Vector3 vel = Vector3.zero;

    Quaternion turn = Quaternion.identity;
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerController>();
        tr = GetComponent<Transform>();
        speed = 15f;

        Camset1 = new Vector3(target.position.x, target.position.y + 5f, target.position.z - 5f);
        //Camset2 = new Vector3(0, 0, 0);
        Camset3 = new Vector3(9f, 2f, 0);
    }


    //private void LateUpdate()
    void Update()
    {
        tr = Camera.transform;

        if (GameManager.Instance.mode_system1)
            System1Cam();

        else if (GameManager.Instance.mode_system3)
            System3Cam();
        else if (GameManager.Instance.mode_system2)
            System2Cam();
        else if (GameManager.Instance.mode_system4)
            System4Cam();
        else if (GameManager.Instance.mode_system5)
            System5Cam();
        //else if(GameManager.Instance.mode_system2)
            //StartCoroutine(camSetting());

        /*
        else if(thePlayer.mode_Wait)
        {
            //tr.position = new Vector3(target.position.x, target.position.y + 4.0f, target.position.z - 10f);
            tr.position = new Vector3(target.position.x, target.position.y + 15.0f,  target.position.z -15f);
            tr.LookAt(target);
        }

        else if (thePlayer.system2)
        {
            //tr.position = new Vector3(race_Target.position.x, race_Target.position.y + 4.0f,race_Target.position.z - 10f);
            StartCoroutine(camSetting());
            tr.LookAt(target);
        }*/
    }

    void System1Cam()
    {
        //tr.position = new Vector3(target.position.x, target.position.y + 10.0f, target.position.z - 13f);
        Camset1 = new Vector3(target.position.x, target.position.y + 5f, target.position.z - 10f);
        tr.transform.position = Camset1;
        
        //tr.LookAt(target);
    }

    void System2Cam()
    {
        Camset2 = new Vector3(target.transform.position.x, target.position.y + 7f, target.position.z - 8f);
        system2Camrot.eulerAngles = new Vector3(25f, 0, 0); // before : x = 20f 
        //StartCoroutine(moveCamera());

        // ****** Test
        tr.transform.position = Vector3.SmoothDamp(tr.transform.position, Camset2, ref vel, 1f);
        tr.transform.rotation = system2Camrot;
        //tr.LookAt(target);
        
        
    }

    IEnumerator moveCamera()
    {
        tr.transform.position = Vector3.SmoothDamp(tr.transform.position, Camset2, ref vel, 2f);
        //Vector3 rot2cam = new Vector3(30f, 0, 0);
        //tr.transform.rotation = Quaternion.Lerp(tr.transform.rotation, system2Cam, 1f);
        yield return waitCam;
        tr.transform.position = Camset2;
        tr.transform.rotation = system2Camrot;
        yield return null;
        //setCam2 = true;
    }
    void System3Cam()
    {
        //tr.position = new Vector3(0, 5f, -5f);
        //system2Cam.eulerAngles = new Vector3(20f, 0,0);
        Camset3 = new Vector3(target.position.x, target.position.y + 10f, target.position.z - 14f);
        tr.transform.position = Vector3.SmoothDamp(tr.transform.position, Camset3, ref vel, 1f);
        //tr.transform.rotation = Quaternion.Lerp(tr.transform.rotation, system2Cam, 1f);
        
        //transform.Rotate(0, speed * Time.deltaTime,0 );
    }
    void System4Cam()
    {
        transform.Rotate(0, speed * Time.deltaTime,0 );
        //SwipeCharacter.transform.Rotate(0, speed * Time.deltaTime, 0);
    }
    void System5Cam()
    {
        tr.position = new Vector3(target.position.x, target.position.y, target.position.z + 15);
        turn.eulerAngles = new Vector3(0, 180f, 0);
        tr.transform.rotation = Quaternion.Lerp(tr.transform.rotation, turn, 1f);
    }
}
