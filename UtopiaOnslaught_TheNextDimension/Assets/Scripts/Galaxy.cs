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

    private GalaxyType mGalaxyType;

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

    public void Generate(int inSeed, GALXAYTYPES inGalaxyType, int inNumberOfStars, float inGalaxyRadius, float inFlatness, Texture2D inStarColour, int inNumberOfArms = 0)
    {
        StarColor = inStarColour;
        mRandom = new GalaxyRandom(inSeed);

        mStars = new List<Star>();

        switch(inGalaxyType)
        {
            case GALXAYTYPES.Eliptical:
                {
                    Vector3 dimensions = new Vector3(inGalaxyRadius, ((inGalaxyRadius * inFlatness) / 100.0f), inGalaxyRadius);
                    mGalaxyType = new Spherical(mStars, mRandom, inNumberOfStars, dimensions, inStarColour);
                }
                break;
            case GALXAYTYPES.Spiral:
                {
                    Vector3 dimensions = new Vector3(inGalaxyRadius, ((inGalaxyRadius * inFlatness) / 100.0f), inGalaxyRadius);
                    mGalaxyType = new Spiral(mStars, mRandom, inNumberOfStars, dimensions, inStarColour, inNumberOfArms);
                }
                break;
            case GALXAYTYPES.Sombrero:
                {
                    Vector3 dimensions = new Vector3(inGalaxyRadius, ((inGalaxyRadius * inFlatness) / 100.0f), inGalaxyRadius);
                    mGalaxyType = new Spherical(mStars, mRandom, inNumberOfStars, dimensions, inStarColour);
                }
                break;
            default:
                {
                    Vector3 dimensions = new Vector3(inGalaxyRadius, inGalaxyRadius, inGalaxyRadius);

                    mGalaxyType = new Spherical(mStars, mRandom, inNumberOfStars, dimensions, inStarColour);
                }
                break;
        }
    }

    private double Pow3Constrained(double _X)
    {
        double value = System.Math.Pow(_X - 0.5, 3) * 4 + 0.5d;
        return System.Math.Max(System.Math.Min(1, value), 0);
    }

}

