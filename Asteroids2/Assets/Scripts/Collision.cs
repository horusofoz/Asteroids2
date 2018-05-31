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
                GameController.instance.AsteroidLargeHitByBullet(collision.gameObject);
				Destroy(gameObject); // Destroy Bullet
			}

			if (gameObject.tag == "Player")
			{
                GameController.instance.PlayerDied(gameObject);
            }
		}
		else if (collision.gameObject.tag == "AsteroidMedium")
        {
            if (gameObject.tag == "Bullet")
            {
                GameController.instance.AsteroidMediumHitByBullet(collision.gameObject);
                Destroy(gameObject); // Destroy Bullet
            }

            if (gameObject.tag == "Player")
            {
                GameController.instance.PlayerDied(gameObject);
            }
        }
		else if (collision.gameObject.tag == "AsteroidSmall")
        {
            if (gameObject.tag == "Bullet")
            {
                GameController.instance.AsteroidSmallHitByBullet(collision.gameObject);
                Destroy(gameObject);
            }

            if (gameObject.tag == "Player")
            {
                GameController.instance.PlayerDied(gameObject);
            }
        }
    }
}
