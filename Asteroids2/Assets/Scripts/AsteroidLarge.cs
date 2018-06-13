using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidLarge : MonoBehaviour
{
	public Rigidbody2D rbody;
	int speed = 70;
	float spin = 180;
	float screenLeft = -10.4f;
	float screenRight = 10.4f;
	float screenBottom = -6.5f;
	float screenTop = 6.5f;
	int spawnBox;
    Vector2 spawnLocation;

	// Use this for initialization
	void Start()
	{
		SetLargeAsteroidPosition();
		SetAsteroidPhysics();
	}

	// Update is called once per frame
	void Update()
	{
		UpdateAsteroids();
	}
   
	public void SetLargeAsteroidPosition()
	{
		//List the 4 possible spawn boxes
        List<Vector2> vectors = new List<Vector2>();

        //Left spawn box
		float xLeft = screenLeft;
        float yLeft = Random.Range(screenBottom, screenTop);
        Vector2 leftSpawn = new Vector2(xLeft, yLeft);
        vectors.Add(leftSpawn);

		//Right spawn box
		float xRight = screenRight;
		float yRight = Random.Range(screenBottom, screenTop);
        Vector2 rightSpawn = new Vector2(xRight, yRight);
        vectors.Add(rightSpawn);
        
        //Bottom spawn box
        float xBottom = Random.Range(screenLeft, screenRight);
		float yBottom = screenBottom;
		Vector2 bottomSpawn = new Vector2(xBottom, yBottom);
        vectors.Add(bottomSpawn);

		//Top spawn box
		float xTop = Random.Range(screenLeft, screenRight);
		float yTop = screenTop;
		Vector2 topSpawn = new Vector2(xTop, yTop);
        vectors.Add(topSpawn);

        //Choose one spawn box at random
		int num = Random.Range(0, 4);
		spawnBox = num;
        var randomVectorFromList = vectors[num];
		spawnLocation = randomVectorFromList;

        //Set new random vector as spawn position
		transform.position = spawnLocation;
	}

    void SetAsteroidPhysics()
	{
		//Set asteroid direction              Vector2 direction = new Vector2();         if (spawnBox == 0)
		{
			direction.x = 1f;             if (spawnLocation.y < 0f)             { 				direction.y = Random.Range(0.25f, 0.25f);             }             else if (spawnLocation.y > 0f)             {                 direction.y = Random.Range(-0.25f, -0.25f);             }             else direction.y = Random.Range(-0.75f, 0.75f);         }         if (spawnBox == 1)         {             direction.x = -1f;             if (spawnLocation.y < 0f)             {                 direction.y = Random.Range(0.25f, 0.5f);             }             else if (spawnLocation.y > 0f)             {                 direction.y = Random.Range(-0.25f, -0.5f);             }             else direction.y = Random.Range(-0.75f, 0.75f);         }         if (spawnBox == 2)         {             if (spawnLocation.x < 0f)             {                 direction.x = Random.Range(0.25f, 0.5f);             }             else if (spawnLocation.x > 0f)             {                 direction.x = Random.Range(-0.25f, -0.5f);             }             else direction.x = Random.Range(-0.75f, 0.75f);             direction.y = 1f;         }         if (spawnBox == 3)         {             if (spawnLocation.x < 0f)             {                 direction.x = Random.Range(0.25f, 0.5f);             }             else if (spawnLocation.x > 0f)             {                 direction.x = Random.Range(-0.25f, -0.5f);             }             else direction.x = Random.Range(-0.75f, 0.75f);             direction.y = -1f;         }          //Apply the direction & speed         rbody.AddForce(direction * speed);          //Apply object rotation         rbody.AddTorque(spin);
	}

	void UpdateAsteroids()
	{
        ScreenWrap();
	}

    //When asteroid leaves screen it wraps around to opposite side
    void ScreenWrap()
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