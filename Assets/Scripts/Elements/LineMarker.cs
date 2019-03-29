/*
 *  Leandro M. da Costa - 03/2017
 *  
 *  Slots Unity Projec 
 *  
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI.Extensions;

public class LineMarker : MonoBehaviour {
	
	private UILineRenderer lR;
	Vector2[] positions;
	private bool show = false;
	public bool Show { get { return show; } set { show = value; } }
	private int _line = 0;
	public int line { get { return _line; } }

	public Vector3 positionNumberR { 
		get {
			Vector2 a = new Vector2 ();
			a.x = positions [0].x -0.2f;
			a.y = positions [0].y;
			return a;
		}
	}

	public Vector2 positionNumberL { 
		get {
			Vector2 a = new Vector2 ();
			a.x = positions [positions.Length -1].x +0.2f;
			a.y = positions [positions.Length -1].y;
			return a;
		}
	}

	public void initLine (Vector2[] points, Color Cor, float size, string LorR , int index) {
		_line = index;
		if (!GetComponent<UILineRenderer> ())
			this.gameObject.AddComponent<UILineRenderer> ();
		lR = GetComponent<UILineRenderer>();
		Vector2[] _positions = new Vector2[points.Length];
		for (int i = 0; i < points.Length; i++) {
			_positions [i] = new Vector2(points [i].x, points [i].y);
		}
		positions = _positions;
		lR.Points = positions;
		lR.LineThickness = size;
		lR.color = Cor;
		lR.material = new Material (Shader.Find ("Sprites/Default"));
		show = true;

	}
		

}