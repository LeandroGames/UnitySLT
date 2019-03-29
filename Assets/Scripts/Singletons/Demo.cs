/*
 *  Leandro M. da Costa - 03/2017
 *  
 *  Slots Unity Projec 
 *  
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Demo : GenericSingleton<Demo> {

	private float timeCount = 0;
	private int _stage = 0;
	public int stage { 
		set { _stage = value; } 
		get { return _stage; } 
	}

	public float[] Interval;
	public GameObject[] Windows;

	GameObject[] lines;
	GameObject[] numbers;
	GameObject[] frames;

	void Update() {
		if (Globals.Credit > 0) {
			if (Globals.DemoMode) {
				Globals.DemoMode = false;
				_stage = -1; 
				switchWindow ();
			}
		}

		if (Globals.DemoMode) {
			timeCount += Time.deltaTime;
			if (timeCount >= Interval[_stage]) {
				timeCount = 0;
				switchWindow ();
			}
		}
	}


	public void switchWindow() {
		_stage++;
		if (_stage >= Windows.Length) {
			_stage = 0;
		} 
			
		for(int i = 0; i < Windows.Length; i++) {
			if (i == _stage) {
				Windows [i].SetActive (true);
			} else { 
				Windows [i].SetActive (false);
			}
		}

	}

	public void TurnOffDemo() {
		
		Messenger.Instance._sendMessege (messages.PRESSTOPLAY, true, Color.red, Color.yellow, 0.1f);
		for(int i = 0; i < Windows.Length; i++) {
			if (i == 0) {
				Windows [i].SetActive (true);
			} else { 
				Windows [i].SetActive (false);
			}
		}


	}
}
