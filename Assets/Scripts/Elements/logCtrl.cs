using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class logCtrl : GenericSingleton<logCtrl> {

	public GameObject content;
	public GameObject RowPrefab;
	private List<regRow> regs;
	private List<GameObject> listrow = new List<GameObject>();


	public void Populate () {
		RoundValidation VALIDATION = RoundValidation.Instance;
		foreach (GameObject obj in listrow) {
			Destroy (obj);
		}
		List<DataRound> rounds = VALIDATION.savedRound;
		foreach (DataRound rnd in rounds) {
			
			GameObject objrow = Instantiate (RowPrefab, content.transform) as GameObject;
			regRow reg = objrow.GetComponent<regRow> ();
			reg.reg (
				rnd.ID.ToString(),
				rnd.LINE.ToString(),
				rnd.BET.ToString(),
				rnd.CREDIT_PREVIOUS.ToString(),
				rnd.CREDIT_CURRENT.ToString(),
				rnd.CREDIT_PAID.ToString(),
				rnd.SORTITION,
				rnd.DATATIME.ToString(),
				rnd.PRIZE.ToString() 
			);
			listrow.Add (objrow);
				
		}
	}
	

}
