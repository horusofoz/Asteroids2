﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour {
    
	public GameObject asteroidLarge;
	public GameObject asteroidMedium;
	public GameObject asteroidSmall;

    public static AsteroidSpawner instance = null;

    //Temporary static start positions
	Vector2 asteroidMediumStartPosition = new Vector2(20, 20);//static values for now
	Vector2 asteroidSmallStartPosition = new Vector2(40, 40);//static values for now

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

    public void SpawnMediumAsteroids(int num)
	{
		for (int i = 0; i < num; i++)
		{
			Instantiate(asteroidMedium, asteroidMediumStartPosition, Quaternion.identity);
		}
	}

    public void SpawnSmallAsteroids(int num)
	{
		for (int i = 0; i < num; i++)
		{
			Instantiate(asteroidSmall, asteroidSmallStartPosition, Quaternion.identity);
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