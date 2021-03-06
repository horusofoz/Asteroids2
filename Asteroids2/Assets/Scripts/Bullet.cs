﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    private Rigidbody2D rbody;
    [SerializeField] float speed = 15.0f;

    public AudioClip bulletSound;

    // Use this for initialization
	void Start ()
    {
        rbody = GetComponent<Rigidbody2D>();
        rbody.velocity = transform.up * speed;
        SoundManager.instance.PlaySingle(bulletSound);
	}

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
