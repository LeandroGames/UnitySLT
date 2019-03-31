/*
 *  Leandro M. da Costa - 03/2017
 *  
 *  Slots Unity Projec 
 *  
 */
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

//using com.spacepuppy;
//using com.spacepuppy.Collections;
using RotaryHeart.Lib.SerializableDictionary;

[System.Serializable]
public class keyDictionary : SerializableDictionaryBase<KeyCode,CallFunctionsKey> { }


public class Controlls : GenericSingleton<Controlls> {



	[Space(20)]
	[Header("Choose a keyboard key")]
	public keyDictionary KeyToFuncion = new keyDictionary();
	[Space(250)]
	[Tooltip("This is a temporary value set in inspector... gambiarra!")]
	public GameObject logWindow;


	[SerializeField]
	private RoundValidation VALIDATION;

	void Update() {
		if(Input.anyKeyDown  && Globals.ActiveToUse) {
			KeyCode tkey = KeyCode.None;
			foreach(KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
				if (Input.GetKeyDown (kcode))
					tkey = kcode;
			if (tkey == KeyCode.None)
				return;
			if (KeyToFuncion.ContainsKey (tkey)) 
				KeyToFuncion [tkey].Calls.Invoke ();
		}
	}


	void OnDisable() {
		
	}


	public void Start() {

		if (SaveLoad.ContainsThis ("Credit")) {
			Globals.Credit = float.Parse (SaveLoad.getData ("Credit"));
		} else {
			SaveLoad.AddData ("Credit", Globals.Credit.ToString ());
		}

		if (SaveLoad.ContainsThis ("Bet")) {
			Globals.setBet(int.Parse (SaveLoad.getData ("Bet"))-1);
		} else {
			SaveLoad.AddData ("Bet", Globals.Bet.ToString ());
		}
		VALIDATION = RoundValidation.Instance;
			
		VALIDATION.LoadReg ();
		VALIDATION.LoadData ();
	}


	public void insertCredit(float crd) {
		if (Globals.DemoMode) {
			if (Globals.IsBonus) {
				Globals.Gain = 0;
				Globals.prizesRaffled.Clear ();
				BonusBase.Instance.Settings[BonusBase.Instance.currentIndex].GetComponent<BonusSettings>().EndBonus ();
				Globals.IsBonus = false;
			}
			Globals._Demo.TurnOffDemo ();
			Globals._Slots.resetSlots ();
		}

		Globals.Credit += crd;
		Globals._SoundCtrl.PlaySoundName (Globals._SoundCtrl.SND_CASHIN, false, Globals._SoundCtrl._CoinsSound);
		SaveLoad.AddData ("Credit", Globals.Credit.ToString ());
	}

	public void Sortition() {

		if (Globals.DemoMode || Globals.IsBonus || Globals.IsJackpot)
			return;

		if (Slots.Instance.Stage == Globals.FREETOPLAY && Globals.Credit >= Globals.inLine * Globals.Bet) {
			Globals._SoundCtrl.clearQueue ();
			Globals._SoundCtrl.PlaySoundName ("rolling", false, Globals._SoundCtrl._SlotsSound);
		}
		Slots.Instance.Sortition ();
	}

	public void LineUp() {
		if (Globals.DemoMode || Globals.IsBonus || Globals.IsJackpot)
			return;
		
		if (Slots.Instance.Stage == Globals.FREETOPLAY) {
			Globals.markLineReset ();
			Globals._SoundCtrl.PlaySoundName (Globals._SoundCtrl.SND_LINEMORE,false, Globals._SoundCtrl._AudioSrc);
			Globals.visualLine (1);
			SaveLoad.AddData ("inLine", Globals.inLine.ToString ());
			Globals.LineViewer ();
		}
	}

	public void LineDown() {

		if (Globals.DemoMode || Globals.IsBonus || Globals.IsJackpot)
			return;
		
		if (Slots.Instance.Stage == Globals.FREETOPLAY) {
			Globals.markLineReset ();
			Globals._SoundCtrl.PlaySoundName (Globals._SoundCtrl.SND_LINELESS,false, Globals._SoundCtrl._AudioSrc);
			Globals.visualLine (-1);
			SaveLoad.AddData ("inLine", Globals.inLine.ToString ());
			Globals.LineViewer ();
		}
	}

	public void BetUp() {
		
		if (Globals.DemoMode || Globals.IsBonus || Globals.IsJackpot)
			return;
		
		if (Slots.Instance.Stage == Globals.FREETOPLAY) {
			Globals.markLineReset ();
			Globals.setBet (1);
			Globals._SoundCtrl.PlaySoundName (Globals._SoundCtrl.SND_BETMORE,false, Globals._SoundCtrl._AudioSrc);
			SaveLoad.AddData ("Bet", Globals.Bet.ToString ());
		}
	}

	public void BetDown() {

		if (Globals.DemoMode || Globals.IsBonus || Globals.IsJackpot)
			return;
		
		if (Slots.Instance.Stage == Globals.FREETOPLAY) {
			Globals.setBet (-1);
			Globals._SoundCtrl.PlaySoundName (Globals._SoundCtrl.SND_BETLESS,false, Globals._SoundCtrl._AudioSrc);
			SaveLoad.AddData ("Bet", Globals.Bet.ToString ());
		}
	}


	public static void OnINSIFICIENTCREDITS () {
		Globals._SoundCtrl.PlaySoundName (Globals._SoundCtrl.SND_NOCREDITS, false, Globals._SoundCtrl._CoinsSound);
	}


	public void PayHistoryTrigger() {

		if (logWindow.activeInHierarchy) {
			logWindow.SetActive (false);
		} else {
			logWindow.SetActive (true);
			logCtrl.Instance.Populate ();
		}
	}
	public void ZeroingOfCredit() {
		Globals.Credit = 0;
		Globals._Slots.resetSlots ();
		Globals.DemoMode = true;
		Globals._Slots.Stage = 0;
		SaveLoad.AddData ("Credit", Globals.Credit.ToString ());
	}

	public void setAutoPlay()
	{
		if (Globals.DemoMode) {
			Globals.autoPlay = false;
			return;
		}

		Globals.autoPlay = !Globals.autoPlay;
		if (Globals.IsBonus) {
			BonusBase.Instance.Settings [BonusBase.Instance.currentIndex].GetComponent<BonusSettings> ().CallAutoPlayDelay ();
		}
	}
			

	public void Exit() {
		Application.Quit ();
	}

}

