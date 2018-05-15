using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Asteroid3 : MonoBehaviour
{
	public Rigidbody2D rbody;
	public float maxSpeed;
	public float maxSpin;
    public float screenLeft;
	public float screenRight;
	public float screenBottom;
	public float screenTop;
    public float spawnBoundaryLeft;
	public float spawnBoundaryRight;
	public float spawnBoundaryBottom;
	public float spawnBoundaryTop;
	float x;
	float y;
	bool withinViewport = false;
	Vector2 startingPosition;
	Vector2 spawnArea = new Vector2();

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

		else if (withinViewport == false)
		{
			Vector2 newPos = transform.position;

            if (transform.position.y > spawnBoundaryTop)
            {
				newPos.y = spawnBoundaryBottom;
            }
			if (transform.position.y < spawnBoundaryBottom)
            {
				newPos.y = spawnBoundaryTop;
            }
			if (transform.position.x > spawnBoundaryRight)
            {
				newPos.x = spawnBoundaryLeft;
            }
			if (transform.position.x < spawnBoundaryLeft)
            {
				newPos.x = spawnBoundaryRight;
            }

            transform.position = newPos;
		}
	}

	//Spawn the asteroids
	public void AsteroidSpawn()
	{
		//Left spawn box
        float xLeft = Random.Range(spawnBoundaryLeft, screenLeft);
        float yLeft = Random.Range(screenBottom, screenTop);
		Vector2 leftSpawn = new Vector2(xLeft, yLeft);

        //Right spawn box
        float xRight = Random.Range(screenRight, spawnBoundaryRight);
        float yRight = Random.Range(screenBottom, screenTop);
		Vector2 rightSpawn = new Vector2(xRight, yRight);

        //Bottom spawn box
        float xBottom = Random.Range(screenLeft, screenRight);
        float yBottom = Random.Range(screenBottom, spawnBoundaryBottom);
		Vector2 bottomSpawn = new Vector2(xBottom, yBottom);

        //Top spawn box
        float xTop = Random.Range(screenLeft, screenRight);
        float yTop = Random.Range(spawnBoundaryTop, screenTop);
		Vector2 topSpawn = new Vector2(xTop, yTop);

		//Create Random Spawn location
		Vector2 spawnPosition = new Vector2((xLeft + xRight + xBottom + xTop), (yLeft + yRight + yBottom + yTop));

        transform.position = spawnPosition;

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
}