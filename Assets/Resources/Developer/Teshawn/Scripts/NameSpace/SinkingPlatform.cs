using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniGames;
using MiniGames.SinkingPlatforms;

public class SinkingPlatform : MonoBehaviour
{
    private Vector3 m_originalSPawnPoint;

    private GameObject Sinking;

    private void Start()
    {
        Sinking = this.gameObject;
        m_originalSPawnPoint = this.transform.position;

    }
    private void Update()
    {
        SinkingPlatforms.Sinking(Sinking, m_originalSPawnPoint);
    }
}
