using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] GameObject text_Coin;
    [SerializeField] GameObject g_Tutorial;
    [SerializeField] GameObject g_Gauge;
    [SerializeField] GameObject g_Result;
    [SerializeField] GameObject g_Current1;
    [SerializeField] GameObject g_Current2;

    public bool isTutorial;


    bool fingerRight;
    bool fingerLeft;



    Player thePlayer;
    Manager theManager;


    // Start is called before the first frame update
    void Start()
    {
       
        thePlayer = FindObjectOfType<Player>();
        theManager = FindObjectOfType<Manager>();
        // start = state_normal
        setSystem1();
        isTutorial = false;
       
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.mode_system1)
        {
            setSystem1();
        }
        else if(GameManager.Instance.mode_system2)
        {
            setSystem2();
        }
        else if(GameManager.Instance.mode_system3)
        {
            setSystem3();
        }
        else if(GameManager.Instance.mode_system5)
        {
            setSystem5();
        }

        

    }


    void setSystem1()
    {
        g_Current1.SetActive(true);
        g_Current2.SetActive(true);

        g_Tutorial.SetActive(false);
        text_Coin.SetActive(true);
        g_Result.SetActive(false);
    }
    void setSystem3()
    {
        g_Gauge.SetActive(false);

    }
    void setSystem2()
    {
        g_Tutorial.SetActive(false);

        text_Coin.SetActive(true);

        
        //if(tutoShow == true)
        //   return;
            
        //StartCoroutine(ShowTutorial());

    }
    void setSystem5()
    {
        g_Result.SetActive(true);
        g_Current1.SetActive(false);
        g_Current2.SetActive(false);
    }
    void ShowResult()
    {
        g_Result.SetActive(true);
    }

    public void RaceStart()
    {
        thePlayer.StartJump();
        //thePlayer.mode_Wait = false;
        //thePlayer.mode_Race = true;
    }
    public void button_System2()
    {
        GameManager.Instance.mode_system3 = false;
        GameManager.Instance.mode_system2 = true;
    }
}
