using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private Rigidbody		    rigidbodyPlayer;
	private MeshRenderer     	meshRendererPlayer;
	private	SphereCollider		sphereColliderPlayer;
	public	string 				materialAtual = "Plastico";
	public  GameObject			luzPlayer;

	void Awake() {

		rigidbodyPlayer	     = GetComponent<Rigidbody>();
		meshRendererPlayer   = GetComponent<MeshRenderer>();
		sphereColliderPlayer = GetComponent<SphereCollider>();
		checarMaterialLuz();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		

		
	}

	void OnTriggerEnter(Collider other){
		
		// TROCAR DE MATERIAL 
		if(other.tag.Equals("MaterialUtilizavel")){

			materialAtual = other.GetComponent<Atributos_Materiais>().nomeMaterial;

			sphereColliderPlayer.material.bounciness = other.GetComponent<Atributos_Materiais>().bounce;

			rigidbodyPlayer.mass = other.GetComponent<Atributos_Materiais>().peso;

			meshRendererPlayer.material = other.GetComponent<MeshRenderer>().material;
			
			checarMaterialLuz();
		}
	}

	public void checarMaterialLuz(){

		// LIGAR LUZ INTERNA PLAYER
        if(materialAtual.Equals("Luz")){

            luzPlayer.gameObject.SetActive(true);
        }else{

            luzPlayer.gameObject.SetActive(false);
        }
	}
}
