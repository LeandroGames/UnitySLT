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
public static class Globals  {
	
	public static Controlls _Ctrl = Controlls.Instance;
	public static Slots _Slots = Slots.Instance;
	public static Demo _Demo =  Demo.Instance;
	public static ParticleSystem particlesCoins = GameObject.FindWithTag ("CoinParticle").GetComponent<ParticleSystem> ();
	public static SoundControll _SoundCtrl = SoundControll.Instance;
	public static Jackpot _jackpot = Jackpot.Instance;
	public static Dictionary<int, Prize> Prizes = new Dictionary<int, Prize> ();
	public static Dictionary<int, Line> Lines = new Dictionary<int, Line> ();
	public static Dictionary<int, Sprite> Icons = new Dictionary<int, Sprite>();
	public static Dictionary<int, LineMarker> vLines = new Dictionary<int, LineMarker>();
	public static Dictionary<int, RuntimeAnimatorController> Animes = new Dictionary<int, RuntimeAnimatorController> ();

	public static Dictionary<int, Dictionary<int, float>> prizesRaffled = new Dictionary<int, Dictionary<int, float>> ();

	private static Dictionary<int, List<GameObject>> markFrames = new Dictionary<int, List<GameObject>>();
	private static Dictionary<int, int> valueFrames = new Dictionary<int, int>();
	private static float mf_countTime = 0;
	private static float mf_time = 1f;
	private static int mf_line = 0;
	public static bool lineMarkerActive = false;

	public const int FREETOPLAY = 0;
	public const int PLAYING 	= 1; 
	public const int NOTUSED 	= 2;
	public const int CHECKPRIZE = 3; 
	public const int PAYPRIZE	= 4;


	private static bool _autoPlay = false;
	public static bool autoPlay {
		get { return _autoPlay; }
		set { _autoPlay = value; }
	}

	private static bool _ActiveToUse = false;
	public static bool ActiveToUse {
		get { return _ActiveToUse; }
		set { _ActiveToUse = value; }
	}

	private static bool _isDemo = false;
	public static bool DemoMode {
		get { return _isDemo; }
		set { _isDemo = value; }
	}

	private static bool _isBonus = false;
	public static bool IsBonus {
		get { return _isBonus; }
		set { _isBonus = value; }
	}

	private static bool _isJackpot = false;
	public static bool IsJackpot {
		get { return _isJackpot; }
		set { _isJackpot = value; }
	}

	private static float _Gain = 0;
	public static float Gain {
		get { return _Gain; }
		set { _Gain = value; }
	}

	private static float _Cred = 0;
	public static float Credit {
		get { return _Cred; }
		set { 
			_Cred = value; 
			GameObject.FindWithTag ("Credits").GetComponent<Text> ().text = ((int)_Cred).ToString();
		}
	}

	private static float _Bet = 1;
	public static float Bet {
		get { return _Bet; }
		set { _Bet = value; }
	}

	private static int _inLine = 1;
	public static int inLine {
		get { return _inLine; }
		set { _inLine = value; }
	}

	private static float _CredValue = 0.10f;
	public static float CreditValue {
		get { return _CredValue; }
		set { _CredValue = value; }
	}

	public static void markLineReset() {
		for (int i = 0; i <= Lines.Count; i++) {
			if (markFrames.ContainsKey (i)) {
				for (int o = 0; o < markFrames [i].Count; o++) {
					GameObject.Destroy (markFrames [i] [o]);
				}
			}
		}
		lineMarkerActive = false;
		mf_countTime = 0;
		mf_line = 0;
		valueFrames.Clear ();
		markFrames.Clear();
	}


	public static void ctrlMarkLine() {
		if (mf_line == 0) {
			for (int l = 0; l <= Lines.Count; l++) {
				if (markFrames.ContainsKey (l)) {
					mf_line = l;
					break;
				}
			}
			for (int i = 0; i <= Lines.Count; i++) {
				if (markFrames.ContainsKey (i)) {
					valueFrames [i] = i;
					if (mf_line != i && markFrames [i] != null) {
						for (int o = 0; o < markFrames [i].Count; o++) {
							if(markFrames [i] [o]) 
								markFrames [i] [o].GetComponent <FrameMarker> ().FrameActive (false);
						}
					} else if(markFrames [i] != null) {
						for (int o = 0; o < markFrames [i].Count; o++) {
							if(markFrames [i] [o]) 
								markFrames [i] [o].GetComponent <FrameMarker> ().FrameActive (true);
						}
					}
				}
			}
		}
		if (mf_line > 0) {
			mf_countTime += Time.deltaTime;
		}
		if(mf_countTime >= mf_time) {
			if (markFrames.Count > 1)
				mf_line++;
			
			if (markFrames.ContainsKey (mf_line) == false) {
				for (int l = mf_line; l <= Lines.Count; l++) {
					if (markFrames.ContainsKey (l)) {
						mf_line = l;
						break;
					}

					mf_line = 0;

				}
			}
			if (mf_line > 0) {
				for (int i = 0; i <= Lines.Count; i++) {
					if (markFrames.ContainsKey (i)) {
						if (mf_line != i) {
							for (int o = 0; o < markFrames [i].Count; o++) {
								if (markFrames [i] [o])
									markFrames [i] [o].GetComponent <FrameMarker> ().FrameActive (false);
							}
						} else {
							for (int o = 0; o < markFrames [i].Count; o++) {
								if (markFrames [i] [o])
									markFrames [i] [o].GetComponent <FrameMarker> ().FrameActive (true);
							}
						}
					}
				}
			}
			mf_countTime = 0;
		}
		if (mf_line > Lines.Count) {
			mf_line = 0;
		}
	}


	public static void OnFinishRaffle() {
		_Gain = PrizeWon ();
		if (_Gain > 0) {
			lineMarkerActive = true;
		}
	}

	public static float PrizeWon() {
		float _prizeWon = 0;
		for (int i = 1; i <= Lines.Count; i++) {
			if (prizesRaffled.ContainsKey (i)) {
				for (int j = 0; j < Icons.Count; j++) {
					if (prizesRaffled [i].ContainsKey (j)) {
						_prizeWon += prizesRaffled [i] [j];
					}
				}
			}
		}
		return _prizeWon * _Bet;
	}

	public static void visualLine(int i) {
		_inLine += i;
		if (_inLine > Lines.Count)
			_inLine = 1;
		if (_inLine <= 0)
			_inLine = Lines.Count;

		if (GameObject.FindGameObjectWithTag ("lineValue")) {
			GameObject.FindGameObjectWithTag ("lineValue").GetComponent<Text> ().text = _inLine + "";
		}
	}

	public static void fixLine(int i) {
		_inLine = i;
		if (_inLine > Lines.Count)
			_inLine = 1;
		if (_inLine <= 0)
			_inLine = Lines.Count;


		if (GameObject.FindGameObjectWithTag ("lineValue")) {
			GameObject.FindGameObjectWithTag ("lineValue").GetComponent<Text> ().text = _inLine + "";
		}
	}

	public static void setBet(int i) {
		_Bet += (float)i;
		if (_Bet > 10)
			_Bet = 1;
		if (_Bet <= 0)
			_Bet = 10;


		if (GameObject.FindGameObjectWithTag ("BetValue")) {
			GameObject.FindGameObjectWithTag ("BetValue").GetComponent<Text> ().text = _Bet + "";
		}
	}


	public static void uniqueLine(int i) {
		Lines [i].VisualLine ();
	}

	public static void LineViewer() {
		DestroyLines ();
		for (int i = 1; i < inLine+1; i++) {
			if (Lines.ContainsKey (i)) {
				Lines [i].VisualLine ();
			}
		}
	}

	public static void DestroyLines() {
		GameObject[] vlines = GameObject.FindGameObjectsWithTag ("vLine");
		foreach (GameObject lin in vlines) {
			GameObject.Destroy (lin); 
		}
		GameObject[] nlines = GameObject.FindGameObjectsWithTag ("nLine");
		for (int i = 0; i < nlines.Length; i++) {
			if(i >= _inLine) nlines [i].GetComponent<Image> ().color = xCor (new Vector4 (105, 105, 105, 255));
		}
	}

	public static void ResetPrizesDic() {
		
		prizesRaffled.Clear ();
		prizedb.Clear ();
	}


		
	public static void checkPrize(Vector3[] raffle) {
		for (int L = 1; L <= _inLine; L++) {
		for (int p = 0; p < Icons.Count; p++) {
			if (Prizes.ContainsKey (p)) {
				for (int t = 0; t < Prizes [p].Sequence.Count; t++) {	
					
						int[] pline = codeprizeToValue (Lines [L].getLine (), raffle);
						if (Prizes [p].sequence (t) == "AAAAA") {
							if (pline [0] == p && pline [1] == p &&
								pline [2] == p && pline [3] == p && pline [4] == p) {
								_isJackpot = true;
								reward (0, 4, L , p, "AAAAA"); 
								Debug.Log("ACUMULADO");
								Jackpot.Instance.payJackpot ();
							}
						} else if (Prizes [p].sequence (t) == "BBBBB") {
							if (pline [0] == p && pline [1] == p &&
								pline [2] == p && pline [3] == p && pline [4] == p) {
								_isBonus = true;
								reward (0, 4, L , p, "BBBBB"); 
								Debug.Log("BONUS 1");
								float[] bv = new float[5] { 200, 400, 600, 800, 1000 };
								BonusBase.Instance.InitBonus (2, bv[Random.Range (0,4)]);
							}
						} else if (Prizes [p].sequence (t) == "0BBBB" && !_isBonus) {
							if (Lines [L].L_or_R == "R") {
								if (pline [0] == p && pline [1] == p &&
									pline [2] == p && pline [3] == p) {
									_isBonus = true;
									reward (0, 3, L , p, "0BBBB"); 
									Debug.Log("BONUS 2");
									float[] bv = new float[5] { 100, 200, 300, 400, 500 };
									BonusBase.Instance.InitBonus (1, bv[Random.Range (0,4)]);
								}
							} else {
								if (pline [1] == p && pline [2] == p &&
									pline [3] == p && pline [4] == p) {
									_isBonus = true;
									reward (1, 4, L , p, "0BBBB"); 
									Debug.Log("BONUS 2");
									float[] bv = new float[5] { 100, 200, 300, 400, 500 };
									BonusBase.Instance.InitBonus (1, bv[Random.Range (0,4)]);
								}
							}
						} else if (Prizes [p].sequence (t) == "00BBB" && !_isBonus) {
							if (Lines [L].L_or_R == "R") {
								if (pline [0] == p && pline [1] == p &&
									pline [2] == p) {
									_isBonus = true;
									reward (0, 2, L , p, "00BBB"); 
									Debug.Log("BONUS 3");
									float[] bv = new float[5] { 50, 100, 150, 200, 250 };
									BonusBase.Instance.InitBonus (0, bv[Random.Range (0,4)]);
								}
							} else {
								if (pline [2] == p && pline [3] == p &&
									pline [4] == p) {
									_isBonus = true;
									reward (2, 4, L , p, "00BBB"); 
									Debug.Log("BONUS 3");
									float[] bv = new float[5] { 50, 100, 150, 200, 250 };
									BonusBase.Instance.InitBonus (0, bv[Random.Range (0,4)]);
								}
							}
						} else if (Prizes [p].sequence (t) == "11111") {
							if (pline [0] == p && pline [1] == p &&
							   pline [2] == p && pline [3] == p && pline [4] == p) {
								reward (0, 4, L , p, "11111"); 
							}
						} else if (Prizes [p].sequence (t) == "01111") {
							if (Lines [L].L_or_R == "R") {
								if (pline [1] == p && pline [2] == p &&
								    pline [3] == p && pline [4] == p) {
									reward (1, 4, L , p, "01111"); 
								}
							} else {
								if (pline [0] == p && pline [1] == p &&
								   pline [2] == p && pline [3] == p) { 
									reward (0, 3, L , p, "01111");
								}
							}
						} else if (Prizes [p].sequence (t) == "11110") {
							if (Lines [L].L_or_R == "R") {
								if (pline [0] == p && pline [1] == p &&
								    pline [2] == p && pline [3] == p) { 
									reward (0, 3, L , p, "11110");
								}
							} else {
								if (pline [1] == p && pline [2] == p &&
									pline [3] == p && pline [4] == p) {
									reward (1, 4, L , p, "11110"); 
								}
							}
						} else if (Prizes [p].sequence (t) == "00111") {
							if (Lines [L].L_or_R == "R") {
								if (pline [2] == p && pline [3] == p &&
								    pline [4] == p) { 
									reward (2, 4, L , p, "00111"); 
								}
							} else {
								if (pline [0] == p && pline [1] == p &&
								   pline [2] == p) { 
									reward (0, 2, L , p, "00111"); 
								}
							}
						} else if (Prizes [p].sequence (t) == "01110") {
							if( pline[1] == p && pline[2] == p &&
								pline[3] == p ) 
							{ 
								reward (1, 3, L , p, "01110"); 
							}
						} else if (Prizes [p].sequence (t) == "11100") {
							if (Lines [L].L_or_R == "R") {
								if (pline [0] == p && pline [1] == p &&
								    pline [2] == p) { 
									reward (0, 2, L , p, "11100"); 
								}
							} else {
								if (pline [2] == p && pline [3] == p &&
									pline [4] == p) { 
									reward (2, 4, L , p, "11100"); 
								}
							}
						}
						//
				 	}
				}
			}
		}


	}

	static Dictionary<int, string> prizedb = new Dictionary<int, string> ();

	static void reward(int frst,int ends, int line, int icon, string code) {

		if (!prizedb.ContainsKey (line)) {
			prizedb.Add (line, icon+"."+code);
			if(!DemoMode) 
				_SoundCtrl.PlayPrizeSound (icon, code);

			for (int a = frst; a <= ends; a++) {

				GameObject tmp = Resources.Load ("frame") as GameObject;
				Color tmpcor = Lines [line].myColor;
				
				GameObject objnew = GameObject.Instantiate (tmp, slotsRaffle (line) [a].transform.position, Quaternion.identity, GameObject.Find ("Frames").transform);
				objnew.GetComponent<FrameMarker> ().setAllInfos (icon, tmpcor, Color.black);
				objnew.tag = "frame";
				objnew.name = "frame_" + line + "_" + a;
	
				if (markFrames.ContainsKey (line)) {
					List<GameObject> lst = markFrames [line];
					lst.Add (objnew);
					markFrames.Remove (line);
					markFrames.Add (line, lst);
				} else {
					List<GameObject> lst = new List<GameObject> ();
					lst.Add (objnew);
					markFrames.Add (line, lst);
				}
				uniqueLine (line);
			}

			float _v = Prizes [icon].Value (code);
			if (prizesRaffled.ContainsKey (line) == false) {
				Dictionary<int, float> _x = new Dictionary<int, float> ();
				_x.Add (icon, _v);
				prizesRaffled.Add (line, _x);
			} else {
				if (!prizesRaffled [line].ContainsKey (icon)) {
					prizesRaffled [line].Add (icon, _v);
				}
			}
		} 
	}

	public static int[] codeprizeToValue(string[] linecode, Vector3[] raffle) {
		int[] toreturn = { 0, 0, 0, 0, 0 };
		for (int i = 0; i < 5; i++) {
			if (linecode [i] [0].ToString () == "x") {
				toreturn [i] = (int)raffle [i].z;

			} else if (linecode [i] [0].ToString () == "y") {
				toreturn [i] = (int)raffle [i].y;
			
			} else if (linecode [i] [0].ToString () == "z") {
				toreturn [i] = (int)raffle [i].x;
			} else {
				Debug.LogError ("Paremetro incorreto");
				return null;
			}
		}
		return toreturn;
	}


	public static void AnimeIconParticleCtrl(bool state) {
		if (state) {
			if(particlesCoins.isStopped) 
				particlesCoins.Play ();
			if(!Globals.DemoMode)
				_SoundCtrl.PlaySoundName (Globals._SoundCtrl.SND_COINSPAY, true, _SoundCtrl._CoinsSound);
		} else {
			if(particlesCoins.isPlaying) 
				particlesCoins.Stop ();
			
			_SoundCtrl.stopSound (_SoundCtrl._CoinsSound);
		}
	}

	public static void addAnim(int id, RuntimeAnimatorController anim)
	{
		Animes.Add (id, anim);
	}

	public static void upIcons(int key, Sprite img) {
		Icons.Add (key, img);
	}

	public static int[] lineInRaffle(Vector3[] raffle,int line) {

		int[] toreturn = new int[5];

		if (raffle.Length != 5 || line == 0) {
			Debug.LogError ("Erro de paramentros");
			return null;
		}
			
		string[] isline = Lines[line].getLine();
	

		if (isline == null) {
			Debug.LogError ("Erro de paramentros");
			return null;
		} else {
			int[] raffleline = new int[5];
			for (int i = 0; i < 5; i++) {
				string xtmp = "x" + i ;
				string ytmp = "y" + i ;
				string ztmp = "z" + i ;

				if (string.Compare(isline[i],xtmp) == 0) {
					
					raffleline [i] = (int)raffle [i].z;

				} else if (string.Compare(isline[i],ytmp) == 0) {
					
					raffleline [i] = (int)raffle [i].y;

				} else if (string.Compare(isline[i],ztmp) == 0) {
					
					raffleline [i] = (int)raffle [i].x;

				}
			}
			toreturn = raffleline;
		}

		return toreturn;
	}


	public static GameObject[] slotsRaffle(int line) {
		
		GameObject[] toreturn = new GameObject[5];

		if (line == 0) {
			Debug.LogError ("Erro de paramentros");
			return null;
		}

		string[] isline = Lines[line].getLine();

		if (isline == null) {
			Debug.LogError ("Erro de paramentros");
			return null;
		} else {

			for (int i = 0; i < 5; i++) {
				string xtmp = "x" + i ;
				string ytmp = "y" + i ;
				string ztmp = "z" + i ;

				if (isline[i] == xtmp) {

					toreturn [i] = _Slots.getRealIcon (i, 0);

				} else if (string.Compare(isline[i],ytmp) == 0) {

					toreturn [i] = _Slots.getRealIcon (i, 1); 

				} else if (string.Compare(isline[i],ztmp) == 0) {

					toreturn [i] = _Slots.getRealIcon (i, 2);

				}
			}
		}

	 return toreturn;
	}


	public static Vector4 xCor(Vector4 vct){
		Vector4 color = new Vector4(vct.x/255, vct.y/255, vct.z/255, vct.w/255);
		return color;
	}

	public static bool inRollStage(bool[] boolarray)
	{
		for (int i = 0; i < boolarray.Length; i++)
		{
			if (boolarray[i])
			{
				return boolarray[i];
			}
		}
		return false;
	}

	public static void OnCHECKPRIZE() {
		RoundValidation VALIDATION = RoundValidation.Instance;
		if (prizesRaffled.Count > 0) {
			Slots.Instance._stage = PAYPRIZE; 
			AnimeIconParticleCtrl (true);
			Slots.Instance.tempGain = Gain;
			Slots.Instance.tempPay = Credit;
			if (!DemoMode) {
				VALIDATION.finishRound ();
				Credit += Gain;
				Gain = 0;
			}
			SaveLoad.AddData ("Credit", Credit.ToString ());

		} else {
			Slots.Instance._stage = FREETOPLAY;
			VALIDATION.finishRound ();
			if (DemoMode)
				Slots.Instance.limitSortDemo = 0;
		}

	}

	public static void OnPAYPRIZE() {
		Slots.Instance.cntPayTime += Time.deltaTime;
		if (Slots.Instance.cntPayTime >= 0.1f) {
			Slots.Instance.cntPayTime = 0;
			Slots.Instance.tempGain--;
			Slots.Instance.tempPay++;
			Slots.Instance.limitPayDemo++;
			if (Slots.Instance.limitPayDemo >= 50 && DemoMode)
				Slots.Instance.tempGain = 0;
			if (Slots.Instance.limitPayDemo >= 30 && !DemoMode && autoPlay) {
				Slots.Instance.Sortition ();
				Slots.Instance.limitPayDemo = 0;
			}
		}
		GameObject.FindWithTag ("GainValue").GetComponent<Text> ().text = ((int)Slots.Instance.tempGain).ToString();
		if (!DemoMode)
			GameObject.FindWithTag ("Credits").GetComponent<Text> ().text = ((int)Slots.Instance.tempPay).ToString();

		if (Slots.Instance.tempGain <= 0) {
			Slots.Instance._stage = FREETOPLAY;
			AnimeIconParticleCtrl (false);
			if (DemoMode) {
				Slots.Instance.limitSortDemo = 0;
			}
		}
	}

	public static void OnPLAYDEMOMODE() {
		Slots.Instance.limitSortDemo += Time.deltaTime;
		if (Slots.Instance.limitSortDemo > 2) {
			Slots.Instance.SortitionDemo ();
			visualLine (Lines.Count - inLine);
			LineViewer ();
			DestroyLines ();
		}
	}


	public static void OnEnterDEMOMODE() {
		Slots.Instance.limitSortDemo += Time.deltaTime;
		DemoMode = true;
		if (Slots.Instance.limitSortDemo > 2) {
			Slots.Instance.SortitionDemo ();
			visualLine (Lines.Count - inLine);
			LineViewer ();
			DestroyLines ();
		}
	}

}
