using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSmall : MonoBehaviour
{
    public Rigidbody2D rbody;
    public float minSpeed;
    public float maxSpeed;
    public float minSpin;
    public float maxSpin;
    public float screenLeft;
    public float screenRight;
    public float screenBottom;
    public float screenTop;
    public float spawnBoundaryLeft;
    public float spawnBoundaryRight;
    public float spawnBoundaryBottom;
    public float spawnBoundaryTop;
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
