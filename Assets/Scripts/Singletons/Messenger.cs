using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Messenger : GenericSingleton<Messenger> {
	

	private int slot_stage = -1;
	private int old_slot_stage = 0;
	private Text _status;
	private bool blink = false;
	private bool swp = false;
	private float cnt = 0;
	private Color[] colors = new Color[2];
	private float timecount = 0;
	public bool insufficientCredit = false;
	private float cntCicles = 0;

	void Start () {
		_status = GameObject.FindWithTag ("Status").GetComponent<Text>();
		_sendMessege (messages.WAIT, true, Color.yellow, Color.white, 0.05f);
	
	}


	public void _sendMessege(string msg, bool _blink, Color color_now, Color color_blink, float fps) {
		_status.text = msg;
		blink = _blink;
		colors[0] = color_now;
		colors[1] = color_blink;
		timecount = fps;
	}

	public string currentMessage {
		get { 
			return _status.text; 
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (blink) {
			cnt = cnt >= timecount ? 0 : cnt + Time.deltaTime;
			if (cnt == 0)
				swp = !swp;
			if (swp) {
				if(_status.color != colors[0]) 
					_status.color = colors[0];
			} else {
				if(_status.color != colors[1]) 
					_status.color = colors[1];
			}
		}
		if (!insufficientCredit) {
			slot_stage = Globals._Slots.Stage;
			if (old_slot_stage != slot_stage) {
				old_slot_stage = slot_stage;
				switch (slot_stage) {
				case Globals.FREETOPLAY:
					if (Globals.DemoMode)
						_sendMessege (messages.INSERTCOIN, true, Color.red, Color.white, 0.1f);
					else {
						_sendMessege (messages.PRESSTOPLAY, true, Color.red, Color.yellow, 0.1f);
					}
					break;
				case Globals.PLAYING:
					_sendMessege (messages.GOODLUCK, true, Color.red, Color.blue, 0.2f);
					break;
				case Globals.CHECKPRIZE:
					_sendMessege (messages.GOODLUCK, true, Color.white, Color.red, 0.1f);
					break;
				case Globals.PAYPRIZE:
					_sendMessege (messages.congratulations, true, Color.white, Color.red, 0.1f);
					break;
				default:
					break;
				}
			}
		} else {
			if(currentMessage != messages.WITHOUTCREDIT)
				_sendMessege (messages.WITHOUTCREDIT, true, Color.yellow, Color.red, 0.1f);
			cntCicles += Time.deltaTime;
			if (cntCicles >= 2) {
				cntCicles = 0;
				insufficientCredit = false;
				_sendMessege (messages.PRESSTOPLAY, true, Color.red, Color.yellow, 0.1f);
			}

		} 

		if(Messenger.Instance.currentMessage == messages.WAIT && Globals.Credit > 0)
			Messenger.Instance._sendMessege (messages.PRESSTOPLAY, true, Color.red, Color.yellow, 0.1f);
	}




}

