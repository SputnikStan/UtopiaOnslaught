﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disc : GalaxyBase
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

    public Disc(GalaxyRandom inRandom, int inNumberOfStars, Vector3 inGalaxyRadius, Texture2D inStarColour) : base(inRandom, inStarColour)
    {

        Size = (int)inGalaxyRadius.magnitude;
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
        CenterClusterDensityMean = 0.05f;
        CenterClusterDensityDeviation = 0.015f;
        CenterClusterSizeDeviation = 0.00125f;

        CenterClusterCountMean = 25f;
        CenterClusterCountDeviation = 7f;
        CenterClusterPositionDeviation = 0.125f;

        CentralVoidSizeMean = 25;
        CentralVoidSizeDeviation = 7;

        Generate(inNumberOfStars, inGalaxyRadius, CenterClusterCountMean, CenterClusterCountDeviation, inGalaxyRadius.x * CenterClusterPositionDeviation, inGalaxyRadius.y * CenterClusterPositionDeviation, inGalaxyRadius.z * CenterClusterPositionDeviation);
    }

    override public void Generate(int inStarCount, Vector3 inGalaxyRadius, float countMean = 10f, float countDeviation = 1f,
            float deviationX = 0.025f, float deviationY = 0.025f, float deviationZ = 0.025f
        )
    {
        var centralVoidSize = GalaxySystemRand.NormallyDistributedSingle(CentralVoidSizeDeviation, CentralVoidSizeMean);
        if (centralVoidSize < 0)
            centralVoidSize = 0;
        var centralVoidSizeSqr = centralVoidSize * centralVoidSize;


        int totalStars = inStarCount;


        Vector3 center = new Vector3(0, 0, 0);

        foreach (var star in GenerateNucleus(totalStars, Size, CenterClusterDensityMean, CenterClusterDensityDeviation, CenterClusterPositionDeviation, CenterClusterPositionDeviation, CenterClusterPositionDeviation))
        {
            star.Offset(center);
            star.SetColor(StarColour, Size);
            Stars.Add(star);
            totalStars--;
        }
    }
}