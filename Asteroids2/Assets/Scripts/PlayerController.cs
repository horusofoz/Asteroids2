using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used for setting invisble border/boudnary
[System.Serializable]
public class Boundary
{
    public float xMin, xMax, yMin, yMax;
}

public class PlayerController : MonoBehaviour {

    [SerializeField] private float thrustSpeed = 4.0f;
    [SerializeField] private float maxPlayerSpeed = 6.0f;
    private float rotationSpeed = 1.5F;
    [SerializeField] Boundary boundary;


    Rigidbody2D rbody;

	// Use this for initialization
	void Start ()
    {
        rbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {

        ProcessThrust();
        ProcessRotation();
        KeepPlayerOnScreen();
    }

    void ProcessThrust()
    {
        float thrust = Input.GetAxis("Vertical");
        rbody.AddForce(transform.up * thrustSpeed * thrust);
        rbody.velocity = Vector3.ClampMagnitude(rbody.velocity, maxPlayerSpeed);

        if (rbody.velocity.magnitude > maxPlayerSpeed)
        {
            rbody.velocity = rbody.velocity.normalized * maxPlayerSpeed;
        }
    }

    void ProcessRotation()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.forward * rotationSpeed * -moveHorizontal);
    }

    void KeepPlayerOnScreen()
    {
        rbody.position = new Vector2(
            Mathf.Clamp(rbody.position.x, boundary.xMin, boundary.xMax),
            Mathf.Clamp(rbody.position.y, boundary.yMin, boundary.yMax));
    }
}
