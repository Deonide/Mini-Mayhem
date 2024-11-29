using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public int m_chooseMiniGame;
    public Scene m_scene;
    public int m_index;
    private PlayerMovement m_playerMovement;

    private void Start()
    {

    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene Loaded" + scene.name);
        m_chooseMiniGame = 1;/*Random.Range(0, 2);*/
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
}




