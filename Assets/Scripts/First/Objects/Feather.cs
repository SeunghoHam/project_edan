using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Feather : MonoBehaviour
{
    Manager theManager;

    Rigidbody rigid;

    private int JumpPower = 3;

    WaitForSeconds delay = new WaitForSeconds(0.5f);
    NewPlayerController theNewPlayer;
    Enemy theEnemy;
    private void Start()
    {
        theManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>();
        rigid = GetComponent<Rigidbody>();
        theNewPlayer = FindObjectOfType<NewPlayerController>();
        theEnemy = FindObjectOfType<Enemy>();
    }
    private void Update()
    {
        //if (GameManager.Instance.mode_system2) // system change  -> cleaning
        //    Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other) //feather colllider
    {
        if (other.gameObject.CompareTag("Enemy") && theEnemy.canGet)
        {
            StartCoroutine(FeatherPickUp());
        }

        if (other.gameObject.CompareTag("Player"))
        {
            //StartCoroutine(FeatherPickUp());
            //PickUp();
            Destroy(gameObject);
        }

    }
    void PickUp()
    {
        //transform.position = new Vector3(transform.position.x, 2 * Time.deltaTime,transform.position.z);
        rigid.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
        //transform.position = Vector3.Slerp(transform.position, theNewPlayer.tf_WingCenter.position, 1f);
    }
    IEnumerator FeatherPickUp()
    {
        PickUp();
        yield return delay;
        Destroy(gameObject);
    }
}
