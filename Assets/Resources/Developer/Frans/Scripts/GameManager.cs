using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public int m_chooseMiniGame;
    public Scene m_scene;
    public int m_index;

    // (K) Leaderboard Properties
    [SerializeField] List<PlayerData> playerdataList = new List<PlayerData>();
    private PlayerMovement[] allP_Movement; // (K) Temporary Variable used for counting players.
    public int mainCurrentPlayers = 0;
    public int maxMiniGamePoints = 100; // (K) This is 1st players reward.

    //PlayerDetector properties
    public int m_playersOut = 0;
    public int m_playerOutToStopGame;
    public bool m_isOnePlayerLeft;
    public List<bool> m_outPlayers = new List<bool>();

    public PlayerInputManager m_playerInputManager;

    private void Start()
    {
        m_playerInputManager.onPlayerJoined += CountAmountOfPlayers;
    }

    public void CountAmountOfPlayers(PlayerInput player)
    {
        PlayerMovement[] allP_Movement = FindObjectsOfType<PlayerMovement>();
        mainCurrentPlayers = Mathf.Min(4, allP_Movement.Length); // (K) Count amount of players in scene.
        playerdataList.Clear();
        for (int i = 0; i < mainCurrentPlayers; i++) // (K) Attach PlayerData to existing players.
        {
            PlayerData newPlayerData = new PlayerData
            {
                playerID = i,
                playerScore = 0,
                playerPosition = 0
            };
            //newPlayerData.playerID = i; 
            playerdataList.Add(newPlayerData);
            Debug.Log(newPlayerData.playerID);
        }
    }

    public void CheckScene()
    {
        m_scene = SceneManager.GetActiveScene();
        m_index = m_scene.buildIndex;
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

    public void AddingPlayerToTheBench(int Index)
    {
        //m_outPlayers = new bool[playerdataList.Count]
        for (int i = 0; i < Index; i++)
        {
            m_outPlayers.Add(false);
        }
        m_playerOutToStopGame = playerdataList.Count - 1;
        m_isOnePlayerLeft = false;
    }

    public void PlayerEliminated()
    {
        m_playersOut++;
        if (m_playersOut >= 3)
        {
            Winner();
        }
    }

    public void Winner()
    {
        for (int i = 0; i < playerdataList.Count; i++)
        {
            allP_Movement[i].PlayerOn();
        }

        SceneManager.LoadScene("PlayerHub");
    }
}




