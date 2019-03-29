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
using UnityEngine.UI.Extensions;

public class Line {

	private GameObject _points;
	private Color _Cor;


	public int nLine = 0;
	Vector3[] Matrix = new Vector3[5];
	string _lr = "R";

	public string L_or_R {
		get {  return _lr; }
	}

	public Color myColor {
		get { return _Cor; }
	}

	public Line(Vector3[] newLine, int number, string direction, GameObject points, Color Cor, float size) {
		if (number == 0) {
			Debug.LogError ("Erro de paramentros");
		}
		Matrix = newLine;
		nLine = number;
		_lr = direction;
		_points = points;
		_Cor = Cor;
		VisualLine ();
	}

	public void VisualLine() {
		GameObject tmp = _points as GameObject;
		tmp.tag = "vLine";
		tmp.name = "vLine" + nLine;
		//Debug.Log (tmp.name + " < > " + tmp.tag);

		Transform _PosTmp = Slots.Instance.GetComponent<Transform>();

		if (GameObject.Find ("vLine" + nLine + "(Clone)")) {
			GameObject[] _a = GameObject.FindGameObjectsWithTag ("vLine");
			foreach (GameObject _b in _a) {
				if (_b.name == "vLine" + nLine + "(Clone)")
					GameObject.Destroy (_b);
			}
			GameObject.Destroy (GameObject.Find ("vLine" + nLine + "(Clone)"));
		}

		GameObject dst = GameObject.Find (tmp.name + "(Clone)");
		if (dst) {
			GameObject.Destroy (dst);
		}
		GameObject.Instantiate (tmp, _PosTmp.TransformPoint(new Vector3(5,30,0)), tmp.transform.localRotation, GameObject.Find("Lines").transform);


	
		GameObject tmp2 = Resources.Load ("numberLine") as GameObject;
		//tmp2.transform.localScale = Globals._Slots.gameObject.transform.localScale;
		tmp2.tag = "nLine";
		tmp2.name = "numberLine" + nLine;
		tmp2.GetComponent<Image> ().color = _Cor; //Globals.xCor(new Vector4 (105,105,105, 255));
		tmp2.transform.Find("Text").GetComponent<Text>().text = nLine.ToString();
		Vector3 numPos = new Vector3 ();
		if (_lr == "R") {
			numPos.x = tmp.GetComponent<UILineRenderer> ().Points [0].x - 55f;
			numPos.y = tmp.GetComponent<UILineRenderer> ().Points [0].y - 20f;
			numPos.z = 0;
		} else {
			numPos.x = tmp.GetComponent<UILineRenderer> ().Points [tmp.GetComponent<UILineRenderer> ().Points.Length -1].x - 35f;
			numPos.y = tmp.GetComponent<UILineRenderer> ().Points [tmp.GetComponent<UILineRenderer> ().Points.Length -1].y - 20f;
			numPos.z = 0;
		}

		if (!GameObject.Find (tmp2.name + "(Clone)")) {
			

			GameObject.Instantiate (tmp2, _PosTmp.TransformPoint(numPos), Quaternion.identity, GameObject.Find ("Numbers").transform);
			//nli.transform.localPosition.Set (nli.transform.localPosition.x, nli.transform.localPosition.y, 0);

		} else {
			GameObject.Find (tmp2.name + "(Clone)").GetComponent<Image> ().color = _Cor;
		}

	}

	public void setColor() {

	}


	public string[] getLine()
	{
		string[] sequence = new string[5];

		for (int i = 0; i < 5; i++) {

			if (Matrix [i].x == 1) {

				sequence [i] = "x" + i;

			} else if (Matrix [i].y == 1) {

				sequence [i] = "y"+ i;

			} else if (Matrix [i].z == 1) {

				sequence [i] = "z"+ i;
			}

		}

		return sequence;
	}



}
