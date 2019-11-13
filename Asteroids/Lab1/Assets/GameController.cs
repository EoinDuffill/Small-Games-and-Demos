using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public GameObject hazard;
    public Vector3 spawnValues = new Vector3(14, 0, 10);
    public int hazardCount = 3;
    public float spawnWait = 2;
    public float waveWait = 2;

    private int totalScore = 0;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                float rand1 = (int)(2 * (Mathf.Round(Random.Range(0, 2)) - .5f));
                float rand2 = Random.Range(0, 2);

                Vector3 spawnPosition;
                if (rand2 != 0)
                {
                    spawnPosition = new Vector3(
                    Random.Range(-spawnValues.x, spawnValues.x),
                    spawnValues.y,
                    rand1 * spawnValues.z);
                }
                else
                {
                    spawnPosition = new Vector3(
                    rand1 * spawnValues.x,
                    spawnValues.y,
                    Random.Range(-spawnValues.z, spawnValues.z));

                }
                Quaternion spawnRotation = Quaternion.identity;
                GameObject asteroid = Instantiate(hazard, spawnPosition, spawnRotation);
                asteroid.GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * 4;

                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
        }
    }

    public void AddScore(int score)
    {
        totalScore += score;
        Debug.Log("Current score:" + totalScore);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
