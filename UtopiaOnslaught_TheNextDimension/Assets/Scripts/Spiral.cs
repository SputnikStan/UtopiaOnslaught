using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spiral : GalaxyBase
{

    public int NumberOfArms { get; set; }
    public float StarsInNucleus { get; set; }
    public float NucleusRadius { get; set; }
    public float NucleusDeviation { get; set; }
    public float ArmRadius { get; set; }
    public float ArmRadiusDeviation { get; set; }
    public float ArmSpread { get; set; }

    public Spiral(GalaxyRandom inRandom, int inNumberOfStars, Vector3 inGalaxyRadius, int inNumberOfArms, float inStarsInNucleus, float inNucleusRadius, float inNucleusDeviation = 0.25f, float inArmRadius = 5f, float inArmRadiusDeviation = 0.9f, float inArmSpread = 0.5f) 
        : base(inRandom, inNumberOfStars, inGalaxyRadius)
    {
        NumberOfArms = inNumberOfArms;
        StarsInNucleus = inStarsInNucleus;
        NucleusRadius = inNucleusRadius;
        NucleusDeviation = inNucleusDeviation;
        ArmRadius = inArmRadius;
        ArmRadiusDeviation = inArmRadiusDeviation;
        ArmSpread = inArmSpread;

        Generate();
    }

    override public void Generate( )
    {
        int starsinNucleus = (int)(NumberOfStars * StarsInNucleus);
        int starsInDisc = NumberOfStars - starsinNucleus;

        Vector3 center = Vector3.zero;

        float angleIncrement = (float)(2 * Mathf.PI / NumberOfArms);
        float Angle = 20;
        float AngleOffset = 0;

        for (int i = 0; i < NumberOfArms; i++)
        {
            foreach (var star in GenerateArm(starsInDisc, Angle, AngleOffset, GalaxyBase.GetMax(GalaxyRadius), NucleusRadius * NucleusDeviation, ArmRadius))
            {
                star.Offset(center);
                //star.Swirl(Vector3.up, Swirl * 5);
                star.SetColor(star.ConvertTemperature());
                Stars.Add(star);
            }

            AngleOffset += angleIncrement;
        }

        foreach (var star in GenerateNucleus(starsinNucleus, new Vector3( NucleusRadius, NucleusRadius, NucleusRadius)))
        {
            star.Offset(center);
            star.SetColor(star.ConvertTemperature());
            Stars.Add(star);
        }

    }

    private List<Star> GenerateArm(int inStarCount, float inAngle = 2.0f, float inAngleOffset = 0f, float inMaxRadius = 45, float nucleusRadius = 0.25f, float inArmRadius = 5)
    {
        int totalStars = inStarCount;

        float A = inAngle;
        float angle_offset = inAngleOffset;
        float max_r = inMaxRadius;

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
            float clusterRadius = inArmRadius;

            foreach (var star in GenerateNucleus(starsPerPoint, new Vector3( clusterRadius, clusterRadius, clusterRadius) ))
            {
                star.Offset(point);
                result.Add(star);
            }
        }

        return result;
    }

    // Convert polar coordinates into Cartesian coordinates.
    private Vector3 PolarToCartesian(float r, float theta)
    {
        float x = (float)(r * Mathf.Cos(theta));
        float y = (float)(r * Mathf.Sin(theta));

        return new Vector3(x, 0, y);
    }

    private List<Star> GenerateArm2(int _NumOfStars, float _Rotation, float _Spin, double _ArmSpread, double _StarsAtCenterRatio, float _Thickness, float _GalaxyScale)
    {
        List<Star> result = new List<Star>();

        for (int i = 0; i < _NumOfStars; i++)
        {
            double part = (double)i / (double)_NumOfStars;
            part = System.Math.Pow(part, _StarsAtCenterRatio);

            float distanceFromCenter = (float)part;
            double position = (part * _Spin + _Rotation) * System.Math.PI * 2;

            double xFluctuation = (Pow3Constrained(GalaxyRand.NextDouble()) - Pow3Constrained(GalaxyRand.NextDouble())) * _ArmSpread;
            double yFluctuation = (Pow3Constrained(GalaxyRand.NextDouble()) - Pow3Constrained(GalaxyRand.NextDouble())) * _Thickness;
            double zFluctuation = (Pow3Constrained(GalaxyRand.NextDouble()) - Pow3Constrained(GalaxyRand.NextDouble())) * _ArmSpread;

            float resultX = (float)System.Math.Cos(position) * distanceFromCenter / 2;
            float resultY = (float)0;
            float resultZ = (float)System.Math.Sin(position) * distanceFromCenter / 2;

            Vector3 armPos = new Vector3(resultX, 0, resultZ);
            Vector3 resultPos = new Vector3(((armPos.x + (float)xFluctuation)), 0, ((armPos.z + (float)zFluctuation)));
            //Vector3 starPos = resultPos * _GalaxyScale;
            Vector3 starPos = new Vector3(((resultX + (float)xFluctuation)), ((resultY + (float)yFluctuation)), ((resultZ + (float)zFluctuation)));
            starPos = resultPos * _GalaxyScale;

            var d = starPos.magnitude / _GalaxyScale;
            var m = d * 2000 + (1 - d) * 15000;
            var t = GalaxySystemRand.NormallyDistributedSingle(4000, m, 1000, 40000);

            Color starColor = Color.magenta;
            Star star = new Star(GenerateStarName.Generate(GalaxySystemRand), starPos, starColor, t);

            result.Add(star);
        }

        return result;
    }
}
