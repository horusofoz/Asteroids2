using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour {
	
	void Update () {
        ProcessInput();
	}

    private void ProcessInput()
    {
        // If Level 1
        if (SceneManager.GetActiveScene().name == "Menu Scene")
        {
            StartGame();
        }
    }

    private void StartGame()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            SceneManager.LoadScene(1);
        }
    }
}
