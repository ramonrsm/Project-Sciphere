using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovimentacao : MonoBehaviour {

	[SerializeField] private float	velocidadeMaxGiro = 25;
	[SerializeField] private float	velocidadeMaxGiroMobile = 45;
	[SerializeField] private float	forcaMovimento = 5;
	[SerializeField] private float	forcaMovimentoMobile = 2;
	private Rigidbody	playerRigidbody;
	private Vector3	    mover;
	private Transform 	posCamera;
	private Vector3 	posCameraFrente;

	public GameObject FreeLookCameraRig;
	public bool controleMobile, controlePC = true;

	private float h, v;

	void Awake() {
		
		if (Camera.main != null) {
            posCamera = Camera.main.transform;

			FreeLookCameraRig = GameObject.Find("FreeLookCameraRig");

			if(FreeLookCameraRig != null){
				FreeLookCameraRig.transform.SendMessage("ativarControleMobile", SendMessageOptions.DontRequireReceiver);
			}else{
				Debug.Log("Aviso: FreeLookCameraRig não foi encontrado");
				return;
			}
        }
        else {
            Debug.LogWarning("Aviso: nenhuma câmera principal encontrada. É precisa de uma Câmera marcada como \"MainCamera \""+
							 "para controles relativos à câmera");
			return;
        }
	}

	// Use this for initialization
	void Start () {
		
		playerRigidbody = GetComponent<Rigidbody>();

		if(controleMobile){
			playerRigidbody.maxAngularVelocity = velocidadeMaxGiroMobile;
			FreeLookCameraRig.SendMessage("ativarControleMobile", SendMessageOptions.DontRequireReceiver);
		}
		else if(controlePC){
			playerRigidbody.maxAngularVelocity = velocidadeMaxGiro;
			FreeLookCameraRig.SendMessage("ativarControlePC", SendMessageOptions.DontRequireReceiver);
		}
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
		
		if(controleMobile){
			playerRigidbody.AddForce(mover * forcaMovimentoMobile);
		}
		else if(controlePC){
			playerRigidbody.AddForce(mover * forcaMovimento);
		}
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
