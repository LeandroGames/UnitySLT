using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//using com.spacepuppy;
//using com.spacepuppy.Collections;
using RotaryHeart.Lib.SerializableDictionary;

[System.Serializable()]
public class SoundControll : GenericSingleton<SoundControll> {

	public string SND_MAIN = "";
	public string SND_COINSPAY = "";
	public string SND_CASHIN = "";
	public string SND_ROLLSLOTS = "";
	public string SND_LINEMORE = "";
	public string SND_LINELESS = "";
	public string SND_BETMORE = "";
	public string SND_BETLESS = "";
	public string SND_NOCREDITS = "";

	private AudioSource AudioSrc;
	private AudioSource BackSound;
	private AudioSource CoinsSound;
	private AudioSource SlotsSound;
	private List<AudioClip> Queue = new List<AudioClip>();

	public AudioSource _AudioSrc { get { return AudioSrc; } } 
	public AudioSource _BackSound { get { return BackSound; } } 
	public AudioSource _CoinsSound { get { return CoinsSound; } } 
	public AudioSource _SlotsSound { get { return SlotsSound; } } 
	public List<AudioClip> _Queue { get { return Queue; } }

	public float backSndVolume = 0.2f;

	private AudioClip previousClip;

	[SerializeField()]
	public indexDictionary PrizeSounds;

	[SerializeField()]
	public nameDictionary namedSounds;

	[SerializeField()]
	public nameDictionary subPrizeSounds;

	[System.Serializable]
	public class indexDictionary : SerializableDictionaryBase<int, AudioClip> { }

	[System.Serializable]
	public class nameDictionary : SerializableDictionaryBase<string, AudioClip> { }


	void Start () {
		AudioSrc = this.gameObject.GetComponent<AudioSource>();
		CoinsSound = GameObject.FindWithTag ("CoinParticle").GetComponent<AudioSource> ();
		SlotsSound = Slots.Instance.gameObject.GetComponent<AudioSource> ();
		BackSound = GameObject.FindWithTag ("mainaudio").GetComponent<AudioSource> ();
		BackSound.volume = backSndVolume;
	}


	void Update() {
		if (Queue.Count > 0) {
			if (previousClip != Queue [0]) {
				if (!AudioSrc.isPlaying) {
					previousClip = Queue [0];
					AudioSrc.clip = previousClip;
					if(!Globals.DemoMode)
						AudioSrc.Play ();
					Queue.RemoveAt (0);
				}
			} else {
				Queue.RemoveAt (0);
			}
		}
	}

	public void clearQueue() {
		Queue.Clear ();
		previousClip = null;
	}

	/*
	 * @ Subprize use name format: 0.01110  "icon.prizecode" 
	 * 
	 */
	public void PlayPrizeSound(int iconIndex, string code = "") {
		//Debug.Log("CODE: "+ iconIndex + "." + code);
		if (subPrizeSounds.ContainsKey (iconIndex + "." + code)) {
			if (AudioSrc.clip != subPrizeSounds [iconIndex + "." + code]) {
				if (AudioSrc.isPlaying) {
					if (!Queue.Contains (subPrizeSounds [iconIndex + "." + code]) && !Globals.DemoMode)
						Queue.Add (subPrizeSounds [iconIndex + "." + code]);
				} else
					AudioSrc.clip = subPrizeSounds [iconIndex + "." + code];
			}
			if (!AudioSrc.isPlaying)
				AudioSrc.Play ();
		} else
		if (PrizeSounds.ContainsKey (iconIndex) && !subPrizeSounds.ContainsKey (iconIndex + "." + code)) {
			if (AudioSrc.clip != PrizeSounds [iconIndex]) {
				if (AudioSrc.isPlaying) {
					if (!Queue.Contains (PrizeSounds [iconIndex]) && !Globals.DemoMode)
						Queue.Add (PrizeSounds [iconIndex]);
				} else
					AudioSrc.clip = PrizeSounds [iconIndex];
			}
			if (!AudioSrc.isPlaying)
				AudioSrc.Play ();
		} 
	}


	public void PlaySoundName(string name, bool inloop = false, AudioSource src = null) {
		if (src == null)
			src = AudioSrc;
		if(namedSounds.ContainsKey(name)) {
			src.loop = inloop;
			if (src.clip != namedSounds [name])
				src.clip = namedSounds [name];
			else
				src.Stop ();
			src.Play ();
		}
	}

	public void stopSound(AudioSource src) {
		src.Stop ();
	}

	public void backVolume(float vol) {
		BackSound.volume = vol;
	}

	public void swapBackSound(AudioClip newsoud) {
		BackSound.clip = newsoud;
	}

	public void backSndLoop(bool inloop) {
		BackSound.loop = inloop;
	}

	public void triggerBackSound(bool isplay) {
		if (isplay) {
			if(BackSound.isPlaying) 
				BackSound.Play ();
		} else
			if(!BackSound.isPlaying) 
				BackSound.Stop ();
	}

}

