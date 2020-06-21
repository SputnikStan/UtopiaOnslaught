using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Galaxy
{
    const int numArms = 5;
    const float armSeparationDistance = 2 * Mathf.PI / numArms;
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
        float _Spin = 1.0f;
        float _ArmsSpread = 0.1f;
        double _StarsAtCenterRatio = 0.25f;
        float _StarsInNucleusRatio = 0.01f;
        int _NumberOfArms = 2;
        float galaxyRadius = inGalaxyRadius.magnitude;
        List<Star> Stars = new List<Star>();

        int starsInNucleus = (int)((inNumberOfStars * _StarsInNucleusRatio) + 1);
        int starsInArms = (int)(inNumberOfStars - starsInNucleus);

        for (int i = 0; i < _NumberOfArms; i++)
        {
            Stars.AddRange(GenerateArm(starsInArms / _NumberOfArms, (float)i / (float)_NumberOfArms, _Spin, _ArmsSpread, _StarsAtCenterRatio, inGalaxyRadius.y, galaxyRadius));
        }


        return Stars;
    }

    private Star[] GenerateArm(int _NumOfStars, float _Rotation, float _Spin, double _ArmSpread, double _StarsAtCenterRatio, float _Thickness, float _GalaxyScale)
    {
        Star[] result = new Star[_NumOfStars];

        for (int i = 0; i < _NumOfStars; i++)
        {
            double part = (double)i / (double)_NumOfStars;
            part = System.Math.Pow(part, _StarsAtCenterRatio);

            float distanceFromCenter = (float)part;
            double position = (part * _Spin + _Rotation) * System.Math.PI * 2;

            double xFluctuation = (Pow3Constrained(mRandom.NextDouble()) - Pow3Constrained(mRandom.NextDouble())) * _ArmSpread;
            double yFluctuation = (Pow3Constrained(mRandom.NextDouble()) - Pow3Constrained(mRandom.NextDouble())) * _ArmSpread * _Thickness;
            double zFluctuation = (Pow3Constrained(mRandom.NextDouble()) - Pow3Constrained(mRandom.NextDouble())) * _ArmSpread;

            float resultX = (float)System.Math.Cos(position) * distanceFromCenter / 2;
            float resultY = (float)distanceFromCenter;
            float resultZ = (float)System.Math.Sin(position) * distanceFromCenter / 2;

            Vector3 armPos = new Vector3(resultX, resultY, resultZ);
            Vector3 resultPos = new Vector3(((armPos.x + (float)xFluctuation)), (armPos.x + (float)yFluctuation), ((armPos.z + (float)zFluctuation)));
            //Vector3 starPos = resultPos * _GalaxyScale;
            Vector3 starPos = new Vector3(((resultX + (float)xFluctuation)), ((resultY * (float)yFluctuation)), ((resultZ + (float)zFluctuation)));
            starPos = resultPos * _GalaxyScale;

            Color starColor = Color.white;
            if (StarColor != null)
            {

                float distanceFromCentre = starPos.magnitude;

                float fIndex = ((distanceFromCentre / _GalaxyScale) * (float)StarColor.width);
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

