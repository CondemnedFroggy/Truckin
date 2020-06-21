using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private GameObject timer;

    [SerializeField]
    private int start_mins;

    public float time_remaining = 0;
    private int minutes = 0;
    private int tens_secs = 0;
    private int seconds = 0;

    private string display_time = "0:00";

    void Start()
    {
        minutes = start_mins;
        time_remaining = minutes * 60;
    }

    void Update()
    {
        if (time_remaining < 0)
        {
            time_remaining = 0;
        }
        else
        {
            time_remaining = time_remaining - Time.deltaTime;
            seconds = Mathf.RoundToInt(time_remaining);
            tens_secs = 0;
            minutes = 0;
            while (seconds >= 10)
            {
                seconds = seconds - 10;
                tens_secs++;
            }

            while (tens_secs >= 6)
            {
                tens_secs = tens_secs - 6;
                minutes++;
            }
        }

        timer.GetComponent<Text>().text = minutes + ":" + tens_secs + seconds;
    }
}
