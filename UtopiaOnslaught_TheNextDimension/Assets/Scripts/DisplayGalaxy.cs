using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayGalaxy : MonoBehaviour
{
    public Texture2D mStarColorGradient;
    public Galaxy mGalaxy;
    public ParticleSystem mGalaxyParticles;
    public int Seed = 6666;
    public int NumberOfStars = 1000;
    public Vector3 Dimensions = new Vector3(1024, 1024, 1024);
    public float Flatness = 10.0f;
    public Material LineMaterial;

    private Vector3 Offsets = Vector3.zero;
    private Vector3 Radius = Vector3.zero;


    // Start is called before the first frame update
    void Start()
    {
        mGalaxy = new Galaxy();

        Offsets = new Vector3((Dimensions.x / 2), (Dimensions.y / 2), (Dimensions.z / 2));
        Radius = Dimensions / 2;

        mGalaxy.Generate(Seed, NumberOfStars, Radius, 1, mStarColorGradient);

        RenderGalaxy();

        DrawGalaxyBounds();
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
                ParticleSystem.Particle particle = new ParticleSystem.Particle { position = star.Position, rotation = 0, angularVelocity = 0, startColor = star.Colour, startSize = 1.0f, velocity = Vector3.zero };
                particles.Add(particle);
            }

            //m_NumberOfStars = m_Galaxy.StarCount;
            mGalaxyParticles.SetParticles(particles.ToArray(), particles.Count);
        }


    }

    void DrawGalaxyBounds()
    {
        Vector3 position = transform.position;

        Vector3 centre = Vector3.zero;

        Vector3 corner1 = position + new Vector3(Offsets.x, Offsets.y, Offsets.z);
        Vector3 corner2 = position + new Vector3(Offsets.x, Offsets.y, -Offsets.z);
        Vector3 corner3 = position + new Vector3(-Offsets.x, Offsets.y, -Offsets.z);
        Vector3 corner4 = position + new Vector3(-Offsets.x, Offsets.y, Offsets.z);

        Vector3 corner5 = position + new Vector3(Offsets.x, -Offsets.y, Offsets.z);
        Vector3 corner6 = position + new Vector3(Offsets.x, -Offsets.y, -Offsets.z);
        Vector3 corner7 = position + new Vector3(-Offsets.x, -Offsets.y, -Offsets.z);
        Vector3 corner8 = position + new Vector3(-Offsets.x, -Offsets.y, Offsets.z);

        DrawLine(corner1, corner2, Color.green);
        DrawLine(corner2, corner3, Color.green);
        DrawLine(corner3, corner4, Color.green);
        DrawLine(corner4, corner1, Color.green);

        DrawLine(corner5, corner6, Color.green);
        DrawLine(corner6, corner7, Color.green);
        DrawLine(corner7, corner8, Color.green);
        DrawLine(corner8, corner5, Color.green);

        DrawLine(corner1, corner5, Color.green);
        DrawLine(corner2, corner6, Color.green);
        DrawLine(corner3, corner7, Color.green);
        DrawLine(corner4, corner8, Color.green);
    }

    void DrawLine(Vector3 start, Vector3 end, Color color)
    {
        //Debug.DrawLine(start, end, color);

        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = LineMaterial;
        lr.startColor = color;
        lr.endColor = color;
        lr.startWidth = 0.5f;
        lr.endWidth = 0.5f;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
    }
}
