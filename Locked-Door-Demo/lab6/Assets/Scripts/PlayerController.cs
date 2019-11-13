using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    float verticalVelocity = 0;

    public Inventory inventory;

    public float jumpSpeed = 5;
    public float moveSpeed = 5;

    public float nextFire = 0f;
    public float fireRate = 0.25f;

    float y = 0f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Lock Mouse
        if (Input.GetKey(KeyCode.Escape))
        {
            Screen.lockCursor = false;
        }
        else
        {
            Screen.lockCursor = true;
        }


        // rotate the player object about the Y axis
        float rotation = Input.GetAxis("Mouse X");
        transform.Rotate(0, rotation, 0);
        // rotate the camera (the player's "head") about its X axis

        float updown = Input.GetAxis("Mouse Y");
        // clamp allowed rotation to 30
        if (y + updown > 90 || y + updown < -90)
        {
            updown = 0;
        }

        y += updown;

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

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            inventory.switchSlot(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            inventory.switchSlot(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            inventory.switchSlot(2);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            inventory.useSelectedItem();
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

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        IInventoryItem item = hit.gameObject.GetComponent<IInventoryItem>();
        if (item != null)
        {
            inventory.addItem(item);
        }
    }

}
