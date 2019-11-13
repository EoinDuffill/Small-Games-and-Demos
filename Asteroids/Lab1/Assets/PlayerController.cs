using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public GameObject shot;
    public Transform shotTransform;

    public float speed = 0.5f;
    public float decSpeed = 0.1f;
    public int decMultiplier = 5;
    public float rotationalSpeed = 135;
    public float fireRate = 0.5f;
    private float nextFire = 0f;

    private readonly float xMin = -18f;
    private readonly float xMax = 18f;
    private readonly float zMin = -10.125f;
    private readonly float zMax = 10.125f;

    private int brakeAmount = 1;

    private float forwardMovement = 0f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        Rigidbody r = GetComponent<Rigidbody>();

        if (Input.GetKey(KeyCode.W))
        {
            forwardMovement = speed;

        }
        else
        {
            if (Input.GetKey(KeyCode.S)) brakeAmount = decMultiplier;
            else brakeAmount = 1;

            if (forwardMovement > 0) forwardMovement -= decSpeed * brakeAmount * Time.deltaTime;
            else forwardMovement = 0;
        }

        transform.Translate(new Vector3(
            0f,
            0f,
            forwardMovement * Time.deltaTime));

        float rotationalMovement = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * rotationalMovement * Time.deltaTime * rotationalSpeed);

        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + (1/fireRate);
            Instantiate(
            shot,
            shotTransform.position,
            shotTransform.rotation);
        }

        r.position = new Vector3(
             Mathf.Clamp(r.position.x, xMin, xMax),
             r.position.y,
             Mathf.Clamp(r.position.z, zMin, zMax));
    }
}
