using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour {
    
	public GameObject asteroidLarge;
	public GameObject asteroidMedium;
	public GameObject asteroidSmall;
    public GameObject enemyShooter;
    public GameObject enemyDrone;
    Vector3 asteroidOffset = new Vector3(1, 1, 0);

    public static AsteroidSpawner instance = null;

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

    public void SpawnLargeAsteroids(int num)
	{      
		for (int i = 0; i < num; i++)
		{
			Instantiate(asteroidLarge, parent: transform);
		}
	}

    public void SpawnMediumAsteroids(int num, Transform asteroidLargeTransform)
	{
        GameObject mediumAsteroid = null;
        mediumAsteroid = Instantiate(asteroidMedium, (asteroidLargeTransform.transform.position + asteroidOffset), asteroidLargeTransform.transform.rotation);
        mediumAsteroid.transform.parent = gameObject.transform;
        mediumAsteroid.GetComponent<AsteroidMedium>().SetMediumAsteroidPhysics(asteroidLargeTransform.transform.up);

        GameObject mediumAsteroid2 = null;
        mediumAsteroid2 = Instantiate(asteroidMedium, (asteroidLargeTransform.transform.position - asteroidOffset), asteroidLargeTransform.transform.rotation);
        mediumAsteroid2.transform.parent = gameObject.transform;
        mediumAsteroid2.GetComponent<AsteroidMedium>().SetMediumAsteroidPhysics(-asteroidLargeTransform.transform.up);
    }

    public void SpawnSmallAsteroids(int num, Transform asteroidMediumTransform)
    {
        GameObject smallAsteroid = null;
        smallAsteroid = Instantiate(asteroidSmall, (asteroidMediumTransform.transform.position + asteroidOffset), asteroidMediumTransform.transform.rotation);
        smallAsteroid.transform.parent = gameObject.transform;
        smallAsteroid.GetComponent<AsteroidSmall>().SetSmallAsteroidPhysics(asteroidMediumTransform.transform.up);

        GameObject smallAsteroid2 = null;
        smallAsteroid2 = Instantiate(asteroidSmall, (asteroidMediumTransform.transform.position - asteroidOffset), asteroidMediumTransform.transform.rotation);
        smallAsteroid2.transform.parent = gameObject.transform;
        smallAsteroid2.GetComponent<AsteroidSmall>().SetSmallAsteroidPhysics(-asteroidMediumTransform.transform.up);
    }

    public void SpawnEnemyShooters(int num)
    {
        for (int i = 0; i < num; i++)
        {
            GameObject newEnemyShooter;
            newEnemyShooter = Instantiate(enemyShooter);
            newEnemyShooter.transform.parent = gameObject.transform;
        }
    }

    public void SpawnEnemyDrones(int num)
    {
        for (int i = 0; i < num; i++)
        {
            GameObject newEnemyDrone;
            newEnemyDrone = Instantiate(enemyDrone);
            newEnemyDrone.transform.parent = gameObject.transform;
        }
    }
}