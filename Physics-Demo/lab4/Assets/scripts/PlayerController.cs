using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    float verticalVelocity = 0;

    public float jumpSpeed = 5;
    public float moveSpeed = 5;

    public float nextFire = 0f;
    public float fireRate = 0.25f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // rotate the player object about the Y axis
        float rotation = Input.GetAxis("Mouse X");
        transform.Rotate(0, rotation, 0);
        // rotate the camera (the player's "head") about its X axis
        float updown = Input.GetAxis("Mouse Y");
        Camera.main.transform.Rotate(-updown, 0, 0);
        // moving forwards and backwards
        float forwardSpeed = moveSpeed * Input.GetAxis("Vertical");
        // moving left to right
        float lateralSpeed = moveSpeed * Input.GetAxis("Horizontal");
        // apply gravity
        verticalVelocity += Physics.gravity.y * Time.deltaTime;
        CharacterController characterController = GetComponent<CharacterController>();
        if (Input.GetButton("Jump") && characterController.isGrounded)
        {
            verticalVelocity = jumpSpeed;
        }
        Vector3 speed = new Vector3(lateralSpeed, verticalVelocity, forwardSpeed);
        // transform this absolute speed relative to the player's current rotation
        // i.e. we don't want them to move "north", but forwards depending on where
        // they are facing
        speed = transform.rotation * speed;
        // what is deltaTime?
        // move at a different speed to make up for variable framerates
        characterController.Move(speed * Time.deltaTime);

    }

   
}
