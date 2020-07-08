using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disc : GalaxyBase
{
    public float StarsInNucleus { get; set; }
    public float NucleusRadius { get; set; }
    public float NucleusDeviation { get; set; }

    public Disc(GalaxyRandom inRandom, int inNumberOfStars, Vector3 inGalaxyRadius, Texture2D inStarColour, float inNucleusRadiusDeviation = 0.25f, float inStarsInNucleus = 0.5f, float inInnerNucleusDeviation = 0.9f)
        : base(inRandom, inNumberOfStars, inGalaxyRadius)
    {

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

        foreach (var star in GenerateDisc(starsInDisc, NucleusRadius, NucleusDeviation, galaxyRadius, GalaxyRadius.x, GalaxyRadius.y, GalaxyRadius.z))
        {
            star.Offset(center);
            star.SetColor(star.ConvertTemperature());
            Stars.Add(star);
        }
    }
}