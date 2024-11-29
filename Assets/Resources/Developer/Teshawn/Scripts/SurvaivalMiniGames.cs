using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniGames.Survival;
using MiniGames.SinkingPlatforms;
using System.Linq;
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
    public GameObject m_objectsToDrop;

    [SerializeField]
   // private List<SinkingPlatform> m_floorTiles = new List<SinkingPlatform>();
    private void Start()
    {
        m_cubeSpawnHight = this.transform.position.y;
        StartCoroutine(SpawningTheCubes());

       // m_floorTiles = FindObjectsOfType<SinkingPlatform>().ToList();
        //StartCoroutine(SinkingPlatFormsMiniGame());
    }

    IEnumerator SpawningTheCubes()
    {
        while (true)
        {
           FallingCubes.SpawnGrid(m_amountOfObjects, m_arenaRadius, m_cubeSpawnHight, m_objectSize, m_objectsToDrop);
            yield return new WaitForSeconds(m_spawnDelay);
        }
    }

    //IEnumerator SinkingPlatFormsMiniGame()
    //{
    //    while (true)
    //    {
    //        int randomPlatform = Random.Range(0, m_floorTiles.Count);
    //        m_floorTiles[randomPlatform].G_isFalling = true;
    //        yield return new WaitForSeconds(m_spawnDelay);

    //    }
    //}
}
