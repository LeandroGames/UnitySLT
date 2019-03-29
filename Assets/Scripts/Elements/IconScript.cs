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

public class IconScript : MonoBehaviour {

	public int IconID = 0;
	Animator myAnime = null;

	public int iconId
	{
		get { return IconID; }
		set 
		{ 
			IconID = value;
			setIcon (IconID);
		}
	}

	public Sprite iconSprite
	{
		get { return this.gameObject.GetComponent<Image> ().sprite; }
	}

	public void setIcon(int id)
	{
		if (Globals.Icons.ContainsKey (id)) {
			//if(this.gameObject.GetComponent<Animator>())
			//	this.gameObject.GetComponent<Animator> ().runtimeAnimatorController = null;
			this.gameObject.GetComponent<Image> ().sprite = Globals.Icons[id];

			GameObject[] frames = GameObject.FindGameObjectsWithTag ("frame");

			foreach (GameObject frm in frames) {
				Destroy (frm);
			}
		} 
	}

	public void SetRandomIcon() {
		IconID = Random.Range (0, Globals.Icons.Count);
		setIcon (IconID);
	}

	public void SetHackerdIcon(int icon) {
		IconID = icon;
		setIcon (IconID);
	}

	public void receiveAnimation(RuntimeAnimatorController anime) {
		myAnime.runtimeAnimatorController = anime;
	}

	public void stopAnimation() {
		myAnime.runtimeAnimatorController = null;
	}

	void Start() {
		SetRandomIcon ();
		myAnime = this.gameObject.GetComponent<Animator> ();
	}

}
