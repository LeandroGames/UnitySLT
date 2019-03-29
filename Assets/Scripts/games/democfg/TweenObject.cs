using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class TweenObject : MonoBehaviour {


	public bool shakeOnStart = false;
	public bool zoomOnStart = false;
	public double Xpos = .1;
	public double Ypos = .1;
	public double itime = .4;
	public iTween.EaseType easeType = iTween.EaseType.punch;
	public iTween.LoopType loopType = iTween.LoopType.none;
	public double delay = .6;
	public GameObject[] Objects;


	void Start () {
		if (Objects.Length < 1)
			return;
		
		if (shakeOnStart) {
			Shake ();
		}

		if (zoomOnStart) {
			ZoomOut ();
		}
	}

	public void Shake() {
		foreach(GameObject ob in Objects)
			iTween.ShakePosition (ob, iTween.Hash ("x", Xpos, "y", Ypos, "easeType", easeType, "loopType", loopType, "delay", delay));
	}

	public void zoomin() {
		foreach(GameObject ob in Objects)
			iTween.ScaleTo(ob, iTween.Hash ("x", Xpos, "y", Ypos, "easeType", easeType, "loopType", loopType, "delay", delay, "time", itime, "oncomplete", "active"));

	}

	public void active() {
		Globals.ActiveToUse = true;
	}

	public void  ZoomOut() {
		foreach(GameObject ob in Objects)
			iTween.ScaleTo(ob, iTween.Hash ("x", .0, "y", .0,"easeType", iTween.EaseType.easeInSine, "loopType", iTween.LoopType.none, "delay", .2,"oncomplete", "zoomin", "time", 0));
	}

	
}
