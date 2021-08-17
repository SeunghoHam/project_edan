using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    Player thePlayer;
    private void Start()
    {
        
    }
    public void button_Restart()
    {
        Debug.Log("히히 동작 안해");
        //thePlayer.mode_Normal = true;
        //thePlayer.mode_Race = false;

        //SceneManager.LoadScene("firstGame");
        /*
        thePlayer.mode_Normal = true;
        thePlayer.mode_Wait = false;
        thePlayer.mode_Race =false;*/
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        

    }
}
