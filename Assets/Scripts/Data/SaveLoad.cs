using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[System.Serializable]
public static class SaveLoad {

	private static Dictionary<string,string> savedData = new Dictionary<string, string>();

	public static void AddData(string name, string value) {
		if (savedData.ContainsKey (name))
			savedData.Remove (name);
		savedData.Add (name, value);
		Save ();
	}

	public static bool ContainsThis(string str) {
		Load ();
		return savedData.ContainsKey (str);
	}

	public static string getData(string name) {
		Load ();
		return savedData.ContainsKey (name) ? savedData [name] : null;
	}

	private static void Save() {
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create (Application.persistentDataPath + "/0001.gd");
		bf.Serialize(file, SaveLoad.savedData);
		file.Close();
	}   

	private static void Load() {
		if(File.Exists(Application.persistentDataPath + "/0001.gd")) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/0001.gd", FileMode.Open);
			SaveLoad.savedData = (Dictionary<string,string>)bf.Deserialize(file);
			file.Close();
		}
	}


}