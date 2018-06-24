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
                GameController.instance.PlayerHit(gameObject);
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
                GameController.instance.PlayerHit(gameObject);
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
                GameController.instance.PlayerHit(gameObject);
            }
        }
        else if (collision.gameObject.tag == "EnemyBullet")
        {
            if (gameObject.tag == "Bullet")
            {
                GameController.instance.EnemyBulletHitByBullet(collision.gameObject);
                Destroy(gameObject);
            }

            if (gameObject.tag == "Player")
            {
                GameController.instance.PlayerHit(gameObject);
                Destroy(collision.gameObject);
            }
        }
        else if (collision.gameObject.tag == "EnemyShooter")
        {
            if (gameObject.tag == "Bullet")
            {
                GameController.instance.EnemyShooterHitByBullet(collision.gameObject);
                Destroy(gameObject);
            }

            if (gameObject.tag == "Player")
            {
                GameController.instance.PlayerHit(gameObject);
                Destroy(collision.gameObject);
            }
        }
        else if (collision.gameObject.tag == "EnemyShooter2")
        {
            if (gameObject.tag == "Bullet")
            {
                GameController.instance.EnemyShooter2HitByBullet(collision.gameObject);
                Destroy(gameObject);
            }

            if (gameObject.tag == "Player")
            {
                GameController.instance.PlayerHit(gameObject);
                Destroy(collision.gameObject);
            }
        }
        else if (collision.gameObject.tag == "EnemyDrone")
        {
            if (gameObject.tag == "Bullet")
            {
                GameController.instance.EnemyDroneHitByBullet(collision.gameObject);
                Destroy(gameObject);
            }

            if (gameObject.tag == "Player")
            {
                GameController.instance.PlayerHit(gameObject);
                GameController.instance.EnemyDroneHitByBullet(collision.gameObject);
            }
        }
        else if (collision.gameObject.tag == "PickUp")
        {
            if (gameObject.tag == "Player")
            {
                GameController.instance.PickUpCollected(collision.gameObject);
            }
        }
		if (collision.gameObject.tag == "EnemyBoss")
        {
            if (gameObject.tag == "Bullet")
            {
				GameController.instance.EnemyBossHitByBullet(collision.gameObject);
                Destroy(gameObject); // Destroy Bullet
            }

            if (gameObject.tag == "Player")
            {
                GameController.instance.PlayerHit(gameObject);
            }
        }
    }
}
