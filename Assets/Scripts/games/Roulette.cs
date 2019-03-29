using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Roulette : MonoBehaviour {

	private BonusChoice BC;
	private BonusBase BB;
	private float[] fracs;
	private int objective = 0;
	private int current = 0;
	private bool inRoll = false;
	private Vector3 initialAngle;
	private int count = 0;

	public int loops = 10;
	public int parts = 24;
	public float limit = 345f;
	public float partValue = 15f;
	public float initSpeed = 0.5f;
	public Animator Arrow;

	private float[] rouletteFractions = new float[24];

	void Start() {
		float a = 0;
		for(int f = 0; f < rouletteFractions.Length; f++) {
			rouletteFractions[f] = a;
			a += partValue;
		}
	}

	public void initRoulette() {
		BB = BonusBase.Instance;
		BC = BB.Choices [BB.currentIndex].GetComponent<BonusChoice> ();
		fracs = rouletteFractions;
		objective = BC.selection;
		current = 0;
		initialAngle = transform.localRotation.eulerAngles;
		startRoll ();
	}

	public void startRoll() {
		roll ();
		inRoll = true;
	}

	void roll() {
		Debug.Log ("current: " + current);
		initialAngle.z = rouletteFractions [current];
		gameObject.transform.localRotation = Quaternion.Euler(initialAngle);
		if(current > 0)
			Arrow.SetBool ("move", true);
		if (inRoll) {
			current++;
			if (current >= parts - 1) {
				current = 0;
				count++;
			}
		}
			
		if (count > loops+1 && current == objective)
			inRoll = false;
		if (inRoll) {
			if (count > loops)
				StartCoroutine (WaitAndExecute (initSpeed*4, roll));
			else
				StartCoroutine (WaitAndExecute (initSpeed, roll));
		}
		
	}



	private IEnumerator WaitAndExecute(float waitTime, UnityAction task)
	{
		yield return new WaitForSeconds(waitTime);
		task.Invoke ();
	}

}
