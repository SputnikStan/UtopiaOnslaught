using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spiral : GalaxyBase
{
    public float GalaxyRadius = 100;
    public int NumberOfArms { get; set; }
    public float StarsInArms { get; set; }
    public float StarsInNucleus { get; set; }
    public float NucleusRadius { get; set; }
    public float NucleusDeviation { get; set; }
    public float ArmRadius { get; set; }
    public float ArmRadiusDeviation { get; set; }
    public float ArmSpread { get; set; }
    public float Flatness { get; set; }
    public float DensityMean { get; set; }
    public float DensityDeviation { get; set; }
    public float DeviationX { get; set; }
    public float DeviationY { get; set; }
    public float DeviationZ { get; set; }

    public Spiral(GalaxyRandom inRandom, int inNumberOfStars, float inGalaxyRadius, int inNumberOfArms, float inStarsInNucleus, float inStarsInArms, 
                    float inNucleusRadius, float inNucleusDeviation = 0.25f, 
                    float inArmRadius = 5f, float inArmRadiusDeviation = 0.9f, float inArmSpread = 0.5f, 
                    float inFlatness = 0.25f,
                    float _densityMean = 0.0000025f, float _densityDeviation = 0.000001f,
                    float _deviationX = 0.0000025f,
                    float _deviationY = 0.0000025f,
                    float _deviationZ = 0.0000025f) 
        : base(inRandom)
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

        DensityMean = _densityMean;
        DensityDeviation = _densityDeviation;
        DeviationX = _deviationX;
        DeviationY = _deviationY;
        DeviationZ = _deviationZ;

    Generate();
    }

    override public List<Star> Generate( )
    {
        List<Star> result = new List<Star>();

        int NumberOfStars = 30000;

        int starsInArms = (int)(NumberOfStars * StarsInArms);
        int starsinNucleus = (int)(( NumberOfStars - starsInArms) * StarsInNucleus);
        int starsInDisc = (int)(NumberOfStars - starsInArms - starsinNucleus);

        Vector3 center = Vector3.zero;

        float angleIncrement = (float)(2 * Mathf.PI / NumberOfArms);
        float Angle = 20;
        float AngleOffset = 0;
        float galaxyRadius = GalaxyRadius;

        for (int i = 0; i < NumberOfArms; i++)
        {
            foreach (Star star in GenerateArm(starsInArms, Angle, AngleOffset, galaxyRadius, NucleusRadius * NucleusDeviation, ArmRadius, ArmSpread, Flatness))
            {
                star.Offset(center);
                //star.Swirl(Vector3.up, Swirl * 5);
                //star.SetColor(star.ConvertTemperature());
                result.Add(star);
            }

            AngleOffset += angleIncrement;
        }

        return result;
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
