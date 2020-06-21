using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetScores : MonoBehaviour
{
    private ScoreManager scoreManager;

    [SerializeField]
    private GameObject ScoreNum;

    [SerializeField]
    private GameObject TimeScoreNum;

    [SerializeField]
    private GameObject WindowsMultiplierNum;

    [SerializeField]
    private GameObject TotalScoreNum;

    private float score;
    private float timeScore;
    private float windowsSmashed;
    private float finalScore;

    // Use this for initialization
    void Awake ()
	{
	    scoreManager = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();

	    score = scoreManager.playerInfo.score;
	    timeScore = scoreManager.playerInfo.timeRemaining * 10;
	    windowsSmashed = scoreManager.playerInfo.windowsSmashed;

	    finalScore = (score + timeScore) * windowsSmashed;

	    ScoreNum.GetComponent<Text>().text = Mathf.RoundToInt(score).ToString();
	    TimeScoreNum.GetComponent<NumberCountEffect>().targetNumber = Mathf.RoundToInt(timeScore);
	    WindowsMultiplierNum.GetComponent<NumberCountEffect>().targetNumber = Mathf.RoundToInt(windowsSmashed);
        TotalScoreNum.GetComponent<NumberCountEffect>().targetNumber = Mathf.RoundToInt(finalScore);

        scoreManager.playerInfo.totalScore = finalScore;
    }
}
