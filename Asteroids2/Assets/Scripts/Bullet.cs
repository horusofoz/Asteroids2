using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    private Rigidbody2D rbody;
    [SerializeField] float speed = 15.0f;
    
    // Use this for initialization
	void Start ()
    {
        rbody = GetComponent<Rigidbody2D>();
        rbody.velocity = transform.up * speed;
	}

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
