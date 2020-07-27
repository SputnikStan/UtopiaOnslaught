using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Litipk.ColorSharp.LightSpectrums;

public class Star
{
    private const uint MAX_NAMELENGTH = 8;
    private const char STRING_TERMINATOR = '\0';
    private GalaxyRandom mRandom;
    private Vector3 mPosition;
    private Color mColour;
    private SolarObjects mSolarObjects;

    public Vector3 Position
    {
        get { return mPosition; }
        set { mPosition = value; }
    }

    public Vector3 ParticlePosition
    {
        get { return new Vector3(mPosition.x, mPosition.z, mPosition.y); }
    }

    public float mTemperature { get; internal set; }

    public string mName = string.Empty;

    public string Name
    {
        get { return mName; }
        private set { mName = value;}
    }

    public Color Colour
    {
        get { return mColour; }
        set { mColour = value; }
    }

    public SolarObjects StarSystem
    {
        get { return mSolarObjects; }
        set { mSolarObjects = value; }
    }

    public Star(string _Name, Vector3 _Position, Color _Color, float temp = 0)
    {
        mRandom = new GalaxyRandom((int)(_Position.x * _Position.y * _Position.z));
        Name = _Name;
        mPosition = new Vector3(_Position.x, _Position.y, _Position.z);
        mColour = _Color;
        mTemperature = temp;

        mSolarObjects = Generate_SolarSystem();
    }

    public void Offset(Vector3 offset)
    {
        Position += offset;
    }

    public void Scale( Vector3 scale)
    {
        Position.Scale(scale);
    }

    public void Swirl(Vector3 axis, float amount)
    {
        var d = Position.magnitude;

        var a = (float)Mathf.Pow(d, 0.1f) * amount;

        Position = Quaternion.AngleAxis(a, axis) * Position;
    }



    public void SetColor(Texture2D inStarColour, float inSize)
    {
        Color starColor = Color.white;
        if (inStarColour != null)
        {

            float distanceFromCentre = Position.magnitude;
            float fIndex = ((distanceFromCentre / inSize) * (float)inStarColour.width);
            int colorIndex = (int)Mathf.Clamp(fIndex, 0, (float)inStarColour.width);

            starColor = inStarColour.GetPixel(colorIndex, 0);
        }

        SetColor(starColor);
    }

    public void SetColor(Color inColor)
    {
        mColour = inColor;
    }

    public Color ConvertTemperature()
    {
        return ConvertTemperature(mTemperature);
    }

    public Color ConvertTemperature(float colorTemperature)
    {
        var srgb = new BlackBodySpectrum(colorTemperature).ToSRGB();
        return new Color((float)srgb.R, (float)srgb.G, (float)srgb.B);
    }

    public Color GenerateStarColor(System.Random inRand)
    {
        float temp = inRand.NormallyDistributedSingle(7000, 6000, 1000, 40000);

        return ConvertTemperature(temp);
    }

    public float CalculateTemperature(System.Random rand, float inMaxDistance)
    {
        float d = Position.magnitude / inMaxDistance;
        float m = d * 2000.0f + (1 - d) * 15000.0f;

        return rand.NormallyDistributedSingle(4000, m, 1000, 40000);
    }
    public float CalculateTemperature(float inMaxDistance)
    {
        return CalculateTemperature(new System.Random(), inMaxDistance);
    }

    SolarObjects Generate_SolarSystem()
    {
        SolarObjects so = new SolarObjects();

        return so;
    }
}
