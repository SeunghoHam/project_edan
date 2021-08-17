using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatherManager : MonoBehaviour
{
    public Animator animator;

    void Start() 
    {
        animator.SetBool("Flying", false);
    }
    private void Update() 
    {
    
    }

}
