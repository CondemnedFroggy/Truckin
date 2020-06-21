using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscoreChecking : MonoBehaviour
{
    private ScoreManager scoreManager;

    [SerializeField]
    private GameObject LeaderboardGO;

    private Leaderboard leaderboard;

    [SerializeField]
    private GameObject HighscoreInterface;

    void Start()
    {
        scoreManager = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();

        leaderboard = LeaderboardGO.GetComponent<Leaderboard>();

        if (scoreManager.playerInfo.totalScore > leaderboard.playerInfos[(leaderboard.playerInfos.Count - 1)].totalScore)
        {
            StartCoroutine(SetHighscoreActive(2.0f));
        }
    }

    IEnumerator SetHighscoreActive(float time)
    {
        yield return new WaitForSeconds(time);
        HighscoreInterface.SetActive(true);
    }
}
