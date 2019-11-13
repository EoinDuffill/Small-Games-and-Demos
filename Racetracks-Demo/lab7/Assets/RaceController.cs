using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Vehicles.Car;
using UnityEngine.SceneManagement;

public class RaceController : MonoBehaviour
{
    enum RaceState
    {
        PENDING,
        RACING,
        FINISHED
    };

    GameObject[] AICars;

    RaceState raceState;

    public Text timeText;
    public Text resultText;
    public Text lastLapText;
    public Text bestLapText;
    public Text lapsRemaingingText;

    public float startTime;
    public int laps = 5;

    private float lastLap = float.MaxValue;
    private float bestLap = float.MaxValue;

    

    private bool firstLineCrossComplete = false;

    private bool lapFinished = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(startCountdown());
        raceState = RaceState.PENDING;

        AICars = GameObject.FindGameObjectsWithTag("AICar");

        bestLapText.text = "Best Lap: --:--.---";
        lastLapText.text = "Last Lap: --:--.---";
        timeText.text = "Current Lap: --:--.---";
        lapsRemaingingText.text = "Remaining Laps: " + laps;
    }

    void Update()
    {
        if (raceState == RaceState.RACING)
        {
            timeText.text = "Current Lap: "+formatLapTime(Time.time - startTime);
            

        }
        
        if(lapFinished)
        {
            lapsRemaingingText.text = "Remaining Laps: " + laps;
            lapFinished = false;
            lastLap = (Time.time - startTime);
            lastLapText.text = "Last Lap: " + formatLapTime(Time.time - startTime);

            if(lastLap < bestLap)
            {
                bestLap = lastLap;
                bestLapText.text = "Best Lap: " + formatLapTime(Time.time - startTime);
                
            }
            startTime = Time.time;
        }
        if(raceState == RaceState.FINISHED)
        {
            StartCoroutine(endRace());
            raceState = RaceState.PENDING;
        }
    }

    public static string formatLapTime(float time)
    {
        int minutes = 0;
        int seconds = 0;
        int millis = 0;

        minutes = (int)Mathf.Floor(time / 60);
        seconds = (int)Mathf.Floor(time - (minutes * 60));
        millis = (int)Mathf.Floor((time - (minutes * 60) - seconds) * 1000);

        return minutes.ToString("00")+":"+seconds.ToString("00")+"."+millis;
    }

    IEnumerator startCountdown()
    {
        int count = 3;
        while (count > 0)
        {
            resultText.text = "Race Start in " + count;
            count--;
            yield return new WaitForSeconds(1);
        }
        raceState = RaceState.RACING;
        startTime = Time.time;
        resultText.text = "GO!!";

        foreach (GameObject car in AICars)
        {
            car.GetComponent<CarAIControl>().enabled = true;
        }

        
        resultText.enabled = false;
    }

    IEnumerator endRace()
    {
        ScoreManager.instance.setTime(bestLap, SceneManager.GetActiveScene().buildIndex - 1);

        int count = 5;
        resultText.enabled = true;
        resultText.text = "Race Over! Returning to menu in " + count;
        while (count > 0)
        {
            resultText.text = "Race Over! Returning to menu in " + count;
            count--;
            yield return new WaitForSeconds(1);
        }
        SceneManager.LoadScene(0);
    }

    private void OnTriggerEnter(Collider collider)
    {

        if(collider.tag == "Player")
        {
            if (firstLineCrossComplete)
            {
                if (raceState == RaceState.RACING)
                {
                    lapFinished = true;
                    laps--;
                    if (laps == 0)
                    {
                        raceState = RaceState.FINISHED;
                    }
                }
            }
            
            firstLineCrossComplete = true;
        }
        



    }
}
