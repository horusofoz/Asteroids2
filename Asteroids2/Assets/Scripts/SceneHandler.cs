using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour {

    public static SceneHandler instance = null;

    const int SCENE_MENU = 0;
    const int SCENE_MISSION_START = 1;
    const int SCENE_HELP = 2;
    const int SCENE_GAME = 3;
    const int SCENE_MISSION_FAILED = 4;
    const int SCENE_GAME_WON = 5;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    void Update () {
        ProcessInput();
	}

    private void ProcessInput()
    {
        print(SceneManager.GetActiveScene().buildIndex);

        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case SCENE_MENU:
                LoadMissionScene();
                break;
            case SCENE_MISSION_START:
                ProcessMissionSceneInput();
                break;
            case SCENE_HELP:
                ProcessMissionSceneInput();
                break;
            case SCENE_GAME:
                LoadMissionScene();
                break;
            default:
                break;
        }
    }

    private void LoadMissionScene()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            SceneManager.LoadScene(SCENE_MISSION_START);
        }
    }

    private void ProcessMissionSceneInput()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            SceneManager.LoadScene(SCENE_HELP);
        }
        else if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            SceneManager.LoadScene(SCENE_GAME);
        }
    }

    private void PlayerDies()
    {

    }
}
