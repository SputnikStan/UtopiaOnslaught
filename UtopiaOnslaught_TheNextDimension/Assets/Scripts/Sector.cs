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

    public Sector(Galaxy inGalaxy, Transform parent, Vector3 inPosition, Vector3 inDimensions, Material inLineMaterial)
    {
        GameObject sectorObject = new GameObject($"Sector X{inPosition.x}-Y{inPosition.y}-z{inPosition.z}");
        sectorObject.transform.parent = parent;

        mStars = new List<Star>();

        LineObjects = new List<GameObject>();

        boundingBox = new Bounds
        {
            center = inPosition,
            extents = (inDimensions / 2),
            max = (inPosition + (inDimensions / 2)),
            min = (inPosition - (inDimensions / 2)),
            size = inDimensions
        };

        mStars = GetStars(inGalaxy);

        if(mStars.Count > 0)
        {
            BoundingPoints = GalaxyHelpers.CalculateBounds(inPosition, inDimensions);
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
