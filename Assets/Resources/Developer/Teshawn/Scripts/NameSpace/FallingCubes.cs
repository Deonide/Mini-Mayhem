using System.Collections.Generic;
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
            public static void Sinking(GameObject platformSinking,Vector3 originalSpawnPoint,bool isSkinking, float canBeDroppedCoolDown, float maxCooldown)
            {
                if (platformSinking.transform.position.y < -5f && isSkinking)
                {
                    platformSinking.transform.position = originalSpawnPoint;
                    isSkinking = false;
                    canBeDroppedCoolDown -= Time.deltaTime;
                    
                }
                if(canBeDroppedCoolDown <= 0)
                {
                    canBeDroppedCoolDown = maxCooldown;
                    isSkinking = true;
                }
            }
        }
    }
}