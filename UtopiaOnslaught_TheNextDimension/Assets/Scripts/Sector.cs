using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sector
{
    private Quadrant quadrant;

    private Vector3[] BoundingPoints = new Vector3[8];
    private List<GameObject> LineObjects;

    public Sector(List<Star> inStars, Transform parent, Vector3 inPosition, float inDimension, Material inLineMaterial)
    {
        Vector3 DimensionV = new Vector3(inDimension, inDimension, inDimension);
        GameObject sectorObject = new GameObject($"Sector X{inPosition.x}-Y{inPosition.y}-z{inPosition.z}");
        sectorObject.transform.parent = parent;

        LineObjects = new List<GameObject>();

        Bounds boundingBox = new Bounds
        {
            center = inPosition,
            extents = (DimensionV / 2),
            max = (inPosition + (DimensionV / 2)),
            min = (inPosition - (DimensionV / 2)),
            size = DimensionV
        };

        if(GetStars(inStars, boundingBox) > 0)
        {
            BoundingPoints = GalaxyHelpers.CalculateBounds(inPosition, DimensionV);
            LineObjects = GalaxyHelpers.CreateBox(sectorObject.transform, BoundingPoints, inLineMaterial, Color.grey);
        }
    }

    private int GetStars(List<Star> inStars, Bounds inBoundingBox)
    {
        List<Star> starlist = new List<Star>();

        foreach (Star star in inStars)
        {
            if(inBoundingBox.Contains(star.Position) == true)
            {
                starlist.Add(star);
            }
        }

        return starlist.Count;
    }
}
