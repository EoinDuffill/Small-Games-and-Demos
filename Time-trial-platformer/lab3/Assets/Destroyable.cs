using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : MonoBehaviour {

    // script for the target
    public int timeBonus = 10;

    GameController gameController;

    // Use this for initialization
    void Start () {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        // destroy this object
        if(collision.gameObject.tag != "Player")
        {
            DestroyObject(gameObject);
        }
        
    }
    private void OnDestroy()
    {
        // tell the game controller
        if (gameController != null)
        {
            gameController.TargetDestroyed();
        }
    }
}
