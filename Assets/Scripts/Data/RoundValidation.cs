using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class RoundValidation : GenericSingleton<RoundValidation> {

	public List<DataRound> savedRound = new List<DataRound>();
	public List<RegRound> RoundRegister = new List<RegRound>();


	public void registerRound() {

		string mycode = RoundRegister.Count.ToString();
		RegRound reg = new RegRound ();
		reg.DataTimeInit = System.DateTime.Now.ToString ("yyyy/MM/dd hh:mm:ss");
		reg.roundID = mycode;
		RoundRegister.Add (reg);
		SaveReg ();

	}

	public void finishRound() {
		string mycode = (RoundRegister.Count -1).ToString();
		foreach (RegRound reg in RoundRegister) {
			if (reg.DataTimeInit != "" && reg.roundID == mycode) {
				reg.DataTimeEnd = System.DateTime.Now.ToString ("yyyy/MM/dd hh:mm:ss");
				reg.finished = true;
			} else if(reg.DataTimeInit == "") {
				Debug.LogError ("Erro de registro");
			}
		}
		SaveReg ();
		addDataRoud ();
	}

	public void addDataRoud() {

		foreach (RegRound reg in RoundRegister) {
			if (reg.roundID == (RoundRegister.Count - 1).ToString()) {
				if (reg.finished) {
					DataRound Dat = new DataRound ();
					Dat.ID = (RoundRegister.Count -1).ToString();
					Dat.LINE = Globals.inLine;
					Dat.BET = Globals.Bet;
					Dat.CREDIT_PREVIOUS = Globals.Credit + (Globals.inLine * Globals.Bet);
					Dat.CREDIT_PAID = Globals.inLine * Globals.Bet;
					Dat.CREDIT_CURRENT = Globals.Credit  +  Globals.Gain;
					Dat.SORTITION = 
					Globals._Slots.raffledReg [0].x + "" +
					Globals._Slots.raffledReg [1].x + "" +
					Globals._Slots.raffledReg [2].x + "" +
					Globals._Slots.raffledReg [3].x + "" +
					Globals._Slots.raffledReg [4].x + "." +
					Globals._Slots.raffledReg [0].y + "" +
					Globals._Slots.raffledReg [1].y + "" +
					Globals._Slots.raffledReg [2].y + "" +
					Globals._Slots.raffledReg [3].y + "" +
					Globals._Slots.raffledReg [4].y + "." +
					Globals._Slots.raffledReg [0].z + "" +
					Globals._Slots.raffledReg [1].z + "" +
					Globals._Slots.raffledReg [2].z + "" +
					Globals._Slots.raffledReg [3].z + "" +
					Globals._Slots.raffledReg [4].z;
					Dat.DATATIME = System.DateTime.Now.ToString ("yyyy/MM/dd hh:mm:ss");
					Dat.PRIZE = Globals.Gain;
					savedRound.Add (Dat);
					SaveData ();
				} else {
					Debug.LogError ("Registro não finalizado");
				}
			}
		}
			
	}

	public void SaveData() {
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create (Application.persistentDataPath + "/0002.gd");
		bf.Serialize(file, savedRound);
		file.Close();
	}

	public void LoadData() {
		if(File.Exists(Application.persistentDataPath + "/0002.gd")) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/0002.gd", FileMode.Open);
			savedRound = (List<DataRound>)bf.Deserialize(file);
			file.Close();
		}
	}

	public void SaveReg() {
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create (Application.persistentDataPath + "/0003.gd");
		bf.Serialize(file, RoundRegister);
		file.Close();
	}

	public void LoadReg() {
		if(File.Exists(Application.persistentDataPath + "/0003.gd")) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/0003.gd", FileMode.Open);
			RoundRegister = (List<RegRound>)bf.Deserialize(file);
			file.Close();
		}
	}
}
