/*
 *  Leandro M. da Costa - 03/2017
 *  
 *  Slots Unity Projec 
 *  
 */
using System.Collections;
using System.Collections.Generic;


public class Prize   {

	int Index = 0;

	Dictionary<string, float> _prizesValue = new Dictionary<string, float> ();
	Dictionary<int, string> _prizeSec = new Dictionary<int, string> (); 

	public Prize(int _index) {
		Index = _index;
	}

	public int index {
		get { return Index; }
	}

	public float Value (string code) {
		if (_prizesValue.ContainsKey(code)) {
			return _prizesValue [code];
		}
		return 0;
	}

	public string sequence(int _index) {
		if (_prizeSec.ContainsKey(_index)) {
			return _prizeSec [_index];
		}
		return null;
	}


	public List<string> Sequence {
		get { 
			List<string> _sequences = new List<string>();
			for (int i = 0; i < _prizeSec.Count; i++) {
				if (_prizeSec.ContainsKey (i)) {
					_sequences.Add (_prizeSec [i]);
				}
			}
			return _sequences; 
		}
	}

	public void addPrize(string _sequence, float _prizeValue) {
		_prizesValue.Add (_sequence, _prizeValue);
		_prizeSec.Add (_prizeSec.Count, _sequence);
	}


}
