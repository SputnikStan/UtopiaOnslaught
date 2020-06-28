using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GalaxyHelpers
{
    public enum BOXCORNERS : int { corner1 = 0, corner2, corner3, corner4, corner5, corner6, corner7, corner8 };
    public enum GALAXYDEFINES : int { SECTOREDEPTH = 3 };
    public enum GALXAYTYPES : int { Cluster = 0, Spiral, Sombrero, Disc };

    public static Vector3[] CalculateBounds(Vector3 position, Vector3 dimensions)
    {
        Vector3[] Bounds = new Vector3[8];

        Bounds[(int)BOXCORNERS.corner1] = position + new Vector3(dimensions.x, dimensions.y, dimensions.z);
        Bounds[(int)BOXCORNERS.corner2] = position + new Vector3(dimensions.x, dimensions.y, -dimensions.z);
        Bounds[(int)BOXCORNERS.corner3] = position + new Vector3(-dimensions.x, dimensions.y, -dimensions.z);
        Bounds[(int)BOXCORNERS.corner4] = position + new Vector3(-dimensions.x, dimensions.y, dimensions.z);

        Bounds[(int)BOXCORNERS.corner5] = position + new Vector3(dimensions.x, -dimensions.y, dimensions.z);
        Bounds[(int)BOXCORNERS.corner6] = position + new Vector3(dimensions.x, -dimensions.y, -dimensions.z);
        Bounds[(int)BOXCORNERS.corner7] = position + new Vector3(-dimensions.x, -dimensions.y, -dimensions.z);
        Bounds[(int)BOXCORNERS.corner8] = position + new Vector3(-dimensions.x, -dimensions.y, dimensions.z);

        return Bounds;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="points"></param>
    /// <param name="color"></param>
    public static List<GameObject> CreateBox(Transform parent, Vector3[] points, Material inLineMaterial, Color color)
    {
        List<GameObject> LineObjects = new List<GameObject>();

        LineObjects.Add(CreateLine(parent, points[(int)BOXCORNERS.corner1], points[(int)BOXCORNERS.corner2], inLineMaterial, color));
        LineObjects.Add(CreateLine(parent, points[(int)BOXCORNERS.corner2], points[(int)BOXCORNERS.corner3], inLineMaterial, color));
        LineObjects.Add(CreateLine(parent, points[(int)BOXCORNERS.corner3], points[(int)BOXCORNERS.corner4], inLineMaterial, color));
        LineObjects.Add(CreateLine(parent, points[(int)BOXCORNERS.corner4], points[(int)BOXCORNERS.corner1], inLineMaterial, color));

        LineObjects.Add(CreateLine(parent, points[(int)BOXCORNERS.corner5], points[(int)BOXCORNERS.corner6], inLineMaterial, color));
        LineObjects.Add(CreateLine(parent, points[(int)BOXCORNERS.corner6], points[(int)BOXCORNERS.corner7], inLineMaterial, color));
        LineObjects.Add(CreateLine(parent, points[(int)BOXCORNERS.corner7], points[(int)BOXCORNERS.corner8], inLineMaterial, color));
        LineObjects.Add(CreateLine(parent, points[(int)BOXCORNERS.corner8], points[(int)BOXCORNERS.corner5], inLineMaterial, color));

        LineObjects.Add(CreateLine(parent, points[(int)BOXCORNERS.corner1], points[(int)BOXCORNERS.corner5], inLineMaterial, color));
        LineObjects.Add(CreateLine(parent, points[(int)BOXCORNERS.corner2], points[(int)BOXCORNERS.corner6], inLineMaterial, color));
        LineObjects.Add(CreateLine(parent, points[(int)BOXCORNERS.corner3], points[(int)BOXCORNERS.corner7], inLineMaterial, color));
        LineObjects.Add(CreateLine(parent, points[(int)BOXCORNERS.corner4], points[(int)BOXCORNERS.corner8], inLineMaterial, color));

        return LineObjects;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <param name="color"></param>
    private static GameObject CreateLine(Transform parent, Vector3 start, Vector3 end, Material inLineMaterial, Color color)
    {
        //Debug.DrawLine(start, end, color);

        GameObject myLine = new GameObject($"Line {parent.name}");

        myLine.transform.parent = parent;

        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = inLineMaterial;
        lr.startColor = color;
        lr.endColor = color;
        lr.startWidth = 0.25f;
        lr.endWidth = 0.25f;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);

        return myLine;
    }

    public static  float GetMax(Vector3 v3)
    {
        return Mathf.Max(Mathf.Max(v3.x, v3.y), v3.z);
    }
}
