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

    // Movement
    [SerializeField] private float thrustSpeed = 7.5f;
    [SerializeField] private float maxPlayerSpeed = 10f;
    [SerializeField] private float rotationSpeed = 5.0f;
    [SerializeField] Boundary boundary;
    Rigidbody2D rbody;

    // Shooting
    [SerializeField] private float fireRate = 3f;
    private float fireCountdown = 0f;
    public GameObject bullet;
    public Transform bulletSpawn;

	void Start ()
    {
        rbody = GetComponent<Rigidbody2D>();
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

    private void ProcessShoot()
    {
        if (Input.GetButton("Fire1") == true)
        {
            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }

            fireCountdown -= Time.deltaTime;
        }
    }

    private void Shoot()
    {
        Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
    }

}
