using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //***** About Swipe
    [SerializeField] private float horizonVel = 0f;
    [SerializeField] private int laneNum = 1;
    [SerializeField] private string controlLocked = "n";

    private bool isPositionSetting = false;
    [SerializeField] GameObject g_Player;

    // path 시작 위치 직전으로 설정
    Vector3 StartPosition;
    [SerializeField] Transform g_StartPosition;
    [SerializeField] GameObject moveCharacter;

    Vector3 JumpPos = new Vector3(0, 20f, 0);

    // ***** About Particle
    [SerializeField] ParticleSystem p_speedEffect;
    
    //***** call Reference
    Manager theManager;
    Rotation theRotation;
    CanvasManager theCanvasManager;
    Rigidbody myRigid;
    



    //***** Joystick Controller
    //[SerializeField] private bl_Joystick Joystick;//Joystick reference for assign in inspector
    //[SerializeField] private float Speed = 1; // normal state move speed

    // Upgrade joystick
    JoystickManager theJoyStickManager;

    private float inputX;
    private float inputZ;


 

    //***** Race
    [SerializeField] public float moveSpeed = 60f;
    private bool isRaceStart = false;

    //***** State
    private int hp = 1; // 플레이어의 체력
    public bool isDead; // 
    public bool isCollision;

    //***** Mode
    

    //***** Animator
    [SerializeField] Animator theAnimator;

    //***** Collision
    void Start()
    {
        theAnimator = GetComponent<Animator>();
        theManager = FindObjectOfType<Manager>();
        theRotation = FindObjectOfType<Rotation>();
        theCanvasManager = FindObjectOfType<CanvasManager>();
        theJoyStickManager = GameObject.Find("Image_JoystickBG").GetComponent<JoystickManager>();
        //theJoyStickManager = FindObjectOfType<JoystickManager>();

        isDead = false;
        isCollision =false;
        p_speedEffect.Stop();

        StartPosition = g_StartPosition.position;
    }

    void Update()
    {
       
        if (isDead == false)
        {
            if (GameManager.Instance.mode_system1 == true)
            {
                 UpgradeJoyStick();
                
            }
            //////////////////// 플레이어 위치 0,0,0 으로 설정 해줘야 함
            else if (GameManager.Instance.mode_system2 == true)
            {
                StartCoroutine(PositionSetting());
                TestInput();
                //Swipe();
                //directionSetting();
                p_speedEffect.Play();
                
            }
            else if(GameManager.Instance.mode_system3 == true)
            {
                
            }
           
        }
        else if(isDead == true)
            p_speedEffect.Stop();

    }
    private void FixedUpdate()
    {
        //v_Movement = transform.forward * inputZ;

        //transform.Rotate(Vector3.up * inputX * 300f * Time.deltaTime);
        if(GameManager.Instance.mode_system2)
            return;
        if(GameManager.Instance.mode_system1 == true)
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
           if(Input.GetKeyDown(KeyCode.A))
        {
            transform.localPosition += Vector3.left * 2f;
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            transform.localPosition += Vector3.right * 2f;
        }
    }
    #region MoveFront
    void MoveFront()
    {
        //Debug.Log("플레이어 system2");
        //theRotation.TurnUp();
        //GetComponent<Rigidbody>().velocity = new Vector3(horizonVel, 0, moveSpeed) * Time.deltaTime; // 앞으로 이동과 스와이프
        //transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }
    #endregion
    void rotateCheck()
    {
        if(inputX >= 0 )
            theRotation.TurnRight();
        else if(inputX <= 0 )
            theRotation.TurnLeft();
        
        if(inputZ >=0 )
            theRotation.TurnUp();
        else if(inputZ <=0)
            theRotation.TurnDown();

    }
    void Swipe()
    {
        if ((SwipeManager.swipeLeft) && (laneNum > 0) && (controlLocked == "n"))
        {
            horizonVel = -6f;
            StartCoroutine(stopSlide());
            laneNum -= 1;
            controlLocked = "y";
        }

        if ((SwipeManager.swipeRight) && (laneNum < 2) && (controlLocked == "n"))
        {
            horizonVel = 6f;
            StartCoroutine(stopSlide());
            laneNum += 1;
            controlLocked = "y";
        }
    }

    void UpgradeJoyStick()
    {
        inputX = theJoyStickManager.inputhorizontal();
        inputZ = theJoyStickManager.inputVertical();
        if(inputX ==0 && inputZ ==0)
        {
            theAnimator.SetBool("isRun", false);
        }
        else
        {
            theAnimator.SetBool("isRun", true);
        }
    }
    void moveJoyStick()
    {
        //float v = Joystick.Vertical;
        //float h = Joystick.Horizontal;

        //Vector3 translate = (new Vector3(h, 0, v) * Time.deltaTime) * Speed;
        //transform.Translate(translate);

        
    }
    public void StartJump()
    {
        StartCoroutine(c_StartJump());
    }
    void directionSetting()
    {

        Vector3 side_Left = new Vector3(-4f, 0f, transform.position.z);
        Vector3 side_Middle = new Vector3(0f, 0f, transform.position.z);
        Vector3 side_Right = new Vector3(4f, 0f, transform.position.z);
        if (laneNum == 0)
            transform.position = Vector3.Lerp(transform.position, side_Left, 0.1f);
        else if (laneNum == 1)
            transform.position = Vector3.Lerp(transform.position, side_Middle, 0.1f);
        else if (laneNum == 2)
            transform.position = Vector3.Lerp(transform.position, side_Right, 0.1f);   
    }
    private void OnTriggerEnter(Collider other) 
    {

        if (other.tag == ("EnemyC"))
        {
            isCollision = true;
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.tag ==("EnemyC"))
        {
            isCollision = false;
        }
    }
    IEnumerator stopSlide()
    {
        yield return new WaitForSeconds(0.3f);
        horizonVel = 0;
        controlLocked = "n";
    }

    IEnumerator PositionSetting()
    {
        //transform.position = Vector3.Lerp(transform.position, StartPosition, Time.deltaTime * 1.2f );
        Debug.Log("Player_PositionSetting");
        //transform.localPosition = Vector3.zero;
        //moveCharacter.transform.position = StartPosition;
        theRotation.TurnUp();
        //isPositionSetting = true;
        yield return null;
    }
    IEnumerator c_StartJump()
    {
        transform.position = Vector3.Lerp(transform.position, JumpPos, 1.5f);
        yield return new WaitForSeconds(1f);
        //transform.eulerAngles = new Vector3(90f, 0,0);
        // system2 시작?
        Debug.Log("부스터를 시작한다");

    }

    
}
