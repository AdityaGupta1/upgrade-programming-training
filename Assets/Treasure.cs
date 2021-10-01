using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    private Renderer r;
    // Start is called before the first frame update
    private void Start()
    {
        r = gameObject.GetComponent<Renderer>();
        r.enabled = !Data.tookTreasure;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            r.enabled = false;
            Data.tookTreasure = true;
        }
    }
}
