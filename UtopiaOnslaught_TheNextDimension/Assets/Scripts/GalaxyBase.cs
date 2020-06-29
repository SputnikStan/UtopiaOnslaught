using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class GalaxyBase
{
    public List<Star> Stars { get; private set; }
    public Texture2D StarColour;
    public GalaxyRandom GalaxyRand { get; private set; }
    public System.Random GalaxySystemRand { get { return GalaxyRand.sm_Rand; } }

    public GalaxyBase(GalaxyRandom inRandom, Texture2D inStarColour)
    {
        Stars = new List<Star>();
        GalaxyRand = inRandom;
        StarColour = inStarColour;
    }

    public abstract void Generate(int inStarCount, Vector3 inGalaxyRadius, float countMean = 0.0000025f, float countDeviation = 0.000001f,
            float deviationX = 0.0000025f, float deviationY = 0.0000025f, float deviationZ = 0.0000025f);

    public List<Star> GenerateNucleus(int inStarCount, float size, float densityMean = 0.25f, float densityDeviation = 0.1f, float deviationX = 0.25f, float deviationY = 0.25f, float deviationZ = 0.25f)
    {
        List<Star> result = new List<Star>();

        for (int i = 0; i < inStarCount; i++)
        {
            Vector3 starPos = GalaxyRand.InsideUnitSphere(true);
            starPos.x *= size;
            starPos.y *= size;
            starPos.z *= size;

            var d = starPos.magnitude / size;
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

    private double Pow3Constrained(double _X)
    {
        double value = System.Math.Pow(_X - 0.5, 3) * 4 + 0.5d;
        return System.Math.Max(System.Math.Min(1, value), 0);
    }

}
