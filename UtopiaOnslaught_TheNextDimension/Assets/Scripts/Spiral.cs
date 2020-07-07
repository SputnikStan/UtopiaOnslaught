using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spiral : GalaxyBase
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
    public float InnerNucleusDeviation { get; set; }

    public Spiral(GalaxyRandom inRandom, int inNumberOfStars, Vector3 inGalaxyRadius, Texture2D inStarColour, float inNucleusRadiusDeviation = 0.25f, float inStarsInNucleus = 0.5f, float inInnerNucleusDeviation = 0.9f) : base(inRandom, inStarColour)
    {

        Size = (int)inGalaxyRadius.magnitude;
        Spacing = 5;

        MinimumArms = 2;
        MaximumArms = 7;

        InnerNucleusDeviation = inInnerNucleusDeviation;

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

        Generate(inNumberOfStars, inGalaxyRadius, inNucleusRadiusDeviation, inStarsInNucleus, inGalaxyRadius.x, inGalaxyRadius.y, inGalaxyRadius.z);
    }

    override public void Generate(int inStarCount, Vector3 inGalaxyRadius, float inNucleusRadiusDeviation = 10f, float inStarsInNucleus = 1f,
            float deviationX = 0.025f, float deviationY = 0.025f, float deviationZ = 0.025f
        )
    {
        float galaxyRadius = inGalaxyRadius.magnitude;
        float nucleusRadius = galaxyRadius * inNucleusRadiusDeviation;
        int starsinNucleus = (int)(inStarCount * inStarsInNucleus);
        int starsInDisc = inStarCount - starsinNucleus;

        Vector3 center = Vector3.zero;

        int numArms = (int)GalaxyRand.Range(MinimumArms, MaximumArms);
        float angleIncrement = (float)(2 * Mathf.PI / numArms);
        float Angle = 20;
        float AngleOffset = 0;

        for (int i = 0; i < numArms; i++)
        {
            foreach (var star in GenerateArm(starsInDisc, Angle, AngleOffset, galaxyRadius, (nucleusRadius * InnerNucleusDeviation)))
            {
                star.Offset(center);
                //star.Swirl(Vector3.up, Swirl * 5);
                star.SetColor(star.ConvertTemperature());
                Stars.Add(star);
            }

            AngleOffset += angleIncrement;
        }

        foreach (var star in GenerateNucleus(starsinNucleus, nucleusRadius))
        {
            star.Offset(center);
            star.SetColor(star.ConvertTemperature());
            Stars.Add(star);
        }
    }

    private List<Star> GenerateArm(int inStarCount, float inAngle = 2.0f, float inAngleOffset = 0f, float inMaxRadius = 45, float nucleusRadius = 0.25f)
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
            float clusterRadius = 5.0f;

            foreach (var star in GenerateNucleus(starsPerPoint, clusterRadius, CenterClusterDensityMean, CenterClusterDensityDeviation, clusterRadius, clusterRadius, clusterRadius))
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
