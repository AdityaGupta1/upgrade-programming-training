using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Menu : MonoBehaviour
{
    private Canvas canvas;
    
    private void Start() {
        canvas = GetComponent<Canvas>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            canvas.enabled = !canvas.enabled;
            if (Time.timeScale == 0)
            {
                Debug.Log("changed frm 0 to 1");
                Time.timeScale = 1;
            }
            else
            {
                Debug.Log("changed from 1 to 0");
                Time.timeScale = 0;
            }
        }
    }
}
