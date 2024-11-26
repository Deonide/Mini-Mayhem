using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MiniGames
{
    namespace Survival
    {
        public class FallingCubes
        {
            public static void SpawnGrid(int numberOfCubes, float circleRadius, float magAmp,float yPos, GameObject objectToSPawn)
            {

                for (int i = 0; i < numberOfCubes; i++)
                {
                    float x = UnityEngine.Random.Range(-circleRadius,circleRadius);
                    float z = UnityEngine.Random.Range(-circleRadius, circleRadius);
                    Vector3 pos = new Vector3(x, yPos, z);
                    if (new Vector3(x, yPos, z).magnitude >= circleRadius) continue;

                    GameObject.Instantiate(objectToSPawn, pos,Quaternion.identity);
                }
            }
        }

    }
}