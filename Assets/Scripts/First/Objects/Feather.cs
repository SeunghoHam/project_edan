using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Feather : MonoBehaviour
{
    Manager theManager;

    private Rigidbody rigid;

    private int JumpPower = 3;
    

    // ***** Particle
    public ParticleSystem p_FeatherDrop;
    PlayerController thePlayer;
    NewPlayerController theNewPlayer;
    private void Start()
    {
        theManager = FindObjectOfType<Manager>();
        thePlayer = FindObjectOfType<PlayerController>();
        rigid = GetComponent<Rigidbody>();
        theNewPlayer = FindObjectOfType<NewPlayerController>();
        p_FeatherDrop.Stop();
    }
    private void Update()
    {
        if(GameManager.Instance.mode_system2) // system change  -> cleaning
            Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other) //feather colllider
    {
        if(other.CompareTag("Enemy"))
        {
            p_FeatherDrop.Play();
            StartCoroutine(EnemyFeatherPickUp());
        }
        if(thePlayer.canGet)
        {
            if (other.CompareTag("Player"))
            {
                p_FeatherDrop.Play();
                StartCoroutine(PlayerFeatherPickUp());
            }
        }
        //if(theNewPlayer.)
      
    }
    void PickUp()
    {
        //transform.position = new Vector3(transform.position.x, 2 * Time.deltaTime,transform.position.z);
        rigid.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
    }
    IEnumerator PlayerFeatherPickUp()
    {
        theManager.player_IncreaseFeather(1);
        PickUp();
        yield return new WaitForSeconds(0.6f);
        Destroy(gameObject);
    }
    IEnumerator EnemyFeatherPickUp()
    {
        theManager.enemy_IncreaseFeather(1);
        PickUp();
        yield return new WaitForSeconds(0.6f);
        Destroy(gameObject);
    }

}
