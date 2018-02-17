using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atributos_Materiais : MonoBehaviour {

	public string	nomeMaterial = "";
	
	[Range(0f,1f)]
	public float bounce = 0;

	[Range(0.8f,3.8f)]
	public float peso = 0.8f;

	[Range(1,12)]
	public float forcaMovimento = 6;

	void OnTriggerEnter(Collider other) {
		
		if(other.tag == "Player"){
			this.gameObject.SetActive(false);
		}
	}
}
