using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoviment : MonoBehaviour {

	public float speed = 10f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		
	}

	void FixedUpdate()
	{
		float x = -Input.acceleration.x;
		float z = -Input.acceleration.z;

		transform.Translate(z * speed * Time.deltaTime, 0, x * speed * Time.deltaTime);
	}
}
