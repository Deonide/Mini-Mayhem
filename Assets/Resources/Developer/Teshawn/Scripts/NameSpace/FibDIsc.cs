using System.Numerics;
using UnityEngine;

namespace MiniGames
{
    namespace FallingObjects
    {
        public class FibDIsc 
        {

           
            public enum distType
            {
                FibonacciDisc
            }

            public distType m_disType;
            public int n = 8;
            public float radius = 8;

            private System.Func<int, UnityEngine.Vector3>[] distFuncs => new System.Func<int, UnityEngine.Vector3>[]
            {
                
            };

            public UnityEngine.Vector3 FbiDisc(int i) 
            { 
                var k = i + .5f;
                var r = Mathf.Sqrt((k)/n);
                var theta = Mathf.PI * (1 + Mathf.Sqrt(5)) * k;

                var x = r * Mathf.Cos(theta) * radius;
                var y = r * Mathf.Sin(theta) * radius;

                return new UnityEngine.Vector3(x, 0, y);
            }

           

        }
    }

}

