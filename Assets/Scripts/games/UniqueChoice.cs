using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UniqueChoice : MonoBehaviour {

	public Text text;
	public OnSelectionUnique selector;
	public int selectionIndex = 0;

	public void open(int value) {
		BonusBase BB = BonusBase.Instance;
		text.gameObject.SetActive (true);
		text.text = value.ToString ();
		BB.Settings[BB.currentIndex].GetComponent<BonusSettings>().selection = selectionIndex;
	}

	public void revel(int value) {
		BonusBase BB = BonusBase.Instance;
		text.gameObject.SetActive (true);
		text.text = value.ToString ();
	}
}
