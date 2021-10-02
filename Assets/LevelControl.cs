using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelControl : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            if (SceneManager.GetActiveScene().name == "level 1") {
                SaveData.instance().playerData.posX = -7;
                SceneManager.LoadScene("level 2");
            } else {
                SaveData.instance().playerData.posX = 7;
                SceneManager.LoadScene("level 1");
            }
        }
    }
}