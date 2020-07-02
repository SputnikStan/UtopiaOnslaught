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

        MinimumArms = 3;
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

        foreach (var star in GenerateArms(starsInDisc, nucleusRadius, InnerNucleusDeviation, galaxyRadius, deviationX, deviationY, deviationZ))
        {
            star.Offset(center);
            star.SetColor(star.ConvertTemperature());
            Stars.Add(star);
        }

            //foreach (var star in GenerateNucleus(starsinNucleus, nucleusRadius))
            //{
            //    star.Offset(center);
            //    star.SetColor(star.ConvertTemperature());
            //    Stars.Add(star);
            //}

            //foreach (var star in GenerateDisc(starsInDisc, nucleusRadius, InnerNucleusDeviation, galaxyRadius, deviationX, deviationY, deviationZ))
            //{
            //    star.Offset(center);
            //    star.Swirl(Vector3.up, Swirl);
            //    //star.Offset(center);
            //    star.SetColor(star.ConvertTemperature());
            //   Stars.Add(star);
            //}
    }

    private List<Star> GenerateArms(int inStarCount, float nucleusRadius, float InnerNucleusDeviation = 0.25f, float galaxyRadius = 0.1f, float deviationX = 0.25f, float deviationY = 0.25f, float deviationZ = 0.25f)
    {
        float _ArmSpread = 0.8f;
        float _Rotation = 4f;
        float _ArmSeparationDistance = 8f;

        List<Star> result = new List<Star>();

        float percentStarInCentre = 60;

        for (int i = 0; i < inStarCount; i++)
        {
            //float armsOffetSetMax = 0.5f;
            //float rotationFactor = 0f;

            float distance = GalaxyRand.Next();
            distance = distance * (percentStarInCentre / 100);
            float slopeMod = 0.2f; // between 0 and 1, higher is more linear
            distance = (Mathf.Pow(distance, 1f / 3f) - 1f) / (1 - slopeMod);

            // Choose an angle between 0 and 2 * PI.
            float angle = GalaxyRand.Next() * 2.0f * Mathf.PI;
            float armOffset = GalaxyRand.Next() * _ArmSpread;

            armOffset = armOffset - _ArmSpread / 2;
            armOffset = armOffset * (1 / distance);

            float squaredArmOffset = Mathf.Pow(armOffset, 2);

            if (armOffset < 0)
                squaredArmOffset = squaredArmOffset * -1;
            armOffset = squaredArmOffset;

            float rotation = distance * _Rotation;

            // Compute the angle of the arms.
            angle = (int)(angle / _ArmSeparationDistance) * _ArmSeparationDistance + armOffset + rotation;

            // Convert polar coordinates to 2D cartesian coordinates.
            float x = ((Mathf.Sin(angle) * distance) * deviationX);
            float z = ((Mathf.Cos(angle) * distance) * deviationZ);

            float distanceFromCentre = new Vector2(x, z).magnitude;
            if (galaxyRadius < distanceFromCentre)
            {
                distanceFromCentre = galaxyRadius;
            }
            float distanceFromCentreScalar = nucleusRadius * (1 - (distanceFromCentre / galaxyRadius));
            float y = GalaxyRand.InsideUnitSphere(true).y * distanceFromCentreScalar;

            Vector3 starPos = new Vector3(x, y, z);

            var d = starPos.magnitude / galaxyRadius;
            var m = d * 2000 + (1 - d) * 15000;
            var t = GalaxySystemRand.NormallyDistributedSingle(4000, m, 1000, 40000);
            Color starColor = Color.magenta;
            result.Add(new Star(GenerateStarName.Generate(GalaxySystemRand), starPos, starColor, t));
        }
        return result;
    }

    /*
    private Star[] GenerateArms(int _NumOfStars, float _Rotation, float _ArmSpread, float _ArmSeparationDistance, float _Thickness, float _GalaxyScale)
    {
        float percentStarInCentre = 100;
        Star[] result = new Star[_NumOfStars];

        for (int i = 0; i < _NumOfStars; i++)
        {
            //float armsOffetSetMax = 0.5f;
            //float rotationFactor = 0f;

            float distance = mRandom.Next();
            distance = distance * (percentStarInCentre / 100);
            float slopeMod = 0.2f; // between 0 and 1, higher is more linear
            distance = (Mathf.Pow(distance, 1f / 3f) - 1f) / (1 - slopeMod);

            // Choose an angle between 0 and 2 * PI.
            float angle = mRandom.Next() * 2 * Mathf.PI;
            float armOffset = mRandom.Next() * _ArmSpread;

            armOffset = armOffset - _ArmSpread / 2;
            armOffset = armOffset * (1 / distance);

            float squaredArmOffset = Mathf.Pow(armOffset, 2);

            if (armOffset < 0)
                squaredArmOffset = squaredArmOffset * -1;
            armOffset = squaredArmOffset;

            float rotation = distance * _Rotation;

            // Compute the angle of the arms.
            angle = (int)(angle / _ArmSeparationDistance) * _ArmSeparationDistance + armOffset + rotation;

            // Convert polar coordinates to 2D cartesian coordinates.
            float x = ((Mathf.Sin(angle) * distance) * _GalaxyScale);
            float z = ((Mathf.Cos(angle) * distance) * _GalaxyScale);

            float distanceFromCentre = new Vector2(x, z).magnitude;
            if (_GalaxyScale < distanceFromCentre)
            {
                distanceFromCentre = _GalaxyScale;
            }
            float distanceFromCentreScalar = _Thickness * (1 - (distanceFromCentre / _GalaxyScale));
            float y = mRandom.InsideUnitSphere(true).y * distanceFromCentreScalar;

            Vector3 starPos = new Vector3(x, y, z);

            Color starColor = Color.white;
            if (StarColor != null)
            {
                float fIndex = (((Mathf.Abs(armOffset) / _ArmSpread)) * (float)StarColor.width);
                int colorIndex = (int)Mathf.Clamp(fIndex, 0, (float)StarColor.width);

                starColor = StarColor.GetPixel(colorIndex, 0);
            }

            result[i] = new Star("Star Name", starPos, starColor);
        }

        return result;
    }

    private Star[] GenerateArm(int _NumOfStars, float _Rotation, float _ArmSpread, float _ArmSeparationDistance, float _Thickness, float _GalaxyScale)
    {
        float percentStarInCentre = 100;
        Star[] result = new Star[_NumOfStars];

        for (int i = 0; i < _NumOfStars; i++)
        {
            //float armsOffetSetMax = 0.5f;
            //float rotationFactor = 0f;

            float distance = mRandom.Next();
            distance = distance * ( percentStarInCentre / 100);
            float slopeMod = 0.2f; // between 0 and 1, higher is more linear
            distance = (Mathf.Pow(distance, 1f / 3f) - 1f) / (1 - slopeMod);

            // Choose an angle between 0 and 2 * PI.
            float angle = mRandom.Next() * 2 * Mathf.PI;
            float armOffset = mRandom.Next() * _ArmSpread;

            armOffset = armOffset - _ArmSpread / 2;
            armOffset = armOffset * (1 / distance);

            float squaredArmOffset = Mathf.Pow(armOffset, 2);

            if (armOffset < 0)
                squaredArmOffset = squaredArmOffset * -1;
            armOffset = squaredArmOffset;

            float rotation = distance * _Rotation;

            // Compute the angle of the arms.
            angle = (int)(angle / _ArmSeparationDistance) * _ArmSeparationDistance + armOffset + rotation;

            // Convert polar coordinates to 2D cartesian coordinates.
            float x = ((Mathf.Sin(angle) * distance) * _GalaxyScale);
            float z = ((Mathf.Cos(angle) * distance) * _GalaxyScale);

            float distanceFromCentre = new Vector2(x,z).magnitude;
            if(_GalaxyScale < distanceFromCentre)
            {
                distanceFromCentre = _GalaxyScale;
            }
            float distanceFromCentreScalar = _Thickness * (1 - (distanceFromCentre / _GalaxyScale));
            float y = mRandom.InsideUnitSphere(true).y * distanceFromCentreScalar;

            Vector3 starPos = new Vector3(x, y, z);

            Color starColor = Color.white;
            if (StarColor != null)
            {
                float fIndex = (((Mathf.Abs(armOffset) / _ArmSpread)) * (float)StarColor.width);
                int colorIndex = (int)Mathf.Clamp(fIndex, 0, (float)StarColor.width);

                starColor = StarColor.GetPixel(colorIndex, 0);
            }

            result[i] = new Star("Star Name", starPos, starColor);
        }

        return result;
    }
    */
}
