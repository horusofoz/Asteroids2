using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour {
    
	public GameObject asteroidLarge;
	public GameObject asteroidMedium;
	public GameObject asteroidSmall;

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
		for (int i = 0; i < num; i++)
		{
            GameObject mediumAsteroid = null;
			mediumAsteroid = Instantiate(asteroidMedium, asteroidLargeTransform.transform.position, asteroidLargeTransform.transform.rotation);
            mediumAsteroid.transform.parent = gameObject.transform;
		}
	}

    public void SpawnSmallAsteroids(int num, Transform asteroidMediumTransform)
    {
		for (int i = 0; i < num; i++)
		{
            GameObject smallAsteroid = null;
            smallAsteroid = Instantiate(asteroidSmall, asteroidMediumTransform.transform.position, asteroidMediumTransform.transform.rotation);
            smallAsteroid.transform.parent = gameObject.transform;
        }
	}
}