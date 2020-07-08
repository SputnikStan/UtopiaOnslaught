using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class GalaxyBase
{
    public List<Star> Stars { get; private set; }

    public int NumberOfStars { get; set; }
    public Vector3 GalaxyRadius { get; set; }

    //public Texture2D StarColour;
    public GalaxyRandom GalaxyRand { get; private set; }
    public System.Random GalaxySystemRand { get { return GalaxyRand.sm_Rand; } }

    public GalaxyBase(GalaxyRandom inRandom, int inNumberOfStars, Vector3 inGalaxyRadius)
    {
        Stars = new List<Star>();
        GalaxyRand = inRandom;
        NumberOfStars = inNumberOfStars;
        GalaxyRadius = inGalaxyRadius;
        //StarColour = inStarColour;
    }

    public abstract void Generate();

    public List<Star> GenerateSphere(float _size,
            float _densityMean = 0.0000025f, float _densityDeviation = 0.000001f,
            float _deviationX = 0.0000025f,
            float _deviationY = 0.0000025f,
            float _deviationZ = 0.0000025f)
    {
        List<Star> result = new List<Star>();

        var density = Mathf.Max(0, GalaxySystemRand.NormallyDistributedSingle(_densityDeviation, _densityMean));
        var countMax = Mathf.Max(0, (int)(_size * _size * _size * density));

        var count = GalaxySystemRand.Next(countMax);

        for (int i = 0; i < count; i++)
        {
            var pos = new Vector3(
                GalaxySystemRand.NormallyDistributedSingle(_deviationX * _size, 0),
                GalaxySystemRand.NormallyDistributedSingle(_deviationY * _size, 0),
                GalaxySystemRand.NormallyDistributedSingle(_deviationZ * _size, 0)
            );
            var d = pos.magnitude / _size;
            var m = d * 2000 + (1 - d) * 15000;
            var t = GalaxySystemRand.NormallyDistributedSingle(4000, m, 1000, 40000);

            Color starColor = Color.magenta;
            Star star = new Star(GenerateStarName.Generate(GalaxySystemRand), pos, starColor, t);

            result.Add(star);
        }

        return result;
    }

    public List<Star> GenerateNucleus(int inStarCount, Vector3 inRadius)
    {
        List<Star> result = new List<Star>();

        for (int i = 0; i < inStarCount; i++)
        {
            Vector3 starPos = GalaxyRand.InsideUnitSphere(false);
            starPos.x *= inRadius.x;
            starPos.y *= inRadius.y;
            starPos.z *= inRadius.z;

            var d = starPos.magnitude / inRadius.magnitude;
            var m = d * 2000 + (1 - d) * 15000;
            var t = GalaxySystemRand.NormallyDistributedSingle(4000, m, 1000, 40000);

            Color starColor = Color.magenta;
            Star star = new Star(GenerateStarName.Generate(GalaxySystemRand), starPos, starColor, t);

            result.Add(star);
        }

        return result;
    }

    public List<Star> GenerateDisc(int inStarCount, float nucleusRadius, float InnerNucleusDeviation = 0.25f, float galaxyRadius = 0.1f, float deviationX = 0.25f, float deviationY = 0.25f, float deviationZ = 0.25f)
    {
        List<Star> result = new List<Star>();

        int starsInDisc = inStarCount;

        while (starsInDisc >= 0)
        {
            Vector3 starPos = GalaxyRand.InsideUnitSphere(true);

            starPos.x *= deviationX;
            starPos.y *= deviationY;
            starPos.z *= deviationZ;

            float distance = starPos.magnitude;
            if (distance > (nucleusRadius * InnerNucleusDeviation))
            {
                var d = starPos.magnitude / galaxyRadius;
                var m = d * 2000 + (1 - d) * 15000;
                var t = GalaxySystemRand.NormallyDistributedSingle(4000, m, 1000, 40000);

                Color starColor = Color.magenta;
                Star star = new Star(GenerateStarName.Generate(GalaxySystemRand), starPos, starColor, t);

                star.SetColor(star.ConvertTemperature());
                result.Add(star);

                starsInDisc--;
            }
        }

        return result;
    }

    public List<Star> GenerateBand(int inStarCount, float nucleusRadius, float InnerNucleusDeviation = 0.25f, float galaxyRadius = 0.1f, float deviationX = 0.25f, float deviationY = 0.25f, float deviationZ = 0.25f)
    {
        List<Star> result = new List<Star>();

        int starsInDisc = inStarCount;
        float minRadius = (nucleusRadius * InnerNucleusDeviation);

        while (starsInDisc >= 0)
        {
            Vector3 starPosUnit = GalaxyRand.InsideUnitSphere(true);

            float distanceX = GalaxyRand.Range((nucleusRadius* InnerNucleusDeviation), (galaxyRadius * (deviationX / galaxyRadius)));
            float distanceZ = GalaxyRand.Range((nucleusRadius * InnerNucleusDeviation), (galaxyRadius * (deviationZ / galaxyRadius)));

            float angle = GalaxyRand.Next() * Mathf.PI * 2.0f;
            float x = Mathf.Cos(angle) * distanceX;
            float y = starPosUnit.y * deviationY;
            float z = Mathf.Sin(angle) * distanceZ;

            Vector3 starPos = new Vector3(x, y, z);

            float distance = starPos.magnitude;
            //if (distance > (nucleusRadius * InnerNucleusDeviation))
            {
                var d = starPos.magnitude / galaxyRadius;
                var m = d * 2000 + (1 - d) * 15000;
                var t = GalaxySystemRand.NormallyDistributedSingle(4000, m, 1000, 40000);

                Color starColor = Color.magenta;
                Star star = new Star(GenerateStarName.Generate(GalaxySystemRand), starPos, starColor, t);

                star.SetColor(star.ConvertTemperature());
                result.Add(star);

                starsInDisc--;
            }
        }

        return result;
    }

    protected double Pow3Constrained(double _X)
    {
        double value = System.Math.Pow(_X - 0.5, 3) * 4 + 0.5d;
        return System.Math.Max(System.Math.Min(1, value), 0);
    }

    public static float GetMax(Vector3 v3)
    {
        return Mathf.Max(Mathf.Max(v3.x, v3.y), v3.z);
    }
}
