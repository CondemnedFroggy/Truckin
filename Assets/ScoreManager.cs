using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> Figures;

    public PlayerInfo playerInfo;

    private int windows_smashed = 0;

    [SerializeField]
    private GameObject timer;

    private void Start()
    {
        playerInfo.score = 50000;

        DontDestroyOnLoad(gameObject);
    }


    public void RemoveScore(int score)
    {
        playerInfo.score -= score;

        string temp = playerInfo.score.ToString();
        while(temp.Length < 5)
        {
            temp = "0" + temp;
        }

        int i = 0;
        foreach (GameObject figure in Figures)
        {
            figure.GetComponent<Text>().text = temp[i].ToString();
            i++;
        }
    }

    public void WindowSmashed()
    {
        playerInfo.windowsSmashed++;
    }

    private void FixedUpdate()
    {
        RemoveScore(3);
    }

    public void GameOver()
    {
        playerInfo.timeRemaining = timer.GetComponent<Timer>().time_remaining;
    }
}
