using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sombrero : GalaxyBase
{
    public int NumberOfRings { get; set; }
    public float StarsInRings { get; set; }
    public float StarsInNucleus { get; set; }
    public float NucleusRadius { get; set; }
    public float NucleusDeviation { get; set; }
    public float OuterRadius { get; set; }
    public float RingRadius { get; set; }
    public float RingSpread { get; set; }
    public float Flatness { get; set; }

    public Sombrero(GalaxyRandom inRandom, int inNumberOfStars, Vector3 inGalaxyRadius, float inStarsInNucleus, float inStarsInRings,
                    float inNucleusRadius, float inOuterRadius = 0.25f,
                    float inRingRadius = 5f, float inRingSpread = 0.5f,
                    float inFlatness = 0.25f)
        : base(inRandom, inNumberOfStars, inGalaxyRadius)
    {
        StarsInNucleus = inStarsInNucleus;
        StarsInRings = inStarsInRings;
        NucleusRadius = inNucleusRadius;
        OuterRadius = inGalaxyRadius.magnitude * inOuterRadius;
        RingRadius = inRingRadius;
        RingSpread = inRingSpread;
        Flatness = inFlatness;

        Generate();
    }

    override public void Generate( )
    {
        int starsInRing = (int)(NumberOfStars * StarsInRings);
        int starsinNucleus = (int)((NumberOfStars - starsInRing) * StarsInNucleus);
        int starsInDisc = (int)(NumberOfStars - starsInRing - starsinNucleus);

        Vector3 center = Vector3.zero;

        foreach (var star in GenerateRing(starsInRing, OuterRadius, RingRadius, GalaxyBase.GetMax(GalaxyRadius), NucleusRadius * NucleusDeviation, RingRadius, RingSpread))
        {
            star.Offset(center);
            //star.Swirl(Vector3.up, Swirl * 5);
            star.SetColor(star.ConvertTemperature());
            Stars.Add(star);
        }

        Vector3 disc = new Vector3(GalaxyRadius.x, NucleusRadius * Flatness, GalaxyRadius.z);

        foreach (var star in GenerateDisc(starsInDisc, NucleusRadius, NucleusDeviation, GalaxyRadius.magnitude))
        {
            star.Offset(center);
            star.SetColor(star.ConvertTemperature());
            Stars.Add(star);
        }

        foreach (var star in GenerateNucleus(starsinNucleus, new Vector3(NucleusRadius, NucleusRadius, NucleusRadius)))
        {
            star.Offset(center);
            star.SetColor(star.ConvertTemperature());
            Stars.Add(star);
        }

    }

    private List<Star> GenerateRing(int inStarCount, float inOuterRadius = 2.0f, float inRingRadius = 5f, float inMaxRadius = 45, float nucleusRadius = 0.25f, float inArmRadius = 5, float inArmSpread = 1.0f)
    {
        int totalStars = inStarCount;

        float num_theta = 100;

        List<Star> result = new List<Star>();

        List<Vector3> points = new List<Vector3>();

        float dtheta = (float)(2 * Mathf.PI / num_theta);    // Five degrees.
        float theta = 0;
        for (int i = 0; i < num_theta; i++)
        {
            // Convert to Cartesian coordinates.

            float x = (float)(inOuterRadius * Mathf.Cos(theta));
            float z = (float)(inOuterRadius * Mathf.Sin(theta));

            Vector3 pos = new Vector3(x, 0, z);

            points.Add(pos);
            theta += dtheta;
        }

        int starsPerPoint = totalStars / points.Count;
        foreach (Vector3 point in points)
        {
            Vector3 centre = new Vector3(point.x, 0, point.z);

            int starsInPointNucleus = (int)((float)starsPerPoint * (1.0f - (point.y / num_theta)));

            List<Star> list = GenerateNucleus(starsInPointNucleus, new Vector3(inRingRadius, inRingRadius, inRingRadius), false);
            for (int i = 0; i < list.Count; i++)
            {
                Star star = list[i];
                star.Offset(centre);
                result.Add(star);
            }
        }

        return result;
    }
}