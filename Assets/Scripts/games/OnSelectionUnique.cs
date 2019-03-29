using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnSelectionUnique : MonoBehaviour {

	public GameObject toOff;
	public GameObject toOn;
	public Text textValue;
	public Button thisButton;


	public void CallOff() {
		toOff.SetActive (false);
		thisButton.enabled = false;
		BonusBase BB = BonusBase.Instance;
		BB.Settings [BB.currentIndex].GetComponent<BonusSettings>().WinText.text = BB.BonusValue.ToString();
		BB.Settings [BB.currentIndex].GetComponent<BonusSettings>(). finishBonus ();
	}

	public void CallOn() {
		toOn.SetActive (true);
		BonusBase BB = BonusBase.Instance;
		textValue.text = BB.BonusValue.ToString();
		textValue.color = Color.yellow;
	}

	public void reset() {
		toOn.SetActive (false);
		textValue.color = Color.white;
	}


}
