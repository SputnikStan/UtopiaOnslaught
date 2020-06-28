﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quadrant
{
    private Vector3[] Bounds = new Vector3[8];
    private Vector3 Position = Vector3.zero;
    private Vector3 Size = Vector3.zero;

    private List<GameObject> LineObjects;
    private List<Quadrant> Children;

    private List<Sector> Sectors;

    public Quadrant(List<Star> inStars, Transform parent, Vector3 inPosition, float inSize, Material inLineMaterial, int inLevel)
    {
        Color[] quadrantColors = { Color.green, Color.yellow, Color.blue, Color.red, Color.cyan };

        GameObject quadrantObject = new GameObject($"Quadrant X{inPosition.x}-Y{inPosition.y}-z{inPosition.z}");
        quadrantObject.transform.parent = parent;

        Position = inPosition;
        Size = new Vector3(inSize, inSize, inSize);

        Bounds = GalaxyHelpers.CalculateBounds(inPosition, Size);

        if(inLevel == 0)
        {
            LineObjects = GalaxyHelpers.CreateBox(quadrantObject.transform, Bounds, inLineMaterial, quadrantColors[inLevel]);
        }
        /*

        float sectorSize = inSize / 2;

        Vector3 position1 = new Vector3(Position.x - sectorSize, Position.y + sectorSize, Position.z - sectorSize);
        Vector3 position2 = new Vector3(Position.x + sectorSize, Position.y + sectorSize, Position.z - sectorSize);
        Vector3 position3 = new Vector3(Position.x + sectorSize, Position.y + sectorSize, Position.z + sectorSize);
        Vector3 position4 = new Vector3(Position.x - sectorSize, Position.y + sectorSize, Position.z + sectorSize);

        Vector3 position5 = new Vector3(Position.x - sectorSize, Position.y - sectorSize, Position.z - sectorSize);
        Vector3 position6 = new Vector3(Position.x + sectorSize, Position.y - sectorSize, Position.z - sectorSize);
        Vector3 position7 = new Vector3(Position.x + sectorSize, Position.y - sectorSize, Position.z + sectorSize);
        Vector3 position8 = new Vector3(Position.x - sectorSize, Position.y - sectorSize, Position.z + sectorSize);

        if (inLevel == (int)(GalaxyHelpers.GALAXYDEFINES.SECTOREDEPTH - 1))
        {
            Sectors = new List<Sector>
            {
                new Sector(inStars, quadrantObject.transform, position1, sectorSize, inLineMaterial),
                new Sector(inStars, quadrantObject.transform,position2, sectorSize, inLineMaterial),
                new Sector(inStars, quadrantObject.transform,position3, sectorSize, inLineMaterial),
                new Sector(inStars, quadrantObject.transform, position4, sectorSize, inLineMaterial),
                new Sector(inStars, quadrantObject.transform,position5, sectorSize, inLineMaterial),
                new Sector(inStars, quadrantObject.transform, position6, sectorSize, inLineMaterial),
                new Sector(inStars, quadrantObject.transform, position7, sectorSize, inLineMaterial),
                new Sector(inStars, quadrantObject.transform, position8, sectorSize, inLineMaterial)
            };
        }

        int Level = inLevel + 1;

        if(Level <= (int)GalaxyHelpers.GALAXYDEFINES.SECTOREDEPTH)
        {
            Children = new List<Quadrant>
            {
                new Quadrant(inStars, quadrantObject.transform, position1, sectorSize, inLineMaterial, Level),
                new Quadrant(inStars, quadrantObject.transform, position2, sectorSize, inLineMaterial, Level),
                new Quadrant(inStars, quadrantObject.transform, position3, sectorSize, inLineMaterial, Level),
                new Quadrant(inStars, quadrantObject.transform, position4, sectorSize, inLineMaterial, Level),
                new Quadrant(inStars, quadrantObject.transform, position5, sectorSize, inLineMaterial, Level),
                new Quadrant(inStars, quadrantObject.transform, position6, sectorSize, inLineMaterial, Level),
                new Quadrant(inStars, quadrantObject.transform, position7, sectorSize, inLineMaterial, Level),
                new Quadrant(inStars, quadrantObject.transform, position8, sectorSize, inLineMaterial, Level)
            };
        }
        */
    }
}
