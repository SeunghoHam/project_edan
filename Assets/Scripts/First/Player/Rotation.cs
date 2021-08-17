using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] private GameObject Character;

    private Quaternion Left = Quaternion.identity;
    private Quaternion Right = Quaternion.identity;
    private Quaternion Up = Quaternion.identity;
    private Quaternion Down = Quaternion.identity;
    private Quaternion FlyReady = Quaternion.identity;
    private Quaternion Flying = Quaternion.identity;
    private Quaternion Finish = Quaternion.identity;
    private Quaternion f_Left = Quaternion.identity;
    private Quaternion f_Right = Quaternion.identity;
    

    


    
    public void TurnLeft()
    {
        Left.eulerAngles = new Vector3(0, -90f,0);
        Character.transform.rotation = Quaternion.Lerp(Character.transform.rotation, Left, Time.deltaTime * 3.0f);
    }
    public void TurnRight()
    {
        Right.eulerAngles = new Vector3(0, 90f, 0);
        Character.transform.rotation = Quaternion.Lerp(Character.transform.rotation, Right, Time.deltaTime * 3.0f);
    }
    public void TurnDown()
    {
        Down.eulerAngles = new Vector3(0, 180f, 0);
        Character.transform.rotation = Quaternion.Lerp(Character.transform.rotation, Down, Time.deltaTime * 3.0f);
    }
    public void TurnUp()
    {
        Up.eulerAngles= new Vector3(0,0,0);
        Character.transform.rotation = Quaternion.Lerp(Character.transform.rotation, Up,Time.deltaTime * 3.0f);
    }
    public void Fly_Ready()
    {
        FlyReady.eulerAngles = new Vector3(40f, 0 ,0);
        Character.transform.rotation = Quaternion.Lerp(Character.transform.rotation, FlyReady ,Time.deltaTime * 3.0f);
    }
    public void Fly_Ing()
    {
        Flying.eulerAngles  = new Vector3(80f, 0, 0);
        Character.transform.rotation = Quaternion.Lerp(Character.transform.rotation, Flying, 1f);
    }
    public void fly_Left()
    {
        f_Left.eulerAngles = new Vector3(0, 30f,0);
        Character.transform.rotation = Quaternion.Lerp(Character.transform.rotation, f_Left, 3.0f * Time.deltaTime);
    }
    public void fly_Right()
    {
        f_Right.eulerAngles = new Vector3(80f, -30f,0);
        Character.transform.rotation = Quaternion.Lerp(Character.transform.rotation, f_Right, 1f);
    }
    
}
