using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    public GameObject explosion;

    private void OnTriggerEnter2D(Collider2D collision)
    {
		if (collision.gameObject.tag == "AsteroidLarge") 
		{
			if (gameObject.tag == "Bullet")
			{
				print("Asteroid Hit");
				Instantiate(explosion, transform.position, transform.rotation);
				Destroy(collision.gameObject);
				Destroy(gameObject);
			}

			if (gameObject.tag == "Player")
			{
				print("Player Hit");
				//Instantiate(explosion, transform.position, transform.rotation);
                GameController.instance.PlayerDied(collision.gameObject);
            }
		}
		else if (collision.gameObject.tag == "AsteroidMedium")
        {
            if (gameObject.tag == "Bullet")
            {
                print("Asteroid Hit");
                Instantiate(explosion, transform.position, transform.rotation);
                Destroy(collision.gameObject);
                Destroy(gameObject);
            }

            if (gameObject.tag == "Player")
            {
                print("Player Hit");
                Instantiate(explosion, transform.position, transform.rotation);
                Destroy(collision.gameObject);
                Destroy(gameObject);
                SceneHandler.instance.LoadSceneGameOver(); // Need to move to Game Controller
            }
        }
		else if (collision.gameObject.tag == "AsteroidSmall")
        {
            if (gameObject.tag == "Bullet")
            {
                print("Asteroid Hit");
                Instantiate(explosion, transform.position, transform.rotation);
                Destroy(collision.gameObject);
                Destroy(gameObject);
            }

            if (gameObject.tag == "Player")
            {
                print("Player Hit");
                Instantiate(explosion, transform.position, transform.rotation);
                Destroy(collision.gameObject);
                Destroy(gameObject);
                SceneHandler.instance.LoadSceneGameOver(); // Need to move to Game Controller
            }
        }
    }


}
