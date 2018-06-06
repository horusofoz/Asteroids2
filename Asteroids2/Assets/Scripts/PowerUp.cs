using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

    private float lifeTime = 5.0f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }
}
