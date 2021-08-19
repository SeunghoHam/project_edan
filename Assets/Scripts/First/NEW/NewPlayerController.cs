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
    //JoystickManager theJoyStickManager;
    private float inputX;
    private float inputZ;
    private bool canMove = true;
    private float joymoveSpeed = 5f;
    Rigidbody rigid;


    // ***** settingPosiiton
    Vector3 vel = Vector3.zero;
    public Transform g_StartPosition;
    Vector3 StartPosition;
    public Transform g_FinishPosition;
    private Quaternion turnUp = Quaternion.identity;


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
    private static PlayerMovement instance;

    void Awake()
    {
        theAnimator = transform.GetChild(0).GetComponent<Animator>();
        theManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>();
        thePositionManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<PositionManager>();
        theRotation = GameObject.FindGameObjectWithTag("Manager").GetComponent<Rotation>();
        swipeCharacter = transform.GetChild(0).GetComponent<GameObject>();
        theCameraShake = FindObjectOfType<CameraShake>();
        rigid = GetComponent<Rigidbody>();
    }
    void Start()
    {
        StartPosition = g_StartPosition.position;
        joymoveSpeed = JoyStickMovement.Instance.joymoveSpeed;
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
            //moveJoystick();
            if(canMove)
                Move();
            if (Input.GetKeyDown(KeyCode.K))
            {
                theManager.player_IncreaseFeather(1);
            }
        }
        else if (GameManager.Instance.mode_system2 == true)
        {
            theAnimator.SetBool("Flying", true);
            transform.Translate(transform.forward * moveSpeed * Time.deltaTime);
            Swipe();
            if (Input.GetKey(KeyCode.L))
            {
                theAnimator.SetBool("Falling", true);
                rigid.useGravity = false;
                //rigid.AddForce()
            }
            else
            {
                theAnimator.SetBool("Falling", false);
            }
            
        }
        else if (GameManager.Instance.mode_system3 == true)
        {
            canGet = false;
            theAnimator.SetBool("Flying", true);
            setPosition();
            if (Input.GetKeyDown(KeyCode.J))
            {
                GameManager.Instance.setSystem2();
            }
        }



    }

    private void FixedUpdate()
    {
        if(GameManager.Instance.mode_system2 == true)
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

        if (other.CompareTag("setSystem2"))
        {
            theAnimator.SetBool("Move", false);
            canMove = false;
            setRot();
            StartCoroutine(setSystem2());
        }

    }
    IEnumerator setSystem2()
    {

        yield return new WaitForSeconds(1f);
        GameManager.Instance.mode_system1 = false;
        GameManager.Instance.mode_system2 = true;
        yield return null;

    }
    void setRot()
    {
        this.rigid.useGravity = false;
        this.rigid.mass = 1;
        turnUp.eulerAngles = new Vector3(0, 0, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, turnUp, 1);
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
