using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sombrero : GalaxyBase
{
    /// <summary>
    /// Approximate physical size of the galaxy
    /// </summary>
    public int Size { get; set; }

    /// <summary>
    /// Approximate spacing between clusters
    /// </summary>
    //public int Spacing { get; set; }

    /// <summary>
    /// Minimum number of arms
    /// </summary>
    //public int MinimumArms { get; set; }

    /// <summary>
    /// Maximum number of arms
    /// </summary>
    //public int MaximumArms { get; set; }

    //public float ClusterCountDeviation { get; set; }
    //public float ClusterCenterDeviation { get; set; }

    //public float MinArmClusterScale { get; set; }
    //public float ArmClusterScaleDeviation { get; set; }
    //public float MaxArmClusterScale { get; set; }

    //public float Swirl { get; set; }

    //public float CenterClusterScale { get; set; }
    //public float CenterClusterDensityMean { get; set; }
    //public float CenterClusterDensityDeviation { get; set; }
    //public float CenterClusterSizeDeviation { get; set; }

    //public float CenterClusterPositionDeviation { get; set; }
    //public float CenterClusterCountDeviation { get; set; }
    //public float CenterClusterCountMean { get; set; }

    //public float CentralVoidSizeMean { get; set; }
    //public float CentralVoidSizeDeviation { get; set; }
    public float InnerNucleusDeviation { get; set; }

    public Sombrero(GalaxyRandom inRandom, int inNumberOfStars, Vector3 inGalaxyRadius, Texture2D inStarColour, float inNucleusRadiusDeviation = 0.25f, float inStarsInNucleus = 0.15f, float inInnerNucleusDeviation = 0.9f) : base(inRandom, inStarColour)
    {
        Size = (int)inGalaxyRadius.magnitude;
        //Spacing = 5;

        //MinimumArms = 3;
        //MaximumArms = 7;

        InnerNucleusDeviation = inInnerNucleusDeviation;

        //ClusterCountDeviation = 0.35f;
        //ClusterCenterDeviation = 0.2f;

        //MinArmClusterScale = 0.02f;
        //ArmClusterScaleDeviation = 0.02f;
        //MaxArmClusterScale = 0.1f;

        //Swirl = (float)Mathf.PI * 4;

        //CenterClusterScale = 0.19f;
        //CenterClusterDensityMean = 0.05f;
        //CenterClusterDensityDeviation = 0.015f;
        //CenterClusterSizeDeviation = 0.00125f;

        //CenterClusterCountMean = 25f;
        //CenterClusterCountDeviation = 7f;
        //CenterClusterPositionDeviation = 0.125f;

        //CentralVoidSizeMean = 25;
        //CentralVoidSizeDeviation = 7;

        Generate(inNumberOfStars, inGalaxyRadius, inNucleusRadiusDeviation, inStarsInNucleus, inGalaxyRadius.x, inGalaxyRadius.y, inGalaxyRadius.z);
    }

    override public void Generate(int inStarCount, Vector3 inGalaxyRadius, float inNucleusRadiusDeviation = 10f, float inStarsInNucleus = 1f,
            float deviationX = 0.025f, float deviationY = 0.025f, float deviationZ = 0.025f
        )
    {
        float galaxyRadius = GalaxyHelpers.GetMax(inGalaxyRadius);
        float nucleusRadius = galaxyRadius * 0.15f;
        int starsinNucleus = (int)(inStarCount * inStarsInNucleus);
        int starsInDisc = (int) ((inStarCount - starsinNucleus) * 0.4f);
        int starsInRing = inStarCount - starsinNucleus - starsInDisc;

        Vector3 center = Vector3.zero;

        foreach (var star in GenerateNucleus(starsinNucleus, nucleusRadius))
        {
            star.Offset(center);
            star.SetColor(star.ConvertTemperature());
            Stars.Add(star);
        }

        foreach (var star in GenerateDisc(starsInDisc, nucleusRadius, InnerNucleusDeviation, galaxyRadius, deviationX, 2, deviationZ))
        {
            star.Offset(center);
            star.SetColor(star.ConvertTemperature());
            Stars.Add(star);
        }

        foreach (var star in GenerateBand(starsInRing, galaxyRadius, 0.9f, galaxyRadius, deviationX, deviationY, deviationZ))
        {
            star.Offset(center);
            star.SetColor(star.ConvertTemperature());
            Stars.Add(star);
        }
    }
}