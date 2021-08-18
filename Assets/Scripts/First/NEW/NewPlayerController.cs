using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerController : MonoBehaviour
{
    // ***** Swipe
    private float horizonVel;
    private int laneNum = 1;
    private string controlLocked = "n";
    private string boosterLocked = "n";
    public GameObject swipeCharacter;
    private float moveLength = 2f;
    private float moveSpeed = 15f;



    WaitForSeconds slideDelay = new WaitForSeconds(0.3f);
    WaitForSeconds GetDelay = new WaitForSeconds(1f);
    // ***** JoyStick
    JoystickManager theJoyStickManager;
    private float inputX;
    private float inputZ;
    private bool canMove = true;


    // ***** settingPosiiton
    Vector3 vel = Vector3.zero;
    public Transform g_StartPosition;
    Vector3 StartPosition;
    public Transform g_FinishPosition;


    // ***** Particle
    public ParticleSystem speedEffect;
    public ParticleSystem StrongspeedEffect;
    public ParticleSystem jumpEffect;


    // ***** Setting 
    private bool issetPosition;
    private bool finishsetPosition;
    
    public bool canGet = true;

    // ***** Reference
    CameraShake theCameraShake;
    Rotation theRotation;
    Animator theAnimator;
    Manager theManager;
    PositionManager thePositionManager;
    Rigidbody myrigid;

    // Start is called before the first frame update
    void Awake()
    {
        theJoyStickManager = GameObject.FindGameObjectWithTag("JoyStick").GetComponent<JoystickManager>();        
        theAnimator = transform.GetChild(0).GetComponent<Animator>();
        theManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>();
        thePositionManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<PositionManager>();
        theRotation = GameObject.FindGameObjectWithTag("Manager").GetComponent<Rotation>();
        swipeCharacter = transform.GetChild(0).GetComponent<GameObject>();
        theCameraShake = FindObjectOfType<CameraShake>();
        myrigid = GetComponent<Rigidbody>();
    }
    void Start()
    {
        StartPosition = g_StartPosition.position;
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(theCameraShake.Shake(0.15f, 4f));
        }
        if (GameManager.Instance.mode_system1 == true)
        {
            moveJoystick();
            if(Input.GetKeyDown(KeyCode.K))
            {
                theManager.player_IncreaseFeather(1);
            }
        }
        else if(GameManager.Instance.mode_system2 == true)
        {
            theAnimator.SetBool("Flying", true);
            //transform.position += new Vector3(transform.position.x * moveSpeed * Time.deltaTime, transform.position.y, transform.position.z);
            transform.Translate(transform.forward * moveSpeed * Time.deltaTime);
            Swipe();
            if (Input.GetKey(KeyCode.L))
            {
                theAnimator.SetBool("Falling", true);
            }
            else theAnimator.SetBool("Falling", false);
        }
        else if(GameManager.Instance.mode_system3 == true)
        {
            canGet = false;
            setPosition();
            Debug.Log("�ý���3");
            if(Input.GetKeyDown(KeyCode.J))
            {
                GameManager.Instance.setSystem2();
            }
        }
       
       
        
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.mode_system1 == true && canMove)
        {
            
            Vector3 translate = (new Vector3(inputX * Time.deltaTime * 10f, 0, inputZ * Time.deltaTime * 10f));
            transform.Translate(translate);
            rotateCheck();
        }

    }

    void moveJoystick()
    {
        inputX = theJoyStickManager.inputhorizontal();
        inputZ = theJoyStickManager.inputVertical();
        if (inputX == 0 && inputZ == 0)
        {
            theAnimator.SetBool("Move", false);
        }
        else
        {
            theAnimator.SetBool("Move", true);
        }
    }
    void rotateCheck()
    {
        if (inputX > 0)
            theRotation.TurnRight();
        else if (inputX < 0)
            theRotation.TurnLeft();

        if (inputZ > 0)
            theRotation.TurnUp();
        else if (inputZ < 0)
            theRotation.TurnDown();
    }
    void setPosition()
    {
        //swipeCharacter.transform.position = transform.position;
        theAnimator.SetBool("Move", false);
        transform.position = Vector3.SmoothDamp(transform.position, StartPosition, ref vel, 1f);
        theRotation.TurnUp();
    }
    void Swipe()
    {
        //swipeCharacter.transform.position = new Vector3(horizonVel, transform.position.y, transform.position.z);
        if ((SwipeManager.swipeLeft) && (laneNum > 0) && (controlLocked == "n")) // move Left
        {
            // animaiton
            horizonVel -= moveLength;
            laneNum -= 1;
            controlLocked = "y";
        }
        if ((SwipeManager.swipeRight) && (laneNum < 2) && (controlLocked == "n")) // move Right
        {
            horizonVel += moveLength;
            laneNum += 1;
            controlLocked = "y";
        }
    }
    private void OnTriggerEnter(Collider other) //feather colllider
    {
        if (canGet)
        {
            if (other.CompareTag("Feather"))
            {
                canMove = false;
                theAnimator.SetTrigger("PickUp");
                StartCoroutine(PickupDelay());
            }
        }

    }

    IEnumerator stopSlide()
    {
        yield return slideDelay;
        //horizonVel = 0;

        controlLocked = "n";
    }
    IEnumerator PickupDelay()
    {
        yield return GetDelay;
        canMove = true;
        yield return null;
    }
}