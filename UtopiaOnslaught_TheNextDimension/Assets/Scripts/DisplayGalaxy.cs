using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum BOXCORNERS:int { corner1 = 0, corner2, corner3, corner4, corner5, corner6, corner7, corner8 };
enum GALAXYDEFINES : int { SECTOREDEPTH = 3};
public enum GALXAYTYPES : int { Spherical = 0, Spiral, Sombrero, Eliptical };

public class DisplayGalaxy : MonoBehaviour
{
    public Texture2D mStarColorGradient;
    public Galaxy mGalaxy;
    public ParticleSystem mGalaxyParticles;
    public int Seed = 6666;
    public int NumberOfStars = 1000;
    public int NumberOfArms = 2;
    public float Dimensions = 1024;
    public float Flatness = 10.0f;  // Percentage of Y
    public Material LineMaterial;
    public GALXAYTYPES GalaxyType = GALXAYTYPES.Spherical;
    private Vector3 Offsets = Vector3.zero;
    private Vector3 Radius = Vector3.zero;
    private Vector3 GalaxySize = Vector3.zero;
    private Quadrant GalaxyBounds;

    // Start is called before the first frame update
    void Start()
    {
        mGalaxy = new Galaxy();

        float RoundedDimension = Mathf.Pow(2, Mathf.Ceil(Mathf.Log(Dimensions) / Mathf.Log(2)));
        float Radius = RoundedDimension / 2;

        mGalaxy.Generate(Seed, GalaxyType, NumberOfStars, Radius, Flatness, mStarColorGradient, NumberOfArms);

        GalaxyBounds = new Quadrant(mGalaxy, transform, transform.position, Radius, LineMaterial, 0);

        RenderGalaxy();
    }

    // Update is called once per frame
    void Update()
    {
        //DrawGalaxyBounds();
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
