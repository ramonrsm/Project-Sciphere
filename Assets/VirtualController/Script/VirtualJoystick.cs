using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler {

    private Image       bgImg, joystickImg;
    private Vector3     inputVector;

    // Use this for initialization
    void Start () {

        bgImg = GetComponent<Image>();
        joystickImg = transform.GetChild(0).GetComponent<Image>();
	}

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos; // x,y

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(bgImg.rectTransform, eventData.position, eventData.pressEventCamera, out pos))
        {
            pos.x = pos.x / bgImg.rectTransform.sizeDelta.x;
            pos.y = pos.y / bgImg.rectTransform.sizeDelta.y;

            inputVector = new Vector3(pos.x * 2 - 1, 0, pos.y * 2 -1); // Z PORQUE SERVE PARA JOGOS 3D TAMBÉM

            if (inputVector.magnitude > 1)
            {
                inputVector = inputVector.normalized;
            }

            // MOVIMENTAR JOYSTICK
            joystickImg.rectTransform.anchoredPosition = new Vector3(inputVector.x * (bgImg.rectTransform.sizeDelta.x / 2.5f), 
                                                                     inputVector.z * (bgImg.rectTransform.sizeDelta.y / 2.5f));
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inputVector = Vector3.zero; // new Vector3 (0, 0, 0);
        joystickImg.rectTransform.anchoredPosition = Vector3.zero;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public float Horinzontal()
    {
        return inputVector.x;
    }

    public float Vertical()
    {
        return inputVector.z;
    }

    public bool Apertando(){
        return (inputVector.x != 0 || inputVector.z != 0);
    }
}
