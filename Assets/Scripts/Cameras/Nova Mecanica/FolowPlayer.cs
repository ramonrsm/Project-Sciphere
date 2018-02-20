using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FolowPlayer : MonoBehaviour {

	public Transform	target;
	private Vector3 offset;

	// Use this for initialization
	void Start () {

		offset = transform.position - target.position;
	}

	void LateUpdate()
	{
		Vector3 newPosition = target.position + offset;
		transform.position = newPosition;

		transform.LookAt(target);
	}
}
