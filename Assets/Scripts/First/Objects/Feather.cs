using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Feather : MonoBehaviour
{
    Manager theManager;

    private Rigidbody rigid;

    private int JumpPower = 3;

    WaitForSeconds delay = new WaitForSeconds(0.5f);
    //WaitForSeconds delay2 = new WaitForSeconds(0.6f);
    // ***** Particle
    public ParticleSystem p_FeatherDrop;
    NewPlayerController theNewPlayer;
    Enemy theEnemy;
    private void Start()
    {
        theManager = FindObjectOfType<Manager>();
        rigid = GetComponent<Rigidbody>();
        theNewPlayer = FindObjectOfType<NewPlayerController>();
        theEnemy = FindObjectOfType<Enemy>();
        p_FeatherDrop.Stop();
    }
    private void Update()
    {
        if (GameManager.Instance.mode_system2) // system change  -> cleaning
            Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other) //feather colllider
    {
        if (other.CompareTag("Enemy") && theEnemy.canGet)
        {
            //p_FeatherDrop.Play();
            StartCoroutine(EnemyFeatherPickUp());
        }

        if (other.CompareTag("Player") && theNewPlayer.canGet)
        {
            //p_FeatherDrop.Play();
            StartCoroutine(PlayerFeatherPickUp());
        }

    }
    void PickUp()
    {
        //transform.position = new Vector3(transform.position.x, 2 * Time.deltaTime,transform.position.z);
        rigid.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
    }
    IEnumerator PlayerFeatherPickUp()
    {
        yield return delay;
        theManager.player_IncreaseFeather(1);
        PickUp();
        yield return delay;
        Destroy(gameObject);
    }
    IEnumerator EnemyFeatherPickUp()
    {
        yield return delay;
        theManager.enemy_IncreaseFeather(1);
        PickUp();
        yield return delay;
        Destroy(gameObject);
    }

}
