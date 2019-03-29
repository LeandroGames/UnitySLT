using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

	public void ArrowOn() {
		GetComponent<Animator>().SetBool ("move", true);
	}

	public void ArrowOff() {
		GetComponent<Animator>().SetBool ("move", false);
	}
}
