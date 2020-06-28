﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spiral : GalaxyBase
{
    private readonly float _size;

    private readonly float _densityMean;
    private readonly float _densityDeviation;

    private readonly float _deviationX;
    private readonly float _deviationY;
    private readonly float _deviationZ;

    public Spiral(GalaxyRandom inRandom, int inNumberOfStars, Vector3 inGalaxyRadius, Texture2D inStarColour) : base(inRandom, inStarColour)
    {

        _size = GalaxyHelpers.GetMax(inGalaxyRadius);
        _densityMean = 0.25f;
        _densityDeviation = 0.1f;
        _deviationX = 0.25f;
        _deviationY = 0.25f;
        _deviationZ = 0.25f;

        Generate(inNumberOfStars, inGalaxyRadius);
    }

    override public void Generate(int inNumberOfStars, Vector3 inGalaxyRadius, float countMean = 0.0000025f, float countDeviation = 0.000001f,
            float deviationX = 0.0000025f, float deviationY = 0.0000025f, float deviationZ = 0.0000025f
        )
    {
        var count = Mathf.Max(0, GalaxySystemRand.NormallyDistributedSingle(countDeviation, countMean));

        for (int i = 0; i < count; i++)
        {
            Vector3 center = new Vector3(
                GalaxySystemRand.NormallyDistributedSingle(_deviationX, 0) * _size,
                GalaxySystemRand.NormallyDistributedSingle(_deviationY, 0) * _size,
                GalaxySystemRand.NormallyDistributedSingle(_deviationZ, 0 * _size)
            );


            foreach (var star in GenerateNucleus(inNumberOfStars, _size, _densityMean, _densityDeviation, _deviationX, _deviationY, _deviationZ))
            {
                star.Offset(center);
            }
        }
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
