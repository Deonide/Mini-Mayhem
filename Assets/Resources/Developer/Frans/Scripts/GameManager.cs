using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public int m_chooseMiniGame;
    public Scene m_scene;
    public int m_index;
    private PlayerMovement m_playerMovement;

    // (K) Leaderboard Properties
    [SerializeField] List<PlayerData> playerdataList = new List<PlayerData>();
    private PlayerMovement[] allP_Movement; // (K) Temporary Variable used for counting players.
    public int mainCurrentPlayers = 0;
    public int maxMiniGamePoints = 100; // (K) This is 1st players reward.

    private void Start()
    {
        CountAmountOfPlayers();
    }

    public void CountAmountOfPlayers()
    {

        PlayerMovement[] allP_Movement = FindObjectsOfType<PlayerMovement>();
        mainCurrentPlayers = Mathf.Min(4, allP_Movement.Length); // (K) Count amount of players in scene.

        for (int i = 0; i < mainCurrentPlayers; i++) // (K) Attach PlayerData to existing players.
        {
            PlayerData newPlayerData = new PlayerData
            {
                playerID = i,
                playerScore = 0,
                playerPosition = 0
            };
            playerdataList.Add(newPlayerData);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene Loaded" + scene.name);
        m_chooseMiniGame = 0;/*Random.Range(0, 2);*/
        Debug.Log(m_chooseMiniGame);

    }
    public void CheckScene()
    {
        m_scene = SceneManager.GetActiveScene();
        m_index = m_scene.buildIndex;
    }

    private void Update()
    {
        
    }

    public void AddAndUpdateLeaderBoard(int playerID2, int leaderBoardPos, int maxPointsEarned)
    {
        // (K) lowest points are the amount the last place player gets. Each player after gets double the points.
        int tempPoints = maxMiniGamePoints /= leaderBoardPos;
        for (int i = 0; i < playerdataList.Count; i++)
        {
            if (playerdataList[i].playerID == playerID2)
            {
                playerdataList[i].playerScore += tempPoints;
            }
        }
        // (K) Reorder list based on new scores.
        playerdataList = playerdataList.OrderByDescending(player => player.playerScore).ToList();
    }

    public void CheckCurrentLeaderBoardStatus()
    {
        for (int i = 0; i < playerdataList.Count; i++)
        {
            playerdataList[i].playerPosition = i + 1; // (K) Assign new playerposition
            Debug.Log("Player " + playerdataList[i].playerID + " is in "
                + playerdataList[i].playerPosition + " with a score of " +
                playerdataList[i].playerScore + " points!");
        }
    }
}




