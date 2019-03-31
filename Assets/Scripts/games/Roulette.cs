using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Roulette : MonoBehaviour {

	private BonusSettings BC;
	private BonusBase BB;
	private float[] fracs;
	private int objective = 0;
	private int current = 0;
	private bool inRoll = false;
	private Vector3 initialAngle;
	private int count = 0;

	public int loops = 3;
	public int parts = 24;

	public float partValue = 15f;
	public float initSpeed = 0.02f;
	public Animator Arrow;
	public Button buttonStart;

	private float[] rouletteFractions = new float[24];

	void Start() {
		Arrow.SetBool ("move", false);
		float a = 0;
		for(int f = 0; f < rouletteFractions.Length; f++) {
			rouletteFractions[f] = a;
			a += partValue;
		}

		if(!Globals.DemoMode)
			buttonStart.enabled = true;
		else 
			buttonStart.enabled = false;
	}

	public void initRoulette() {
		BB = BonusBase.Instance;
		BC = BB.Settings [BB.currentIndex].GetComponent<BonusSettings> ();
		fracs = rouletteFractions;
		objective = BC.selection;
		current = 0;
		initialAngle = transform.localRotation.eulerAngles;
		startRoll ();
	}

	public void startRoll() {
		objective = BC.selection;
		roll ();
		inRoll = true;
	}

	void roll() {
		Vector3 tmp = new Vector3 (initialAngle.x, initialAngle.y, rouletteFractions [current]);
		gameObject.transform.localRotation = Quaternion.Euler(tmp);
			Arrow.SetBool ("move", true);
		if (inRoll) {
			current++;
			if (current >= parts - 1) {
				current = 0;
				count++;
			}
		}
			

		if (inRoll) {
			if (count > loops)
				StartCoroutine (WaitAndExecute (initSpeed*4, roll));
			else
				StartCoroutine (WaitAndExecute (initSpeed, roll));
		}

		if (count > loops + 1 && current == objective) {
			inRoll = false;
			BC.finishBonus ();
			count = 0;
			Arrow.SetBool ("move", false);
		}
	}

	public void reset() {
		gameObject.transform.localRotation = Quaternion.Euler(initialAngle);
		inRoll = false;
		count = 0;
		Arrow.SetBool ("move", false);
	}


	private IEnumerator WaitAndExecute(float waitTime, UnityAction task)
	{
		yield return new WaitForSeconds(waitTime);
		task.Invoke ();
	}

}
