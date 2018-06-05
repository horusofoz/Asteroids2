using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrone : MonoBehaviour {

    // Movement
    public Rigidbody2D rbody;
    public float enemySpeed = 10f;
    [SerializeField] Boundary boundary;
    GameObject player;

    // Spawning
    Vector2 spawnLocation;
    float screenLeft = -9.5f;
    float screenRight = 9.5f;
    float screenBottom = -5.2f;
    float screenTop = 5.2f;
    float spawnBoundaryLeft = -9.5f;
    float spawnBoundaryRight = 9.5f;
    float spawnBoundaryBottom = -5.6f;
    float spawnBoundaryTop = 5.6f;

    

    // Use this for initialization
    void Start()
    {
        SetEnemyDronePosition();
        player = GameObject.Find("Starship");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        SetEnemyDroneDirection();
    }

    public void SetEnemyDronePosition()
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
    }

    void KeepEnemyDroneOnScreen()
    {
        rbody.position = new Vector2(
            Mathf.Clamp(rbody.position.x, boundary.xMin, boundary.xMax),
            Mathf.Clamp(rbody.position.y, boundary.yMin, boundary.yMax));
    }

    void SetEnemyDroneDirection()
    {
        if(player != null)
        {
            Vector2 playerDirection = player.transform.position;

            Vector2 newDroneDirection = new Vector2(
                playerDirection.x - transform.position.x,
                playerDirection.y - transform.position.y
                );

            transform.up = newDroneDirection;
            rbody.velocity = newDroneDirection * enemySpeed;
            print(rbody.velocity.ToString());
        }

        
    }


}
