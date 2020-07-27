using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

#region  Galaxy definition  
public class Galaxy 
{
    protected GalaxyGeneration Parent { get; set; }

    public List<Star> Stars { get; private set; }

    public Vector3 Position;

    public float Radius;
    public Vector3 Dimension;
    public Vector3 Extents;

    public int NumberOfStars { get; set; }
    public float GalaxyRadius { get; set; }
    public int ClusterCount { 
        get 
        {
            int clusterCount = 0;
            if(cluster!= null)
            {
                clusterCount = cluster.ClusterCount;
            }
            return clusterCount;
        } 
    }

    private GalaxyRandom mGalaxyRand { get; set; }
    public GalaxyRandom GalaxyRand { get { return mGalaxyRand; } }
    private System.Random GalaxySystemRand { get { return GalaxyRand.sm_Rand; } }

    public Cluster cluster;

    public Galaxy(GalaxyGeneration inParent, GalaxyRandom inGalaxyRand)
    {
        Parent = inParent;
        Stars = new List<Star>();
        mGalaxyRand = inGalaxyRand;
        GalaxyRadius = inParent.GalaxyRadius;

        cluster = new Cluster(
                GalaxyRand,

                100,

                Parent.Cluster_CountMin,
                Parent.Cluster_CountMax,

                Parent.Cluster_RadiusMin,
                Parent.Cluster_RadiusMax,

                Parent.Cluster_DensityMean,
                Parent.Cluster_DensityDeviation,
                Parent.Cluster_DeviationX,
                Parent.Cluster_DeviationY,
                Parent.Cluster_DeviationZ,

                Parent.Cluster_StarsInNucleus
            );
    }

    public void Generate()
    {
        Vector3 center = Vector3.zero;

        foreach (Star star in cluster.Generate())
        {
            star.Offset(center);
            //star.SetColor(star.ConvertTemperature(star.CalculateTemperature(GalaxyRadius*2)));
            Stars.Add(star);
        }
    }

    public float GetExtents()
    {
        float maxDistance = 0.0f;

        foreach (Star star in Stars)
        {
            float distance = Mathf.Abs(Vector3.Distance(Position, star.Position));
            if (distance > maxDistance)
            {
                maxDistance = distance;
            }
        }

        return maxDistance;
    }

    public Vector3 GetAxisExtents()
    {
        Vector3 maxExtents = Vector3.zero;

        foreach (Star star in Stars)
        {
            float x = 0;
            float y = 0;
            float z = 0;

            if ((x = Mathf.Abs(Position.x - star.Position.x)) > maxExtents.x)
            {
                maxExtents.x = x;
            }

            if ((y = Mathf.Abs(Position.y - star.Position.y)) > maxExtents.y)
            {
                maxExtents.y = y;
            }

            if ((z = Mathf.Abs(Position.z - star.Position.z)) > maxExtents.z)
            {
                maxExtents.z = z;
            }
        }
        return maxExtents * 2;
    }
    public void ScaleGalaxy(float inScale)
    {
        Vector3 dimensions = Vector3.zero;

        foreach (Star star in Stars)
        {
            Vector3 delta = (star.Position - Position);
            float distance = delta.magnitude;
            star.Position = ((delta.normalized) * (distance * inScale));

            if (dimensions.x < Mathf.Abs(delta.x))
            {
                dimensions.x = Mathf.Abs(delta.x);
            }

            if (dimensions.y < Mathf.Abs(delta.y))
            {
                dimensions.y = Mathf.Abs(delta.y);
            }

            if (dimensions.z < Mathf.Abs(delta.z))
            {
                dimensions.z = Mathf.Abs(delta.z);
            }
        }

        Dimension = dimensions;
    }

    public void GenrateStarColor()
    {
        GenrateStarColor(GetExtents());
    }

    public void GenrateStarColor(float inMaxDistance)
    {
        foreach (Star star in Stars)
        {
            float distance = Vector3.Distance(Position, star.Position);

            //star.mTemperature = CalculateTemperature(distance, inMaxDistance);
            star.SetColor(star.ConvertTemperature());
        }
    }


}
#endregion