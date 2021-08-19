using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaugeManager : MonoBehaviour
{
    public Slider sliderFeatherCount;

    Manager theManger;
    private void Awake()
    {
        theManger = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        sliderFeatherCount.value = theManger.currentPlayerFeather /  theManger.maxFeather;
    }
}
