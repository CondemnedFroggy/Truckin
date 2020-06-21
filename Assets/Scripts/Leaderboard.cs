using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{
    public List<PlayerInfo> playerInfos;
    _GameSaveLoad save;

    private GameObject ScoreManager;

    public GameObject LeaderboardCanvas;

    [SerializeField]
    private List<GameObject> LeaderBoardNames;

    [SerializeField]
    private List<GameObject> LeaderBoardScores;

    private void Start()
    {
        save = new _GameSaveLoad();
        playerInfos = new List<PlayerInfo>();
        ScoreManager = GameObject.FindGameObjectWithTag("ScoreManager");

        Load();
    }

    public void SetNameToCombineAndSave(GameObject EnteredNameObject)
    {
        PlayerInfo new_Player = ScoreManager.GetComponent<ScoreManager>().playerInfo;
        new_Player.name = EnteredNameObject.GetComponent<InputField>().text;
        AddPlayer(new_Player);
        Save();

        //set up leaderboard
        for (int i = 0; i < 7; i++)
        {
            LeaderBoardNames[i].GetComponent<Text>().text = playerInfos[i].name;
            LeaderBoardScores[i].GetComponent<Text>().text = Mathf.RoundToInt(playerInfos[i].totalScore).ToString();
        }

        LeaderboardCanvas.GetComponent<MoveUI>().Move(true);
    }

    public void AddPlayer(PlayerInfo player)
    {
        playerInfos.Add(player);
        RankData();

        for (int i = 0; i < playerInfos.Count; i++)
        {
            if (i > 8)
                playerInfos.RemoveAt(i);
        }
        playerInfos.TrimExcess();
    }

    private void RankData()
    {
        Comparison<PlayerInfo> comparer = (PlayerInfo a, PlayerInfo b) => b.totalScore.CompareTo(a.totalScore);
        playerInfos.Sort(comparer);
    }

    public void Save()
    {
        RankData();
        string data = save.SerializeObject(playerInfos);
        save.CreateXML(data);
    }

    public void Load()
    {
        playerInfos = (List<PlayerInfo>)save.DeserializeObject(save.LoadXML());
    }
    
}