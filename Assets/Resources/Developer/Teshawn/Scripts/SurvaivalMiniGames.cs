using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniGames.Survival;
public class SurvaivalMiniGames : MonoBehaviour
{
    private float m_spawnDelay = 2f;
    private int m_amountOfObjects = 1;
    [SerializeField]
    private float m_arenaRadius = 10f;
    private float m_arenaamp = 1.02f;

    private float m_cubeSpawnHight;

    public GameObject m_objectsToDrop;

    private void Start()
    {
        m_cubeSpawnHight = this.transform.position.y;


        StartCoroutine(SpawningTheCubes());
    }

    IEnumerator SpawningTheCubes()
    {
        while (true)
        {
            FallingCubes.SpawnGrid(m_amountOfObjects, m_arenaRadius,m_arenaamp, m_cubeSpawnHight, m_objectsToDrop);
            yield return new WaitForSeconds(m_spawnDelay);
        }
    }
}
