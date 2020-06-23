using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class GalaxyType
{
    protected GalaxyRandom mRandom;

    public abstract void Generate(List<Star> inStars, int inNumberOfStars, Vector3 inGalaxyRadius, Texture2D inStarColour, int inNumberOfArms = 0);
}
