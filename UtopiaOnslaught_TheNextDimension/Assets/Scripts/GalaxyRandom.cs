using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalaxyRandom
{
    private static System.Random sm_Rand;

    public GalaxyRandom(int _Seed)
    {
        sm_Rand = new System.Random(_Seed);
    }

    public Vector3 InsideUnitSphere()
    {
        float u1 = Range(-1.0f, 1.0f);
        float u2 = Range(0.0f, 1.0f);
        float r = (float)System.Math.Sqrt(1.0f - u1 * u1);
        float theta = (float)(2.0f * System.Math.PI * u2);

        return new Vector3((float)(r * System.Math.Cos(theta)), (float)(r * System.Math.Sin(theta)), u1);
    }

    float Range(float a, float b)
    {
        return (float)(a + sm_Rand.NextDouble() * (b - a));
    }

    double Range(double a, double b)
    {
        return a + sm_Rand.NextDouble() * (b - a);
    }

    public double NextDouble()
    {
        return sm_Rand.NextDouble();
    }
}
