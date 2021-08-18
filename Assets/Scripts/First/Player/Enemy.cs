using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public GameObject girl;
    public bool canMove = false;
    public bool canGet = true;
    public bool enemySystem = false;
    private bool canPickup = true;

    private bool isMoving;

    Animator theAnimator;
    Vector3 StartPosition;
    Vector3 vel = Vector3.zero;
    public Transform g_StartPosition;

    public ParticleSystem JumpEffect;
    public ParticleSystem GetFeatherItem;
    public ParticleSystem GetCoinItem;
    public ParticleSystem Steal;

    private float x, y, z;

    WaitForSeconds boost = new WaitForSeconds(2f);
    WaitForSeconds stealWait = new WaitForSeconds(1f);
    WaitForSeconds slideDelay = new WaitForSeconds(0.3f);


    private Quaternion Left = Quaternion.identity;
    private Quaternion Right = Quaternion.identity;
    private Quaternion Up = Quaternion.identity;
    private Quaternion Down = Quaternion.identity;
    private Quaternion Flying = Quaternion.identity;

    Manager theManager;
    PositionManager positionManager;
    FollowerEnemy theFollowerEnemy;
    RotationEnemy theRotation;

    // ***** Swipe
    private float horizonVel;
    private string controlLocked = "n";

    // ****** AI
    public GameObject Target;
    public float Range = 15f;
    WaitForSeconds pickupDelay = new WaitForSeconds(1f);

    Rigidbody rigid;
    BoxCollider boxCollider;
    NavMeshAgent agent;



    void Awake()
    {
        theAnimator = transform.GetChild(0).GetComponent<Animator>();
        theManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>();
        positionManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<PositionManager>();
        theFollowerEnemy = FindObjectOfType<FollowerEnemy>();
        theRotation = FindObjectOfType<RotationEnemy>();


        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        StartPosition = g_StartPosition.position;
        x = transform.position.x;
        y = transform.position.y;
        z = transform.position.z;
        //InvokeRepeating("UpdateTarget", 0f, 0.2f);
        //theAnimator.SetBool("Move", true);
    }

    // Update is called once per frame
    void Update()
    {
        AI();

        UpdateTarget();
        if (canMove == false)
        {
            //if(!canMove)
            if (GameManager.Instance.mode_system1)
            {
                theAnimator.SetBool("Move", true);
                isMoving = true;
                //transform.position = new Vector3(x, y, z);
                if (Input.GetKey(KeyCode.I))
                {
                    //transform.localPosition += Vector3.forward;
                    //transform.localPosition = Vector3.forward * Time.deltaTime;
                    z += Time.deltaTime * 5;
                    //theRotation.TurnUp();
                    TurnUp();
                }
                else if (Input.GetKey(KeyCode.K))
                {
                    //transform.localPosition += Vector3.back;
                    z -= Time.deltaTime * 5;
                    //theRotation.TurnDown();
                    TurnDown();
                }
                else if (Input.GetKey(KeyCode.J))
                {
                    //transform.localPosition += Vector3.left;
                    x -= Time.deltaTime * 5;
                    //theRotation.TurnLeft();
                    TurnLeft();
                }
                else if (Input.GetKey(KeyCode.L))
                {
                    //transform.localPosition += Vector3.right;
                    x += Time.deltaTime * 5;
                    //eRotation.TurnRight();
                    TurnRight();
                }
            }

        }
        if (enemySystem == true && canMove == false && GameManager.Instance.raceFinish == false)
            PosSet();
        if (canMove == true) // system2?
        {
            JumpEffect.Stop();

            //theRotation.Fly_Ing();
            Fly_ing();
            setBooster();
            Swipe();

        }

        if (GameManager.Instance.raceFinish)
            theAnimator.SetBool("Flying", false);


    }

    void AI()
    {
        //agent.SetDestination(target)
    }
    void FreezeVelocity() // AI movement is not used rigidbody
    {
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
    }

    private void OnDrawGizmosSelected()
    {
        // Gizmos.color = Color.red;
        // Gizmos.DrawSphere(this.gameObject.transform.position, Range);   

    }
    void UpdateTarget()
    {
            if (Target != null)
            {

                //canPickup = false;
                transform.position = Vector3.Lerp(transform.position, Target.transform.position, 0.01f);
                //StartCoroutine(PickupDelay());
            }
            if (Target == null)
            {
                GameObject[] Feathers = GameObject.FindGameObjectsWithTag("Feather");
                float shortestDistance = Mathf.Infinity;
                GameObject nearFeather = null;

                foreach (GameObject Feather in Feathers)
                {
                    float DistanceToFeather = Vector3.Distance(transform.position, Feather.transform.position);

                    if (DistanceToFeather < shortestDistance)
                    {
                        shortestDistance = DistanceToFeather;
                        nearFeather = Feather;
                    }

                }

                if (nearFeather != null && shortestDistance <= Range)
                {
                    Target = nearFeather;
                    //transform.Translate(nearFeather.transform.position);
                    //transform.position = Vector3.SmoothDamp(transform.position, Target.transform.position, ref vel, 1f);
                    Debug.Log("야 적 주워!");
                }
                else
                    Target = null;
            }
        
    }
    #region RotateTurn
    public void TurnLeft()
    {
        Left.eulerAngles = new Vector3(0, -90f, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, Left, 3.0f * Time.deltaTime);

    }
    public void TurnRight()
    {
        Right.eulerAngles = new Vector3(0, 90f, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, Right, Time.deltaTime * 3.0f);

    }
    public void TurnDown()
    {
        Down.eulerAngles = new Vector3(0, 180f, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, Down, Time.deltaTime * 3.0f);

    }
    public void TurnUp()
    {
        Up.eulerAngles = new Vector3(0, 0, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, Up, Time.deltaTime * 3.0f);

    }
    public void Fly_ing()
    {
        Flying.eulerAngles = new Vector3(80f, 0, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, Flying, Time.deltaTime * 3.0f);
    }
    #endregion


    void Swipe()
    {
        girl.transform.position = new Vector3(horizonVel, transform.position.y, transform.position.z);
        //girl.transform.position = new Vector3(horizonvel, transform.position.y, transform.position.z);
        //if (Input.GetKeyDown(KeyCode.J) && (controlLocked == "n"))
        if (Input.GetKeyDown(KeyCode.J))
        {
            //moveLeft();
            horizonVel += -2f;
        }
        //else if (Input.GetKeyDown(KeyCode.L) && (controlLocked == "n"))
        if (Input.GetKeyDown(KeyCode.L))
        {
            //moveRight();
            horizonVel += 2f;
        }
    }
    void setPosition()
    {
        transform.position = Vector3.SmoothDamp(transform.position, StartPosition, ref vel, 1f);
        TurnUp();

    }
    void moveLeft()
    {

        theAnimator.SetTrigger("f_Left");
        //horizonVel += -2f;
        horizonVel = -2f;
        StartCoroutine(stopSlide());
        controlLocked = "y";
    }

    void moveRight()
    {
        theAnimator.SetTrigger("f_Right");
        StartCoroutine(stopSlide());
        //horizonVel += 2f;
        horizonVel = 2f;
        controlLocked = "y";
    }
    private void OnTriggerEnter(Collider other)
    {
        if (canGet)
        {
            if (other.gameObject.CompareTag("Feather"))
            {
                theAnimator.SetTrigger("PickUp");
            }
        }
    }
    void PosSet()
    {
        StartCoroutine(PositionSetting());
        //theRotation.Fly_Ready();
        theAnimator.SetBool("Move", false);
        theAnimator.SetBool("Flying", true);

    }
    IEnumerator PickupDelay()
    {
        yield return pickupDelay;
        canPickup = true;
    }
    IEnumerator PositionSetting()
    {
        Fly_ing();
        JumpEffect.Play();
        transform.position = Vector3.SmoothDamp(transform.position, StartPosition, ref vel, 1f);
        yield return new WaitForSeconds(2f);
        //canMove = true;
    }
    IEnumerator stopSlide()
    {
        yield return slideDelay;
        //horizonVel = 0;

        controlLocked = "n";
    }
    IEnumerator stealDelay()
    {
        yield return stealWait;
        GameManager.Instance.canSteal = true;
    }
    void setBooster()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            theManager.enemy_DecreaseFeather(3);
            StartCoroutine(Booster());
        }
    }
    IEnumerator Booster()
    {
        theFollowerEnemy.moveSpeed = 20f;
        yield return boost;
        theFollowerEnemy.moveSpeed = 8f;
    }
}
