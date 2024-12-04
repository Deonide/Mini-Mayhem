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

    public int m_index = 0;

    private PlayerInputManager m_playerInputManager;

    void Start()
    {
        m_playerInputManager = GetComponent<PlayerInputManager>();
        m_playerInputManager.playerPrefab = m_playerCharacters[m_index];
    }

    public void SwitchPrefab()
    {
        //Elke keer dat de er een speler inspawned telt de index 1tje op en word de prefab die de input manager moet spawnen verandert.
        //De max aantal spelers word bepaald in de inputmanager
/*        if (m_playerInputManager.maxPlayerCount == 2)
        {
            if (m_index != 1)
            {
                GameManager.Instance.CountAmountOfPlayers();
                m_index += 1;
                m_playerInputManager.playerPrefab = m_playerCharacters[m_index];
            }
        }*/
        if (m_playerInputManager.maxPlayerCount == 4)
        {
            if (m_index != 3)
            {
               //GameManager.Instance.CountAmountOfPlayers();
                m_index += 1;
                m_playerInputManager.playerPrefab = m_playerCharacters[m_index];
            }
        }
    }

    public void AssignSpawner()
    {
        //Hetzelfde gebeurt maar dan voor de spawnlocatie.
        for (int i = 0; i < 4; i++)
        {
            m_playerCharacters[i].transform.position = new Vector3(m_playerSpawners[i].transform.position.x, m_playerSpawners[i].transform.position.y, m_playerSpawners[i].transform.position.z);
        }
    }
}
