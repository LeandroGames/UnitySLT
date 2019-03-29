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

public class Roll : MonoBehaviour {

	public GameObject slotTop;
	public GameObject slotMeans;
	public GameObject slotLow;

	public GameObject slotLast3;
	public GameObject slotLast2;
	public GameObject slotLast1;

	public GameObject[] realsIcons {
		get {
			GameObject[] tmp = new GameObject[3];
			tmp [0] = slotLast1;
			tmp [1] = slotLast2;
			tmp [2] = slotLast3;
			return tmp;
		}
	}

	public void setLast(int a, int b, int c)
	{
		slotLast3.GetComponent<IconScript> ().iconId = a;
		slotLast2.GetComponent<IconScript> ().iconId = b;
		slotLast1.GetComponent<IconScript> ().iconId = c;
	}



	public void setNew(int a, int b, int c)
	{
		slotTop.GetComponent<IconScript> ().iconId = a;
		slotMeans.GetComponent<IconScript> ().iconId = b;
		slotLow.GetComponent<IconScript> ().iconId = c;
	}



}
