using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cluster :GalaxyBase
{
    #region  Cluster Variables

    public float GalaxyRadius = 100;

    public int CountMin = 3;
    public int CountMax = 7;
    public int ClusterCount = 0;

    public float RadiusMin = 0.25f;
    public float RadiusMax = 0.5f;

    public float DensityMean = 0.0000025f;
    public float DensityDeviation = 0.000001f;
    public float DeviationX = 0.0000025f;
    public float DeviationY = 0.0000025f;
    public float DeviationZ = 0.0000025f;

    public float StarsInNucleus = 1.0f;

    #endregion

    public Cluster(

            GalaxyRandom inGalaxyRand,

            float inGalaxyRadius = 100,

            int inCountMin = 3,
            int inCountMax = 7,

            float inRadiusMin = 0.25f,
            float inRadiusMax = 0.5f,

            float inDensityMean = 0.0000025f,
            float inDensityDeviation = 0.000001f,
            float inDeviationX = 0.0000025f,
            float inDeviationY = 0.0000025f,
            float inDeviationZ = 0.0000025f,

            float inStarsInNucleus = 1.0f
        )
         : base(inGalaxyRand)
    {
        GalaxyRadius = inGalaxyRadius;

        CountMin = inCountMin;
        CountMax = inCountMax;

        RadiusMin = inRadiusMin;
        RadiusMax = inRadiusMax;

        DensityMean = inDensityMean;
        DensityDeviation = inDensityDeviation;
        DeviationX = inDeviationX;
        DeviationY = inDeviationY;
        DeviationZ = inDeviationZ;

        StarsInNucleus = inStarsInNucleus;
    }

    override public List<Star> Generate()
    {
        Color[] clusterColor = { Color.red, Color.blue, Color.cyan, Color.gray, Color.green, Color.magenta, Color.white, Color.yellow };
        Vector3[] offsets = { new Vector3 ( 75, 75, 75 ), new Vector3(-75, 75, 75) , new Vector3(75, -75, -75), new Vector3(-75, -75, -75) };
        List<Star> result = new List<Star>();

        ClusterCount = GalaxyRand.Range(CountMin, CountMax);
 
        Vector3 centre = Vector3.zero;

        for (int i=0; i< ClusterCount; i++)
        {
            float clusterRadius = (GalaxyRadius * GalaxyRand.Range(RadiusMin, RadiusMax));

            float centreScale = GalaxyRand.Range(0, GalaxyRadius- clusterRadius);
            centre.x *= centreScale;
            centre.y *= centreScale;
            centre.z *= centreScale;

            Color starColor = clusterColor[(i % 8)];

            foreach (Star star in GenerateSphere(clusterRadius, DensityMean, DensityDeviation, DeviationX, DeviationY, DeviationZ, StarsInNucleus))
            {
                //star.Offset(offsets[(i % 4)]);
                star.Offset(centre);
                star.SetColor(star.ConvertTemperature());
                //star.SetColor(starColor);
                result.Add(star);
            }

            centre = GalaxyRand.InsideUnitSphere(true);
        }

        return result;
    }


}
