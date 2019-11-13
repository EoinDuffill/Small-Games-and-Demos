using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public Text track1;
    public Text track2;
    public Button track2Button;


    // Start is called before the first frame update
    void Start()
    {
        if(ScoreManager.instance != null)
        {
            track1.text = RaceController.formatLapTime(ScoreManager.instance.trackTimes[0]);
            track2.text = RaceController.formatLapTime(ScoreManager.instance.trackTimes[1]);

            if(ScoreManager.instance.trackTimes[0] < 27f)
            {
                track2Button.enabled = true;
            }
            else
            {
                track2Button.enabled = false;
            }
        }
        else
        {
            track1.text = "--:--.---";
            track2.text = "--:--.---";
            track2Button.enabled = false;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loadTrack(int trackIndex)
    {
        SceneManager.LoadScene(trackIndex);
    }
}
