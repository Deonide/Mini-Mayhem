using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinningPlayer : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> UIS = new List<GameObject>();

    [SerializeField]
    private int m_winnningPlayerID;
    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject go in UIS)
        {
            go.SetActive(false);
        }

        m_winnningPlayerID = GameManager.Instance.m_winningPlayer;

        switch (m_winnningPlayerID)
        {
            case 0:
                PlayerOneWins();
                break;
            case 1:
                PlayerTwoWins();
                break;
            case 2:
                PlayerThreeWins();
                break;
            case 3:
                PlayerFourWins();
                break;
        }
    }

    private void PlayerOneWins()
    {
        for (int i = 0; i < UIS.Count; i++)
        {
            if(i == 0)
            {
                UIS[i].SetActive(true);
            }
        }
    }    
    
    private void PlayerTwoWins()
    {
        for (int i = 0; i < UIS.Count; i++)
        {
            if(i == 1)
            {
                UIS[i].SetActive(true);
            }
        }
    }

    private void PlayerThreeWins()
    {
        for (int i = 0; i < UIS.Count; i++)
        {
            if (i == 2)
            {
                UIS[i].SetActive(true);
            }
        }
    }

    private void PlayerFourWins()
    {
        for (int i = 3; i < UIS.Count; i++)
        {
            if (i == 3)
            {
                UIS[i].SetActive(true);
            }
        }
    }
}
