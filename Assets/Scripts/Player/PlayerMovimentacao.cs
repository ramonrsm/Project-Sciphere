﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovimentacao : MonoBehaviour {

	[SerializeField] private float	velocidadeMaxGiro = 25;
	[SerializeField] private float	forcaMovimento = 5;
	private Rigidbody	playerRigidbody;
	private Vector3	    mover;
	private Transform 	posCamera;
	private Vector3 	posCameraFrente;

	void Awake() {
		
		if (Camera.main != null) {
            posCamera = Camera.main.transform;
        }
        else {
            Debug.LogWarning("Aviso: nenhuma câmera principal encontrada. É precisa de uma Câmera marcada como \"MainCamera \""+
							 "para controles relativos à câmera");
        }
	}

	// Use this for initialization
	void Start () {
		
		playerRigidbody = GetComponent<Rigidbody>();
		playerRigidbody.maxAngularVelocity = velocidadeMaxGiro;
	}
	
	// Update is called once per frame
	void Update () {

		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");

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
}