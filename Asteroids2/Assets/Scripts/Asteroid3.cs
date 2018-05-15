using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Random to pick which edge of the screen to spawn at 
Create new empty vector2
If top or bottom, set Y value to 0 or max
If left or right, set X value to 0 or max
Randomise orther X or Y value depending on screen edge selected
Spawn object position at that vector
*/


public class Asteroid3 : MonoBehaviour
{
	public float maxSpeed;
	public float maxSpin;
	public Rigidbody2D rbody;
	public float screenTop;
	public float screenBottom;
	public float screenLeft;
	public float screenRight;
	float x;
	float y;
	Vector2 startingPosition;
	bool withinViewport = false;

	// Use this for initialization
	void Start()
	{
		AsteroidSpawn();
	}

	// Update is called once per frame
	void Update()
	{
		UpdateAsteroids();
	}

	//Checks whether asteroid is on screen or not
	void IsWithinViewport()
	{
		if (transform.position.y < screenTop && transform.position.y > screenBottom && transform.position.x < screenRight && transform.position.x > screenLeft)
		{
			withinViewport = true;
			ScreenWrap();
		}
	}

	//When asteroid leaves screen it wraps around to opposite side
	void ScreenWrap()
	{
		if (withinViewport == true)
		{
			Vector2 newPos = transform.position;

			if (transform.position.y > screenTop)
			{
				newPos.y = screenBottom;
			}
			if (transform.position.y < screenBottom)
			{
				newPos.y = screenTop;
			}
			if (transform.position.x > screenRight)
			{
				newPos.x = screenLeft;
			}
			if (transform.position.x < screenLeft)
			{
				newPos.x = screenRight;
			}

			transform.position = newPos;
		}
	}

	//Spawn the asteroids
	public void AsteroidSpawn()
	{
		//Start location
        x = Random.Range(screenLeft, screenRight);
		y = Random.Range(screenBottom, screenTop);
		startingPosition = new Vector2(x, y);
		transform.position = startingPosition;

		//Add random amount of Speed & Spin
        Vector2 speed = new Vector2(Random.Range(-maxSpeed, maxSpeed), Random.Range(-maxSpeed, maxSpeed));
        float spin = Random.Range(-maxSpin, maxSpin);

        //Add properties to component in Unity
        rbody.AddForce(speed);
        rbody.AddTorque(spin);
	}

	void UpdateAsteroids()
	{
		IsWithinViewport();
        ScreenWrap();
	}

    /*void spawnArea ()
	{
		Vector2 spawnArea = new Vector2();
        float 
	}*/
}