using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GalaxyGeneration : MonoBehaviour
{

    public ParticleSystem mGalaxyParticles;
    public ParticleSystem mGalaxyDust;

    public int Seed = 6666;
    public int NumberOfStars = 1000;
    public float GalaxyRadius = 100;
    public enum GALXAYTYPES : int { Cluster = 0, Spiral, Sombrero, Sphere, all };

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

    public bool Cluster_UniformDeviation = false;
    public float Cluster_DensityMean = 0.0000025f;
    public float Cluster_DensityDeviation = 0.000001f;
    public float Cluster_DeviationX = 0.0000025f;
    public float Cluster_DeviationY = 0.0000025f;
    public float Cluster_DeviationZ = 0.0000025f;

    public float Cluster_StarsInNucleus = 1.0f;

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

    public float DensityMean = 0.0000025f;
    public float DensityDeviation = 0.000001f;
    public float DeviationX = 0.0000025f;
    public float DeviationY = 0.0000025f;
    public float DeviationZ = 0.0000025f;

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
    //    private Vector3 Offsets = Vector3.zero;
    //    private Vector3 Radius = Vector3.zero;
    //    private Vector3 GalaxySize = Vector3.zero;
    private Quadrant GalaxyBounds;

    private Galaxy mGalaxy;

    public Texture2D StarColor { get; set; }

    private GalaxyRandom mRandom;
    public GalaxyRandom Random { get { return mRandom; } }
    public int EditorSetSeed()
    {
        System.Random rand = new System.Random();
        float seed = System.DateTime.Now.Millisecond * rand.Next();
        return (int)Mathf.Abs(seed);
    }

    void Start()
    {
        mRandom = new GalaxyRandom(Seed);

        Generate(GalaxyType, NumberOfStars, 100, Flatness, mStarColorGradient, Spiral_NumberOfArms);

        PostProcess();

        GalaxyBounds = new Quadrant(mGalaxy.Stars, transform, transform.position, mGalaxy.Radius, LineMaterial, 0);

        RenderGalaxy();
    }

    // Update is called once per frame
    //void Update()
    //{
    //    //DrawGalaxyBounds();
    //}

    void OnGUI()
    {
        GUIStyle gsGalaxyGenerator = new GUIStyle();
        gsGalaxyGenerator.normal.textColor = Color.blue;
        gsGalaxyGenerator.normal.background = MakeTex(600, 1, new Color(1.0f, 1.0f, 1.0f, 0.5f));
        // Starts an area to draw elements
        GUILayout.BeginArea(new Rect(10, 10, 400, 200), gsGalaxyGenerator);

        GUI.color = Color.black;
        GUILayout.Label($"Galaxy Seed {Seed}", GUILayout.MinWidth(100), GUILayout.Width(300));
        GUILayout.Label($"Galaxy Stars {mGalaxy.Stars.Count}", GUILayout.MinWidth(100), GUILayout.Width(300));
        GUILayout.Label($"Galaxy Clusters {mGalaxy.ClusterCount}", GUILayout.MinWidth(100), GUILayout.Width(300));
        GUILayout.Label($"Galaxy Radius {mGalaxy.Radius}", GUILayout.MinWidth(100), GUILayout.Width(300));
        GUILayout.Label($"Galaxy Extents {mGalaxy.Extents.x},{mGalaxy.Extents.y},{mGalaxy.Extents.z}", GUILayout.MinWidth(100), GUILayout.Width(300));
        GUILayout.Label($"Galaxy Dimensions {mGalaxy.Dimension.x},{mGalaxy.Dimension.y},{mGalaxy.Dimension.z}", GUILayout.MinWidth(100), GUILayout.Width(300));

        GUILayout.EndArea();
    }

    private Texture2D MakeTex(int width, int height, Color col)
    {
        Color[] pix = new Color[width * height];

        for (int i = 0; i < pix.Length; i++)
            pix[i] = col;

        Texture2D result = new Texture2D(width, height);
        result.SetPixels(pix);
        result.Apply();

        return result;
    }

    private void PostProcess()
    {
        mGalaxy.ScaleGalaxy(GalaxyRadius);
        float extent = (mGalaxy.GetExtents());
        float RoundedDimension = Mathf.Pow(2, Mathf.Ceil(Mathf.Log(extent) / Mathf.Log(2)));
        mGalaxy.Radius = extent;

        mGalaxy.GenrateStarColor(mGalaxy.Radius);

        float dimimension = RoundedDimension * 2;
        mGalaxy.Dimension = new Vector3(dimimension, dimimension, dimimension);
        mGalaxy.Extents = mGalaxy.GetAxisExtents();
    }


    public void Generate(GALXAYTYPES inGalaxyType, int inNumberOfStars, float inGalaxyRadius, float inFlatness, Texture2D inStarColour, int inNumberOfArms = 0)
    {
        StarColor = inStarColour;

        mGalaxy = new Galaxy(this, mRandom);
        mGalaxy.Generate();

        /*
        switch(inGalaxyType)
        {
            case GALXAYTYPES.Cluster:
                {
                    mGalaxy = new Cluster(mRandom, NumberOfStars, inGalaxyRadius, Cluster_RadiusMin, Cluster_RadiusMax, Cluster_CountMin, Cluster_CountMax, Cluster_Flatness);
                }
                break;
            case GALXAYTYPES.Sphere:
                {
                    mGalaxy = new Cluster(mRandom, NumberOfStars, inGalaxyRadius, Sphere_RadiusMin, Sphere_RadiusMax, 1, 1, Sphere_Flatness);
                }
                break;

            case GALXAYTYPES.Spiral:
                {
                    float NucleusRadius = inGalaxyRadius * Spiral_NucleusRadius;
                    float ArmRadius = inGalaxyRadius * Spiral_ArmRadius;

                    mGalaxy = new Spiral(mRandom, NumberOfStars, inGalaxyRadius,
                                        Spiral_NumberOfArms, Spiral_StarsInNucleus, Spiral_StarsInArms,
                                        NucleusRadius, Spiral_NucleusRadiusDeviation,
                                        ArmRadius, Spiral_ArmRadiusDeviation, Spiral_ArmSpread,
                                        Spiral_Flatness,
                                        DensityMean, DensityDeviation,
                                        DeviationX, DeviationY, DeviationZ
                                        );
                }
                break;
            case GALXAYTYPES.Sombrero:
                {
                    mGalaxy = new Sombrero(mRandom, NumberOfStars, inGalaxyRadius, StarsInNucleus, StarsInRing,
                                        NucleusRadius, OuterRadius,
                                        RingRadius, RingSpread,
                                        Flatness);
                }
                break;
            default:
                {
                    mGalaxy = new Cluster(mRandom, inNumberOfStars, inGalaxyRadius, Cluster_RadiusMin, Cluster_RadiusMax, 1, 1);
                }
                break;
        }
        */

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

