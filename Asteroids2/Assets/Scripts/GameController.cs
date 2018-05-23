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

    private int currentLevel = 1;

    private bool levelLoaded = false;
    private bool levelReset = false;

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

    private void ManageGameState()
    {
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case SCENE_GAME:
                while(levelLoaded == false)
                {
                    SetupGameScene();
                    levelLoaded = true;
                    levelReset = false;
                }
                CheckAsteroidsRemaining();
                break;
            case SCENE_GAME_OVER:
            case SCENE_GAME_WON:
                while (levelReset == false)
                {
                    levelLoaded = false;
                    levelReset = true;
                }
                break;
            default:
                break;
        }
    }

    private void SetupGameScene()
    {
        SetupCurrentLevel();
        print("Level Loaded");
        levelLoaded = true;
    }

    private void SetupCurrentLevel()
    {
        switch (currentLevel)
        {
            case 1:
                AsteroidSpawner.instance.SpawnLargeAsteroids(3);
				AsteroidSpawner.instance.SpawnMediumAsteroids(2);//temp - to be deleted once they spawn on large asteroid death
				AsteroidSpawner.instance.SpawnSmallAsteroids(2);//temp - to be deleted once they spawn on large asteroid death
                break;
            case 2:
                // TODO
                break;
            case 3:
                // TODO
                break;
            default:
                break;
        }
    }

    private void CheckAsteroidsRemaining()
    {
        // If no more asteroids
        if (AsteroidSpawner.instance.transform.childCount == 0)
        {
            SceneHandler.instance.LoadSceneGameWon();
        }
    }
}