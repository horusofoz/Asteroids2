using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour {

    public Rigidbody2D rbody;
    int enemySpeed = 100;
    float screenLeft = -10f;
    float screenRight = 10f;
    float screenBottom = -6f;
    float screenTop = 6f;
    float spawnBoundaryLeft = -9.1f;
    float spawnBoundaryRight = 9.1f;
    float spawnBoundaryBottom = -5.25f;
    float spawnBoundaryTop = 5.25f;
    bool withinViewport = false;
    Vector2 spawnLocation;

    // Shooting
    [SerializeField] private float fireRate = 5f;
    private float fireCountdown = 1f;
    public GameObject bullet;
    public Transform bulletSpawn;


    Renderer renderer;

        

    // Use this for initialization
    void Start()
    {
        SetEnemyShooterPosition();
        renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        IsWithinViewport();
        ScreenWrap();
        ProcessShoot();
    }

    

    public void SetEnemyShooterPosition()
    {

        //List the 4 possible spawn boxes
        List<Vector2> vectors = new List<Vector2>();

        //Left spawn box
        float xLeft = Random.Range(spawnBoundaryLeft, screenLeft);
        float yLeft = Random.Range(screenBottom, screenTop);
        Vector2 leftSpawn = new Vector2(xLeft, yLeft);
        vectors.Add(leftSpawn);

        //Right spawn box
        float xRight = Random.Range(screenRight, spawnBoundaryRight);
        float yRight = Random.Range(screenBottom, screenTop);
        Vector2 rightSpawn = new Vector2(xRight, yRight);
        vectors.Add(rightSpawn);

        //Bottom spawn box
        float xBottom = Random.Range(screenLeft, screenRight);
        float yBottom = Random.Range(screenBottom, spawnBoundaryBottom);
        Vector2 bottomSpawn = new Vector2(xBottom, yBottom);
        vectors.Add(bottomSpawn);

        //Top spawn box
        float xTop = Random.Range(screenLeft, screenRight);
        float yTop = Random.Range(spawnBoundaryTop, screenTop);
        Vector2 topSpawn = new Vector2(xTop, yTop);
        vectors.Add(topSpawn);

        //Choose one spawn box at random
        int count = vectors.Count;
        int spawnBoxNum = Random.Range(0, 3);  // 0 Left, 1 Right, 2 Bottom, 3 Top
        var randomVectorFromList = vectors[spawnBoxNum];
        spawnLocation = randomVectorFromList;

        //Set new random vector as spawn position
        transform.position = spawnLocation;

        // Get random direction
        float randomDirection = Random.Range(-45f, 45);

        //Set object facing inward then randomise direction
        switch (spawnBoxNum)
        {
            case 0:
                transform.Rotate(new Vector3(0, 0, 270 + randomDirection));
                break;
            case 1:
                transform.Rotate(new Vector3(0, 0, 90 + randomDirection));
                break;
            case 2:
                transform.Rotate(new Vector3(0, 0, 0 + randomDirection));
                break;
            case 3:
                transform.Rotate(new Vector3(0, 0, 180 + randomDirection));
                break;
            default:
                
                break;
        }

        rbody.AddForce(transform.up * enemySpeed);


    }

    private void Shoot()
    {
        Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
    }

    private void ProcessShoot()
    {
        if (renderer.isVisible) // If shooter on screen
        {
            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }

            fireCountdown -= Time.deltaTime;
        }
    }



    //Checks whether EnemyShooter is on screen or not
    void IsWithinViewport()
    {
        if (transform.position.y < screenTop && transform.position.y > screenBottom && transform.position.x < screenRight && transform.position.x > screenLeft)
        {
            withinViewport = true;
            ScreenWrap();
        }
    }

    //When EnemyShooter leaves screen it wraps around to opposite side
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
