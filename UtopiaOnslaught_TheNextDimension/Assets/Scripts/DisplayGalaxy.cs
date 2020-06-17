using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum BOXCORNERS:int { corner1 = 0, corner2, corner3, corner4, corner5, corner6, corner7, corner8 }

public class BoundingBox
{
    public Material LineMaterial;
    Vector3[] Bounds = new Vector3[8];
    Vector3[] Quadrant1 = new Vector3[8];
    Vector3[] Quadrant2 = new Vector3[8];
    Vector3[] Quadrant3 = new Vector3[8];
    Vector3[] Quadrant4 = new Vector3[8];
    Vector3[] Quadrant5 = new Vector3[8];
    Vector3[] Quadrant6 = new Vector3[8];
    Vector3[] Quadrant7 = new Vector3[8];
    Vector3[] Quadrant8 = new Vector3[8];

    Vector3 CentreTop;
    Vector3 CentreMiddle;
    Vector3 CentreBottom;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="position"></param>
    /// <param name="dimensions"></param>
    /// <param name="lineMaterial"></param>
    public BoundingBox(Vector3 position, Vector3 dimensions, Material lineMaterial)
    {
        LineMaterial = lineMaterial;

        Bounds[(int)BOXCORNERS.corner1] = position + new Vector3(dimensions.x, dimensions.y, dimensions.z);
        Bounds[(int)BOXCORNERS.corner2] = position + new Vector3(dimensions.x, dimensions.y, -dimensions.z);
        Bounds[(int)BOXCORNERS.corner3] = position + new Vector3(-dimensions.x, dimensions.y, -dimensions.z);
        Bounds[(int)BOXCORNERS.corner4] = position + new Vector3(-dimensions.x, dimensions.y, dimensions.z);

        Bounds[(int)BOXCORNERS.corner5] = position + new Vector3(dimensions.x, -dimensions.y, dimensions.z);
        Bounds[(int)BOXCORNERS.corner6] = position + new Vector3(dimensions.x, -dimensions.y, -dimensions.z);
        Bounds[(int)BOXCORNERS.corner7] = position + new Vector3(-dimensions.x, -dimensions.y, -dimensions.z);
        Bounds[(int)BOXCORNERS.corner8] = position + new Vector3(-dimensions.x, -dimensions.y, dimensions.z);

        CentreTop = CalcCentrePoint(Bounds[(int)BOXCORNERS.corner1], Bounds[(int)BOXCORNERS.corner7]);
        CentreTop.y = Bounds[(int)BOXCORNERS.corner1].y;
        CentreMiddle = CalcCentrePoint(Bounds[(int)BOXCORNERS.corner1], Bounds[(int)BOXCORNERS.corner7]);
        CentreBottom = CalcCentrePoint(Bounds[(int)BOXCORNERS.corner1], Bounds[(int)BOXCORNERS.corner7]);
        CentreBottom.y = Bounds[(int)BOXCORNERS.corner7].y;

        Quadrant1 = CalculateQuadrant1(Bounds);
        Quadrant2 = CalculateQuadrant2(Bounds);
        Quadrant3 = CalculateQuadrant3(Bounds);
        Quadrant4 = CalculateQuadrant4(Bounds);
        Quadrant5 = CalculateQuadrant5(Bounds);
        Quadrant6 = CalculateQuadrant6(Bounds);
        Quadrant7 = CalculateQuadrant7(Bounds);
        Quadrant8 = CalculateQuadrant8(Bounds);
    }

    /// <summary>
    /// 
    /// </summary>
    public void DrawBounds()
    {
        //DrawBox(Bounds, Color.green);

        DrawBox(Quadrant1, Color.green);
        DrawBox(Quadrant2, Color.green);
        DrawBox(Quadrant3, Color.green);
        DrawBox(Quadrant4, Color.green);
        DrawBox(Quadrant5, Color.green);
        DrawBox(Quadrant6, Color.green);
        DrawBox(Quadrant7, Color.green);
        DrawBox(Quadrant8, Color.green);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="point1"></param>
    /// <param name="point2"></param>
    /// <returns></returns>
    Vector3 CalcCentrePoint(Vector3 point1, Vector3 point2)
    {
        Vector3 result = Vector3.zero;

        result.x = (((point1.x + point2.x) / 2.0f));
        result.y = (((point1.y + point2.y) / 2.0f));
        result.z = (((point1.z + point2.z) / 2.0f));

        return result;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="points"></param>
    /// <returns></returns>
    public Vector3[] CalculateQuadrant1(Vector3[] points)
    {
        Vector3[] quadrant = new Vector3[8];

        quadrant[(int)BOXCORNERS.corner1] = Bounds[(int)BOXCORNERS.corner1];
        quadrant[(int)BOXCORNERS.corner2] = CalcCentrePoint(Bounds[(int)BOXCORNERS.corner1], Bounds[(int)BOXCORNERS.corner2]);
        quadrant[(int)BOXCORNERS.corner3] = CentreTop;
        quadrant[(int)BOXCORNERS.corner4] = CalcCentrePoint(Bounds[(int)BOXCORNERS.corner4], Bounds[(int)BOXCORNERS.corner1]);

        quadrant[(int)BOXCORNERS.corner5] = new Vector3(quadrant[(int)BOXCORNERS.corner1].x, CentreMiddle.y, quadrant[(int)BOXCORNERS.corner1].z);
        quadrant[(int)BOXCORNERS.corner6] = new Vector3(quadrant[(int)BOXCORNERS.corner2].x, CentreMiddle.y, quadrant[(int)BOXCORNERS.corner2].z);
        quadrant[(int)BOXCORNERS.corner7] = new Vector3(quadrant[(int)BOXCORNERS.corner3].x, CentreMiddle.y, quadrant[(int)BOXCORNERS.corner3].z);
        quadrant[(int)BOXCORNERS.corner8] = new Vector3(quadrant[(int)BOXCORNERS.corner4].x, CentreMiddle.y, quadrant[(int)BOXCORNERS.corner4].z);

        return quadrant;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="points"></param>
    /// <returns></returns>
    public Vector3[] CalculateQuadrant2(Vector3[] points)
    {
        Vector3[] quadrant = new Vector3[8];

        quadrant[(int)BOXCORNERS.corner1] = CalcCentrePoint(Bounds[(int)BOXCORNERS.corner1], Bounds[(int)BOXCORNERS.corner2]);
        quadrant[(int)BOXCORNERS.corner2] = Bounds[(int)BOXCORNERS.corner2];
        quadrant[(int)BOXCORNERS.corner3] = CalcCentrePoint(Bounds[(int)BOXCORNERS.corner2], Bounds[(int)BOXCORNERS.corner3]);
        quadrant[(int)BOXCORNERS.corner4] = CentreTop;

        quadrant[(int)BOXCORNERS.corner5] = new Vector3(quadrant[(int)BOXCORNERS.corner1].x, CentreMiddle.y, quadrant[(int)BOXCORNERS.corner1].z);
        quadrant[(int)BOXCORNERS.corner6] = new Vector3(quadrant[(int)BOXCORNERS.corner2].x, CentreMiddle.y, quadrant[(int)BOXCORNERS.corner2].z);
        quadrant[(int)BOXCORNERS.corner7] = new Vector3(quadrant[(int)BOXCORNERS.corner3].x, CentreMiddle.y, quadrant[(int)BOXCORNERS.corner3].z);
        quadrant[(int)BOXCORNERS.corner8] = new Vector3(quadrant[(int)BOXCORNERS.corner4].x, CentreMiddle.y, quadrant[(int)BOXCORNERS.corner4].z);

        return quadrant;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="points"></param>
    /// <returns></returns>
    public Vector3[] CalculateQuadrant3(Vector3[] points)
    {
        Vector3[] quadrant = new Vector3[8];

        quadrant[(int)BOXCORNERS.corner1] = CalcCentrePoint(Bounds[(int)BOXCORNERS.corner2], Bounds[(int)BOXCORNERS.corner3]);
        quadrant[(int)BOXCORNERS.corner2] = Bounds[(int)BOXCORNERS.corner3];
        quadrant[(int)BOXCORNERS.corner3] = CalcCentrePoint(Bounds[(int)BOXCORNERS.corner3], Bounds[(int)BOXCORNERS.corner4]);
        quadrant[(int)BOXCORNERS.corner4] = CentreTop;

        quadrant[(int)BOXCORNERS.corner5] = new Vector3(quadrant[(int)BOXCORNERS.corner1].x, CentreMiddle.y, quadrant[(int)BOXCORNERS.corner1].z);
        quadrant[(int)BOXCORNERS.corner6] = new Vector3(quadrant[(int)BOXCORNERS.corner2].x, CentreMiddle.y, quadrant[(int)BOXCORNERS.corner2].z);
        quadrant[(int)BOXCORNERS.corner7] = new Vector3(quadrant[(int)BOXCORNERS.corner3].x, CentreMiddle.y, quadrant[(int)BOXCORNERS.corner3].z);
        quadrant[(int)BOXCORNERS.corner8] = new Vector3(quadrant[(int)BOXCORNERS.corner4].x, CentreMiddle.y, quadrant[(int)BOXCORNERS.corner4].z);

        return quadrant;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="points"></param>
    /// <returns></returns>
    public Vector3[] CalculateQuadrant4(Vector3[] points)
    {
        Vector3[] quadrant = new Vector3[8];

        quadrant[(int)BOXCORNERS.corner1] = CalcCentrePoint(Bounds[(int)BOXCORNERS.corner4], Bounds[(int)BOXCORNERS.corner1]);
        quadrant[(int)BOXCORNERS.corner2] = CentreTop;
        quadrant[(int)BOXCORNERS.corner3] = CalcCentrePoint(Bounds[(int)BOXCORNERS.corner3], Bounds[(int)BOXCORNERS.corner4]);
        quadrant[(int)BOXCORNERS.corner4] = Bounds[(int)BOXCORNERS.corner4];

        quadrant[(int)BOXCORNERS.corner5] = new Vector3(quadrant[(int)BOXCORNERS.corner1].x, CentreMiddle.y, quadrant[(int)BOXCORNERS.corner1].z);
        quadrant[(int)BOXCORNERS.corner6] = new Vector3(quadrant[(int)BOXCORNERS.corner2].x, CentreMiddle.y, quadrant[(int)BOXCORNERS.corner2].z);
        quadrant[(int)BOXCORNERS.corner7] = new Vector3(quadrant[(int)BOXCORNERS.corner3].x, CentreMiddle.y, quadrant[(int)BOXCORNERS.corner3].z);
        quadrant[(int)BOXCORNERS.corner8] = new Vector3(quadrant[(int)BOXCORNERS.corner4].x, CentreMiddle.y, quadrant[(int)BOXCORNERS.corner4].z);

        return quadrant;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="points"></param>
    /// <returns></returns>
    public Vector3[] CalculateQuadrant5(Vector3[] points)
    {
        Vector3[] quadrant = new Vector3[8];

        quadrant[(int)BOXCORNERS.corner1] = Bounds[(int)BOXCORNERS.corner1];
        quadrant[(int)BOXCORNERS.corner1].y = CentreMiddle.y;
        quadrant[(int)BOXCORNERS.corner2] = CalcCentrePoint(Bounds[(int)BOXCORNERS.corner1], Bounds[(int)BOXCORNERS.corner2]);
        quadrant[(int)BOXCORNERS.corner2].y = CentreMiddle.y;
        quadrant[(int)BOXCORNERS.corner3] = CentreMiddle;
        quadrant[(int)BOXCORNERS.corner4] = CalcCentrePoint(Bounds[(int)BOXCORNERS.corner4], Bounds[(int)BOXCORNERS.corner1]);
        quadrant[(int)BOXCORNERS.corner4].y = CentreMiddle.y;

        quadrant[(int)BOXCORNERS.corner5] = new Vector3(quadrant[(int)BOXCORNERS.corner1].x, CentreBottom.y, quadrant[(int)BOXCORNERS.corner1].z);
        quadrant[(int)BOXCORNERS.corner6] = new Vector3(quadrant[(int)BOXCORNERS.corner2].x, CentreBottom.y, quadrant[(int)BOXCORNERS.corner2].z);
        quadrant[(int)BOXCORNERS.corner7] = new Vector3(quadrant[(int)BOXCORNERS.corner3].x, CentreBottom.y, quadrant[(int)BOXCORNERS.corner3].z);
        quadrant[(int)BOXCORNERS.corner8] = new Vector3(quadrant[(int)BOXCORNERS.corner4].x, CentreBottom.y, quadrant[(int)BOXCORNERS.corner4].z);

        return quadrant;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="points"></param>
    /// <returns></returns>
    public Vector3[] CalculateQuadrant6(Vector3[] points)
    {
        Vector3[] quadrant = new Vector3[8];

        quadrant[(int)BOXCORNERS.corner1] = CalcCentrePoint(Bounds[(int)BOXCORNERS.corner1], Bounds[(int)BOXCORNERS.corner2]);
        quadrant[(int)BOXCORNERS.corner1].y = CentreMiddle.y;
        quadrant[(int)BOXCORNERS.corner2] = Bounds[(int)BOXCORNERS.corner2];
        quadrant[(int)BOXCORNERS.corner2].y = CentreMiddle.y;
        quadrant[(int)BOXCORNERS.corner3] = CalcCentrePoint(Bounds[(int)BOXCORNERS.corner2], Bounds[(int)BOXCORNERS.corner3]);
        quadrant[(int)BOXCORNERS.corner3].y = CentreMiddle.y;
        quadrant[(int)BOXCORNERS.corner4] = CentreMiddle;

        quadrant[(int)BOXCORNERS.corner5] = new Vector3(quadrant[(int)BOXCORNERS.corner1].x, CentreBottom.y, quadrant[(int)BOXCORNERS.corner1].z);
        quadrant[(int)BOXCORNERS.corner6] = new Vector3(quadrant[(int)BOXCORNERS.corner2].x, CentreBottom.y, quadrant[(int)BOXCORNERS.corner2].z);
        quadrant[(int)BOXCORNERS.corner7] = new Vector3(quadrant[(int)BOXCORNERS.corner3].x, CentreBottom.y, quadrant[(int)BOXCORNERS.corner3].z);
        quadrant[(int)BOXCORNERS.corner8] = new Vector3(quadrant[(int)BOXCORNERS.corner4].x, CentreBottom.y, quadrant[(int)BOXCORNERS.corner4].z);

        return quadrant;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="points"></param>
    /// <returns></returns>
    public Vector3[] CalculateQuadrant7(Vector3[] points)
    {
        Vector3[] quadrant = new Vector3[8];

        quadrant[(int)BOXCORNERS.corner1] = CalcCentrePoint(Bounds[(int)BOXCORNERS.corner2], Bounds[(int)BOXCORNERS.corner3]);
        quadrant[(int)BOXCORNERS.corner1].y = CentreMiddle.y;
        quadrant[(int)BOXCORNERS.corner2] = Bounds[(int)BOXCORNERS.corner3];
        quadrant[(int)BOXCORNERS.corner2].y = CentreMiddle.y;
        quadrant[(int)BOXCORNERS.corner3] = CalcCentrePoint(Bounds[(int)BOXCORNERS.corner3], Bounds[(int)BOXCORNERS.corner4]);
        quadrant[(int)BOXCORNERS.corner3].y = CentreMiddle.y;
        quadrant[(int)BOXCORNERS.corner4] = CentreMiddle;

        quadrant[(int)BOXCORNERS.corner5] = new Vector3(quadrant[(int)BOXCORNERS.corner1].x, CentreBottom.y, quadrant[(int)BOXCORNERS.corner1].z);
        quadrant[(int)BOXCORNERS.corner6] = new Vector3(quadrant[(int)BOXCORNERS.corner2].x, CentreBottom.y, quadrant[(int)BOXCORNERS.corner2].z);
        quadrant[(int)BOXCORNERS.corner7] = new Vector3(quadrant[(int)BOXCORNERS.corner3].x, CentreBottom.y, quadrant[(int)BOXCORNERS.corner3].z);
        quadrant[(int)BOXCORNERS.corner8] = new Vector3(quadrant[(int)BOXCORNERS.corner4].x, CentreBottom.y, quadrant[(int)BOXCORNERS.corner4].z);

        return quadrant;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="points"></param>
    /// <returns></returns>
    public Vector3[] CalculateQuadrant8(Vector3[] points)
    {
        Vector3[] quadrant = new Vector3[8];

        quadrant[(int)BOXCORNERS.corner1] = CalcCentrePoint(Bounds[(int)BOXCORNERS.corner4], Bounds[(int)BOXCORNERS.corner1]);
        quadrant[(int)BOXCORNERS.corner1].y = CentreMiddle.y;
        quadrant[(int)BOXCORNERS.corner2] = CentreMiddle;
        quadrant[(int)BOXCORNERS.corner3] = CalcCentrePoint(Bounds[(int)BOXCORNERS.corner3], Bounds[(int)BOXCORNERS.corner4]);
        quadrant[(int)BOXCORNERS.corner3].y = CentreMiddle.y;
        quadrant[(int)BOXCORNERS.corner4] = Bounds[(int)BOXCORNERS.corner4];
        quadrant[(int)BOXCORNERS.corner4].y = CentreMiddle.y;

        quadrant[(int)BOXCORNERS.corner5] = new Vector3(quadrant[(int)BOXCORNERS.corner1].x, CentreBottom.y, quadrant[(int)BOXCORNERS.corner1].z);
        quadrant[(int)BOXCORNERS.corner6] = new Vector3(quadrant[(int)BOXCORNERS.corner2].x, CentreBottom.y, quadrant[(int)BOXCORNERS.corner2].z);
        quadrant[(int)BOXCORNERS.corner7] = new Vector3(quadrant[(int)BOXCORNERS.corner3].x, CentreBottom.y, quadrant[(int)BOXCORNERS.corner3].z);
        quadrant[(int)BOXCORNERS.corner8] = new Vector3(quadrant[(int)BOXCORNERS.corner4].x, CentreBottom.y, quadrant[(int)BOXCORNERS.corner4].z);

        return quadrant;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="points"></param>
    /// <param name="color"></param>
    void DrawBox(Vector3[] points, Color color)
    {
        DrawLine(points[(int)BOXCORNERS.corner1], points[(int)BOXCORNERS.corner2], color);
        DrawLine(points[(int)BOXCORNERS.corner2], points[(int)BOXCORNERS.corner3], color);
        DrawLine(points[(int)BOXCORNERS.corner3], points[(int)BOXCORNERS.corner4], color);
        DrawLine(points[(int)BOXCORNERS.corner4], points[(int)BOXCORNERS.corner1], color);

        DrawLine(points[(int)BOXCORNERS.corner5], points[(int)BOXCORNERS.corner6], color);
        DrawLine(points[(int)BOXCORNERS.corner6], points[(int)BOXCORNERS.corner7], color);
        DrawLine(points[(int)BOXCORNERS.corner7], points[(int)BOXCORNERS.corner8], color);
        DrawLine(points[(int)BOXCORNERS.corner8], points[(int)BOXCORNERS.corner5], color);

        DrawLine(points[(int)BOXCORNERS.corner1], points[(int)BOXCORNERS.corner5], color);
        DrawLine(points[(int)BOXCORNERS.corner2], points[(int)BOXCORNERS.corner6], color);
        DrawLine(points[(int)BOXCORNERS.corner3], points[(int)BOXCORNERS.corner7], color);
        DrawLine(points[(int)BOXCORNERS.corner4], points[(int)BOXCORNERS.corner8], color);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <param name="color"></param>
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

public class GalaxyTree
{
    private Vector3 Position = Vector3.zero;
    private Vector3 Size = Vector3.zero;

    private List<GalaxyBranch> GalaxyBranches;
}

public class GalaxyBranch
{
    private Vector3 Position = Vector3.zero;
    private Vector3 Size = Vector3.zero;

    private List<GalaxyLeaf> GalaxyLeafs;
}

public class GalaxyLeaf
{
    public Material LineMaterial;
    private Vector3[] Bounds = new Vector3[8];
    private List<GameObject> LineObjects;

    public GalaxyLeaf(Vector3 position, Vector3 dimensions, Material lineMaterial)
    {
        LineObjects = new List<GameObject>();

        LineMaterial = lineMaterial;

        Bounds[(int)BOXCORNERS.corner1] = position + new Vector3(dimensions.x, dimensions.y, dimensions.z);
        Bounds[(int)BOXCORNERS.corner2] = position + new Vector3(dimensions.x, dimensions.y, -dimensions.z);
        Bounds[(int)BOXCORNERS.corner3] = position + new Vector3(-dimensions.x, dimensions.y, -dimensions.z);
        Bounds[(int)BOXCORNERS.corner4] = position + new Vector3(-dimensions.x, dimensions.y, dimensions.z);

        Bounds[(int)BOXCORNERS.corner5] = position + new Vector3(dimensions.x, -dimensions.y, dimensions.z);
        Bounds[(int)BOXCORNERS.corner6] = position + new Vector3(dimensions.x, -dimensions.y, -dimensions.z);
        Bounds[(int)BOXCORNERS.corner7] = position + new Vector3(-dimensions.x, -dimensions.y, -dimensions.z);
        Bounds[(int)BOXCORNERS.corner8] = position + new Vector3(-dimensions.x, -dimensions.y, dimensions.z);

        CreateBounds(Bounds, Color.green);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="points"></param>
    /// <param name="color"></param>
    void CreateBounds(Vector3[] points, Color color)
    {
        LineObjects.Add(CreateLine(points[(int)BOXCORNERS.corner1], points[(int)BOXCORNERS.corner2], color));
        LineObjects.Add(CreateLine(points[(int)BOXCORNERS.corner2], points[(int)BOXCORNERS.corner3], color));
        LineObjects.Add(CreateLine(points[(int)BOXCORNERS.corner3], points[(int)BOXCORNERS.corner4], color));
        LineObjects.Add(CreateLine(points[(int)BOXCORNERS.corner4], points[(int)BOXCORNERS.corner1], color));

        LineObjects.Add(CreateLine(points[(int)BOXCORNERS.corner5], points[(int)BOXCORNERS.corner6], color));
        LineObjects.Add(CreateLine(points[(int)BOXCORNERS.corner6], points[(int)BOXCORNERS.corner7], color));
        LineObjects.Add(CreateLine(points[(int)BOXCORNERS.corner7], points[(int)BOXCORNERS.corner8], color));
        LineObjects.Add(CreateLine(points[(int)BOXCORNERS.corner8], points[(int)BOXCORNERS.corner5], color));

        LineObjects.Add(CreateLine(points[(int)BOXCORNERS.corner1], points[(int)BOXCORNERS.corner5], color));
        LineObjects.Add(CreateLine(points[(int)BOXCORNERS.corner2], points[(int)BOXCORNERS.corner6], color));
        LineObjects.Add(CreateLine(points[(int)BOXCORNERS.corner3], points[(int)BOXCORNERS.corner7], color));
        LineObjects.Add(CreateLine(points[(int)BOXCORNERS.corner4], points[(int)BOXCORNERS.corner8], color));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <param name="color"></param>
    private GameObject CreateLine(Vector3 start, Vector3 end, Color color)
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

        return myLine;
    }
}

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
    private Vector3 GalaxySize = Vector3.zero;

    private BoundingBox Bounds;

    // Start is called before the first frame update
    void Start()
    {
        mGalaxy = new Galaxy();

        Vector3 RoundedDimension = Vector3.zero;
        RoundedDimension.x = Mathf.Pow(2, Mathf.Ceil(Mathf.Log(Dimensions.x) / Mathf.Log(2)));
        RoundedDimension.y = Mathf.Pow(2, Mathf.Ceil(Mathf.Log(Dimensions.y) / Mathf.Log(2)));
        RoundedDimension.z = Mathf.Pow(2, Mathf.Ceil(Mathf.Log(Dimensions.z) / Mathf.Log(2)));
        Dimensions = RoundedDimension;

        Radius = RoundedDimension / 2;

        mGalaxy.Generate(Seed, NumberOfStars, Radius, 1, mStarColorGradient);

        RenderGalaxy();

        GalaxySize = new Vector3(GetMax(Radius), GetMax(Radius), GetMax(Radius));
        Bounds = new BoundingBox(transform.position, GalaxySize, LineMaterial);

        Bounds.DrawBounds();
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

    public float GetMax(Vector3 v3)
    {
        return Mathf.Max(Mathf.Max(v3.x, v3.y), v3.z);
    }
}
