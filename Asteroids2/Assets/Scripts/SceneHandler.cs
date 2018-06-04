using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{

    public static SceneHandler instance = null;

    const int SCENE_MENU = 0;
    const int SCENE_CREDITS = 1;
    const int SCENE_HELP = 2;
    const int SCENE_GAME = 3;
    const int SCENE_MISSION_COMPLETE = 4;
    const int SCENE_GAME_OVER = 5;
    const int SCENE_GAME_WON = 6;

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

    void Update()
    {
        //ProcessInput();
    }
    /*
    private void ProcessInput()
    {
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case SCENE_MENU:
                LoadSceneIntro();
                break;
            case SCENE_CREDITS:
                ProcessInputIntro();
                break;
            case SCENE_HELP:
                LoadSceneIntro();
                break;
            case SCENE_GAME:
                //No action
                break;
            case SCENE_MISSION_COMPLETE:
                ProcessInputMissionComplete();
                break;
            case SCENE_GAME_OVER:
            case SCENE_GAME_WON:
                ProcessInputGameOver();
                break;
            default:
                break;
        }
    }
    


    private void ProcessInputIntro()
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

    private void ProcessInputMissionComplete()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            SceneManager.LoadScene(SCENE_GAME);
        }
    }

    private void ProcessInputGameOver()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            SceneManager.LoadScene(SCENE_MENU);
        }
    }
    */

    // Scene Loaders
    public void LoadSceneMenu()
    {
        SceneManager.LoadScene(SCENE_MENU);
    }

    public void LoadSceneCredits()
    {
        SceneManager.LoadScene(SCENE_CREDITS);
    }

    public void LoadSceneHelp()
    {
        SceneManager.LoadScene(SCENE_HELP);
    }

    public void LoadSceneGame()
    {
        SceneManager.LoadScene(SCENE_GAME);
    }

    public void LoadSceneMissionComplete()
    {
        SceneManager.LoadScene(SCENE_MISSION_COMPLETE);
    }

    public void LoadSceneGameOver()
    {
        SceneManager.LoadScene(SCENE_GAME_OVER);
    }

    public void LoadSceneGameWon()
    {
        SceneManager.LoadScene(SCENE_GAME_WON);
    }

    

    

    
}
