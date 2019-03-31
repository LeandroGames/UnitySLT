/*
 *  Leandro M. da Costa - 03/2017
 *  
 *  Slots Unity Project
 *  
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class Jackpot : GenericSingleton<Jackpot> {

	private Text JackpotText;
	private float _Jackpot = 0;
	public float jackpot_initial;
	public float jackpot_limit;
	public float percentageIncrease = 0.01f;
	public float jackpot_current { get { return _Jackpot; } }
	public GameObject PayWindow;
	


	public void Start() {
		JackpotText = GameObject.FindWithTag ("Jackpot").GetComponent<Text>();
		if (File.Exists (Application.persistentDataPath + "/0000.gd")) {
			Load ();
			if(_Jackpot > jackpot_limit / Globals.CreditValue)
				_Jackpot = jackpot_limit / Globals.CreditValue;
			Save ();
		} else {
			_Jackpot = jackpot_initial / Globals.CreditValue;
		}
		JackpotText.text = MonetaryString (_Jackpot);
		DisplayJackpot ();

	}

	public void payJackpot() {
		StartCoroutine (WaitAndExecute (3f, returnGame));
		PayWindow.SetActive (true);
	}

	void closeWindow() {
		PayWindow.SetActive (false);
	}

	void returnGame() {
		Globals.IsJackpot = false;
		if (!Globals.DemoMode) {
			int tmp = (int)(jackpot_current * Globals.CreditValue);
			Globals.Gain = (float)tmp;
			StartCoroutine (WaitAndExecute (10f, closeWindow));
		} else
			StartCoroutine (WaitAndExecute (3f, closeWindow));
		Slots.Instance._stage = Globals.CHECKPRIZE;
		Jackpot.Instance.resetJackpot ();
	}

	public string MonetaryString(float jackp) {
		return (_Jackpot * Globals.CreditValue).ToString ("C2");
	}

	public void DisplayJackpot() {
		JackpotText.text = MonetaryString(_Jackpot);
	}
		
	public void Save() {
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create (Application.persistentDataPath + "/0000.gd");
		bf.Serialize(file, _Jackpot);
		file.Close();
	}

	public void setupJackpot(float value) {
		_Jackpot += value * percentageIncrease;
		Save ();
		DisplayJackpot ();
	}

	public void resetJackpot() {
		_Jackpot = jackpot_initial / Globals.CreditValue;
		Save ();
		DisplayJackpot ();
	}

	public void Load() {
		if(File.Exists(Application.persistentDataPath + "/0000.gd")) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/0000.gd", FileMode.Open);
			_Jackpot = (float)bf.Deserialize(file);
			file.Close();
		}
	}

	private IEnumerator WaitAndExecute(float waitTime, UnityAction task)
	{
		yield return new WaitForSeconds(waitTime);
		task.Invoke ();
	}


}
