using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spiral : GalaxyBase
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

    public Spiral(GalaxyRandom inRandom, int inNumberOfStars, Vector3 inGalaxyRadius, int inNumberOfArms, float inStarsInNucleus, float inStarsInArms, 
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
        int starsInArms = (int)(NumberOfStars * StarsInArms);
        int starsinNucleus = (int)(( NumberOfStars - starsInArms) * StarsInNucleus);
        int starsInDisc = (int)(NumberOfStars - starsInArms - starsinNucleus);

        Vector3 center = Vector3.zero;

        float angleIncrement = (float)(2 * Mathf.PI / NumberOfArms);
        float Angle = 20;
        float AngleOffset = 0;
        float galaxyRadius = GalaxyRadius.magnitude;

        for (int i = 0; i < NumberOfArms; i++)
        {
            foreach (var star in GenerateArm(starsInArms, Angle, AngleOffset, galaxyRadius, NucleusRadius * NucleusDeviation, ArmRadius, ArmSpread, Flatness))
            {
                star.Offset(center);
                //star.Swirl(Vector3.up, Swirl * 5);
                //star.SetColor(star.ConvertTemperature());
                Stars.Add(star);
            }

            AngleOffset += angleIncrement;
        }

        Vector3 disc = new Vector3(GalaxyRadius.x, GalaxyRadius.y, GalaxyRadius.z);

        foreach (var star in GenerateDisc(starsInDisc, NucleusRadius, NucleusDeviation, galaxyRadius, Flatness))
        {
            star.Offset(center);
            star.SetColor(star.ConvertTemperature());
            Stars.Add(star);
        }

        foreach (var star in GenerateNucleus(starsinNucleus, new Vector3( NucleusRadius, NucleusRadius, NucleusRadius), false))
        {
            star.Offset(center);
            star.SetColor(star.ConvertTemperature());
            Stars.Add(star);
        }

    }

    private List<Star> GenerateArm(int inStarCount, float inAngle = 2.0f, float inAngleOffset = 0f, float inMaxRadius = 45, float nucleusRadius = 0.25f, float inArmRadius = 5, float inArmSpread = 1.0f, float inFlatness = 1.0f)
    {
        int totalStars = inStarCount;

        float A = inAngle;
        float angle_offset = inAngleOffset;
        float max_r = inMaxRadius + nucleusRadius;

        List<Star> result = new List<Star>();

        List<Vector3> points = new List<Vector3>();

        const float dtheta = (float)(5 * Mathf.PI / 180);    // Five degrees.
        for (float theta = 0; ; theta += dtheta)
        {
            // Calculate r.
            float r = A * theta;
            // Convert to Cartesian coordinates.
            Vector3 pos =  PolarToCartesian(r, theta + angle_offset);

            if(pos.magnitude > nucleusRadius)
            {
                points.Add(pos);
            }
            // If we have gone far enough, stop.
            if (r > max_r) break;
        }

        int starsPerPoint = totalStars / points.Count;
        foreach (Vector3 point in points)
        {
            Vector3 centre = new Vector3(point.x, 0, point.z);

            int starsInPointNucleus = (int)((float)starsPerPoint * (1.0f - (point.y / max_r)));

            List<Star> list = GenerateNucleus(starsInPointNucleus, new Vector3(inArmRadius * inArmSpread, inArmRadius, inArmRadius * inArmSpread), false);
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
