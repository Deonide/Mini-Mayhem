using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniGames.Survival;
using MiniGames.SinkingPlatforms;
public class SurvaivalMiniGames : MonoBehaviour
{
    [SerializeField]
    private float m_spawnDelay = 2f;
    [SerializeField]
    private int m_amountOfObjects = 10;
    [SerializeField]
    private float m_arenaRadius = 10f;
    [SerializeField]
    private float m_objectSize = 1;
    [SerializeField]
    private float m_cubeSpawnHight;
    public GameObject m_objectsToDrop, m_floorTiles;

    private void Start()
    {
        m_cubeSpawnHight = this.transform.position.y;
        Instantiate(m_floorTiles);
        StartCoroutine(SpawningTheCubes());
    }

    IEnumerator SpawningTheCubes()
    {
        while (true)
        {
            FallingCubes.SpawnGrid(m_amountOfObjects, m_arenaRadius, m_cubeSpawnHight, m_objectSize, m_objectsToDrop);
            yield return new WaitForSeconds(m_spawnDelay);
        }
    }


    
}
