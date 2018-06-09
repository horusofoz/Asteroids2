using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Wave
{
    public int AsteroidsToSpawn;
    public int EnemyShootersToSpawn;
    public int EnemyDronesToSpawn;

    public Wave(int numAsteroids, int numEnemyShooters, int numEnemyDrones)
    {
        AsteroidsToSpawn = numAsteroids;
        EnemyShootersToSpawn = numEnemyShooters;
        EnemyDronesToSpawn = numEnemyDrones;
    }
}


public class GameController : MonoBehaviour {

    // Required for singleton pattern
    public static GameController instance = null;

    // Level Management
    const int SCENE_MENU = 0;
    const int SCENE_CREDITS = 1;
    const int SCENE_HELP = 2;
    const int SCENE_GAME = 3;
    const int SCENE_MISSION_COMPLETE = 4;
    const int SCENE_GAME_OVER = 5;
    const int SCENE_GAME_WON = 6;
    private bool levelLoaded = false;
    private float sceneLoadDelay = 2.0f;
    private bool gameReset = false;

    // Wave Management
    private int currentLevel = 1;
    private int currentWave = 1;
    private int levelWaves = 0;
    private float waveTimer = 20.0f;
    private float currentWaveTime = 0.0f;
    List<List<Wave>> waveList = new List<List<Wave>>();

    // UI
    private Text nextWaveTimerUI;
    private Text scoreText;

    // Score Management
    private static int score;
    private int scoreLargeAsteroid = 250;
    private int scoreMediumAsteroid = 375;
    private int scoreSmallAsteroid = 500;
    private int scoreEnemyShooter = 1000;
    private int scoreEnemyDrone = 1000;
    private float levelTime = 0.0f;
    private int timeBonus = 0;
    private int levelScore = 0;
    private int levelBonusTime = 0;

    // Store reference to explosion
    public GameObject explosion;
    public AudioClip pickUpSound;

    // Pickup Management
    private int enemiesDestroyed = 0;

    public float fireRateBonus = 0f;
    public GameObject pickupFireRate;

    public int bulletLevel = 1;
    public GameObject pickupBullet;

    public GameObject pickupShield;
    public GameObject shield;
    //public bool shielded = false;
    public int shieldCount = 0;


    public List<int> pickupCountList = new List<int>();
    public List<GameObject> pickupTypeList = new List<GameObject>();

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

    private void Start()
    {
        CreateWaveLists();
        ResetPickupLists();
    }

    private void CreateWaveLists()
    {
        // Level 1
        waveList.Add(new List<Wave> {
            new Wave(0, 2, 0),
            new Wave(0, 2, 0),
            new Wave(0, 3, 0)
        });

        // Level 2
        waveList.Add(new List<Wave> {
            new Wave(1, 2, 1),
            new Wave(1, 2, 2),
            new Wave(2, 2, 2)
        });

        // Level 3
        waveList.Add(new List<Wave> {
            new Wave(2, 2, 0),
            new Wave(2, 3, 0),
            new Wave(3, 4, 0)
        });

    }

    private void ResetPickupLists()
    {
        pickupCountList = new List<int>() { 2, 3, 3 };
        pickupTypeList.Clear();
        pickupTypeList.Add(pickupBullet);
        pickupTypeList.Add(pickupFireRate);
        pickupTypeList.Add(pickupShield);
    }

    private void Update()
    {
        ManageGameState();
    }

    private void ManageGameState()
    {
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case SCENE_MENU:
                gameReset = false;
                break;
            case SCENE_GAME:
                while (levelLoaded == false)
                {
                    SetupCurrentLevel();
                    levelLoaded = true;
                }
                UpdateWaveTimer();
                CheckWaveSpawnConditions();
                levelTime += Time.deltaTime;
                break;

            case SCENE_MISSION_COMPLETE:
                levelLoaded = false;
                SetLevelCompleteScoreUI();
                break ;
            case SCENE_GAME_OVER:
            case SCENE_GAME_WON:
                if(gameReset == false)
                {
                    SetFinalScoreUI();
                    ResetGame();
                }
                
                break;
            default:
                break;
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
        fireRateBonus = 0;
        bulletLevel = 1;
        enemiesDestroyed = 0;
        Invoke("DelayedSceneLoad", sceneLoadDelay);
    }

    public void AsteroidLargeHitByBullet(GameObject asteroidLarge)
    {
        Instantiate(explosion, asteroidLarge.transform.position, asteroidLarge.transform.rotation);
		AsteroidSpawner.instance.SpawnMediumAsteroids(2, asteroidLarge.transform);
        CheckPickUpSpawnConditions(asteroidLarge);
        Destroy(asteroidLarge);
        AddScore(scoreLargeAsteroid);
    }

    public void AsteroidMediumHitByBullet(GameObject asteroidMedium)
    {
        Instantiate(explosion, asteroidMedium.transform.position, asteroidMedium.transform.rotation);
        AsteroidSpawner.instance.SpawnSmallAsteroids(2, asteroidMedium.transform);
        CheckPickUpSpawnConditions(asteroidMedium);
        Destroy(asteroidMedium);
        AddScore(scoreMediumAsteroid);
    }

    public void AsteroidSmallHitByBullet(GameObject asteroidSmall)
    {
        Instantiate(explosion, asteroidSmall.transform.position, asteroidSmall.transform.rotation);
        CheckPickUpSpawnConditions(asteroidSmall);
        Destroy(asteroidSmall);
        AddScore(scoreSmallAsteroid);
    }

    public void EnemyShooterHitByBullet(GameObject enemyShooter)
    {
        Instantiate(explosion, enemyShooter.transform.position, enemyShooter.transform.rotation);
        CheckPickUpSpawnConditions(enemyShooter);
        Destroy(enemyShooter);
        AddScore(scoreEnemyShooter);
    }

    public void EnemyDroneHitByBullet(GameObject enemyDrone)
    {
        Instantiate(explosion, enemyDrone.transform.position, enemyDrone.transform.rotation);
        CheckPickUpSpawnConditions(enemyDrone);
        Destroy(enemyDrone);
        AddScore(scoreEnemyDrone);
    }

    private void DelayedSceneLoad()
    {
        SceneHandler.instance.LoadSceneGameOver(); // Should probably move this direct into scene handler and have an overload allowing delayed 
    }

    public void SpawnWave()
    {
        AsteroidSpawner.instance.SpawnLargeAsteroids(waveList[currentLevel - 1][currentWave - 1].AsteroidsToSpawn);
        AsteroidSpawner.instance.SpawnEnemyShooters(waveList[currentLevel - 1][currentWave - 1].EnemyShootersToSpawn);
        AsteroidSpawner.instance.SpawnEnemyDrones(waveList[currentLevel - 1][currentWave - 1].EnemyDronesToSpawn);
        currentWave++;
        currentWaveTime = 0.0f;
    }

    private void SetupCurrentLevel()
    {
        levelTime = 0.0f;
        currentWave = 1;
        levelWaves = waveList[currentLevel - 1].Count;
        scoreText = GameObject.Find("ScoreUI").GetComponent<Text>();
        nextWaveTimerUI = GameObject.Find("NextWave").GetComponent<Text>();
        ApplyShield();
        UpdateScoreUI();
        SpawnWave();
    }

    private void CheckWaveSpawnConditions()
    {
        // If not the last wave
        if (currentWave <= levelWaves)
        {
            // If spawn timer expired
            currentWaveTime += Time.deltaTime;
            if (currentWaveTime >= waveTimer)
            {
                SpawnWave();
            }
        }

        // If no more asteroids
        if (AsteroidSpawner.instance.transform.childCount == 0)
        {
            if (currentWave <= levelWaves)
            {
                SpawnWave();
            }
            else 
            {
                currentLevel++;

                if(currentLevel > waveList.Count)
                {
                    SceneHandler.instance.LoadSceneGameWon();
                }
                else
                {
                    SetLevelCompleteScore();
                    SceneHandler.instance.LoadSceneMissionComplete();
                }                
            }

        }
    }

    public void ResetGame()
    {
        print("Resetting Game");
        ClearSpawners();
        currentLevel = 1;
        currentWave = 1;
        levelWaves = 0;
        levelLoaded = false;
        gameReset = true;
        score = 0;
        ResetPickupLists();
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        UpdateScoreUI();
    }

    public void UpdateScoreUI()
    {
        scoreText.text = score.ToString();
    }

    public void UpdateWaveTimer()
    {
        if (currentWave - 1 < levelWaves)
        {
            nextWaveTimerUI.text = ("Next Wave   " + (-(int)currentWaveTime + 20).ToString("00"));
        }
        else
        {
            nextWaveTimerUI.text = ("Last Wave");
        }
        
    }

    public void SetFinalScoreUI()
    {
        scoreText = GameObject.Find("Final Score").GetComponent<Text>();
        scoreText.text = "Final Score\n" + score.ToString();
    }

    public void SetLevelCompleteScoreUI()
    {
        scoreText = GameObject.Find("Level Score").GetComponent<Text>();
        scoreText.text = "Score\n" + levelScore + "\nTime Bonus\n" + timeBonus + "\nTotal\n" + score;
    }

    public void SetLevelCompleteScore()
    {
        levelScore = score;
        timeBonus = 0;

        levelBonusTime = (int)(waveList[currentLevel - 1].Count * waveTimer);
        if ((int)levelTime < levelBonusTime)
        {
            timeBonus = (levelBonusTime - (int)levelTime) * 1000;
        }

        score += timeBonus;
    }

    public void CheckPickUpSpawnConditions(GameObject enemy)
    {
        enemiesDestroyed++;
        if (enemiesDestroyed % 2 == 0) //If number of enemies destroyed is divisble by 5 without any remainder
        {
            ChooseRandomPickup(enemy);
            
        }
    }

    public void PickUpCollected(GameObject pickup)
    {
        SoundManager.instance.PlaySingle(pickUpSound);
        if (pickup.name.Contains("PickUpBullet"))
        {
            print("Picked up a bullet powerup!");
            IncreaseBulletLevel();
        }
        if (pickup.name.Contains("PickUpFireRate"))
        {
            print("Picked up a firerate powerup!");
            IncreaseFireRateBonus();
        }
        if (pickup.name.Contains("PickUpLife"))
        {
            print("Picked up a life powerup!");
        }
        if (pickup.name.Contains("PickUpShield"))
        {
            print("Picked up a shield powerup!");
            if(shieldCount < 3)
            {
                shieldCount++;
            }
            ApplyShield();
        }
        Destroy(pickup);
    }

    private void IncreaseBulletLevel()
    {
        if (bulletLevel < 3)
        {
            bulletLevel += 1;
        }
    }

    public void IncreaseFireRateBonus()
    {
        if(fireRateBonus < 3)
        {
            fireRateBonus += 1.0f;
        }
    }

    private void ChooseRandomPickup(GameObject enemy)
    {
        if (pickupCountList.Count < 1)
        {
            print("No pickups left");
        }
        else
        {
            int randomPickupNum = UnityEngine.Random.Range(0, pickupCountList.Count - 1);

            Instantiate(pickupTypeList[randomPickupNum], enemy.transform.position, Quaternion.identity);
            pickupCountList[randomPickupNum] = pickupCountList[randomPickupNum] - 1;
            if (pickupCountList[randomPickupNum] == 0)
            {
                pickupCountList.RemoveAt(randomPickupNum);
                pickupTypeList.RemoveAt(randomPickupNum);
            }
        }
        
    }

    private void ApplyShield()
    {
        if(shieldCount > 0)
        {
            GameObject player = GameObject.Find("Starship");
            GameObject shieldApplied = Instantiate(shield, player.transform.position, player.transform.rotation);
            shieldApplied.transform.parent = player.transform;
        }
    }

    public void RemoveShield()
    {
        shieldCount--;
        if (shieldCount <= 0)
        {
            shield = GameObject.Find("Shield (Clone)");
            Destroy(shield);
        }
    }

    public void PlayerHit(GameObject player)
    {
        if(shieldCount > 0)
        {
            RemoveShield();



        }
        else
        {
            PlayerDied(player);
        }
    }
}
