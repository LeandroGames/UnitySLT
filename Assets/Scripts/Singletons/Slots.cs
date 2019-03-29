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

[System.Serializable]
public class Slots : GenericSingleton<Slots> {


	GameObject[] Rolls = new GameObject[5];
	public float decrement = 0.2f;
	public float RollForce = 20;
	public float minSpeed = 3f;

	public float breakPositionY = 985;
	public float finalPositionY = 128;

	private Vector3[] _raffledsreg = new Vector3[5];
	public Vector3[] raffledReg {
		get { return _raffledsreg; }
	}

	float[] countUpDown =  {0,0,0,0,0};
	int[] direction = new int[5] {1,1,1,1,1}; // 1 = up / 2 = down 
	bool[] started = {false, false, false, false, false};
	int moment = 0; // 0 = inicio do giro / 1 = fim do giro;
	bool[] UpDownEffect = {false, false, false, false, false};
	int[] timesEffect = {0,0,0,0,0};

	float[] initPositionY = new float[5];

	GameObject[] getRolls { get{ return Rolls; } }

	bool[] rolling = {false, false, false, false, false};
	float[] SpeedReal =  new float[5]{0,0,0,0,0};
	Vector3[] initials = new Vector3[5];
	[HideInInspector]
	public int _stage = 0;

	[HideInInspector]
	public float cntPayTime = 0;
	[HideInInspector]
	public float tempGain = 0;
	[HideInInspector]
	public float tempPay = 0;

	[HideInInspector]
	public float limitPayDemo = 0;
	[HideInInspector]
	public float limitSortDemo = 0;

	Vector3[] newicons = new Vector3[5];
	public Vector3[] raffleds { get { return newicons; } }

	public bool hackraffle = false;
	public Vector3[] hackvector = new Vector3[5];

	public void Start() {
		GameObject.FindWithTag ("Credits").GetComponent<Text> ().text = Globals.Credit+"";
		for (int i = 0; i < 5; i++) {
			Rolls [i] = GameObject.FindWithTag ("Roll"+(i+1));
			initials [i] = Rolls [i].transform.position;
			initPositionY[i] = Rolls [i].GetComponent<RectTransform>().anchoredPosition.y;
		}
		recordNewIcons ();
		if (SaveLoad.ContainsThis ("inLine")) {
			Globals.fixLine(int.Parse (SaveLoad.getData ("inLine")));
			Globals.LineViewer ();
		} else {
			SaveLoad.AddData ("inLine", Globals.inLine.ToString ());
		}
			

	}

	void Update () {
		if (_stage == Globals.CHECKPRIZE) {
			Globals.OnCHECKPRIZE ();
		} else if (_stage == Globals.PAYPRIZE) {
			Globals.OnPAYPRIZE ();
		} else if (_stage == Globals.FREETOPLAY && Globals.Credit <= 0 && !Globals.DemoMode) {
			Globals.OnEnterDEMOMODE ();
		} else if (Globals.DemoMode && _stage == Globals.FREETOPLAY) {
			Globals.OnPLAYDEMOMODE ();
		} else if (Globals.inRollStage (rolling) || Globals.inRollStage (UpDownEffect)) {
			rollUpdate ();
			MolejoUpdate ();
		} else if (_stage == Globals.FREETOPLAY && Globals.autoPlay && Globals.Credit >= Globals.inLine * Globals.Bet) {
			Globals._SoundCtrl.clearQueue ();
			Globals._SoundCtrl.PlaySoundName ("rolling", false, Globals._SoundCtrl._SlotsSound);
			Slots.Instance.Sortition ();
		}
			

		Globals.ctrlMarkLine ();
	}


	public GameObject getRealIcon(int _roll, int _line) {
		if (_roll > 4 || _line > 2)
			return null;
		
		List<GameObject> _rl = new List<GameObject> () {
			Rolls [_roll].GetComponent<Roll> ().slotLast1,
			Rolls [_roll].GetComponent<Roll> ().slotLast2,
			Rolls [_roll].GetComponent<Roll> ().slotLast3
		};
			
		return _rl [_line];
	}

		

	private void rollUpdate() {
		for (int i = 0; i < 5; i++) {
			if (rolling[i] && !UpDownEffect[i]) {

				if(Rolls [i].GetComponent<RectTransform> ().anchoredPosition.y <= breakPositionY)
					SpeedReal[i] -= decrement;

				if (SpeedReal [i] < minSpeed) {
					SpeedReal [i] = minSpeed;
				}

				Vector3 tmp = Rolls[i].transform.position;
				Vector3 tmp2 = new Vector3 (tmp.x, tmp.y - (SpeedReal[i]  * Time.deltaTime), tmp.z);
				Rolls[i].transform.position = tmp2;
			}

			if (Rolls [i].GetComponent<RectTransform> ().anchoredPosition.y <= finalPositionY && !UpDownEffect[i] ) {
				SpeedReal [i] = 0;
				rolling [i] = false;
				Rolls [i].GetComponent<RectTransform> ().anchoredPosition = 
					new Vector2(Rolls [i].GetComponent<RectTransform> ().anchoredPosition.x, initPositionY [i]);
				restoreNewIcons (i);
				moment = 0;
				started [i] = true;
				UpDownEffect [i] = true;
			}
		}
	}

	public void MolejoUpdate() {
		
		for(int k = 0; k < 5; k++) {
			float _tm;
			float _tm2;
			if (moment == 0) {
				_tm = -0.2f;
				_tm2 = 0.2f;
			} else {
				_tm = 0.2f;
				_tm2 = -0.2f;
			}

			if (UpDownEffect [k]) {
				countUpDown[k] += Time.deltaTime;
				if (countUpDown[k] >= 0.01f) {
					Vector3 tmp = Rolls[k].transform.position;
					Vector3 tmp2;
					timesEffect[k]++;
					countUpDown[k] = 0;
					if (direction [k] == 1) {
						if (timesEffect[k] > 5) {
							timesEffect[k] = 0;
							direction [k] = 2;
						}
						tmp2 = new Vector3 (tmp.x, tmp.y + _tm, tmp.z);				
						Rolls[k].transform.position = tmp2;

					} else if (direction [k] == 2) {
						if (timesEffect[k] > 5) {
							timesEffect[k] = 0;
							direction [k] = 1;
							UpDownEffect [k] = false;
						}
						tmp2 = new Vector3 (tmp.x, tmp.y + _tm2, tmp.z);
						Rolls[k].transform.position = tmp2;
					}

				}
			}
		}

		if (moment == 1) {
			for (int a = 0; a < 5; a++) {
				if (started [a] && !UpDownEffect [a]) {
					Rolls [a].GetComponent<RectTransform> ().anchoredPosition = 
						new Vector2(Rolls [a].GetComponent<RectTransform> ().anchoredPosition.x, initPositionY [a]);
					rolling [a] = true;
					SpeedReal [a] = RollForce;
					started [a] = false;
				}
			}
		} else if (moment == 0) {
			for (int a = 0; a < 5; a++) {
				if (started [a] && !UpDownEffect [a]) {
					started [a] = false;
					Rolls [a].GetComponent<RectTransform> ().anchoredPosition = 
						new Vector2(Rolls [a].GetComponent<RectTransform> ().anchoredPosition.x, initPositionY [a]);
					if (a == 4) {
						Globals.checkPrize (raffleds);
						Globals.OnFinishRaffle ();
						if (!Globals.IsBonus && !Globals.IsJackpot)
							_stage = Globals.CHECKPRIZE;
				
					}
				}
			}
		}
	}


	public void Sortition() {
		RoundValidation VALIDATION = RoundValidation.Instance;
		if (_stage == Globals.FREETOPLAY && !Globals.DemoMode) {
			if ((Globals.inLine * Globals.Bet) <= Globals.Credit) {
				VALIDATION.registerRound ();
				Globals.markLineReset ();
				Globals.AnimeIconParticleCtrl (false);
				randomFakes ();
				Globals.Credit -= Globals.inLine * Globals.Bet;
				SaveLoad.AddData ("Credit", Globals.Credit.ToString ());
				Jackpot.Instance.setupJackpot (Globals.inLine * Globals.Bet);
				Globals.DestroyLines ();	
				moment = 1;
				for (int i = 0; i < 5; i++) 
				{
					started [i] = true;
					UpDownEffect [i] = true;
					countUpDown [i] = 0;
				}
				if (!hackraffle) 
				{
					raffle ();
				} 
				else 
				{
					hack (hackvector);
				}
				recordNewIcons ();
				Globals.ResetPrizesDic ();
				_stage = Globals.PLAYING;
			} 
			else 
			{
				Messenger.Instance.insufficientCredit = true;
				Controlls.OnINSIFICIENTCREDITS ();
			}
		} 
		else if (_stage == Globals.PAYPRIZE && !Globals.DemoMode) 
		{ // Stage is Paying and Demo Mode is off
			Globals._SoundCtrl.clearQueue();
			Globals.AnimeIconParticleCtrl (false);
			_stage = Globals.FREETOPLAY; 
			tempPay = 0;
			tempGain = 0;
			GameObject.FindWithTag ("GainValue").GetComponent<Text> ().text = ((int)Globals.Gain).ToString();
			GameObject.FindWithTag ("Credits").GetComponent<Text> ().text = ((int)Globals.Credit).ToString();
		}

	}

	public void SortitionDemo() {
		if (_stage == Globals.FREETOPLAY) {
			Globals.markLineReset ();
			Globals.AnimeIconParticleCtrl (false);
			randomFakes ();
			limitSortDemo = 0;
			Globals.DestroyLines ();	
			moment = 1;
			for (int i = 0; i < 5; i++) {
				started [i] = true;
				UpDownEffect [i] = true;
				countUpDown [i] = 0;
			}
			if (!hackraffle) {
				raffle ();
			} else {
				hack (hackvector);
			}
			recordNewIcons ();
			Globals.ResetPrizesDic ();
			_stage = Globals.PLAYING;
		} 

	}

	public void resetSlots() {
		_stage = Globals.FREETOPLAY;
		Globals.DestroyLines ();
		for (int i = 0; i < 5; i++) {
			countUpDown [i] = 0;
			direction [i] = 1;
			started [i] = false;
			UpDownEffect [i] = false;
			timesEffect [i] = 0;
			rolling [i] = false;
			SpeedReal [i] = 0;
			initialPosition (i);
		}
		GameObject.FindWithTag ("GainValue").GetComponent<Text> ().text = "0";
	}


	public int Stage
	{
		get { return _stage; }
		set { 
			_stage = value; 
		}
	}


	public void raffle() {
		Roll[] tmp = new Roll[5];
		for (int i = 0; i < 5; i++) {
			tmp [i] = GameObject.FindWithTag ("Roll" + (i + 1)).GetComponent<Roll> ();
			tmp [i].slotTop.GetComponent<IconScript> ().SetRandomIcon ();
			tmp [i].slotMeans.GetComponent<IconScript>().SetRandomIcon ();
			tmp [i].slotLow.GetComponent<IconScript>().SetRandomIcon ();
			_raffledsreg[i].x = tmp [i].slotTop.GetComponent<IconScript> ().iconId;
			_raffledsreg[i].y = tmp [i].slotMeans.GetComponent<IconScript> ().iconId;
			_raffledsreg[i].z = tmp [i].slotLow.GetComponent<IconScript> ().iconId;
		}
	}

	public void hack(Vector3[] hacked) {
		Roll[] tmp = new Roll[5];
		for (int i = 0; i < 5; i++) {
			_raffledsreg[i].x = (int)hacked[i].x;
			_raffledsreg[i].y = (int)hacked[i].y;
			_raffledsreg[i].z = (int)hacked[i].z;
			tmp [i] = GameObject.FindWithTag ("Roll" + (i + 1)).GetComponent<Roll> ();
			tmp[i].GetComponent<Roll> ().setNew((int)_raffledsreg[i].x,(int)_raffledsreg[i].y,(int)_raffledsreg[i].z);
		}
	}

	public void recordNewIcons() {
		Roll[] tmp = new Roll[5];
		for (int i = 0; i < 5; i++) {
			tmp [i] = GameObject.FindWithTag ("Roll" + (i + 1)).GetComponent<Roll> ();
			newicons [i].Set (
				tmp [i].slotTop.GetComponent<IconScript>().iconId,
				tmp [i].slotMeans.GetComponent<IconScript>().iconId,
				tmp [i].slotLow.GetComponent<IconScript>().iconId);
		}
	}


	public void restoreNewIcons(int val) {
		Roll temp = GameObject.FindWithTag ("Roll" + (val +1)).GetComponent<Roll> ();
		temp.setLast ((int)newicons [val].x, (int)newicons [val].y, (int)newicons [val].z);
	}

	public void randomFakes() {
		GameObject[] fakes;
		fakes = GameObject.FindGameObjectsWithTag ("fakeIcon");
		for (int i = 0; i < fakes.Length; i++) {
			fakes [i].GetComponent<IconScript> ().SetRandomIcon ();
		}
	}


	public void initialPosition(int val) {
		if (val > 4) {
			Debug.Log("Erro... valor invalido => " + val); 
		} else {
			Rolls [val].transform.position = initials [val];
			restoreNewIcons (val);
		}
	}

}
