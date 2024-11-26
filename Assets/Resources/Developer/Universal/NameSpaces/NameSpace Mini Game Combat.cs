using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MiniGames
{
    namespace Combat
    {
        public static class SpawnBomb
        {
            public static void SpawningBombs(GameObject bomb, Vector3 bombSpawnPoint)
            {
                //Spawned de bomb in.
                GameObject.Instantiate(bomb, bombSpawnPoint, Quaternion.identity);
            }
        }

        public static class BumperDucks
        {
            public static void BumpingDucks(float speed, float bounce)
            {
                //Als de spelers tegen elkaar aan bouncen dan bouncen ze van elkaar af.
/*              gameObject.*/
            }
        }
    }
}



public class NameSpaceMiniGameCombat : MonoBehaviour
{
}
