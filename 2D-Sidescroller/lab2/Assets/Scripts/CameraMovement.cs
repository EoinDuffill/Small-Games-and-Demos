using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private GameObject player;
    private GameObject background;

    public float offset = 10;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        background = GameObject.FindWithTag("Background");
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + offset, -10);
        background.transform.position = new Vector3(player.transform.position.x, player.transform.position.y/1.75f + offset, 0);
    }
}
