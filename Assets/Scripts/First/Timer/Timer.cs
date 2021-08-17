using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] Material skybox_Day;
    [SerializeField] private float time_Start;
    private float time_Current;
    private float time_End;
    private bool isEnded;

 
    [SerializeField] Slider sliderTimer;


    //*****call reference*****
    Player thePlayer; 
    CameraManager theCamera;
    void Start()
    {
        time_Start = 30f;
        thePlayer = FindObjectOfType<Player>();
        theCamera = FindObjectOfType<CameraManager>();

        timer_Reset();
        //RenderSettings.skybox = skybox_Day;
        
    }

    // Update is called once per frame
    void Update()
    {
        

        if(isEnded)
            return; 

        timer_Check();

        sliderTimer.value = time_Current / time_Start;
    }
    
    void timer_Check()
    {
        time_Current = time_Start - Time.time;
        if(time_Current > time_End) // 현재 시간 > 종료시간 ( 0 )
        {
            //Debug.Log(time_Current);
        }
        else if(!isEnded)
        {
            timer_End();
        }
    }

    void timer_End()
    {
        //Debug.Log("timer_end");
        time_Current = time_End;
        isEnded = true;

        //GameManager.Instance.mode_system1 = false;
        //GameManager.Instance.mode_system3 = true;
    }


    void timer_Reset()
    {
        time_End = 0f;
        isEnded = false;
        time_Current = time_Start;
    }

    IEnumerator SkyboxChnage()
    {

        yield return null;
    }
}