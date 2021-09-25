using System;
using UnityEngine;

public class Menu : MonoBehaviour {
    private Canvas canvas;

    private void Start() {
        canvas = GetComponent<Canvas>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            canvas.enabled = !canvas.enabled;
        }
    }
}
