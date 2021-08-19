using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStickMovement : MonoBehaviour
{

    private static JoyStickMovement instance;
    public GameObject smallStick;
    public GameObject BGStick;
    Vector3 stickFirstPosition;
    public Vector3 joyVec;
    Vector3 joyStickFirstPosition;
    float stickRadius;

    public float joymoveSpeed;
    public static JoyStickMovement Instance // singlton
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<JoyStickMovement>();
                if(instance == null)
                {
                    var instanceContainer = new GameObject ("JoyStickMovement");
                    instance = instanceContainer.AddComponent<JoyStickMovement>();
                }
            }
            return instance;
        }
    }
    void Start()
    {
        stickRadius = BGStick.gameObject.GetComponent<RectTransform>().sizeDelta.y / 4;
        joyStickFirstPosition = BGStick.transform.position;
        joymoveSpeed = 5f;
    }

    public void PointDown() // call = Screentocuh
    {
        BGStick.gameObject.SetActive(true);
        BGStick.transform.position= Input.mousePosition;
        smallStick.transform.position = Input.mousePosition;
        stickFirstPosition = Input.mousePosition;
    }
    public void Drag(BaseEventData baseEventData)
    {
        
        PointerEventData pointerEventData = baseEventData as PointerEventData;
        Vector3 DragPosition = pointerEventData.position;
        joyVec = (DragPosition - stickFirstPosition).normalized;


        
        float stickDistance = Vector3.Distance ( DragPosition, stickFirstPosition);
        Debug.Log(joyVec.x + "  " + joyVec.y);
        if(stickDistance < stickRadius)
        {
            smallStick.transform.position = stickFirstPosition + joyVec * stickDistance;
        }
        else
        {
            smallStick.transform.position = stickFirstPosition + joyVec * stickRadius;
        }
    }
    public void Drop()
    {
        joyVec = Vector3.zero;
        BGStick.gameObject.SetActive(false);

        //BGStick.transform.position= joyStickFirstPosition;
        //smallStick.transform.position = joyStickFirstPosition;
    }
}
