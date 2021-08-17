using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] PlayerManager playerManager;
    [SerializeField] float movementSpeed = 3f;
    [SerializeField] float controlSpeed = 5f;

    // touch
    [SerializeField] bool isTouching;
    float touchPosX;
    Vector3 direction;
    // Start is called before the first frame update
    private void Awake()
    {
        playerManager = FindObjectOfType<PlayerManager>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        if (Input.GetMouseButtonDown(0))
            Debug.Log("Å¬¸¯Áß");
    }
    private void FixedUpdate()
    {
        if (playerManager.playerState == PlayerManager.PlayerState.System1)
        {
            System1Input();
        }
        else if (playerManager.playerState == PlayerManager.PlayerState.Move)
        {
            transform.position = new Vector3(touchPosX, transform.position.y, transform.position.z);
            transform.position += Vector3.forward * movementSpeed * Time.fixedDeltaTime;
            GetInput();
            if (isTouching)
            {
                touchPosX += Input.GetAxis("Mouse X") * controlSpeed * Time.fixedDeltaTime;
            }
        }

        
    }


    void System1Input()
    {
        Debug.Log("system1input");
    }
    void GetInput()
    {
        if (Input.GetMouseButton(0))
        {
            isTouching = true;

        }
        else
        {
            isTouching = false;
        }
    }
}
