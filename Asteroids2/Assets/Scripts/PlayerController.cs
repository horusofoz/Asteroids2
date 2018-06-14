using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used for setting invisble border/boudnary
[System.Serializable]
public class Boundary
{
    public float xMin, xMax, yMin, yMax;
}

public class PlayerController : MonoBehaviour {

    public bool playerAlive = false;

    // Movement
    [SerializeField] private float thrustSpeed = 7.5f;
    [SerializeField] private float maxPlayerSpeed = 10f;
    [SerializeField] private float rotationSpeed = 3.0f;
    [SerializeField] Boundary boundary;
    Rigidbody2D rbody;

    // Shooting
    private float fireRate = 3f;
    
    private float fireCountdown = 0f;
    public GameObject bulletYellow;
    public GameObject bulletRed;
    public GameObject bulletGreen;
    public GameObject bulletLaser;
    public Transform bulletSpawn1;
    public Transform bulletSpawn2;
    public Transform bulletSpawn3;

    void Start ()
    {
        rbody = GetComponent<Rigidbody2D>();
        playerAlive = true;
    }
	
    void Update()
    {
        ProcessShoot();
    }

	void FixedUpdate ()
    {
        ProcessThrust();
        ProcessRotation();
        KeepPlayerOnScreen();
    }

    void ProcessThrust()
    {
        float thrust = Input.GetAxis("Vertical");
        Vector2 direction = new Vector2(transform.up.x, transform.up.y);
        rbody.velocity = direction * thrustSpeed * thrust;
        rbody.velocity = Vector2.ClampMagnitude(rbody.velocity, maxPlayerSpeed);
        
    }

    void ProcessRotation()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.forward * rotationSpeed * -moveHorizontal);
    }

    void KeepPlayerOnScreen()
    {
        rbody.position = new Vector2(
            Mathf.Clamp(rbody.position.x, boundary.xMin, boundary.xMax),
            Mathf.Clamp(rbody.position.y, boundary.yMin, boundary.yMax));
    }

    void ProcessShoot()
    {
        if (Input.GetButton("Fire1") == true)
        {
            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / (fireRate + GameController.instance.fireRateBonus);
            }

            fireCountdown -= Time.deltaTime;
        }
    }

    void Shoot()
    {
            switch (GameController.instance.bulletLevel)
            {
                case 0:
                    Instantiate(bulletYellow, bulletSpawn1.position, bulletSpawn1.rotation);
                    break;

                case 1:
                    Instantiate(bulletRed, bulletSpawn2.position, bulletSpawn2.rotation);
                    Instantiate(bulletRed, bulletSpawn3.position, bulletSpawn3.rotation);
                break;

                case 2:
                    Instantiate(bulletGreen, bulletSpawn1.position, bulletSpawn1.rotation);
                    Instantiate(bulletGreen, bulletSpawn2.position, bulletSpawn2.rotation);
                    Instantiate(bulletGreen, bulletSpawn3.position, bulletSpawn3.rotation);
                break;

                case 3:
                    Instantiate(bulletLaser, bulletSpawn1.position, bulletSpawn1.rotation);
                break;

                default:
                        break;
            }
    }
}
