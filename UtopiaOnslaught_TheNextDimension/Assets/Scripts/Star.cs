using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star
{
    private const uint MAX_NAMELENGTH = 8;
    private const char STRING_TERMINATOR = '\0';

    private Vector3 mPosition;
    private char[] mName;
    private Color mColour;
    private SolarObjects mSolarObjects;

    public Vector3 Position
    {
        get { return mPosition; }
        set { mPosition = value; }
    }

    public string Name
    {
        get { return mName.ToString(); }
        set
        {
            for (int i = 0; i < MAX_NAMELENGTH; i++) mName[i] = value[i];
            mName[MAX_NAMELENGTH] = STRING_TERMINATOR;
        }
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

    public Star(string _Name, Vector3 _Position, Color _Color)
    {
        mName = new char[MAX_NAMELENGTH + 1];
        for (int i = 0; i < MAX_NAMELENGTH; i++) mName[i] = _Name[i];
        mName[MAX_NAMELENGTH] = STRING_TERMINATOR;

        mPosition = _Position;
        mColour = _Color;

        mSolarObjects = Generate_SolarSystem();
    }

    SolarObjects Generate_SolarSystem()
    {
        SolarObjects so = new SolarObjects();

        return so;
    }
}
