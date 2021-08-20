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
    WaitForSeconds flying = new WaitForSeconds(1f);
    // ***** JoyStick
    //JoystickManager theJoyStickManager;
    private float inputX;
    private float inputZ;
    private bool canMove = true;
    private float joymoveSpeed = 5f;
    Rigidbody rigid;

    private bool system2Flying = true;


    // ***** settingPosiiton
    Vector3 vel = Vector3.zero;
    public Transform tf_FinishPosition;

    public Transform tf_WingCenter;
    private Quaternion up = Quaternion.identity;
    private Quaternion left = Quaternion.identity;
    private Quaternion right = Quaternion.identity;

    // ***** Particle
    public ParticleSystem speedEffect;
    public ParticleSystem upperEffect;

    // ***** Setting 
    private bool issetPosition;
    private bool finishsetPosition;

    public bool canGet = true;
    // ***** Time
    private float timeLeft = 1.0f;
    private float timeNext = 0.0f;


    // ***** Floating
    [SerializeField] GameObject text_multipler;
    // ***** Reference
    CameraShake theCameraShake;
    Rotation theRotation;
    Animator theAnimator;
    Manager theManager;
    PositionManager thePositionManager;
    ScoreManager theScoreManager;
    CoinManager theCoinManager;

    /*
    public static PlayerMovement Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PlayerMovement>();
                if (instance == null)
                {
                    var instanceContainer = new GameObject("PlayerMovement");
                    instance = instanceContainer.AddComponent<PlayerMovement>();

                }
            }
            return instance;
        }
    }
    private static PlayerMovement instance;*/

    void Awake()
    {
        theAnimator = transform.GetChild(0).GetComponent<Animator>();
        theManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>();
        thePositionManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<PositionManager>();
        theRotation = GameObject.FindGameObjectWithTag("Manager").GetComponent<Rotation>();
        theScoreManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<ScoreManager>();
        theCoinManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<CoinManager>();
        swipeCharacter = transform.GetChild(0).GetComponent<GameObject>();
        theCameraShake = FindObjectOfType<CameraShake>();
        rigid = GetComponent<Rigidbody>();
    }
    void Start()
    {
        //StartPosition = g_StartPosition.position;
        joymoveSpeed = JoyStickMovement.Instance.joymoveSpeed;
        up.eulerAngles = new Vector3(0, 0, 0);

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(theCameraShake.Shake(0.15f, 4f));
        }
        if (GameManager.Instance.mode_system1 == true)
        {
            if (canMove)
                Move();
            if (Input.GetKeyDown(KeyCode.K))
            {
                theManager.player_IncreaseFeather(1);
            }
        }
        else if (GameManager.Instance.mode_system2 == true)
        {
            if (system2Flying)
            {
                speedEffect.Play();
                theAnimator.SetBool("Flying", true);
                Fly();
                transform.Translate(transform.forward * moveSpeed * Time.deltaTime);
                transform.Translate(Vector3.down * Time.deltaTime);
                if (Input.GetKey(KeyCode.L))
                {
                    theAnimator.SetBool("Falling", true);
                }
                else
                {
                    theAnimator.SetBool("Falling", false);
                }
            }



        }
        else if (GameManager.Instance.mode_system3 == true)
        {
            canGet = false;
            theAnimator.SetBool("Flying", true);
            setRot();
            if (Input.GetKeyDown(KeyCode.J))
            {
                GameManager.Instance.setSystem2();
            }
        }




    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.mode_system2 == true)
        {
            this.rigid.useGravity = true;
        }
    }
    void Move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        //rigid.velocity = new Vector3(moveHorizontal * joymoveSpeed, rigid.velocity.y, moveVertical * joymoveSpeed);
        if (JoyStickMovement.Instance.joyVec.x != 0 || JoyStickMovement.Instance.joyVec.z != 0)
        {
            theAnimator.SetBool("Move", true);


            rigid.velocity = new Vector3(JoyStickMovement.Instance.joyVec.x * joymoveSpeed,
    rigid.velocity.y,
    JoyStickMovement.Instance.joyVec.y * joymoveSpeed);

            rigid.rotation = Quaternion.LookRotation(new Vector3(JoyStickMovement.Instance.joyVec.x,
            0,
             JoyStickMovement.Instance.joyVec.y));
        }
        else
        {
            theAnimator.SetBool("Move", false);
        }
    }
    /*
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
    }*/

    void Fly()
    {

        if (JoyStickMovement.Instance.joyVec.x > 0.5f)
        {
            right.eulerAngles = new Vector3(20f, 0, 0);
            transform.Rotate(new Vector3(0, 20f, 0) * Time.deltaTime);
        }
        else if (JoyStickMovement.Instance.joyVec.x < -0.5f)
        {
            transform.Rotate(new Vector3(0, -20f, 0) * Time.deltaTime);
        }

        if ((JoyStickMovement.Instance.joyVec.y > 0.5f) && theManager.currentPlayerFeather >= 1)
        {
            transform.Translate(Vector3.up * 10 * Time.deltaTime);
            if (Time.time > timeNext)
            {
                timeNext = Time.time + timeLeft;
                theManager.player_DecreaseFeather(1);
                theAnimator.SetBool("Falling", true);
                upperEffect.Play();
            }

            //StartCoroutine(DecreaseFeather());
        }
        else
        {
            //theAnimator.SetBool("Falling", false);
            upperEffect.Stop();
        }
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
            if (other.gameObject.CompareTag("Feather"))
            {
                theManager.player_IncreaseFeather(1);
                canMove = false;
                theAnimator.SetTrigger("PickUp");
                StartCoroutine(PickupDelay());
            }
        }

        if (other.gameObject.CompareTag("setSystem2"))
        {
            theAnimator.SetBool("Move", false);
            canMove = false;
            setRot();
            StartCoroutine(setSystem2());
        }
        int i;
        if(other.gameObject.CompareTag("Multipler"))
        {
            for(i=0; i< 2; i++)
            {
                Debug.Log("multiper collision");
                theScoreManager.distanceScore *= 2;
            }

        }
        i =0;
        if (other.gameObject.CompareTag("RaceFinish"))
        {
            setsystem3();
        }
        if(other.gameObject.CompareTag("Coin"))
        {
            theCoinManager.AddCoins(other.transform.position, 1);
        }
    }
    void setsystem3()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, up, 1f);
        system2Flying = false;
        transform.position = Vector3.Lerp(transform.position,tf_FinishPosition.transform.position, 1f);
        //transform.position = Vector3.SmoothDamp(transform.position, tf_FinishPosition.transform.position, ref vel, 1f);
        speedEffect.Stop();
        upperEffect.Stop();
        theAnimator.SetBool("Flying", false);
        
    }
    IEnumerator floatingtextAnim()
    {
        
        yield return new WaitForSeconds(0.3f);
    }
    IEnumerator DecreaseFeather()
    {
        theManager.player_DecreaseFeather(1);
        yield return flying;
    }
    IEnumerator setSystem2()
    {
        GameManager.Instance.mode_system1 = false;
        GameManager.Instance.mode_system2 = true;
        yield return null;
    }
    void setRot()
    {
        
        transform.rotation = Quaternion.Lerp(transform.rotation, up, 1);
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
