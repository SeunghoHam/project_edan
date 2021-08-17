using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //***** About Swipe
    [SerializeField] private float horizonVel;
    [SerializeField] private int laneNum = 1;
    private string controlLocked = "n";
    private string boosterLocked ="n";

    //***** About Joystick
    JoystickManager theJoyStickManager;
    private float inputX;
    private float inputZ;


    private bool setPosition = false;
    private bool finishsetPosition = false;
    private bool speedOn = false;
    private bool isStartBooster = false;

    public bool canGet = true;

    Vector3 StartPosition;
    Vector3 vel = Vector3.zero;
    public Transform g_StartPosition;
    public Transform g_FinishPosition;
    [SerializeField] GameObject SwipeCharacter;
    Rotation theRotation;

    public Animator theAnimator;

    //***** ParticleSystem
    public ParticleSystem speedEffect;

     public ParticleSystem StrongspeedEffect;

    public ParticleSystem JumpEffect;
    WaitForSeconds slideDelay = new WaitForSeconds(0.3f);
    WaitForSeconds rotateDelay = new WaitForSeconds(0.2f);
    WaitForSeconds boost = new WaitForSeconds(2f);
    WaitForSeconds SettingTime = new WaitForSeconds(2f);


    // ***** Reference
    PositionManager positionManager;
    Follower theFollower;
    Manager theManager;
    SwipeManager theSwipeManager;
    CameraShake theCameraShake;

    private void Awake()
    {
        theAnimator = GetComponent<Animator>();
        theRotation = FindObjectOfType<Rotation>();
        theFollower = FindObjectOfType<Follower>();
        theSwipeManager = FindObjectOfType<SwipeManager>();
        theManager = FindObjectOfType<Manager>();
        theCameraShake = FindObjectOfType<CameraShake>();
        theJoyStickManager = GameObject.Find("Image_JoystickBG").GetComponent<JoystickManager>();
    }
    private void Start()
    {
            
        speedEffect.Stop();
        StrongspeedEffect.Stop();
        StartPosition = g_StartPosition.position;
    }
    void Update()
    {/*
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(theCameraShake.Shake(.15f , 4f));
        }

        if (GameManager.Instance.mode_system1 == true)
        {
                //move_Joystick();
        }
        else if(GameManager.Instance.mode_system2 == true)
        {

            //TestInput();
            setBooster(); // swipeup = booster
            Swipe();
            theRotation.Fly_Ing();
            speedEffect.Play();
        }
        else if (GameManager.Instance.mode_system3 == true)
        {
            if(setPosition == false)
            {
                JumpEffect.Play();
                theAnimator.SetBool("Move", false);
                theAnimator.SetBool("Flying", true);
                //theAnimator.SetBool("Move", false);
                
                SwipeCharacter.transform.position = new Vector3(0, 0, 0); // SwipeCharacter's 'position
                theRotation.Fly_Ready();
                StartCoroutine(PositionSetting());
            }
               
        }
        else if(GameManager.Instance.mode_system4 == true)
        {
            setFinishPosition();
            theRotation.setFinish();
            SwipeCharacter.transform.Rotate(0, 5f * Time.deltaTime, 0);
            //Debug.Log("게임이 끝났당께");
            theAnimator.SetBool("End", true);
            theAnimator.SetBool("Flying", false);
            speedEffect.Stop();
            
        }


      */  
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.mode_system2)
            return;
        if (GameManager.Instance.mode_system1 == true)
        {
            Vector3 translate = (new Vector3(inputX * Time.deltaTime * 10f,
                                       0,
                                       inputZ * Time.deltaTime * 10));
            transform.Translate(translate);
            rotateCheck();
        }
    }
    void TestInput()
    {
        //SwipeCharacter.transform.position = new Vector3(horizonVel, transform.position.y, transform.position.z);
        if (Input.GetKeyDown(KeyCode.A))
        {
            horizonVel = -2f;
            //SwipeCharacter.transform.localPosition += Vector3.Lerp(SwipeCharacter.transform.localPosition, Vector3.left, 2f * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            //horizonVel = 2f;
            theRotation.fly_Left();
            //SwipeCharacter.transform.localPosition += Vector3.Lerp(SwipeCharacter.transform.localPosition, Vector3.right, 2f * Time.deltaTime);
        }
    }

    void Swipe()
    {
        //theRotation.Fly_Ing();
         SwipeCharacter.transform.position = new Vector3(horizonVel, transform.position.y, transform.position.z);
        // 리지드바디로 넣어줘보자getcompo < rigidbody> . velocity = new vectoctor
        if ((SwipeManager.swipeLeft) && (laneNum > 0) && (controlLocked == "n")) // move Left
        {
            theAnimator.SetTrigger("f_Left");
            horizonVel += -2f;
            //transform.position = Vector3.left * 2f;
            //SwipeCharacter.transform.localPosition += Vector3.left * 2f;
            //SwipeCharacter.transform.localPosition += Vector3.Lerp(SwipeCharacter.transform.localPosition, Vector3.left, 2f * Time.deltaTime);
            StartCoroutine(stopSlide());
            laneNum -= 1;
            controlLocked = "y";
        }

        if ((SwipeManager.swipeRight) && (laneNum < 2) && (controlLocked == "n")) // move Right
        {
            theAnimator.SetTrigger("f_Right");
            horizonVel += 2f;
            //transform.position = Vector3.right * 2f;
            //SwipeCharacter.transform.localPosition += Vector3.right * 2f;
            //SwipeCharacter.transform.localPosition += Vector3.Lerp(SwipeCharacter.transform.localPosition, Vector3.right, 2f * Time.deltaTime);
            StartCoroutine(stopSlide());
            laneNum += 1;
            controlLocked = "y";
        }
    }
    void move_Joystick()
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
    void setFinishPosition()
    {
        transform.position = Vector3.Lerp(transform.position, g_FinishPosition.position, Time.deltaTime);
        //SwipeCharacter.transform.position = Vector3.Lerp(SwipeCharacter.transform.position, transform.position, Time.deltaTime);
        SwipeCharacter.transform.position = Vector3.SmoothDamp(SwipeCharacter.transform.position, transform.position, ref vel, 1f);
       
    }
    void StartSystem2()
    {
        GameManager.Instance.mode_system1 = false;
        GameManager.Instance.mode_system2 = true;
        GameManager.Instance.mode_system3 = false;
    } 
    public void setBooster()
    {
        if((SwipeManager.swipeUp) && (theManager.currentPlayerFeather >=2) &&(boosterLocked == "n"))
        {
            theManager.player_DecreaseFeather(3);
            StartCoroutine(Booster());
            boosterLocked = "y";
        }
    }
     private void OnTriggerEnter(Collider other) //feather colllider
     {
        if(canGet)
        {
            if (other.CompareTag("Feather"))
            {
                //featherMove = true;
                theAnimator.SetTrigger("GetFeather");
                //Debug.Log("깃털 겟 에니메이션");
                //theAnimator.SetTrigger("GetFeather");

            }
        }
       
    }
    IEnumerator flyturn_Left()
    {
        theRotation.fly_Left();
        yield return rotateDelay;
        theRotation.Fly_Ing();
    }
    IEnumerator flyturn_Right()
    {
        theRotation.fly_Right();
        yield return rotateDelay;
        theRotation.Fly_Ing();
    }
    IEnumerator Booster()
    {

        theFollower.moveSpeed = 20f;
        yield return boost;
        boosterLocked = "n";
        theFollower.moveSpeed = 8f;
        StrongspeedEffect.Stop();

    }
    IEnumerator stopSlide()
    {
        yield return slideDelay;
        //horizonVel = 0;

        controlLocked = "n";
    }
      IEnumerator PositionSetting()
      {

        //transform.position = Vector3.Lerp(transform.position, StartPosition, 1f * Time.deltaTime); // Character's positoin
        transform.position = Vector3.SmoothDamp(transform.position, StartPosition, ref vel, 1f);
        canGet = true;

        

        //transform.position = Vector3.MoveTowards(transform.position, StartPosition, 0.1f);
        //SwipeCharacter.transform.position = Vector3.zero;
        
        yield return SettingTime;
        
        //yield return new WaitForSeconds(0.5f);
        //setPosition = true;
        StartSystem2();
         yield return null;
        //JumpEffect.Stop();    
    }
}
