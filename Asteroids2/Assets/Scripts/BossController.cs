using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossController : MonoBehaviour {

	public GameObject enemyBoss;

    //Movement
	public Rigidbody2D rbody;
    float maxSpeed = 3f;
	Vector2 currentPosition;
	bool movingToStartPosition = false;
           

	// Use this for initialization
	void Start ()
	{
		movingToStartPosition = true;
		MoveOntoScreen();
	}
	
	// Update is called once per frame
	void Update ()
	{
		//StoreCurrentPosition();
		//StopAtStartingPoint();
	}

	void FixedUpdate()
	{
		StopAtStartingPoint();
	}

    void StoreCurrentPosition()
	{
		currentPosition = transform.position;
	}

    void MoveOntoScreen()
	{
        Vector2 bossMoves = transform.up;
        rbody.velocity = bossMoves.normalized * maxSpeed;

	}

    void StopAtStartingPoint()
	{
		if (movingToStartPosition == true)
		{
			currentPosition = transform.position;
			Vector2 newStartLocation = new Vector2(0, 3.7f);
            if (currentPosition == newStartLocation)
            {
				rbody.velocity = Vector2.zero;
            }
			movingToStartPosition = false;
		}
	}
}
