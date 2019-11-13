using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public Text timeText;
    public Text enemiesRemainingText;
    public Text gameStateText;
    public Text timerText;

    public float timeLeft = 10.0f;

    bool gameOver = false;
    string gameState = "Game In Progress";

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            if (!gameOver)
            {
                gameState = "Game Lost";
                gameOver = true;
            }          
            timeLeft = 0;
        }

        if (!gameOver)
        {
            timeText.text = "Time Left: " + timeLeft.ToString("0");
            timerText.text = Time.time.ToString("0.00");
        }
        
        gameStateText.text = gameState;
        enemiesRemainingText.text = "Remaining Targets: "+GameObject.FindObjectsOfType<Destroyable>().Length.ToString();
    }

    public void TargetDestroyed()
    {
        if (GameObject.FindObjectsOfType<Destroyable>().Length == 0 && !gameOver)
        {
            gameState = "Game Won";
            gameOver = true;
        }
        else
        {
            timeLeft += 3;
        }
    }
}
