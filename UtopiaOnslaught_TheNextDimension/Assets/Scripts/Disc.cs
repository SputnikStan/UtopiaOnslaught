using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disc : GalaxyBase
{
    public int NumberOfArms { get; set; }
    public float StarsInArms { get; set; }
    public float StarsInNucleus { get; set; }
    public float NucleusRadius { get; set; }
    public float NucleusDeviation { get; set; }
    public float ArmRadius { get; set; }
    public float ArmRadiusDeviation { get; set; }
    public float ArmSpread { get; set; }
    public float Flatness { get; set; }

    public Disc(GalaxyRandom inRandom, int inNumberOfStars, Vector3 inGalaxyRadius, int inNumberOfArms, float inStarsInNucleus, float inStarsInArms,
                    float inNucleusRadius, float inNucleusDeviation = 0.25f,
                    float inArmRadius = 5f, float inArmRadiusDeviation = 0.9f, float inArmSpread = 0.5f,
                    float inFlatness = 0.25f)
        : base(inRandom, inNumberOfStars, inGalaxyRadius)
    {
        NumberOfArms = inNumberOfArms;
        StarsInNucleus = inStarsInNucleus;
        StarsInArms = inStarsInArms;
        NucleusRadius = inNucleusRadius;
        NucleusDeviation = inNucleusDeviation;
        ArmRadius = inArmRadius;
        ArmRadiusDeviation = inArmRadiusDeviation;
        ArmSpread = inArmSpread;
        Flatness = inFlatness;

        Generate();
    }

    override public void Generate( )
    {
        float galaxyRadius = GalaxyRadius.magnitude;
        int starsinNucleus = (int)(NumberOfStars * StarsInNucleus);
        int starsInDisc = NumberOfStars - starsinNucleus;

        Vector3 center = Vector3.zero;

        foreach (var star in GenerateNucleus(starsinNucleus, new Vector3(NucleusRadius, NucleusRadius, NucleusRadius)))
        {
            star.Offset(center);
            star.SetColor(star.ConvertTemperature());
            Stars.Add(star);
        }

        foreach (var star in GenerateDisc(starsInDisc, NucleusRadius, NucleusDeviation, galaxyRadius))
        {
            star.Offset(center);
            star.SetColor(star.ConvertTemperature());
            Stars.Add(star);
        }
    }
}