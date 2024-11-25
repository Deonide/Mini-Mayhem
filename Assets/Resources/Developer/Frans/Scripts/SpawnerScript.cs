using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Spawnerscript : MonoBehaviour
{
    //Credits naar Brian Rocha voor het helpen hiermee!
    [SerializeField] 
    private List<GameObject> m_playerSpawners = new List<GameObject>();
    [SerializeField] 
    private List<GameObject> m_playerCharacters = new List<GameObject>();
    [SerializeField]
    private int m_index = 0;

    private PlayerInputManager m_playerInputManager;

    void Start()
    {
        m_playerInputManager = GetComponent<PlayerInputManager>();
        m_playerInputManager.playerPrefab = m_playerCharacters[m_index];
    }


    public void SwitchPrefab()
    {
        if (m_playerInputManager.maxPlayerCount == 2)
        {
            if (m_index != 1)
            {
                m_index += 1;
                m_playerInputManager.playerPrefab = m_playerCharacters[m_index];
            }
        }
        else if (m_playerInputManager.maxPlayerCount == 4)
        {
            if (m_index != 3)
            {
                m_index += 1;
                m_playerInputManager.playerPrefab = m_playerCharacters[m_index];
            }
        }
    }

    public void AssignSpawner()
    {
        for (int i = 0; i < 4; i++)
        {
            m_playerCharacters[i].transform.position = new Vector3(m_playerSpawners[i].transform.position.x, m_playerSpawners[i].transform.position.y, m_playerSpawners[i].transform.position.z);
            Debug.Log(i);
        }
    }

}
