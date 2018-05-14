using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Use this for initialization
    void Start()
    {
		//Start location
		x = Random.Range(screenLeft, screenRight);
		y = Random.Range(screenBottom, screenTop);
		startingPosition = new Vector2(x, y);
		transform.position = startingPosition;

        //Add random amount of Speed & Spin
        Vector2 speed = new Vector2(Random.Range(-maxSpeed, maxSpeed), Random.Range(-maxSpeed, maxSpeed));
        float spin = Random.Range(-maxSpin, maxSpin);

        //Add properties to Rigidbody2D Component in Unity
        rbody.AddForce(speed);
        rbody.AddTorque(spin);
    }

    // Update is called once per frame
    void Update()
    {
        //Screen Wrapping
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