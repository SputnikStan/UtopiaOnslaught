using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cluster : GalaxyBase
{
    /// <summary>
    /// Approximate physical size of the galaxy
    /// </summary>
    public int Size { get; set; }

    /// <summary>
    /// Approximate spacing between clusters
    /// </summary>
    public int Spacing { get; set; }

    /// <summary>
    /// Minimum number of arms
    /// </summary>
    public int MinimumArms { get; set; }

    /// <summary>
    /// Maximum number of arms
    /// </summary>
    public int MaximumArms { get; set; }

    public int ClusterMax { get; set; }
    public int ClusterMin { get; set; }

    public float ClusterCountDeviation { get; set; }
    public float ClusterCenterDeviation { get; set; }

    public float MinArmClusterScale { get; set; }
    public float ArmClusterScaleDeviation { get; set; }
    public float MaxArmClusterScale { get; set; }

    public float Swirl { get; set; }

    public float CenterClusterScale { get; set; }
    public float CenterClusterDensityMean { get; set; }
    public float CenterClusterDensityDeviation { get; set; }
    public float CenterClusterSizeDeviation { get; set; }

    public float CenterClusterPositionDeviation { get; set; }
    public float CenterClusterCountDeviation { get; set; }
    public float CenterClusterCountMean { get; set; }

    public float CentralVoidSizeMean { get; set; }
    public float CentralVoidSizeDeviation { get; set; }

    public Cluster( GalaxyRandom inRandom, int inNumberOfStars, Vector3 inGalaxyRadius, Texture2D inStarColour, int inClusterMin = 5, int inClusterMax = 10)
     : base(inRandom, inNumberOfStars, inGalaxyRadius)
    {

        Size = (int)GalaxyHelpers.GetMax(inGalaxyRadius); 
        Spacing = 5;

        MinimumArms = 3;
        MaximumArms = 7;

        ClusterCountDeviation = 0.35f;
        ClusterCenterDeviation = 0.2f;

        MinArmClusterScale = 0.02f;
        ArmClusterScaleDeviation = 0.02f;
        MaxArmClusterScale = 0.1f;

        Swirl = (float)Mathf.PI * 4;

        CenterClusterScale = 0.19f;
        CenterClusterDensityMean = 0.1f;
        CenterClusterDensityDeviation = 0.015f;
        CenterClusterSizeDeviation = 0.00125f;

        CenterClusterCountMean = 25f;
        CenterClusterCountDeviation = 7f;
        CenterClusterPositionDeviation = 0.75f;

        CentralVoidSizeMean = 25;
        CentralVoidSizeDeviation = 7;

        Generate();
    }

    override public void Generate()
    {
        int totalStars = NumberOfStars;

        int starsInCluster = (int)(NumberOfStars / GalaxyRand.Range(ClusterMin, ClusterMax));
        float galaxyRadius = GalaxyHelpers.GetMax(GalaxyRadius);

        while (totalStars > 0)
        {
            Vector3 center = GalaxyRand.InsideUnitSphere(true);
            float clusterRadius = GalaxyRand.Range(galaxyRadius / 2, galaxyRadius) * CenterClusterPositionDeviation;
            float radius = (galaxyRadius - clusterRadius);
            center.x *= radius;
            center.y *= radius;
            center.z *= radius;

            foreach (var star in GenerateNucleus(starsInCluster, new Vector3(clusterRadius,clusterRadius, clusterRadius) ))
            {
                star.Offset(center);
                star.SetColor(star.ConvertTemperature());
                Stars.Add(star);
                totalStars--;
            }
        }
    }
}
