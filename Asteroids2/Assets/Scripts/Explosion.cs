using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

    public float lifeTime = 1.0f;

    public AudioClip explosionSound;

	// Use this for initialization
	void Start ()
    {
        Destroy(gameObject, lifeTime);
        SoundManager.instance.PlaySingleWithRandomPitch(explosionSound);
    }
}
