using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

	// Update is called once per frame
	void Update () {
        Debug.Log("Menu Update Loop Start");
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused)
            {
                Debug.Log("Menu Update Resume");
                Resume();
            }
            else
            {
                Debug.Log("Menu Update Pause");
                Pause();
            }
        }
        Debug.Log("Menu Update Loop End");
	}

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
}
