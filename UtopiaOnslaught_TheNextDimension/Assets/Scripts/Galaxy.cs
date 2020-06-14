using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Galaxy
{
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

    public void Generate(int inSeed, int inNumberOfStars, Vector3 inGalaxyRadius, float inFlatness, Texture2D inStarColour)
    {
        StarColor = inStarColour;
        float galaxyRadius = inGalaxyRadius.magnitude;
        mRandom = new GalaxyRandom(inSeed);

        mStars = new List<Star>();

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

            mStars.Add(new Star("Star Name", starPos, starColor));
        }
    }

    private double Pow3Constrained(double _X)
    {
        double value = System.Math.Pow(_X - 0.5, 3) * 4 + 0.5d;
        return System.Math.Max(System.Math.Min(1, value), 0);
    }

}

