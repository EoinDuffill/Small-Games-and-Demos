using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBehaviour : MonoBehaviour
{

    private GameObject player;
    public float speedMin = 1f;
    public float speedMax = 3f;
    private float speed = 0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        speed = Random.Range(speedMin, speedMax);
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null && (player.transform.position - transform.position).magnitude > 1)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed*Time.deltaTime);
        }
    }
}
