using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{

    public static SceneHandler instance = null;

    const int SCENE_MENU = 0;
    const int SCENE_INTRO = 1;
    const int SCENE_HELP = 2;
    const int SCENE_GAME = 3;
    const int SCENE_MISSION_COMPLETE_SCENE = 4;
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
        ProcessInput();
    }

    private void ProcessInput()
    {
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case SCENE_MENU:
                LoadSceneIntro();
                break;
            case SCENE_INTRO:
                ProcessInputIntro();
                break;
            case SCENE_HELP:
                LoadSceneIntro();
                break;
            case SCENE_GAME:
                //No action
                break;
            case SCENE_MISSION_COMPLETE_SCENE:
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

    private void LoadSceneIntro()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(SCENE_INTRO);
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

    public void LoadSceneGameOver()
    {
        SceneManager.LoadScene(SCENE_GAME_OVER);
    }

    public void LoadSceneGameWon()
    {
        SceneManager.LoadScene(SCENE_GAME_WON);
    }

    public void LoadSceneMissionComplete()
    {
        SceneManager.LoadScene(SCENE_MISSION_COMPLETE_SCENE);
    }

}
