using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cluster : GalaxyBase
{
    public int ClusterMax { get; set; }
    public int ClusterMin { get; set; }


    public float ClusterRadiusMax { get; set; }
    public float ClusterRadiusMin { get; set; }

    public float ClusterFlatness { get; set; }

    public Cluster( GalaxyRandom inRandom, int inNumberOfStars, Vector3 inGalaxyRadius, float inClusterRadiusMin, float inClusterRadiusMax, int inClusterMin = 5, int inClusterMax = 10, float inFlatness = 1.0f)
     : base(inRandom, inNumberOfStars, inGalaxyRadius)
    {
        ClusterMin = inClusterMin;
        ClusterMax = inClusterMax;
        ClusterRadiusMin = inGalaxyRadius.magnitude * inClusterRadiusMin;
        ClusterRadiusMax = inGalaxyRadius.magnitude * inClusterRadiusMax;

        ClusterFlatness = inFlatness;

        Generate();
    }

    override public void Generate()
    {
        int totalStars = NumberOfStars;

        int numberOfClusters = GalaxyRand.Range(ClusterMin, ClusterMax);
 
        int starsInCluster = (int)(NumberOfStars / numberOfClusters);
        float galaxyRadius = GalaxyRadius.magnitude; // GalaxyHelpers.GetMax(GalaxyRadius);

        Vector3 center = Vector3.zero;

        for (int i=0; i< numberOfClusters; i++)
        {
            float clusterRadius = GalaxyRand.Range(ClusterRadiusMin, ClusterRadiusMax);

            float centreScale = GalaxyRand.Range(0, (galaxyRadius - clusterRadius));
            center.x *= centreScale;
            center.y *= centreScale;
            center.z *= centreScale;

            foreach (Star star in GenerateNucleus(starsInCluster, new Vector3(clusterRadius,clusterRadius, clusterRadius), false, ClusterFlatness))
            {
                star.Offset(center);
                star.SetColor(star.ConvertTemperature());
                Stars.Add(star);
                totalStars--;
            }

            center = GalaxyRand.InsideUnitSphere(true);
        }
    }
}
