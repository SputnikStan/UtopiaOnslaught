using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Galaxy : MonoBehaviour
{

    public ParticleSystem mGalaxyParticles;
    public int Seed = 6666;
    public int NumberOfStars = 1000;
    public float GalaxyRadius = 100;
    public enum GALXAYTYPES : int { Cluster = 0, Spiral, Sombrero, Sphere };

    public GALXAYTYPES GalaxyType = GALXAYTYPES.Cluster;

// Cluster Galaxy variables
    public int Cluster_CountMin = 3;
    public int Cluster_CountMax = 7;

    public float Cluster_RadiusMin = 0.25f;
    public float Cluster_RadiusMax = 0.5f;

    public float Cluster_NucleusRadius = 10.0f;
    public float Cluster_NucleusRadiusDeviation = 0.25f;
    public float ClusterStarsInNucleus = 0.5f;

    public float Cluster_Flatness = 10.0f;  // Percentage of Y

    // Spiral Galaxy Variables

    public int Spiral_NumberOfArms = 2;
    public float Spiral_StarsInArms = 0.7f;
    public float Spiral_ArmRadius = 0.25f;
    public float Spiral_ArmRadiusDeviation = 0.25f;
    public float Spiral_ArmSpread = 1f;
    public float Spiral_NucleusRadius = 10.0f;
    public float Spiral_NucleusRadiusDeviation = 0.25f;
    public float Spiral_StarsInNucleus = 0.5f;
    public float Spiral_Flatness = 10.0f;  // Percentage of Y

    // Sphere Galaxy Variables

    public float Sphere_RadiusMin = 0.25f;
    public float Sphere_RadiusMax = 0.5f;
    public float Sphere_Flatness = 10.0f;  // Percentage of Y

    // const float armSeparationDistance = 2 * Mathf.PI / numArms;
    //    const float armOffsetMax = 0.5f;
    //    const float rotationFactor = 5;
    //    const float randomOffsetXY = 0.02f;

    public Texture2D mStarColorGradient;
    
    public float NucleusRadius = 10.0f;
    public float NucleusRadiusDeviation = 0.25f;
    public float StarsInNucleus = 0.5f;

    public float StarsInRing = 0.7f;
    public float OuterRadius = 0.7f;
    public float RingRadius = 4;
    public float RingSpread = 0.7f;




    public float InnerNucleusDeviation = 0.9f;
    public float Dimensions = 1024;
    public float Flatness = 10.0f;  // Percentage of Y
    public Material LineMaterial;
    private Vector3 Offsets = Vector3.zero;
    private Vector3 Radius = Vector3.zero;
    private Vector3 GalaxySize = Vector3.zero;
    private Quadrant GalaxyBounds;

    private GalaxyBase mGalaxy;

    public Texture2D StarColor { get; set; }

    private GalaxyRandom mRandom;
    public int EditorSetSeed()
    {
        System.Random rand = new System.Random();
        float seed = System.DateTime.Now.Millisecond * rand.Next();
        return (int)Mathf.Abs(seed);
    }

    void Start()
    {
        mRandom = new GalaxyRandom(Seed);

        Generate(GalaxyType, NumberOfStars, Dimensions, Flatness, mStarColorGradient, Spiral_NumberOfArms);

        float extent = (mGalaxy.GetExtents() * 2);
        float RoundedDimension = Mathf.Pow(2, Mathf.Ceil(Mathf.Log(extent) / Mathf.Log(2)));
        float Radius = RoundedDimension / 2;

        GalaxyBounds = new Quadrant(mGalaxy.Stars, transform, transform.position, Radius, LineMaterial, 0);

        RenderGalaxy();
    }

    // Update is called once per frame
    //void Update()
    //{
    //    //DrawGalaxyBounds();
    //}

    public void Generate(GALXAYTYPES inGalaxyType, int inNumberOfStars, float inGalaxyRadius, float inFlatness, Texture2D inStarColour, int inNumberOfArms = 0)
    {
        StarColor = inStarColour;


        switch(inGalaxyType)
        {
            case GALXAYTYPES.Cluster:
                {
                    mGalaxy = new Cluster(mRandom, NumberOfStars, new Vector3(GalaxyRadius, GalaxyRadius, GalaxyRadius), Cluster_RadiusMin, Cluster_RadiusMax, Cluster_CountMin, Cluster_CountMax, Cluster_Flatness);
                }
                break;
            case GALXAYTYPES.Sphere:
                {
                    mGalaxy = new Cluster(mRandom, NumberOfStars, new Vector3(GalaxyRadius, GalaxyRadius, GalaxyRadius), Sphere_RadiusMin, Sphere_RadiusMax, 1, 1, Sphere_Flatness);
                }
                break;

            case GALXAYTYPES.Spiral:
                {
                    float NucleusRadius = GalaxyRadius * Spiral_NucleusRadius;
                    float ArmRadius = NucleusRadius * Spiral_ArmRadius;

                    mGalaxy = new Spiral(mRandom, NumberOfStars, new Vector3(GalaxyRadius, GalaxyRadius, GalaxyRadius),
                                        Spiral_NumberOfArms, Spiral_StarsInNucleus, Spiral_StarsInArms,
                                        NucleusRadius, Spiral_NucleusRadiusDeviation,
                                        ArmRadius, Spiral_ArmRadiusDeviation, Spiral_ArmSpread,
                                        Spiral_Flatness);
                }
                break;
            case GALXAYTYPES.Sombrero:
                {
                    mGalaxy = new Sombrero(mRandom, NumberOfStars, new Vector3(GalaxyRadius, GalaxyRadius, GalaxyRadius), StarsInNucleus, StarsInRing,
                                        NucleusRadius, OuterRadius,
                                        RingRadius, RingSpread,
                                        Flatness);
                }
                break;
            default:
                {
                    Vector3 dimensions = new Vector3(inGalaxyRadius, inGalaxyRadius, inGalaxyRadius);

                    mGalaxy = new Cluster(mRandom, inNumberOfStars, dimensions, Cluster_RadiusMin, Cluster_RadiusMax, 1, 1);
                }
                break;
        }

        //mGalaxy.GenrateStarColor();
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

