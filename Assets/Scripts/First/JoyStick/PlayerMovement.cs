using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<PlayerMovement>();
                if(instance == null)
                {
                    var instanceContainer = new GameObject ("PlayerMovement");
                    instance = instanceContainer.AddComponent<PlayerMovement>();
                    
                }
            }
            return instance;
        }
    }
    private static PlayerMovement instance;
    Rigidbody rigid;
    private float moveSpeed = 1000f;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        //rigid = transform.GetChild(0).GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        
        rigid.velocity = new Vector3(moveHorizontal * moveSpeed, rigid.velocity.y, moveVertical * moveSpeed);
        if(JoyStickMovement.Instance.joyVec.x != 0 || JoyStickMovement.Instance.joyVec.z != 0)
        {
            rigid.velocity = new Vector3(JoyStickMovement.Instance.joyVec.x, rigid.velocity.y, JoyStickMovement.Instance.joyVec.y);
            rigid.rotation =Quaternion.LookRotation(new Vector3(JoyStickMovement.Instance.joyVec.x ,0, JoyStickMovement.Instance.joyVec.y));
        }
    }
    /*
    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        
        rigid.velocity = new Vector3(moveHorizontal * moveSpeed, rigid.velocity.y, moveVertical * moveSpeed);
        if(JoyStickMovement.Instance.joyVec.x != 0 || JoyStickMovement.Instance.joyVec.z != 0)
        {
            rigid.velocity = new Vector3(JoyStickMovement.Instance.joyVec.x, rigid.velocity.y, JoyStickMovement.Instance.joyVec.y);
            rigid.rotation =Quaternion.LookRotation(new Vector3(JoyStickMovement.Instance.joyVec.x ,0, JoyStickMovement.Instance.joyVec.y));
        }
        
    }*/
}
