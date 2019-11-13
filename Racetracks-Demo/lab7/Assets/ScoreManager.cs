using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance = null;

    public float[] trackTimes = new float[2] { 100f, 100f };

    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
            //if not, set instance to this
            instance = this;
        //If instance already exists and it's not this:
        else if (instance != this)
            // Then destroy this. This enforces the singleton pattern,
            // meaning there can only ever be one instance of a ScoreManager.
            Destroy(gameObject);
        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }
    public void setTime(float newTime, int trackNumber)
    {
        if (trackTimes[trackNumber] > newTime)
            trackTimes[trackNumber] = newTime;
    }
}