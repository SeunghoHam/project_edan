using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FIrstPlayer : MonoBehaviour
{ 
    [SerializeField] PlayerManager playerManager;

    private Rigidbody rigid;

    [SerializeField] bool isGround;

    private void Awake()
    {
        playerManager = FindObjectOfType<PlayerManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        GetComponent<Renderer>().material = playerManager.collectedObjMat;

        playerManager.collidedList.Add(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Grounded();
        }
    }

    void Grounded()
    {
        isGround = true;
        //playerManager.playerState = PlayerManager.PlayerState.Move;
        playerManager.playerState = PlayerManager.PlayerState.System1;

        rigid.useGravity = false;
        rigid.constraints = RigidbodyConstraints.FreezeAll;

        Destroy(this, 1f);
    }
}