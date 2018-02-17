using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaMovel : MonoBehaviour {

	[SerializeField] private float distanciaX = 10f, velocidade = 3f, tempoEsperaPlataforma = 1f;
	private Vector3 posInicial, posFinal, destino;

	// Use this for initialization
	void Start () {
		
		posInicial = transform.position;
		posFinal = new Vector3(posInicial.x + distanciaX, posInicial.y, posInicial.z);
		destino = posFinal;
	}
	
	// Update is called once per frame
	void Update () {
		
		transform.position = Vector3.MoveTowards(transform.position, destino, velocidade * Time.deltaTime);

		if(transform.position == destino){
			StartCoroutine("tempoEspera");
		}else{
			StopCoroutine("tempoEspera");
		}
	}

	public IEnumerator tempoEspera()
	{
		yield return new WaitForSeconds(tempoEsperaPlataforma);
		
		if(destino == posInicial){
			destino = posFinal;
		}else if(destino == posFinal){
			destino = posInicial;
		}
	}
}
