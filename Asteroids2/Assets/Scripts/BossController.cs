using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour {

    //Movement
	public Rigidbody2D rbody;
    float enemySpeed = 2.5f;
    [SerializeField] Boundary boundary;
    GameObject player;

	// Spawning
    Vector2 spawnLocation;
	float screenTop = 5.2f;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void KeepEnemyBossOnScreen()
    {
        rbody.position = new Vector2(
            Mathf.Clamp(rbody.position.x, boundary.xMin, boundary.xMax),
            Mathf.Clamp(rbody.position.y, boundary.yMin, boundary.yMax));
    }
}
