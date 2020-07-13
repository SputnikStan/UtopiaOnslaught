using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(Galaxy))]
public class GalaxyEditor : Editor
{
    override public void OnInspectorGUI()
    {
        Galaxy myScript = target as Galaxy;

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Seed");
        myScript.Seed = EditorGUILayout.IntField(myScript.Seed, GUILayout.MinWidth(100), GUILayout.Width(100));
        if (GUILayout.Button("Re-Seed"))
        {
            myScript.Seed = myScript.EditorSetSeed();
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Number Of Stars");
        myScript.NumberOfStars = (int)EditorGUILayout.Slider(myScript.NumberOfStars, 1000, 1000000, GUILayout.Width(200));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Galaxy Radius");
        myScript.GalaxyRadius = (int)EditorGUILayout.Slider(myScript.GalaxyRadius, 100, 10000, GUILayout.Width(200));
        EditorGUILayout.EndHorizontal();


        myScript.GalaxyType =(Galaxy.GALXAYTYPES) EditorGUILayout.EnumPopup(myScript.GalaxyType, GUILayout.MinWidth(100), GUILayout.Width(100));

        switch (myScript.GalaxyType)
        {
            case Galaxy.GALXAYTYPES.Cluster:
                {
                    EditorGUI.indentLevel++;

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("Cluster Min");
                    myScript.Cluster_CountMin = EditorGUILayout.IntField(myScript.Cluster_CountMin, GUILayout.MinWidth(100), GUILayout.Width(100));
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("Cluster Max");
                    myScript.Cluster_CountMax = EditorGUILayout.IntField(myScript.Cluster_CountMax, GUILayout.MinWidth(100), GUILayout.Width(100));
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("Cluster Radius Min");
                    myScript.Cluster_RadiusMin = EditorGUILayout.FloatField(myScript.Cluster_RadiusMin, GUILayout.MinWidth(100), GUILayout.Width(100));
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("Cluster Radius Max");
                    myScript.Cluster_RadiusMax = EditorGUILayout.FloatField(myScript.Cluster_RadiusMax, GUILayout.MinWidth(100), GUILayout.Width(100));
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("Flatness");
                    myScript.Cluster_Flatness = EditorGUILayout.Slider(myScript.Cluster_Flatness, 0, 1, GUILayout.Width(200));
                    EditorGUILayout.EndHorizontal();

                    EditorGUI.indentLevel--;
                }
                break;
            case Galaxy.GALXAYTYPES.Sphere:
                {
                    EditorGUI.indentLevel++;

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("Cluster Radius Min");
                    myScript.Sphere_RadiusMin = EditorGUILayout.FloatField(myScript.Sphere_RadiusMin, GUILayout.MinWidth(100), GUILayout.Width(100));
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("Cluster Radius Max");
                    myScript.Sphere_RadiusMax = EditorGUILayout.FloatField(myScript.Sphere_RadiusMax, GUILayout.MinWidth(100), GUILayout.Width(100));
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("Flatness");
                    myScript.Sphere_Flatness = EditorGUILayout.Slider(myScript.Sphere_Flatness, 0, 1, GUILayout.Width(200));
                    EditorGUILayout.EndHorizontal();


                    EditorGUI.indentLevel--;
                }
                break;
            case Galaxy.GALXAYTYPES.Spiral:
                {
                    EditorGUI.indentLevel++;

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("Number of Arms");
                    myScript.Spiral_NumberOfArms = (int)EditorGUILayout.Slider(myScript.Spiral_NumberOfArms, 1, 10, GUILayout.Width(200));
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("Stars in Arms");
                    myScript.Spiral_StarsInArms = EditorGUILayout.Slider(myScript.Spiral_StarsInArms, 0, 1, GUILayout.Width(200));
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("Arm Radius");
                    myScript.Spiral_ArmRadius = EditorGUILayout.Slider(myScript.Spiral_ArmRadius, 0, 1, GUILayout.Width(200));
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("Arm Radius Deviation");
                    myScript.Spiral_ArmRadiusDeviation = EditorGUILayout.Slider(myScript.Spiral_ArmRadiusDeviation, 0, 1, GUILayout.Width(200));
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("Arm Spread");
                    myScript.Spiral_ArmSpread = EditorGUILayout.Slider(myScript.Spiral_ArmSpread, 0, 2, GUILayout.Width(200));
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("Stars in Nucleus");
                    myScript.Spiral_StarsInNucleus = EditorGUILayout.Slider(myScript.Spiral_StarsInNucleus, 0, 1, GUILayout.Width(200));
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("Nucleus Radius");
                    myScript.Spiral_NucleusRadius = EditorGUILayout.Slider(myScript.Spiral_NucleusRadius, 0, 1, GUILayout.Width(200));
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("Nucleus Radius Deviation");
                    myScript.Spiral_NucleusRadiusDeviation = EditorGUILayout.Slider(myScript.Spiral_NucleusRadiusDeviation, 0, 1, GUILayout.Width(200));
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("Flatness");
                    myScript.Spiral_Flatness = EditorGUILayout.Slider(myScript.Spiral_Flatness, 0, 1, GUILayout.Width(200));
                    EditorGUILayout.EndHorizontal();

                    EditorGUI.indentLevel--;
                }
                break;
            default:
                break;

        }

    }
}
