using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spiral : GalaxyType
{
    Texture2D StarColor;

    public Spiral(List<Star> inStars, GalaxyRandom inRandom, int inNumberOfStars, Vector3 inGalaxyRadius, Texture2D inStarColour, int inNumberOfArms = 0)
    {
        mRandom = inRandom;
        StarColor = inStarColour;

        Generate(inStars, inNumberOfStars, inGalaxyRadius, inStarColour, inNumberOfArms);
    }

    override public void Generate(List<Star> inStars, int inNumberOfStars, Vector3 inGalaxyRadius, Texture2D inStarColour, int inNumberOfArms = 0)
    {
        float _rotationFactor = 3f;
        float _ArmsSpread = .5f;
        float _StarsInNucleusRatio = 0.01f;
        float _armSeparationDistance = 2 * Mathf.PI / inNumberOfArms;
        float galaxyRadius = GalaxyHelpers.GetMax(inGalaxyRadius);

        int starsInNucleus = (int)((inNumberOfStars * _StarsInNucleusRatio) + 1);
        int starsInArms = (int)(inNumberOfStars - starsInNucleus);

        inStars.AddRange(GenerateArm(inNumberOfStars, _rotationFactor, _ArmsSpread, _armSeparationDistance, inGalaxyRadius.y, galaxyRadius));
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
            float distanceFromCentreScalar = (1 - (distanceFromCentre / _GalaxyScale));
            float y = mRandom.InsideUnitSphere(true).y;
            y = y * _Thickness;
            y = y * distanceFromCentreScalar;

            starPos.y = y;

            Color starColor = Color.white;
            if (StarColor != null)
            {
                float fIndex = (((distance * distance )) * (float)StarColor.width);
                int colorIndex = (int)Mathf.Clamp(fIndex, 0, (float)StarColor.width);

                starColor = StarColor.GetPixel(colorIndex, 0);
            }

            result[i] = new Star("Star Name", starPos, starColor);
        }

        return result;
    }
}
