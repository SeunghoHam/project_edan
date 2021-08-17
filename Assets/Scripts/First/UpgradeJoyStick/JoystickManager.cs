using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class JoystickManager : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private Image imageJoyStickBG;
    private Image imageJoyStick;

    private Vector2 posInput;

    private void Start() 
    {
        imageJoyStickBG= GetComponent<Image>();
        imageJoyStick = transform.GetChild(0).GetComponent<Image>();
    }



    public void OnDrag(PointerEventData eventData)
    {
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(
            imageJoyStickBG.rectTransform, 
            eventData.position, 
            eventData.pressEventCamera, 
            out posInput))
        {
            posInput.x = posInput.x / (imageJoyStickBG.rectTransform.sizeDelta.x);
            posInput.y = posInput.y / (imageJoyStickBG.rectTransform.sizeDelta.y);


            // normalize
            if(posInput.magnitude > 1.0f)
            {
                posInput = posInput.normalized;
            }


            // joystick move
            imageJoyStick.rectTransform.anchoredPosition = new Vector2(posInput.x * (imageJoyStickBG.rectTransform.sizeDelta.x / 2) , 
                                                                        posInput.y * (imageJoyStickBG.rectTransform.sizeDelta.y / 2));
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        posInput = Vector2.zero;
        imageJoyStick.rectTransform.anchoredPosition = Vector2.zero;
    }

    public float inputhorizontal()
    {
        if(posInput.x != 0)
            return posInput.x;
        else 
            return  Input.GetAxis("Horizontal");
    }

    public float inputVertical()
    {
        if(posInput.y != 0)
            return posInput.y;
        else 
            return Input.GetAxis("Vertical");
    }
}
