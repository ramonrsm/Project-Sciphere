using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class Analogico : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler {

	private Image fundoImg;
	private Image joystickImg;
	private Vector2 inputVector;
	public float [] posicaoAnalogico = new float[2];
	private void Start(){

		fundoImg = GetComponent<Image> ();
		joystickImg = transform.GetChild (0).GetComponent<Image> ();

	}

	public virtual void OnDrag(PointerEventData ped){
		
		Vector2 pos;

		if (RectTransformUtility.ScreenPointToLocalPointInRectangle (fundoImg.rectTransform,ped.position, ped.pressEventCamera, out pos)) {

			pos.x = (pos.x / (fundoImg.rectTransform.sizeDelta.x / 2));
			pos.y = (pos.y / (fundoImg.rectTransform.sizeDelta.y / 2));

			inputVector = new Vector2 ((pos.x * 2) + 0.01f, (pos.y * 2) - 0.01f);
			inputVector = (inputVector.magnitude > 1.0f)?inputVector.normalized:inputVector;

			joystickImg.rectTransform.anchoredPosition = 
			new Vector2 (inputVector.x *(fundoImg.rectTransform.sizeDelta.x/4.3f),inputVector.y * (fundoImg.rectTransform.sizeDelta.y/4.3f));

			posicaoAnalogico [0] = inputVector.x;
			posicaoAnalogico [1] = inputVector.y;
		}

	}

	public virtual void OnPointerDown(PointerEventData ped){
		OnDrag (ped);
	}

	public virtual void OnPointerUp(PointerEventData ped){

		inputVector = Vector2.zero;
		joystickImg.rectTransform.anchoredPosition = Vector2.zero;
		posicaoAnalogico [0] = inputVector.x;
		posicaoAnalogico [1] = inputVector.y;
	}

}
