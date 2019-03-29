/*
 *  Leandro M. da Costa - 03/2017
 *  
 *  Slots Unity Projec 
 * 
 *  OBSOLETO
 *  OBSOLETO
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Frame : MonoBehaviour {

	public Sprite[] frames;
	private Image mainFrame;
	public float fps = 0.5f;
	bool show = true;
	float timer = 0;
	int frameindex = 0;

	public bool showing {
		get { return show; }
		set { 
			show = value;
		}
	}

	void Start () {
		mainFrame = this.GetComponent<Image> ();
	}

	void Update () {
		if (show) {
			timer += Time.deltaTime;
			if (timer >= fps) {
				frameindex++;
				timer = 0;
			}
			if (frameindex >= frames.Length) {
				frameindex = 0;
			}
			mainFrame.sprite = frames [frameindex];
		} 
	}
}
