using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Galaxy : MonoBehaviour
{
    public enum GALXAYTYPES : int { Cluster = 0, Spiral, Sombrero, Disc, Sphere };

    const int numArms = 2;
   // const float armSeparationDistance = 2 * Mathf.PI / numArms;
    const float armOffsetMax = 0.5f;
    const float rotationFactor = 5;
    const float randomOffsetXY = 0.02f;

    public Texture2D mStarColorGradient;
    public ParticleSystem mGalaxyParticles;
    public int Seed = 6666;
    public int NumberOfStars = 1000;
    public int NumberOfArms = 2;
    public int MaxClusters = 10;
    public int MinClusters = 5;
    public float NucleusRadiusDeviation = 0.25f;
    public float StarsInNucleus = 0.5f;
    public float InnerNucleusDeviation = 0.9f;
    public float Dimensions = 1024;
    public float Flatness = 10.0f;  // Percentage of Y
    public Material LineMaterial;
    public GALXAYTYPES GalaxyType = GALXAYTYPES.Cluster;
    private Vector3 Offsets = Vector3.zero;
    private Vector3 Radius = Vector3.zero;
    private Vector3 GalaxySize = Vector3.zero;
    private Quadrant GalaxyBounds;

    private GalaxyBase mGalaxy;

    public Texture2D StarColor { get; set; }

    private GalaxyRandom mRandom;

    void Start()
    {
        float RoundedDimension = Mathf.Pow(2, Mathf.Ceil(Mathf.Log(Dimensions) / Mathf.Log(2)));
        float Radius = RoundedDimension / 2;
        mRandom = new GalaxyRandom(Seed);

        Generate(GalaxyType, NumberOfStars, Radius, Flatness, mStarColorGradient, NumberOfArms);

        GalaxyBounds = new Quadrant(mGalaxy.Stars, transform, transform.position, Radius, LineMaterial, 0);

        RenderGalaxy();
    }

    // Update is called once per frame
    void Update()
    {
        //DrawGalaxyBounds();
    }

    public void Generate(GALXAYTYPES inGalaxyType, int inNumberOfStars, float inGalaxyRadius, float inFlatness, Texture2D inStarColour, int inNumberOfArms = 0)
    {
        StarColor = inStarColour;


        switch(inGalaxyType)
        {
            case GALXAYTYPES.Sphere:
                {
                    Vector3 dimensions = new Vector3(inGalaxyRadius, inGalaxyRadius, inGalaxyRadius);

                    mGalaxy = new Cluster(mRandom, inNumberOfStars, dimensions, inStarColour, 1, 1);
                }
                break;
            case GALXAYTYPES.Disc:
                {
                    Vector3 dimensions = new Vector3(inGalaxyRadius, ((inGalaxyRadius * inFlatness) / 100.0f), inGalaxyRadius);
                    mGalaxy = new Disc(mRandom, inNumberOfStars, dimensions, inStarColour, NucleusRadiusDeviation, StarsInNucleus, InnerNucleusDeviation);
                }
                break;
            case GALXAYTYPES.Spiral:
                {
                    Vector3 dimensions = new Vector3(inGalaxyRadius, ((inGalaxyRadius * inFlatness) / 100.0f), inGalaxyRadius);
                    mGalaxy = new Spiral(mRandom, inNumberOfStars, dimensions, inStarColour);
                }
                break;
            case GALXAYTYPES.Sombrero:
                {
                    Vector3 dimensions = new Vector3(inGalaxyRadius, ((inGalaxyRadius * inFlatness) / 100.0f), inGalaxyRadius);
                    mGalaxy = new Sombrero(mRandom, inNumberOfStars, dimensions, inStarColour);
                }
                break;
            default:
                {
                    Vector3 dimensions = new Vector3(inGalaxyRadius, inGalaxyRadius, inGalaxyRadius);

                    mGalaxy = new Cluster(mRandom, inNumberOfStars, dimensions, inStarColour, MinClusters, MaxClusters);
                }
                break;
        }
    }

    private void RenderGalaxy()
    {
        if (mGalaxyParticles != null)
        {
            List<ParticleSystem.Particle> particles = new List<ParticleSystem.Particle>();
            foreach (Star star in mGalaxy.Stars)
            {
                ParticleSystem.Particle particle = new ParticleSystem.Particle { position = star.ParticlePosition, rotation = 0, angularVelocity = 0, startColor = star.Colour, startSize = 1.0f, velocity = Vector3.zero };
                particles.Add(particle);
            }

            //m_NumberOfStars = m_Galaxy.StarCount;
            mGalaxyParticles.SetParticles(particles.ToArray(), particles.Count);
        }


    }

}

