using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidLarge : MonoBehaviour
{
	public Rigidbody2D rbody;
	int minSpeed = 35;
	int maxSpeed = 55;
	int minSpin = 45;
	int maxSpin = 80;
    float screenLeft = -10.7f;
	float screenRight = 10.7f;
	float screenBottom = -6.8f;
	float screenTop = 6.8f;
    float spawnBoundaryLeft = -11.4f;
	float spawnBoundaryRight = 11.4f;
	float spawnBoundaryBottom = -7.5f;
	float spawnBoundaryTop = 7.5f;
	bool withinViewport = false;
    Vector2 spawnLocation;

	// Use this for initialization
	void Start()
	{
		SetLargeAsteroidPosition();
	}

	// Update is called once per frame
	void Update()
	{
		UpdateAsteroids();
	}
   
	public void SetLargeAsteroidPosition()
	{
        
		//List the 4 possible spawn boxes         List<Vector2> vectors = new List<Vector2>();          //Left spawn box         float xLeft = Random.Range(spawnBoundaryLeft, screenLeft);         float yLeft = Random.Range(screenBottom, screenTop);         Vector2 leftSpawn = new Vector2(xLeft, yLeft);         vectors.Add(leftSpawn);          //Right spawn box         float xRight = Random.Range(screenRight, spawnBoundaryRight);         float yRight = Random.Range(screenBottom, screenTop);         Vector2 rightSpawn = new Vector2(xRight, yRight);         vectors.Add(rightSpawn);                  //Bottom spawn box         float xBottom = Random.Range(screenLeft, screenRight);         float yBottom = Random.Range(screenBottom, spawnBoundaryBottom);         Vector2 bottomSpawn = new Vector2(xBottom, yBottom);         vectors.Add(bottomSpawn);          //Top spawn box         float xTop = Random.Range(screenLeft, screenRight);         float yTop = Random.Range(spawnBoundaryTop, screenTop);         Vector2 topSpawn = new Vector2(xTop, yTop);         vectors.Add(topSpawn);          //Choose one spawn box at random         int count = vectors.Count; 		int num = Random.Range(0, 3);         var randomVectorFromList = vectors[num];         spawnLocation = randomVectorFromList;          //Set new random vector as spawn position         transform.position = spawnLocation;          //Add random amount of Speed & Spin         Vector2 speed = new Vector2(Random.Range(minSpeed, maxSpeed), Random.Range(minSpeed, maxSpeed));         float spin = Random.Range(minSpin, maxSpin);         rbody.AddForce(speed);         rbody.AddTorque(spin);
	}

	void UpdateAsteroids()
	{
		IsWithinViewport();
        ScreenWrap();
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
}

/*
 * When collision detects large asteroid hit
 * It calls AsteroidLargeHit()
 * AsteroidLargeHit calls SpawnAsteroidMedium x 2, then destroy large asteroid
 * 
 */