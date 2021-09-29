using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class SaveInfo : MonoBehaviour {
    [SerializeField] private int saveNumber;
    
    private Text saveTimeText;

    private void Start() {
        transform.Find("panel_saveInfo/text_saveNumber").gameObject.GetComponent<Text>().text = "Save " + saveNumber;

        saveTimeText = transform.Find("panel_saveInfo/text_saveTime").gameObject.GetComponent<Text>();

        Load(true);
    }

    private void UpdateTime(DateTime time) {
        saveTimeText.text = time.ToString("MM/dd/yy hh:mm tt");
    }

    private SaveData CreateSaveData() {
        SaveData data = new SaveData();

        DateTime time = DateTime.UtcNow;
        UpdateTime(time.ToLocalTime());
        data.time = time.ToBinary();

        return data;
    }

    private void LoadFromSaveData(SaveData data) {
        // TODO
    }

    private String GetSavePath() {
        return Application.persistentDataPath + "/playerSave" + saveNumber + ".dat";
    }

    public void Save() {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(GetSavePath());
        bf.Serialize(file, CreateSaveData());
        file.Close();
        Debug.Log("Saved: " + saveNumber);
    }

    public void Load() {
        Load(false);
    }

    private void Load(bool updateTime) {
        String path = GetSavePath();
        
        if (!File.Exists(path)) {
            return;
        }
        
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(path, FileMode.Open);
        SaveData data = (SaveData) bf.Deserialize(file);
        file.Close();

        if (updateTime) {
            UpdateTime(DateTime.FromBinary(data.time).ToLocalTime());
        }
        else {
            LoadFromSaveData(data);
            Debug.Log("Loaded: " + saveNumber);
        }
    }
}

[Serializable]
public class SaveData {
    public long time;
}