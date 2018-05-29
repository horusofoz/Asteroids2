using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    // Required for singleton pattern
    public static GameController instance = null;

    // Level Management
    const int SCENE_MENU = 0;
    const int SCENE_INTRO = 1;
    const int SCENE_HELP = 2;
    const int SCENE_GAME = 3;
    const int SCENE_MISSION_COMPLETE_SCENE = 4;
    const int SCENE_GAME_OVER = 5;
    const int SCENE_GAME_WON = 6;
    private bool levelLoaded = false;
    private bool levelReset = false;
    private float sceneLoadDelay = 2.0f;

    // Wave Management
    private int currentLevel = 1;
    private int currentWave = 1;
    private int levelWaves = 0;
    private float waveTimer = 10.0f;
    private float currentWaveTime = 0.0f;

    // Store reference to explosion
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
                while (levelLoaded == false)
                {
                    SetupCurrentLevel();
                    levelLoaded = true;
                    levelReset = false;
                }
                CheckWaveSpawnConditions();
                break;

            case SCENE_MISSION_COMPLETE_SCENE:
                levelLoaded = false;
                break ;
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

    private void SetupCurrentLevel()
    {
        switch (currentLevel)
        {
            case 1:
                levelWaves = 3;
                SpawnWave(currentWave);
                break;
            case 2:
                levelWaves = 3;
                SpawnWave(currentWave);
                break;
            case 3:
                levelWaves = 3;
                SpawnWave(currentWave);
                break;
            default:
                break;
        }

        print("Loading Level: " + currentLevel);
    }

    private void CheckWaveSpawnConditions()
    {
        // If spawn timer expired
        currentWaveTime += Time.deltaTime;
        if(currentWaveTime >= waveTimer)
        {
            SpawnWave(currentWave);
        }

        // If no more asteroids
        if (AsteroidSpawner.instance.transform.childCount == 0)
        {
            if(currentWave <= levelWaves)
            {
                SpawnWave(currentWave);
            }
            else
            {
                currentLevel++;
                SceneHandler.instance.LoadSceneMissionComplete();
            }
            
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
        ResetGame();
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

    public void SpawnWave(int waveNumber)
    {
        print("Spawning Wave: " + currentWave);
        switch (waveNumber)
        {
            
            case 1:
                AsteroidSpawner.instance.SpawnLargeAsteroids(1);
                currentWave++;
                break;
            case 2:
                AsteroidSpawner.instance.SpawnLargeAsteroids(3);
                currentWave++;
                break;
            case 3:
                AsteroidSpawner.instance.SpawnLargeAsteroids(5);
                currentWave++;
                break;
            default:
                break;
        }
        currentWaveTime = 0.0f;
        
    }

    public void ResetGame()
    {
        currentLevel = 1;
        currentWave = 1;
        levelWaves = 0;
        print("Resetting Game");
    }

}
