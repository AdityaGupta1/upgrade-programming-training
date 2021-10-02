using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour {
    private Renderer rendererComponent;

    // Start is called before the first frame update
    private void Start() {
        rendererComponent = gameObject.GetComponent<Renderer>();
        rendererComponent.enabled = !SaveData.instance().tookTreasure;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            rendererComponent.enabled = false;
            SaveData.instance().tookTreasure = true;
        }
    }
}