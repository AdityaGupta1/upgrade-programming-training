using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelControl : MonoBehaviour
{
    public string levelName;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("got");
        if (other.CompareTag("Player"))
        {
            if (SceneManager.GetActiveScene().name == "very cool scene")
            {
                SceneManager.LoadScene("SampleScene");
            }
            else
            {
                SceneManager.LoadScene("very cool scene");
            }


        }
    }
}
