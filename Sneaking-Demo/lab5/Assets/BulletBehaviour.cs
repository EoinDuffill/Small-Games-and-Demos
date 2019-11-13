using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour {

    float timeout = 5f;
    float startTime = 0f;

    public float bulletSpeed = 25f;

	// Use this for initialization
	void Start () {
        startTime = Time.time;
        Rigidbody r = GetComponent<Rigidbody>();
        r.velocity = transform.forward * bulletSpeed;
    }
	
	// Update is called once per frame
	void Update () {
		if(Time.time > startTime + timeout)
        {
            DestroyObject(gameObject);
        }       

	}

    // script for the bullet
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != "Enemy")
        {
            DestroyObject(gameObject);
            Debug.Log("destroyed");
        }
        
    }
}
