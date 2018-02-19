using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FolowPlayer : MonoBehaviour {

	public GameObject	target;	
	public float distanceYTarget = 1.2f;
	public float distanceZTarget = 4f;

	public float distancia;

	// Use this for initialization
	void Start () {
		viewCamera01();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void LateUpdate()
	{
		verificarAlturaTarget();
		transform.position = new Vector3(target.transform.position.x, distanceYTarget, target.transform.position.z - distanceZTarget);
	}

	void verificarAlturaTarget(){

		float alturaTarget = target.transform.position.y;
		float alturCamera = transform.position.y + distanceYTarget;

		distancia = alturCamera - alturaTarget;

		
	}

	void viewCamera01(){

		transform.position = new Vector3(target.transform.position.x, distanceYTarget + distanceYTarget, distanceZTarget);
		transform.rotation = Quaternion.Euler(7f, 0, 0);
	}
}
