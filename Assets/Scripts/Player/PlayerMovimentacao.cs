using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovimentacao : MonoBehaviour {

	[SerializeField] private float	velocidadeMaxGiro = 25;
	[SerializeField] private float	forcaMovimento = 5;
	private Rigidbody	playerRigidbody;
	private Vector3	    mover;
	private Transform 	posCamera;
	private Vector3 	posCameraFrente;

	private GameObject FreeLookCameraRig;
	public bool controleMobile, controlePC = true;

	private float h, v;

	void Awake() {
		
		if (Camera.main != null) {
            posCamera = Camera.main.transform;
        }
        else {
            Debug.LogWarning("Aviso: nenhuma câmera principal encontrada. É precisa de uma Câmera marcada como \"MainCamera \""+
							 "para controles relativos à câmera");
			return;
        }

		FreeLookCameraRig = GameObject.Find("FreeLookCameraRig");

		if(FreeLookCameraRig != null){
			FreeLookCameraRig.transform.SendMessage("ativarControleMobile", SendMessageOptions.DontRequireReceiver);
		}else{
			Debug.Log("Aviso: FreeLookCameraRig não foi encontrado");
			return;
		}
	}

	// Use this for initialization
	void Start () {
		
		if(controleMobile){
			transform.SendMessage("ativarControleMobile", true, SendMessageOptions.DontRequireReceiver);
		}
		else if(controlePC){
			transform.SendMessage("ativarControlePC", true, SendMessageOptions.DontRequireReceiver);
		}

		playerRigidbody = GetComponent<Rigidbody>();
		playerRigidbody.maxAngularVelocity = velocidadeMaxGiro;
	}
	
	// Update is called once per frame
	void Update () {

		if(controleMobile){
			h = -Input.acceleration.x;
			v = -Input.acceleration.z;
		}
		else if(controlePC){
			h = Input.GetAxis("Horizontal");
			v = Input.GetAxis("Vertical");
		}

		// calcular direção de movimento
        if (posCamera != null) {

            // calcular direção relativa a câmera:
            posCameraFrente = Vector3.Scale(posCamera.forward, new Vector3(1, 0, 1)).normalized;
            mover = (v*posCameraFrente + h*posCamera.right).normalized;
        }
        else {
			 // Usar direção relativa ao mundo em caso de não haver cÂmera principal
            mover = (v*Vector3.forward + h*Vector3.right).normalized;
        }
	}

	void FixedUpdate() {
		
		playerRigidbody.AddForce(mover * forcaMovimento);
	}

	public void ativarControleMobile(){
		controleMobile = true;
		controlePC = !controleMobile;
		FreeLookCameraRig.SendMessage("ativarControleMobile", SendMessageOptions.DontRequireReceiver);
	}

	public void ativarControlePC(){
		controlePC = true;
		controleMobile = !controlePC;
		FreeLookCameraRig.SendMessage("ativarControlePC", SendMessageOptions.DontRequireReceiver);
	}
}
