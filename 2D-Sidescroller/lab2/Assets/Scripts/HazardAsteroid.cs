using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardAsteroid : MonoBehaviour
{
    public AnimationCurve curve;
    public bool movementInX;
    public bool movementInY;

    private float startingX = 0;
    private float startingY = 0;
    private float x = 0;
    private float y = 0;

    // Start is called before the first frame update
    void Start()
    {
        startingX = gameObject.transform.position.x;
        startingY = gameObject.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (movementInX) x = startingX + curve.Evaluate((Time.time % curve.length));
        else x = transform.position.x;

        if (movementInY) y = startingY + curve.Evaluate((Time.time % curve.length));
        else y = transform.position.y;

        transform.position = new Vector3(x, y, transform.position.z);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
