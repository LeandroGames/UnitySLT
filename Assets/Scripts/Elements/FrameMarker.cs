using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class FrameMarker : MonoBehaviour {

	public UILineRenderer Edge;
	public GameObject Icon;
	public Image BackIcon;
	public Image FireAnim;

	public void setColorEdge(Color Cor) {

		Edge.GetComponent<UILineRenderer> ().color = Cor;
	}

	public void setColorBackIcon(Color Cor) {

		BackIcon.color = Cor;
	}

	public void FrameActive(bool state) {
		Edge.enabled = state;
		Icon.GetComponent<Image>().enabled = state;
		BackIcon.enabled = state;
		FireAnim.enabled = state;
	}

	public void Anime(int number) {
		Icon.GetComponent<Animator> ().SetInteger ("icon", number);
	}

	public void setAllInfos(int number, Color colorEdge, Color colorBack) {
		Edge.GetComponent<UILineRenderer> ().color = colorEdge;
		BackIcon.color = colorBack;

		if (Globals.Icons.ContainsKey (number))
			Icon.GetComponent<Image> ().sprite = Globals.Icons [number];
		else {
			Debug.LogError ("erro de parametros");
		}

		Icon.AddComponent<Animator> ();
		Icon.GetComponent<Animator> ().runtimeAnimatorController = Resources.Load ("Animation/IconCtrl") as RuntimeAnimatorController;
		Icon.GetComponent<Animator> ().enabled = true;
		Icon.GetComponent<Animator> ().SetInteger ("icon", number);
	}

}
