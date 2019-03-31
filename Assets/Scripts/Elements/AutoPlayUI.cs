using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoPlayUI : MonoBehaviour {

	public Text autoPlayText;
	public Color AutoIsOn;
	public Color AutoIsOff;


	void Start () {
		autoPlayText.color = Globals.autoPlay ? AutoIsOn : AutoIsOff;
	}

	public void updateStatus() {
		Controlls.Instance.setAutoPlay ();
		autoPlayText.color = Globals.autoPlay ? AutoIsOn : AutoIsOff;
	}

}
