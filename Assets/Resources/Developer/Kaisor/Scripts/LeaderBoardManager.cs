using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LeaderBoardManager : MonoBehaviour
{
    [SerializeField] List<PlayerData> playerdataList = new List<PlayerData>(); 
    public int mainCurrentPlayers = 0;
    public int maxMiniGamePoints = 100; // (K) 4x this is 1st players reward.

    private void Awake()
    {
        PlayerMovement[] allP_Movement = FindObjectsOfType<PlayerMovement>();

        mainCurrentPlayers = Mathf.Min(4, allP_Movement.Length); // (K) Count amount of players in scene.

        for (int i = 0; i < mainCurrentPlayers; i++) // (K) Attach a playerID to existing players.
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

    public void GrantPointsToOnePlayer(int playerID2, int leaderBoardPos) 
    {
        // (K) lowest points are the amount the last place player gets. Each player after gets double the points.
        int tempPoints = maxMiniGamePoints /= leaderBoardPos;
        for (int i = 0;i < playerdataList.Count; i++)
        {
            if (playerdataList[i].playerID == playerID2)
            {
                playerdataList[i].playerScore += tempPoints;
            }
        }
    }

    public void AddAndUpdateLeaderBoard(int maxPointsEarned)
    {
        // (K) Grant all instances points based on list order with each loop halving points earned.
        int tempPoints = maxPointsEarned;
        for(int i = 0;i < playerdataList.Count; i++)
        {
            playerdataList[i].playerScore += tempPoints;
            tempPoints /= 2;
        }
        // (K) Reorder list based on new scores.
        playerdataList = playerdataList.OrderByDescending(player => player.playerScore).ToList(); 
    }

    public void CheckCurrentLeaderBoardStatus()
    {      
        for (int i = 0;i < playerdataList.Count; i++)
        {
            playerdataList[i].playerPosition = i + 1; // (K) Assign new playerposition
            Debug.Log("Player " + playerdataList[i].playerID + " is in "
                + playerdataList[i].playerPosition + " with a score of " + 
                playerdataList[i].playerScore + " points!");
        }
    }
}
