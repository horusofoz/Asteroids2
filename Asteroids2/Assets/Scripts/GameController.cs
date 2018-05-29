using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public static GameController instance = null;

    const int SCENE_MENU = 0;
    const int SCENE_INTRO = 1;
    const int SCENE_HELP = 2;
    const int SCENE_GAME = 3;
    const int SCENE_GAME_OVER = 4;
    const int SCENE_GAME_WON = 5;

    private int currentLevel = 1;

    private bool levelLoaded = false;
    private bool levelReset = false;

    private float sceneLoadDelay = 2.0f;

    public GameObject explosion;

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
                    ClearSpawners();
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
				//AsteroidSpawner.instance.SpawnMediumAsteroids(2);//temp - to be deleted once they spawn on large asteroid death
				//AsteroidSpawner.instance.SpawnSmallAsteroids(2);//temp - to be deleted once they spawn on large asteroid death
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

    public void ClearSpawners()
    {
        foreach (Transform child in AsteroidSpawner.instance.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void PlayerDied(GameObject player)
    {
        Instantiate(explosion, player.transform.position, player.transform.rotation);
        Destroy(player);
        Invoke("DelayedSceneLoad", sceneLoadDelay);
    }  

    public void AsteroidLargeHitByBullet(GameObject asteroidLarge)
    {
        Instantiate(explosion, asteroidLarge.transform.position, asteroidLarge.transform.rotation);
        AsteroidSpawner.instance.SpawnMediumAsteroids(2, asteroidLarge.transform);
        Destroy(asteroidLarge);
    }

    public void AsteroidMediumHitByBullet(GameObject asteroidMedium)
    {
        Instantiate(explosion, asteroidMedium.transform.position, asteroidMedium.transform.rotation);
        AsteroidSpawner.instance.SpawnSmallAsteroids(2, asteroidMedium.transform);
        Destroy(asteroidMedium);
    }

    public void AsteroidSmallHitByBullet(GameObject asteroidSmall)
    {
        Instantiate(explosion, asteroidSmall.transform.position, asteroidSmall.transform.rotation);
        Destroy(asteroidSmall);
    }

    private void DelayedSceneLoad()
    {
        SceneHandler.instance.LoadSceneGameOver(); // Should probably move this direct into scene handler and have an overload allowing delayed 
    }
}