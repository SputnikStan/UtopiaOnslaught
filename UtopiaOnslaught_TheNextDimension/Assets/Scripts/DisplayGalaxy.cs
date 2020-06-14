using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayGalaxy : MonoBehaviour
{
    public Texture2D mStarColorGradient;
    public Galaxy mGalaxy;
    public ParticleSystem mGalaxyParticles;

    // Start is called before the first frame update
    void Start()
    {
        mGalaxy = new Galaxy();

        mGalaxy.Generate(6666, 1000, 10.0f);

        RenderGalaxy();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void RenderGalaxy()
    {
        if (mGalaxyParticles != null)
        {
            List<ParticleSystem.Particle> particles = new List<ParticleSystem.Particle>();
            foreach (Star star in mGalaxy.Stars)
            {
                ParticleSystem.Particle particle = new ParticleSystem.Particle { position = star.Position, rotation = 0, angularVelocity = 0, startColor = star.Colour, startSize = 1.0f, velocity = Vector3.zero };
                particles.Add(particle);
            }

            //m_NumberOfStars = m_Galaxy.StarCount;
            mGalaxyParticles.SetParticles(particles.ToArray(), particles.Count);
        }
    }

}
