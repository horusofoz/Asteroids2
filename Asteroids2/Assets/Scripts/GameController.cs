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

public class PickUp<Key, Val> // Custom class based on key/value pair to make value mutable. https://stackoverflow.com/questions/13454721/how-to-modify-a-keyvaluepair-value
{
    public Key Name { get; set; }
    public Val Remaining { get; set; }

    public PickUp() { }

    public PickUp(Key key, Val val)
    {
        this.Name = key;
        this.Remaining = val;
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
    const int SCENE_PLAYER_DIED = 7;
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
    private Text playerDiedLifeCounterUI;
    private Text BulletCounterUI;
    private Text FireRateCounterUI;
    private Text ShieldCounterUI;
    private Text LifeCounterUI;
    private Image BulletCounterIconUI;
    private Image FireRateCounterIconUI;
    private Image ShieldCounterIconUI;
    private Image LifeCounterIconUI;
    public Sprite BulletCounterIconUIYellow;
    public Sprite FireRateCounterIconUIYellow;
    public Sprite ShieldCounterIconUIYellow;
    public Sprite LifeCounterIconUIYellow;
    public Sprite BulletCounterIconUIRed;
    public Sprite FireRateCounterIconUIRed;
    public Sprite ShieldCounterIconUIRed;
    public Sprite LifeCounterIconUIRed;
    public Sprite BulletCounterIconUIBlue;
    public Sprite FireRateCounterIconUIBlue;
    public Sprite ShieldCounterIconUIBlue;
    public Sprite LifeCounterIconUIBlue;
    private Color BlueUI = new Color(0.2117647f, 0.7333333f, 0.9607844f);
    private Color RedUI = new Color(0.6745098f, 0.2235294f, 0.2235294f);
    private Color YellowUI = new Color(0.9686275f, 0.9058824f, 0.3372549f);
    

    // Score Management
    private static int score;
    private int scoreLargeAsteroid = 250;
    private int scoreMediumAsteroid = 375;
    private int scoreSmallAsteroid = 500;
    private int scoreEnemyShooter = 1000;
    private int scoreEnemyDrone = 1000;
    private int scorePickUp = 1500;
    private float levelTime = 0.0f;
    private int timeBonus = 0;
    private int levelScore = 0;
    private int levelBonusTime = 0;

    // Store reference to explosion
    public GameObject explosionSmall;
    public GameObject explosionLarge;
    public GameObject explosionMedium;
    public AudioClip pickUpSound;

    // Pick Up Objects
    public GameObject pickupFireRate;
    public GameObject pickupBullet;
    public GameObject pickupShield;
    public GameObject pickupLife;
    public GameObject pickupScore;
    public List<PickUp<GameObject, int>> pickupList = new List<PickUp<GameObject, int>>();

    // Pick Up Counters
    private int enemiesDestroyed = 0;
    public float fireRateBonus = 0f;
    public int bulletLevel = 0;
    private Renderer shield;
    public int shieldCount = 0;
    public AudioClip shieldHitSound;
    public AudioClip shieldDestroyed;
    private int lives = 0;

    private int pickUpSpawnRate = 3;

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
            new Wave(1, 0, 0),
            new Wave(1, 0, 0),
            new Wave(2, 0, 0)
        });

        // Level 2
        waveList.Add(new List<Wave> {
            new Wave(1, 0, 1),
            new Wave(0, 0, 2),
            new Wave(2, 0, 1),
            new Wave(2, 0, 2)
        });

        // Level 3
        waveList.Add(new List<Wave> {
            new Wave(1, 1, 0),
            new Wave(1, 1, 1),
            new Wave(0, 2, 0),
            new Wave(0, 2, 2)
        });

        // Level 4
        waveList.Add(new List<Wave> {
            new Wave(1, 1, 1),
            new Wave(2, 2, 2),
            new Wave(1, 2, 3),
            new Wave(3, 3, 3)
        });

    }

    private void ResetPickupLists()
    {
        pickupList.Clear();
        pickupList.Add(new PickUp<GameObject, int>(pickupBullet, 3));
        pickupList.Add(new PickUp<GameObject, int>(pickupFireRate, 3));
        pickupList.Add(new PickUp<GameObject, int>(pickupShield, 3));
        pickupList.Add(new PickUp<GameObject, int>(pickupLife, 3));
        pickupList.Add(new PickUp<GameObject, int>(pickupScore, 500));
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
                break;
            case SCENE_GAME_OVER:
            case SCENE_GAME_WON:
                if (gameReset == false)
                {
                    SetFinalScoreUI();
                    ResetGame();
                }
                break;
            case SCENE_PLAYER_DIED:
                levelLoaded = false;
                ClearSpawners();
                UpdatePlayerDiedLifeCounter();
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
        Instantiate(explosionSmall, player.transform.position, player.transform.rotation);
        Destroy(player);
        fireRateBonus = 0;
        bulletLevel = 0;
        enemiesDestroyed = 0;
        if(lives < 1)
        {
            Invoke("DelayedGameOverSceneLoad", sceneLoadDelay);
        }
        else
        {
            DecreaseLifeCount();
            Invoke("DelayedPlayerDiedSceneLoad", sceneLoadDelay);
            ResetPickupLists();
            if(lives > 0)
            {
                var lifePickUp = pickupList.Find(x => x.Name.name.ToString() == "PickUpLife");
                lifePickUp.Remaining = lifePickUp.Remaining - lives;
            }
        }
    }

    public void AsteroidLargeHitByBullet(GameObject asteroidLarge)
    {
        Instantiate(explosionLarge, asteroidLarge.transform.position, asteroidLarge.transform.rotation);
        AsteroidSpawner.instance.SpawnMediumAsteroids(2, asteroidLarge.transform);
        CheckPickUpSpawnConditions(asteroidLarge);
        Destroy(asteroidLarge);
        AddScore(scoreLargeAsteroid);
    }

    public void AsteroidMediumHitByBullet(GameObject asteroidMedium)
    {
        Instantiate(explosionMedium, asteroidMedium.transform.position, asteroidMedium.transform.rotation);
        AsteroidSpawner.instance.SpawnSmallAsteroids(2, asteroidMedium.transform);
        CheckPickUpSpawnConditions(asteroidMedium);
        Destroy(asteroidMedium);
        AddScore(scoreMediumAsteroid);
    }

    public void AsteroidSmallHitByBullet(GameObject asteroidSmall)
    {
        Instantiate(explosionSmall, asteroidSmall.transform.position, asteroidSmall.transform.rotation);
        CheckPickUpSpawnConditions(asteroidSmall);
        Destroy(asteroidSmall);
        AddScore(scoreSmallAsteroid);
    }

    public void EnemyShooterHitByBullet(GameObject enemyShooter)
    {
        Instantiate(explosionSmall, enemyShooter.transform.position, enemyShooter.transform.rotation);
        CheckPickUpSpawnConditions(enemyShooter);
        Destroy(enemyShooter);
        AddScore(scoreEnemyShooter);
    }

    public void EnemyDroneHitByBullet(GameObject enemyDrone)
    {
        Instantiate(explosionSmall, enemyDrone.transform.position, enemyDrone.transform.rotation);
        CheckPickUpSpawnConditions(enemyDrone);
        Destroy(enemyDrone);
        AddScore(scoreEnemyDrone);
    }

    private void DelayedGameOverSceneLoad()
    {
        SceneHandler.instance.LoadSceneGameOver(); // Should probably move this direct into scene handler and have an overload allowing delayed 
    }

    private void DelayedPlayerDiedSceneLoad()
    {
        SceneHandler.instance.LoadScenePlayerDied(); // Should probably move this direct into scene handler and have an overload allowing delayed 
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
        BulletCounterUI = GameObject.Find("PickUpCounterTextBullet").GetComponent<Text>();
        FireRateCounterUI = GameObject.Find("PickUpCounterTextFireRate").GetComponent<Text>();
        ShieldCounterUI = GameObject.Find("PickUpCounterTextShield").GetComponent<Text>();
        LifeCounterUI = GameObject.Find("PickUpCounterTextLife").GetComponent<Text>();
        BulletCounterIconUI = GameObject.Find("PickUpCounterIconBullet").GetComponent<Image>();
        FireRateCounterIconUI = GameObject.Find("PickUpCounterIconFireRate").GetComponent<Image>();
        ShieldCounterIconUI = GameObject.Find("PickUpCounterIconShield").GetComponent<Image>();
        LifeCounterIconUI = GameObject.Find("PickUpCounterIconLife").GetComponent<Image>();

        shield = GameObject.Find("Shield").GetComponent<Renderer>();

        UpdatePickUpCountersUI();
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

                if (currentLevel > waveList.Count)
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
        lives = 0;
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
        if (enemiesDestroyed % pickUpSpawnRate == 0) //If number of enemies destroyed is divisble by 5 without any remainder
        {
            ChooseRandomPickup(enemy);
        }
    }

    public void PickUpCollected(GameObject pickup)
    {
        SoundManager.instance.PlaySingle(pickUpSound);
        if (pickup.name.Contains("PickUpBullet"))
        {
            IncreaseBulletLevel();
            UpdatePickUpList(pickup);
        }
        if (pickup.name.Contains("PickUpFireRate"))
        {
            IncreaseFireRateBonus();
            UpdatePickUpList(pickup);
        }
        if (pickup.name.Contains("PickUpShield"))
        {
            if (shieldCount < 3)
            {
                shieldCount++;
                UpdatePickUpList(pickup);
            }
            ApplyShield();
        }
        if (pickup.name.Contains("PickUpLife"))
        {
            IncreaseLifeCount();
            UpdatePickUpList(pickup);
        }
        UpdatePickUpCountersUI();
        AddScore(scorePickUp);
        Destroy(pickup);
    }

    private void IncreaseBulletLevel()
    {
        if (bulletLevel < 3)
        {
            bulletLevel += 1;
            UpdatePickUpCounterUI("bullet");
        }
    }

    public void IncreaseFireRateBonus()
    {
        if (fireRateBonus < 3)
        {
            fireRateBonus += 1.0f;
            UpdatePickUpCounterUI("fireRate");
        }
    }

    private void ChooseRandomPickup(GameObject enemy)
    {

        if (pickupList.Count < 1)
        {
            print("Spawning Score Pickup");
            Instantiate(pickupScore, enemy.transform.position, Quaternion.identity);
        }
        else
        {
            int randomPickupNum = UnityEngine.Random.Range(0, pickupList.Count);
            Instantiate(pickupList[randomPickupNum].Name, enemy.transform.position, Quaternion.identity);
        }
    }

    private void ApplyShield()
    {
        if (shieldCount > 0)
        {
            shield.enabled = true;
        }
    }

    public void RemoveShield()
    {
        shieldCount--;

        if (shieldCount <= 0)
        {
            shield.enabled = false; // Remove shield renderer
            SoundManager.instance.PlayVoice(shieldDestroyed);
        }

        var shieldPickUp = pickupList.Find(x => x.Name.name.ToString() == "PickUpShield"); // Get shield pickup from list

        if(shieldPickUp != null) // If shield pickup exists in the pickup list
        {
            if (shieldPickUp.Remaining < 3)
            {
                shieldPickUp.Remaining++;
                print("Shield pickup still in pickup list. Incrementing reamining to " + shieldPickUp.Remaining);
            }
        }
        else // re-add to the pickupList with all shields available
        {
            print("Shield pickup not in pickup list. Readding.");
            pickupList.Add(new PickUp<GameObject, int>(pickupShield, 1));
        }
        UpdatePickUpCountersUI();
    }

    public void PlayerHit(GameObject player)
    {
        if (shieldCount > 0)
        {
            print("Shield Hit");
            SoundManager.instance.PlaySingle(shieldHitSound);
            RemoveShield();
        }
        else
        {
            PlayerDied(player);
        }
    }

    public void UpdatePickUpList(GameObject pickUp)
    {
        print(pickUp.name + " collected.");

        var pickUpCollected = pickupList.Find(x => x.Name.ToString() == pickUp.ToString().Replace("(Clone)", ""));

        if(pickUpCollected != null)
        {
            if (pickUpCollected.Remaining > 0)
            {
                pickUpCollected.Remaining--;

                if (pickUpCollected.Remaining < 1)
                {
                    print("Removing " + pickUpCollected.Name.name);
                    pickupList.Remove(pickUpCollected);
                }
            }
        }

        foreach (var item in pickupList)
        {
            print(item.Name.name + " " + item.Remaining + " Remaining");
        }
    }

    public void UpdatePickUpCountersUI()
    {
        UpdatePickUpCounterUI("bullet");
        UpdatePickUpCounterUI("fireRate");
        UpdatePickUpCounterUI("life");
        UpdatePickUpCounterUI("shield");
    }

    public void UpdatePickUpCounterUI(string pickUp)
    {
        switch (pickUp)
        {
            case "bullet":
                BulletCounterUI.text = bulletLevel.ToString();
                UpdatePickUpCounterTextColour(BulletCounterUI, bulletLevel);
                UpdateBulletCounterIcon(BulletCounterIconUI, bulletLevel);
                break;

            case "fireRate":
                FireRateCounterUI.text = fireRateBonus.ToString();
                UpdatePickUpCounterTextColour(FireRateCounterUI, (int)fireRateBonus);
                UpdateFireRateCounterIcon(FireRateCounterIconUI, (int)fireRateBonus);
                break;

            case "life":
                LifeCounterUI.text = lives.ToString();
                UpdatePickUpCounterTextColour(LifeCounterUI, lives);
                UpdateLifeCounterIcon(LifeCounterIconUI, lives);
                break;

            case "shield":
                ShieldCounterUI.text = shieldCount.ToString();
                UpdatePickUpCounterTextColour(ShieldCounterUI, shieldCount);
                UpdateShieldCounterIcon(ShieldCounterIconUI, shieldCount);
                break;

            default:
                break;
        }
    }

    public void UpdatePickUpCounterTextColour(Text pickUpText, int counter)
    {
        switch (counter)
        {
            case 0:
            case 1:
                pickUpText.color = YellowUI;
                break;
            case 2:
                pickUpText.color = RedUI;
                break;
            case 3:
                pickUpText.color = BlueUI;
                break;
            default:
                break;
        }
    }

    public void UpdateBulletCounterIcon(Image pickUp, int counter)
    {
        switch (counter)
        {
            case 0:
            case 1:
                pickUp.sprite = BulletCounterIconUIYellow;
                break;
            case 2:
                pickUp.sprite = BulletCounterIconUIRed;
                break;
            case 3:
                pickUp.sprite = BulletCounterIconUIBlue;
                break;
            default:
                break;
        }
    }

    public void UpdateFireRateCounterIcon(Image pickUp, int counter)
    {
        switch (counter)
        {
            case 0:
            case 1:
                pickUp.sprite = FireRateCounterIconUIYellow;
                break;
            case 2:
                pickUp.sprite = FireRateCounterIconUIRed;
                break;
            case 3:
                pickUp.sprite = FireRateCounterIconUIBlue;
                break;
            default:
                break;
        }
    }

    public void UpdateShieldCounterIcon(Image pickUp, int counter)
    {
        switch (counter)
        {
            case 0:
            case 1:
                pickUp.sprite = ShieldCounterIconUIYellow;
                break;
            case 2:
                pickUp.sprite = ShieldCounterIconUIRed;
                break;
            case 3:
                pickUp.sprite = ShieldCounterIconUIBlue;
                break;
            default:
                break;
        }
    }

    public void UpdateLifeCounterIcon(Image pickUp, int counter)
    {
        switch (counter)
        {
            case 0:
            case 1:
                pickUp.sprite = LifeCounterIconUIYellow;
                break;
            case 2:
                pickUp.sprite = LifeCounterIconUIRed;
                break;
            case 3:
                pickUp.sprite = LifeCounterIconUIBlue;
                break;
            default:
                break;
        }
    }


    public void IncreaseLifeCount()
    {
        if(lives < 3)
        {
            lives++;
            UpdatePickUpCounterUI("life");
        }
    }

    public void DecreaseLifeCount()
    {
        lives--;
        UpdatePickUpCounterUI("life");
        var lifePickUp = pickupList.Find(x => x.Name.name.ToString() == "PickUpLife");
        if(lifePickUp != null) // If life pickup exists in the pickup list
        {
            if (lifePickUp.Remaining < 3)
            {
                lifePickUp.Remaining++;
                print("Life pickup still in pickup list. Incrementing reamining to " + lifePickUp.Remaining);
            }
            else // re-add to the pickupList with all shields available
            {
                print("Life pickup not in pickup list. Re-adding.");
                pickupList.Add(new PickUp<GameObject, int>(pickupLife, 1));
            }
        }
    }

    public void UpdatePlayerDiedLifeCounter()
    {
        playerDiedLifeCounterUI = GameObject.Find("PlayerDiedLifeCounter").GetComponent<Text>();
        playerDiedLifeCounterUI.text = lives.ToString();
    }
}



// Todo
// Make player flash while invulnerable
// Mini explosions for player vs enemy bullet
// Refactor icon updating code to remove duplication code smell
