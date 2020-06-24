using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sombrero : GalaxyType
{
    public Sombrero(List<Star> inStars, GalaxyRandom inRandom, int inNumberOfStars, Vector3 inGalaxyRadius, Texture2D inStarColour)
    {
        mRandom = inRandom;

        Generate(inStars, inNumberOfStars, inGalaxyRadius, inStarColour, 0);
    }

    override public void Generate(List<Star> inStars, int inNumberOfStars, Vector3 inGalaxyRadius, Texture2D inStarColour, int inNumberOfArms = 0)
    {
        float galaxyRadius = inGalaxyRadius.magnitude;

        for (int i = 0; i < inNumberOfStars; i++)
        {

            Vector3 starPos = mRandom.InsideUnitSphere(true);

            starPos.x *= inGalaxyRadius.x;
            starPos.y *= inGalaxyRadius.y;
            starPos.z *= inGalaxyRadius.z;

            Color starColor = Color.white;
            if (inStarColour != null)
            {

                float distanceFromCentre = starPos.magnitude;
                float fIndex = ((distanceFromCentre / galaxyRadius) * (float)inStarColour.width);
                int colorIndex = (int)Mathf.Clamp(fIndex, 0, (float)inStarColour.width);

                starColor = inStarColour.GetPixel(colorIndex, 0);
            }

            inStars.Add(new Star("Star Name", starPos, starColor));
        }

    }
}