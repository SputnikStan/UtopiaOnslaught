using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarObjects
{
    private List<Planet> mPlanets;
    private List<AsteroidBelt> mAsteriodBelts;
    private List<Comet> Comets;
    private List<Blackhole> Blackholes;
    private List<JumpGate> JumpGates;

    public SolarObjects()
    {
        mPlanets = new List<Planet>();
        mAsteriodBelts = new List<AsteroidBelt>();
        Comets = new List<Comet>();
        Blackholes = new List<Blackhole>();
        JumpGates = new List<JumpGate>();
    }
}

public class Planet
{
    private List<Moon> Moons;
}

public class AsteroidBelt
{
    private List<Asteroid> mAsteriods;

}

public class Comet
{

}

public class Blackhole
{

}

public class JumpGate
{

}

public class Moon
{

}

public class Asteroid
{

}


