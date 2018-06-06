using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSmall : MonoBehaviour
{
    public Rigidbody2D rbody;
	int speed = 115;
	float spin = 50;
	float screenLeft = -9.2f;
	float screenRight = 9.2f;
	float screenBottom = -5.3f;
	float screenTop = 5.3f;
    float spawnBoundaryLeft = -10;
    float spawnBoundaryRight = 10;
    float spawnBoundaryBottom = -6.1f;
    float spawnBoundaryTop = 6.1f;
    bool withinViewport = false;

    // Update is called once per frame
    void Update()
    {
        UpdateAsteroids();
    }

    public void SetSmallAsteroidPhysics(Vector2 direction)
    {
		//Add random amount of Speed & Spin
        rbody.AddForce(direction * speed);
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
