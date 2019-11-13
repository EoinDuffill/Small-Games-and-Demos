using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnContact : MonoBehaviour
{

    private GameObject player;
    private GameObject projectile;

    GameController gameController;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if(gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannoy find 'GameController' script");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        projectile = GameObject.FindWithTag("Projectile");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(player != null && other.gameObject.tag == "Player")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            Application.LoadLevel(Application.loadedLevel);
        }

        if (projectile != null && other.gameObject.tag == "Projectile")
        {
            gameController.AddScore(100);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        
    }
}
