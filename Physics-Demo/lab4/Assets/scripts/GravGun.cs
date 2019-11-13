using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravGun : MonoBehaviour {

    GameObject heldObject;
    int layerMask = 1 << 8;

    public Transform holdPosition;
    float carryDistance = 1f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (heldObject == null)
            {
                RaycastHit colliderHit;
                if (Physics.Raycast(transform.position, transform.forward, out colliderHit, 10.0f, layerMask))
                {
                    heldObject = colliderHit.collider.gameObject;
                    heldObject.GetComponent<Rigidbody>().useGravity = false;
                }
            }
        }

        if (Input.GetButtonUp("Fire1"))
        {
            // drop the object again
            if (heldObject != null)
            {
                heldObject.GetComponent<Rigidbody>().useGravity = true;
                heldObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                heldObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                heldObject.GetComponent<Rigidbody>().ResetInertiaTensor();
                heldObject.GetComponent<Rigidbody>().AddForce(transform.forward * 4.0f, ForceMode.Impulse);
                heldObject = null;
            }
        }

        if (heldObject != null)
        {
            // move the thing we're holding
            heldObject.GetComponent<Rigidbody>().MovePosition(holdPosition.position);
            heldObject.GetComponent<Rigidbody>().MoveRotation(holdPosition.rotation);
        }
    }
}
