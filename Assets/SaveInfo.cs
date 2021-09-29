using UnityEngine;
using UnityEngine.UI;

public class SaveInfo : MonoBehaviour {
    [SerializeField] private int saveNumber;

    private void Start() {
        transform.Find("panel_saveInfo/text_saveNumber").gameObject.GetComponent<Text>().text = "Save " + saveNumber;
    }

    private void Update() {
        
    }
}
