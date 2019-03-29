using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BonusChoice : MonoBehaviour {

	public Text WinText;
	public Button[] buttons;
	public UnityEvent ReturnToGame;
	public UnityEvent Returned;

	[HideInInspector]
	public int pt;
	[HideInInspector]
	public float[] fc;
	[HideInInspector]
	public int selection = 0;
	[HideInInspector]
	public BonusBase BB;

	private float[] bonusFractions = new float[3];
	public float[] fractions { get { return bonusFractions; } }


	public void Init() {
		BB = BonusBase.Instance;
		selection = 0;
		WinText.gameObject.SetActive (false);
		if (BonusBase.Instance.currentIndex == 0) {
			initUniqueChoice ();
		} else if (BonusBase.Instance.currentIndex == 1) {
			initMultipleChoice ();
		} else if (BonusBase.Instance.currentIndex == 2) {
			initRoulette ();
		}

		if (Globals.DemoMode) {
			StartCoroutine (WaitAndExecute (1f, DemoPlay));
		} else if (Globals.autoPlay) {
			StartCoroutine (WaitAndExecute (1f, AutoPlay));
		}
	}

	void initRoulette() {
		selection = Random.Range (0, 23);
		WinText.text = ((int)Globals.Gain).ToString();
		buttons [0].gameObject.GetComponent<Roulette> ().initRoulette ();
	}

	void initUniqueChoice () {
		int moreorless = Random.Range (0, 3);
		switch (moreorless) {
		case 0:
			bonusFractions [0] = BB.BonusValue / Random.Range (2, 4);
			bonusFractions [1] = BB.BonusValue  / Random.Range (4, 8);
			bonusFractions [2] = BB.BonusValue  - Random.Range (10, BB.BonusValue -10);
			break;

		case 1:
			bonusFractions [0] = BB.BonusValue  * Random.Range (2, 4);
			bonusFractions [1] = BB.BonusValue  * Random.Range (4, 8);
			bonusFractions [2] = BB.BonusValue  + Random.Range (10, BB.BonusValue *10);
			break;

		case 2:
			bonusFractions [0] = BB.BonusValue * Random.Range (1, 3);
			bonusFractions [1] = BB.BonusValue * Random.Range (3, 6);
			bonusFractions [2] = BB.BonusValue  - Random.Range (10, BB.BonusValue -10);
			break;

		case 3:
			bonusFractions [0] = BB.BonusValue  / Random.Range (2, 3);
			bonusFractions [1] = BB.BonusValue  / Random.Range (3, 6);
			bonusFractions [2] = BB.BonusValue  + Random.Range (10, BB.BonusValue *10);
			break;
		}
			
		Globals.Gain = BB.BonusValue;
	}

	void initMultipleChoice () {
		float tt = BB.BonusValue;
		WinText.text = tt.ToString ();
		pt = Random.Range (1, 6);
		fc = new float[pt];
		switch (pt) {
		case 1:
			fc [0] = tt;
			break;
		case 2:
			int x1 = Random.Range (1, 9);
			fc [0] = tt / x1;
			fc [1] = tt / (10-x1);
			break;
		case 3:
			fc [0] = ((tt/100)*35);
			fc [1] = ((tt/100)*45);
			fc [2] = ((tt/100)*20);
			break;
		case 4:
			fc [0] = ((tt/100)*15);
			fc [1] = ((tt/100)*30);
			fc [2] = ((tt/100)*35);
			fc [3] = ((tt/100)*20);
			break;
		case 5:
			fc [0] = ((tt/100)*15);
			fc [1] = ((tt/100)*30);
			fc [2] = ((tt/100)*25);
			fc [3] = ((tt/100)*10);
			fc [4] = ((tt/100)*20);
			break;
		}
		float vsum = 0;
		foreach (float f in fc)
			vsum += f;
		Globals.Gain = vsum;
	}

	public void finishBonus() {
		if (BB.currentIndex == 0) 
			StartCoroutine (WaitAndExecute (1f, InvokeFinishUnique));
		 else 
			if (BB.currentIndex == 1) 
				StartCoroutine (WaitAndExecute (1f, invokeFinishMultiple));
		
	}



	void InvokeFinishUnique() {
		WinText.gameObject.SetActive (true);
		ReturnToGame.Invoke ();
		BonusBase BB = BonusBase.Instance;
		int a = BB.currentIndex;
		int b = 0;

		for (int i = 0; i < buttons.Length; i++) {
			buttons [i].enabled = false;
			if (i != selection) {
				buttons [i].gameObject.GetComponent<UniqueChoice> ().revel ((int)fractions [b]);
				b++;
			}
		}
		StartCoroutine (WaitAndExecute (3f, EndBonus));
	}

	void invokeFinishMultiple() {
		WinText.gameObject.SetActive (true);
		ReturnToGame.Invoke ();
		StartCoroutine (WaitAndExecute (3f, EndBonus));
	}


	public void EndBonus() {

	
		if (BB.currentIndex == 0) {
			UniqueReturnToGame ();
		} else if (BB.currentIndex == 1) {
			MultipleReturnToGame ();
		}

		if (!Globals.DemoMode)
			Globals.Gain = BonusBase.Instance.BonusValue;
		Slots.Instance._stage = Globals.CHECKPRIZE;

		BB.CallOnFinish [BB.currentIndex].Invoke ();
		Returned.Invoke ();

		Globals.IsBonus = false;
		Globals.IsJackpot = false;
	}


	void MultipleReturnToGame() {
		pt = 0;
 		selection = 0;
		for (int k = 0; k < buttons.Length; k++) {
			buttons [k].gameObject.GetComponent<MultipleChoice> ().Reset ();
			buttons [k].enabled = true;
		}
	}

	void UniqueReturnToGame() {
		selection = 0;
		for (int k = 0; k < buttons.Length; k++) {
			buttons [k].gameObject.GetComponent<UniqueChoice> ().selector.reset ();
			buttons [k].enabled = true;
		}
	}



	public void DemoPlay() {
		int tmp = Random.Range (0, buttons.Length - 1);
		if (BonusBase.Instance.currentIndex == 0) {
			selection = tmp;
			buttons [tmp].GetComponent<Button> ().onClick.Invoke ();
			StartCoroutine (WaitAndExecute (1f, delegate{finishBonus();}));
		} else if (BonusBase.Instance.currentIndex == 1) {
			buttons [tmp].GetComponent<MultipleChoice> ().onClick ();
			if (selection < pt + 1 && Globals.DemoMode) {
				StartCoroutine (WaitAndExecute (1f, DemoPlay));
			}
		}
	}

	void AutoPlay() {
		int tmp = Random.Range (0, buttons.Length - 1);
		if (BonusBase.Instance.currentIndex == 0) {
			selection = tmp;
			buttons [tmp].GetComponent<Button> ().onClick.Invoke ();
			StartCoroutine (WaitAndExecute (1f, delegate{finishBonus();}));
		} else if (BonusBase.Instance.currentIndex == 1) {
			buttons [tmp].GetComponent<MultipleChoice> ().onClick ();
			if (selection < pt + 1 && Globals.autoPlay) {
				StartCoroutine (WaitAndExecute (1f, AutoPlay));
			}
		}
	}

	public void CallAutoPlayDelay() {
		StartCoroutine (WaitAndExecute (1f, AutoPlay));
	}


	private IEnumerator WaitAndExecute(float waitTime, UnityAction task)
	{
		yield return new WaitForSeconds(waitTime);
		task.Invoke ();
	}

}
