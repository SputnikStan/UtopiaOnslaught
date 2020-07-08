using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sombrero : GalaxyBase
{
    public float StarsInNucleus { get; set; }
    public float NucleusRadius { get; set; }
    public float NucleusDeviation { get; set; }

    public Sombrero(GalaxyRandom inRandom, int inNumberOfStars, Vector3 inGalaxyRadius, Texture2D inStarColour, float inNucleusRadiusDeviation = 0.25f, float inStarsInNucleus = 0.15f, float inInnerNucleusDeviation = 0.9f)
        : base(inRandom, inNumberOfStars, inGalaxyRadius)
    {

        NucleusDeviation = inInnerNucleusDeviation;


        Generate();
    }

    override public void Generate( )
    {
        float galaxyRadius = GalaxyHelpers.GetMax(GalaxyRadius);
        float nucleusRadius = galaxyRadius * 0.15f;
        int starsinNucleus = (int)(NumberOfStars * StarsInNucleus);
        int starsInDisc = (int) ((NumberOfStars - starsinNucleus) * 0.4f);
        int starsInRing = NumberOfStars - starsinNucleus - starsInDisc;

        Vector3 center = Vector3.zero;

        foreach (var star in GenerateNucleus(starsinNucleus, new Vector3(nucleusRadius, nucleusRadius, nucleusRadius)))
        {
            star.Offset(center);
            star.SetColor(star.ConvertTemperature());
            Stars.Add(star);
        }

        //foreach (var star in GenerateDisc(starsInDisc, nucleusRadius, InnerNucleusDeviation, galaxyRadius, deviationX, 2, deviationZ))
        //{
        //    star.Offset(center);
        //    star.SetColor(star.ConvertTemperature());
        //    Stars.Add(star);
        //}
        
        //foreach (var star in GenerateBand(starsInRing, galaxyRadius, 0.9f, galaxyRadius, deviationX, deviationY, deviationZ))
        //{
        //    star.Offset(center);
        //    star.SetColor(star.ConvertTemperature());
        //    Stars.Add(star);
        //}
    }
}