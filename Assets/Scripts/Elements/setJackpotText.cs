using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setJackpotText : MonoBehaviour {


	void Awake () {
		GetComponent<Text> ().text = Jackpot.Instance.MonetaryString( Jackpot.Instance.jackpot_current);
	}
	

}
