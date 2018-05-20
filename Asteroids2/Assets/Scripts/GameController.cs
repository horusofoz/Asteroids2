using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    const int SCENE_MENU = 0;
    const int SCENE_INTRO = 1;
    const int SCENE_HELP = 2;
    const int SCENE_GAME = 3;
    const int SCENE_GAME_OVER = 4;
    const int SCENE_GAME_WON = 5;

    public static GameController instance = null;

    GameObject asteroidSpawner;

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

    private void Update()
    {
        ManageGameState();
    }

    private void SetupGameScene()
    {
        if (asteroidSpawner == null)
        {
            asteroidSpawner = GameObject.Find("AsteroidSpawner");
        }
    }


    private void CheckAsteroidsRemaining()
    {
        // If no more asteroids
        if (asteroidSpawner.transform.childCount == 0)
        {
            //print("Loading Game Won");
            SceneHandler.instance.LoadSceneGameWon();
        }
    }


    private void ManageGameState()
    {
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case SCENE_GAME:
                SetupGameScene();
                CheckAsteroidsRemaining();
                break;
            default:
                break;
        }
    }

}