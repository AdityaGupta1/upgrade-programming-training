using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveInfo : MonoBehaviour {
    // SaveInfo display fields
    [SerializeField] private int saveNumber;
    private Text saveTimeText;

    // menu elements
    [SerializeField] private GameObject canvas;
    private Menu menu;

    // objects that save to and load from SaveData
    [SerializeField] private GameObject player;
    private PlayerControl playerControl;

    private void Start() {
        transform.Find("panel_saveInfo/text_saveNumber").gameObject.GetComponent<Text>().text = "Save " + saveNumber;
        saveTimeText = transform.Find("panel_saveInfo/text_saveTime").gameObject.GetComponent<Text>();

        menu = canvas.GetComponent<Menu>();

        playerControl = player.GetComponent<PlayerControl>();

        Load(true);
    }

    private void UpdateTime(DateTime time) {
        saveTimeText.text = time.ToString("MM/dd/yy hh:mm tt");
    }

    private void UpdateSaveData() {
        SaveData data = SaveData.instance();

        DateTime time = DateTime.UtcNow;
        UpdateTime(time.ToLocalTime());
        data.time = time.ToBinary();

        data.scene = SceneManager.GetActiveScene().name;
        playerControl.SaveData(data);
    }

    private void LoadData(SaveData data) {
        SaveData.setSaveData(data);
        SceneManager.LoadScene(data.scene);
    }

    private String GetSavePath() {
        return Application.persistentDataPath + "/playerSave" + saveNumber + ".dat";
    }

    public void Save() {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(GetSavePath());
        UpdateSaveData();
        bf.Serialize(file, SaveData.instance());
        file.Close();
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
        } else {
            LoadData(data);
            menu.Close();
        }
    }
}

[Serializable]
public class SaveData {
    private static SaveData INSTANCE = new SaveData();

    private SaveData() { }

    public static SaveData instance() {
        return INSTANCE;
    }

    public static void setSaveData(SaveData data) {
        INSTANCE = data;
    }

    public long time;

    public PlayerSaveData playerData = new PlayerSaveData();
    public bool tookTreasure = false;
    public string scene = "level 1";
}