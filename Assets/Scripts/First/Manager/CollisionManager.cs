using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    public GameObject g_Character1;
    public GameObject g_Character2;
    Manager theManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Check(g_Character1, g_Character2);
    }

    
    void Check(GameObject g_C1, GameObject g_C2)
    {
           
    }
}   
