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
    const int SCENE_PLAYER_DIED = 7;
    const int SCENE_MISSION_BRIEF = 8;

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

    public void LoadScenePlayerDied()
    {
        SceneManager.LoadScene(SCENE_PLAYER_DIED);
    }

    public void LoadSceneMissionBrief()
    {
        SceneManager.LoadScene(SCENE_MISSION_BRIEF);
    }

    
}
