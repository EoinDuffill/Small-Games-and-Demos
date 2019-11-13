using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePhysics : MonoBehaviour
{

    public float speed = 5.0f;

    private float maxTime = 5f;
    private float timeAlive = 0f;

    // Use this for initialization
    void Start()
    {
        Rigidbody r = GetComponent<Rigidbody>();
        r.velocity = transform.forward * speed;
    }

    // Update is called once per frame
    void Update()
    {
        timeAlive += Time.deltaTime;
        if (timeAlive > maxTime) Destroy(gameObject);
    }
}
