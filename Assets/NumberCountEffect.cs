using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberCountEffect : MonoBehaviour
{
    public float targetNumber;
    private float currentNumber = 0;

    [SerializeField]
    private float time_to_target_num;

    private float addition_per_sec = 0;

    private float time_passed = 0;

    [SerializeField]
    private float time_till_audio_start = 0;

    [SerializeField]
    private GameObject AudioSource;

    private bool has_played = false;

    void Start()
    {
        addition_per_sec = targetNumber / time_to_target_num;
    }

    void Update()
    {
        time_passed += Time.deltaTime;

        if(time_passed > time_till_audio_start && has_played == false)
        {
            AudioSource.GetComponent<AudioSource>().Play();
            has_played = true;
        }

        if (time_passed > time_to_target_num)
        {
            currentNumber = targetNumber;
            GetComponent<Text>().text = Mathf.RoundToInt(currentNumber).ToString();
            AudioSource.GetComponent<AudioSource>().Stop();
        }
        else
        {
            currentNumber += Time.deltaTime * addition_per_sec;
            GetComponent<Text>().text = Mathf.RoundToInt(currentNumber).ToString();
        }
    }
}
