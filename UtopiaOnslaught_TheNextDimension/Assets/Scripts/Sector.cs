using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sector
{
    private Quadrant quadrant;

    private Vector3[] BoundingPoints = new Vector3[8];
    private List<GameObject> LineObjects;
    private Bounds boundingBox;
    private List<Star> mStars;

    public Sector(Galaxy inGalaxy, Transform parent, Vector3 inPosition, float inDimension, Material inLineMaterial)
    {
        Vector3 DimensionV = new Vector3(inDimension, inDimension, inDimension);
        GameObject sectorObject = new GameObject($"Sector X{inPosition.x}-Y{inPosition.y}-z{inPosition.z}");
        sectorObject.transform.parent = parent;

        mStars = new List<Star>();

        LineObjects = new List<GameObject>();

        boundingBox = new Bounds
        {
            center = inPosition,
            extents = (DimensionV / 2),
            max = (inPosition + (DimensionV / 2)),
            min = (inPosition - (DimensionV / 2)),
            size = DimensionV
        };

        mStars = GetStars(inGalaxy);

        if(mStars.Count > 0)
        {
            BoundingPoints = GalaxyHelpers.CalculateBounds(inPosition, DimensionV);
            LineObjects = GalaxyHelpers.CreateBox(sectorObject.transform, BoundingPoints, inLineMaterial, Color.grey);
        }
    }

    private List<Star> GetStars(Galaxy inGalaxy)
    {
        List<Star> starlist = new List<Star>();

        foreach (Star star in inGalaxy.Stars)
        {
            if(boundingBox.Contains(star.Position) == true)
            {
                starlist.Add(star);
            }
        }
        return starlist;
    }
}
