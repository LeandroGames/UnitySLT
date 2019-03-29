using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class BonusBase : GenericSingleton<BonusBase> {

	public float DelayStart;

	private int bonusIndex = 0;

	public int currentIndex { 
		get { return bonusIndex; }
	}


	private float maxValue = 0;
	public float BonusValue { 
		get { return maxValue; }
	}

	public UnityEvent[] CallOnStart; 
	public UnityEvent[] CallOnFinish; 
	public GameObject[] Settings;

	public void InitBonus(int index, float bnsVl) {
		bonusIndex = index;
		Debug.Log ("Bonus start " + bonusIndex);
		maxValue = bnsVl;
		StartCoroutine(WaitAndExecute (DelayStart,OnStart));
	}

	public void OnStart() {
		Debug.Log ("Start Bonus " + bonusIndex);
		CallOnStart [bonusIndex].Invoke ();
		Settings [bonusIndex].GetComponent<BonusSettings> ().Init ();
	}

	public void AutoPlay() {
		Settings [bonusIndex].GetComponent<BonusSettings> ().CallAutoPlayDelay ();
	}
		

	private IEnumerator WaitAndExecute(float waitTime, UnityAction task)
	{
		yield return new WaitForSeconds(waitTime);
		task.Invoke ();
	}


}
