using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSmall : MonoBehaviour
{
    public Rigidbody2D rbody;
	int minSpeed = 75;
    int maxSpeed = 95;
    int minSpin = 100;
    int maxSpin = 150;
    float screenLeft = -9.7f;
    float screenRight = 9.7f;
    float screenBottom = -5.8f;
    float screenTop = 5.8f;
    float spawnBoundaryLeft = -10;
    float spawnBoundaryRight = 10;
    float spawnBoundaryBottom = -6.1f;
    float spawnBoundaryTop = 6.1f;
    bool withinViewport = false;
    Vector2 spawnLocation;

    // Use this for initialization
    void Start()
    {
        SetSmallAsteroidPhysics();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAsteroids();
    }

    public void SetSmallAsteroidPhysics()
    {
        //Add random amount of Speed & Spin
        Vector2 speed = new Vector2(Random.Range(minSpeed, maxSpeed), Random.Range(minSpeed, maxSpeed));
        float spin = Random.Range(minSpin, maxSpin);
        rbody.AddForce(speed);
        rbody.AddTorque(spin);
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
