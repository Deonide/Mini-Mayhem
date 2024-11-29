using UnityEngine;


namespace MiniGames
{
    namespace Survival
    {
        public static class FallingCubes
        {
            public static void SpawnGrid(int numberOfCubes, float circleRadius,float yPos, float objectSize,GameObject objectToSPawn)
            {

                for (int i = 0; i < numberOfCubes; i++)
                {
                    float x = Random.Range(-circleRadius,circleRadius);
                    float z = Random.Range(-circleRadius,circleRadius);
                    Vector3 pos = new Vector3(x, yPos, z);
                    if (new Vector3(x, yPos, z).magnitude >= circleRadius) continue;

                    objectToSPawn = GameObject.Instantiate(objectToSPawn, pos *(objectSize * 2),Quaternion.identity);
                }
            }
        }
    }


    namespace SinkingPlatforms
    {
        public static class SinkingPlatforms
        {
            public static void Sinking(GameObject platformSinking,Vector3 originalSpawnPoint)
            {
                if (platformSinking.transform.position.y < -5f)
                {
                    platformSinking.transform.position = originalSpawnPoint;
                }
            }
        }
    }
}