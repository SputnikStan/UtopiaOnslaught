using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sombrero : GalaxyBase
{
    private readonly float _size;

    private readonly float _densityMean;
    private readonly float _densityDeviation;

    private readonly float _deviationX;
    private readonly float _deviationY;
    private readonly float _deviationZ;

    public Sombrero(GalaxyRandom inRandom, int inNumberOfStars, Vector3 inGalaxyRadius, Texture2D inStarColour) : base(inRandom, inStarColour)
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
}