using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public GameObject virtualJoystick;

	public void Restarte(){
		SceneManager.LoadScene(0);
	}

	public bool ApertandoJoystick(){
		return virtualJoystick.GetComponentInChildren<VirtualJoystick>().Apertando();
	}
}
