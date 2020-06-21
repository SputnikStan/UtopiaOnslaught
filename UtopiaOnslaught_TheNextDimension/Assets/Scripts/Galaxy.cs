using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Galaxy
{
    const int numArms = 2;
   // const float armSeparationDistance = 2 * Mathf.PI / numArms;
    const float armOffsetMax = 0.5f;
    const float rotationFactor = 5;
    const float randomOffsetXY = 0.02f;

    private List<Star> mStars;
    public List<Star> Stars
    {
        get { return mStars; }
    }
    public Texture2D StarColor { get; set; }

    private GalaxyRandom mRandom;
    public Galaxy()
    {
        mStars = new List<Star>();
    }

    public void Generate(int inSeed, GALXAYTYPES inGalaxyType, int inNumberOfStars, float inGalaxyRadius, float inFlatness, Texture2D inStarColour)
    {
        StarColor = inStarColour;
        mRandom = new GalaxyRandom(inSeed);

        mStars = new List<Star>();

        switch(inGalaxyType)
        {
            case GALXAYTYPES.Eliptical:
                {
                    Vector3 dimensions = new Vector3(inGalaxyRadius, ((inGalaxyRadius * inFlatness) / 100.0f), inGalaxyRadius); 
                    mStars.AddRange(Spherical(inNumberOfStars, dimensions, inStarColour));
                }
                break;
            case GALXAYTYPES.Spiral:
                {
                    Vector3 dimensions = new Vector3(inGalaxyRadius, ((inGalaxyRadius * inFlatness) / 100.0f), inGalaxyRadius);
                    mStars.AddRange(Spiral(inNumberOfStars, dimensions, inStarColour));
                }
                break;
            case GALXAYTYPES.Sombrero:
                {
                    Vector3 dimensions = new Vector3(inGalaxyRadius, ((inGalaxyRadius * inFlatness) / 100.0f), inGalaxyRadius);
                    mStars.AddRange(Spherical(inNumberOfStars, dimensions, inStarColour));
                }
                break;
            default:
                {
                    Vector3 dimensions = new Vector3(inGalaxyRadius, inGalaxyRadius, inGalaxyRadius);
                    mStars.AddRange(Spherical(inNumberOfStars, dimensions, inStarColour));
                }
                break;
        }
    }

    private List<Star> Spherical(int inNumberOfStars, Vector3 inGalaxyRadius, Texture2D inStarColour)
    {
        float galaxyRadius = inGalaxyRadius.magnitude;
        List<Star> Stars = new List<Star>();

        for (int i = 0; i < inNumberOfStars; i++)
        {

            Vector3 starPos = mRandom.InsideUnitSphere(true);

            starPos.x *= inGalaxyRadius.x;
            starPos.y *= inGalaxyRadius.y;
            starPos.z *= inGalaxyRadius.z;

            Color starColor = Color.white;
            if (StarColor != null)
            {

                float distanceFromCentre = starPos.magnitude;
                float fIndex = ((distanceFromCentre / galaxyRadius) * (float)StarColor.width);
                int colorIndex = (int)Mathf.Clamp(fIndex, 0, (float)StarColor.width);

                starColor = StarColor.GetPixel(colorIndex, 0);
            }

            Stars.Add(new Star("Star Name", starPos, starColor));
        }

        return Stars;
    }

    private List<Star> Spiral(int inNumberOfStars, Vector3 inGalaxyRadius, Texture2D inStarColour)
    {
        float _rotationFactor = 3f;
        float _ArmsSpread = 1.5f;
        float _StarsInNucleusRatio = 0.01f;
        float _armSeparationDistance = 2 * Mathf.PI / numArms;
        float galaxyRadius = GalaxyHelpers.GetMax(inGalaxyRadius);
        List<Star> Stars = new List<Star>();

        int starsInNucleus = (int)((inNumberOfStars * _StarsInNucleusRatio) + 1);
        int starsInArms = (int)(inNumberOfStars - starsInNucleus);

        Stars.AddRange(GenerateArm(inNumberOfStars, _rotationFactor, _ArmsSpread, _armSeparationDistance, inGalaxyRadius.y, galaxyRadius));

        return Stars;
    }

    private Star[] GenerateArm(int _NumOfStars, float _Rotation, float _ArmSpread, float _ArmSeparationDistance, float _Thickness, float _GalaxyScale)
    {
        Star[] result = new Star[_NumOfStars];

        for (int i = 0; i < _NumOfStars; i++)
        {
            //float armsOffetSetMax = 0.5f;
            //float rotationFactor = 0f;

            float distance = mRandom.Next();
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
            Vector3 starPos = new Vector3(((Mathf.Sin(angle) * distance) * _GalaxyScale), 0, ((Mathf.Cos(angle) * distance) * _GalaxyScale));
            float distanceFromCentre = starPos.magnitude;
            float distanceFromCentreScalar = ( 1 - (distanceFromCentre / _GalaxyScale));
            float y = mRandom.InsideUnitSphere(true).y;
            y = y * _Thickness;
            y = y * distanceFromCentreScalar;

            starPos.y = y;

            Color starColor = Color.white;
            if (StarColor != null)
            {
                float fIndex = (((distanceFromCentre / _GalaxyScale)) * (float)StarColor.width);
                int colorIndex = (int)Mathf.Clamp(fIndex, 0, (float)StarColor.width);

                starColor = StarColor.GetPixel(colorIndex, 0);
            }

            result[i] = new Star("Star Name", starPos, starColor);
        }

        return result;
    }

    private double Pow3Constrained(double _X)
    {
        double value = System.Math.Pow(_X - 0.5, 3) * 4 + 0.5d;
        return System.Math.Max(System.Math.Min(1, value), 0);
    }

}

