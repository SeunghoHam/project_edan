using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstaticle : MonoBehaviour
{
    Player thePlayer;
    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PlayerC")
        {
            thePlayer.isDead = true;
        }
    }
}
