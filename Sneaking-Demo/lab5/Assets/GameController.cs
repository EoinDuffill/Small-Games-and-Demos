using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Text gameState;

    private bool goalAchieved = false;

    // Start is called before the first frame update
    void Start()
    {
        gameState.text = "Get To The Exit!";
    }

    // Update is called once per frame
    void Update()
    {
        if (goalAchieved)
        {
            gameState.text = "Goal Achieved";
        }
    }

    public void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "Player")
        {
            goalAchieved = true;
        }
    }
}
