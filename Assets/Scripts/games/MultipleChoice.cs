using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MultipleChoice : MonoBehaviour {

	public Text txtFall;
	public Text txtWin;
	public Animator Anim;
	private BonusBase BB;
	private BonusChoice BC;


	public void onClick() {
		BB = BonusBase.Instance;
		BC = BB.Choices[BB.currentIndex].GetComponent<BonusChoice>();
		GetComponent<Button> ().interactable = false;

		if (BB.Choices[BB.currentIndex].GetComponent<BonusChoice>().selection >= BC.pt) {
			Anim.SetBool ("selected", true);
			Anim.SetBool ("fall", true);
			Button[] buttons = BB.Choices [BB.currentIndex].GetComponent<BonusChoice> ().buttons;
			foreach (Button bt in buttons) {
				bt.GetComponent<Button>(). interactable = false;
			}
			float vsum = 0;
			foreach (float f in BC.fc)
				vsum += f;
			Globals.Gain = vsum;
			StartCoroutine (WaitAndExecute (2f, BC.finishBonus));
		} else {
			Anim.SetBool ("selected", true);
			Anim.SetBool ("win", true);
			txtWin.text = ((int)BC.fc [BC.selection]).ToString();
			Debug.Log("BC.selections "+BC.selection);
		}
		BC.selection++;	
	}

	public void onWin() {
		txtWin.gameObject.SetActive (true);
	}


	public void onFall() {
		txtFall.gameObject.SetActive (true);
	}

	public void Reset() {
		BB = BonusBase.Instance;
		Button[] buttons = BB.Choices [BB.currentIndex].GetComponent<BonusChoice> ().buttons;
		foreach (Button bt in buttons) {
			bt.interactable = true;
			bt.gameObject.GetComponent<MultipleChoice>().txtWin.gameObject.SetActive (false);
			bt.gameObject.GetComponent<MultipleChoice>().txtFall.gameObject.SetActive (false);
		}
	}

	private IEnumerator WaitAndExecute(float waitTime, UnityAction task)
	{
		yield return new WaitForSeconds(waitTime);
		task.Invoke ();
	}
}
