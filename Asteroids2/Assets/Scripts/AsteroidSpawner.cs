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

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SpawnLargeAsteroids(int num)
	{
		for (int i = 0; i < num; i++)
		{
			Instantiate(asteroidLarge, parent: transform);
		}
	}
}


/*
 * SpawnAsteroidMedium(transform DestroyedLargeAsteroidTransform)
 * {
 *      CREATE 2 Medium asteroids in DestroyedLargeAsteroidTransform location
 *      Direction opposite
 *      NESTED under asteroid Spawner NOT large asteroid
 * }
 * 
 * 
 * 
 */