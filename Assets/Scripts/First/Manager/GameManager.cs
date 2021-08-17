using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;

    public bool mode_system1;
    public bool mode_system2;
    public bool mode_system3;
    public bool mode_system4;
    public bool mode_system5;

    public bool canSteal = true;
    public bool raceFinish = false;

    public float normalSpeed = 8.0f;
    public float boostSpeed = 20.0f;
    private void Awake()
    {
        if(null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public static GameManager Instance
    {
        get
        {
            if(null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        mode_system1 = true;
        mode_system2 = false;
        mode_system3 = false;    
        mode_system4 = false;
        mode_system5 = false;
    }

    public void setSystem2()
    {
        mode_system1 = false;
        mode_system2 = true;
        mode_system3 = false;
        mode_system4 = false;
        mode_system5 = false;
    }
}
