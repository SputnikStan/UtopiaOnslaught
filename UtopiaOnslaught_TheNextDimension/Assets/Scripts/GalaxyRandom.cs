using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalaxyRandom
{
    public System.Random sm_Rand;

    public GalaxyRandom(int _Seed)
    {
        sm_Rand = new System.Random(_Seed);
    }

    public Vector3 InsideUnitSphere(bool inUniformlyDistributed = false)
    {
        Vector3 pos = Vector3.zero;

        if(inUniformlyDistributed == false)
        {
            pos = (RandomNormal() * Random.Range(0.0f, 1.0f)); // None Uniformaly distributed
        }
        else
        {
            pos = (RandomNormal() * Mathf.Sqrt(Random.Range(0.0f, 1.0f))); // Uniformaly distributed

        }

        return pos;
    }

    public Vector3 InsideUnitElipse(float inWidth, float inHeight, bool inUniformlyDistributed = false)
    {
        Vector3 pos = Vector3.zero;

        if (inUniformlyDistributed == false)
        {
            pos = (RandomNormal() * Random.Range(0.0f, 1.0f)); // None Uniformaly distributed
        }
        else
        {
            pos = (RandomNormal() * Mathf.Sqrt(Random.Range(0.0f, 1.0f))); // Uniformaly distributed

        }

        return pos;
    }

    public Vector3 RandomNormal()
    {
        Vector3 normal = Vector3.zero;

        double u = NextDouble();
        double v = NextDouble();
        float theta = (float)(u * 2.0 * Mathf.PI);
        float phi = Mathf.Acos((float)(2.0 * v - 1.0));
        float r = Mathf.Pow((float)NextDouble(), 1f / 3f);
        float sinTheta = Mathf.Sin(theta);
        float cosTheta = Mathf.Cos(theta);
        float sinPhi = Mathf.Sin(phi);
        float cosPhi = Mathf.Cos(phi);

        normal.x = r * sinPhi * cosTheta;
        normal.y = r * sinPhi * sinTheta;
        normal.z = r * cosPhi;

        return normal;  
    }

    public float Range(float a, float b)
    {
        return (float)(a + sm_Rand.NextDouble() * (b - a));
    }

    public double Range(double a, double b)
    {
        return a + sm_Rand.NextDouble() * (b - a);
    }

    public double NextDouble()
    {
        return sm_Rand.NextDouble();
    }

    public float Next()
    {
        return (float)sm_Rand.NextDouble();
    }
}
