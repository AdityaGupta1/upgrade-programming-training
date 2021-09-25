using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class PlayerMovement : MonoBehaviour {
    private Rigidbody2D rb2D;
    private Animator animator;

    void Start() {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
        LoadGame();
    }

    [SerializeField] private float moveSpeed = 2f;

    private static readonly int Moving = Animator.StringToHash("moving");
    private static readonly int Direction = Animator.StringToHash("direction");


    void FixedUpdate() {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        rb2D.MovePosition(rb2D.position + new Vector2(x, y) * moveSpeed * Time.deltaTime);
        rb2D.velocity = Vector2.zero;

        animator.SetBool(Moving, x != 0 || y != 0);

        int direction = animator.GetInteger(Direction);

        if (y > 0) {
            direction = 1;
        }
        else if (y < 0) {
            direction = 3;
        }

        if (x > 0) {
            direction = 0;
        }
        else if (x < 0) {
            direction = 2;
        }

        animator.SetInteger(Direction, direction);
    }

    private void OnApplicationQuit() {
        SaveGame();
    }

    PlayerSaveData CreateSaveData() {
        PlayerSaveData data = new PlayerSaveData();

        Vector2 pos = rb2D.position;
        data.posX = pos.x;
        data.posY = pos.y;
        data.direction = animator.GetInteger(Direction);

        return data;
    }

    private void LoadFromSaveData(PlayerSaveData data) {
        rb2D.position = new Vector2(data.posX, data.posY);
        animator.SetInteger(Direction, data.direction);
    }

    void SaveGame() {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath
                                      + "/playerSave.dat");
        bf.Serialize(file, CreateSaveData());
        file.Close();
        Debug.Log("Game data saved!");
    }

    void LoadGame() {
        String path = Application.persistentDataPath + "/playerSave.dat";
        
        if (!File.Exists(path)) {
            Debug.LogError("There is no save data!");
            return;
        }
        
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(path, FileMode.Open);
        PlayerSaveData data = (PlayerSaveData) bf.Deserialize(file);
        file.Close();
        LoadFromSaveData(data);
        Debug.Log("Game data loaded!");
    }
}

[Serializable]
class PlayerSaveData {
    public float posX;
    public float posY;
    public int direction;
}