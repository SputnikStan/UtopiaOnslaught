using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(GalaxyGeneration))]
public class GalaxyEditor : Editor
{
    SerializedProperty m_StarObjects;
    SerializedProperty m_DustObjects;

    void OnEnable()
    {
        // Fetch the objects from the GameObject script to display in the inspector
        m_StarObjects = serializedObject.FindProperty("mGalaxyParticles");
        m_DustObjects = serializedObject.FindProperty("mGalaxyDust");
    }

    override public void OnInspectorGUI()
    {
        GalaxyGeneration myScript = target as GalaxyGeneration;

        //The variables and GameObject from the MyGameObject script are displayed in the Inspector with appropriate labels
        EditorGUILayout.PropertyField(m_StarObjects, new GUIContent("Star Objects"), GUILayout.Height(20));
        EditorGUILayout.PropertyField(m_DustObjects, new GUIContent("Dust Objects"), GUILayout.Height(20));

        // Apply changes to the serializedProperty - always do this at the end of OnInspectorGUI.
        serializedObject.ApplyModifiedProperties();

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
        myScript.GalaxyRadius = (int)EditorGUILayout.Slider(myScript.GalaxyRadius, 1, 1000, GUILayout.Width(200));
        EditorGUILayout.EndHorizontal();


        //myScript.GalaxyType =(GalaxyGeneration.GALXAYTYPES) EditorGUILayout.EnumPopup(myScript.GalaxyType, GUILayout.MinWidth(100), GUILayout.Width(100));

        EditorGUI.indentLevel++;

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Cluster Min");
        int cluserMin = (int)EditorGUILayout.Slider(myScript.Cluster_CountMin, 1, 50, GUILayout.Width(200));
        if (cluserMin < myScript.Cluster_CountMax)
            myScript.Cluster_CountMin = cluserMin;
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Cluster Max");
        int cluserMax = (int)EditorGUILayout.Slider(myScript.Cluster_CountMax, 1, 50, GUILayout.Width(200));
        if (cluserMax > myScript.Cluster_CountMin)
            myScript.Cluster_CountMax = cluserMax;
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Cluster Radius Min");
        float radiusMin = EditorGUILayout.Slider(myScript.Cluster_RadiusMin, 0, 1, GUILayout.Width(200));
        if (radiusMin < myScript.Cluster_RadiusMax)
            myScript.Cluster_RadiusMin = radiusMin;
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Cluster Radius Max");
        float radiusMax = EditorGUILayout.Slider(myScript.Cluster_RadiusMax, 0, 1, GUILayout.Width(200));
        if (radiusMax > myScript.Cluster_RadiusMin)
            myScript.Cluster_RadiusMax = radiusMax;
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Flatness");
        myScript.Cluster_Flatness = EditorGUILayout.Slider(myScript.Cluster_Flatness, 0, 1, GUILayout.Width(200));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("DensityMean");
        myScript.Cluster_DensityMean = EditorGUILayout.Slider(myScript.Cluster_DensityMean, 0, 1, GUILayout.Width(200));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("DensityDeviation");
        myScript.Cluster_DensityDeviation = EditorGUILayout.Slider(myScript.Cluster_DensityDeviation, 0, 1, GUILayout.Width(200));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginVertical();

        myScript.Cluster_UniformDeviation = EditorGUILayout.BeginToggleGroup("Enable Devivation", myScript.Cluster_UniformDeviation);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("DeviationX");
            myScript.Cluster_DeviationX = EditorGUILayout.Slider(myScript.Cluster_DeviationX, 0, 1, GUILayout.Width(200));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("DeviationY");
            myScript.Cluster_DeviationY = EditorGUILayout.Slider(myScript.Cluster_DeviationY, 0, 1, GUILayout.Width(200));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("DeviationZ");
            myScript.Cluster_DeviationZ = EditorGUILayout.Slider(myScript.Cluster_DeviationZ, 0, 1, GUILayout.Width(200));
            EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndToggleGroup();

        if(myScript.Cluster_UniformDeviation == false)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Deviation Unified");
            myScript.Cluster_DeviationX =
                myScript.Cluster_DeviationY =
                    myScript.Cluster_DeviationZ = EditorGUILayout.Slider(myScript.Cluster_DeviationX, 0, 1, GUILayout.Width(200));
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Stars In Nucleus");
        myScript.Cluster_StarsInNucleus = EditorGUILayout.Slider(myScript.Cluster_StarsInNucleus, 0, 1, GUILayout.Width(200));
        EditorGUILayout.EndHorizontal();

        EditorGUI.indentLevel--;

/*
        switch (myScript.GalaxyType)
        {
            case GalaxyGeneration.GALXAYTYPES.Cluster:
                {
                    EditorGUI.indentLevel++;

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("Cluster Min");
                    int cluserMin = (int)EditorGUILayout.Slider(myScript.Cluster_CountMin, 1, 50, GUILayout.Width(200));
                    if (cluserMin < myScript.Cluster_CountMax)
                        myScript.Cluster_CountMin = cluserMin;
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("Cluster Max");
                    int cluserMax = (int)EditorGUILayout.Slider(myScript.Cluster_CountMax, 1, 50, GUILayout.Width(200));
                    if (cluserMax > myScript.Cluster_CountMin)
                        myScript.Cluster_CountMax = cluserMax;
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("Cluster Radius Min");
                    float radiusMin = EditorGUILayout.Slider(myScript.Cluster_RadiusMin, 0, 1, GUILayout.Width(200));
                    if (radiusMin < myScript.Cluster_RadiusMax)
                        myScript.Cluster_RadiusMin = radiusMin;
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("Cluster Radius Max");
                    float radiusMax = EditorGUILayout.Slider(myScript.Cluster_RadiusMax, 0, 1, GUILayout.Width(200));
                    if (radiusMax > myScript.Cluster_RadiusMin)
                        myScript.Cluster_RadiusMax = radiusMax;
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("Flatness");
                    myScript.Cluster_Flatness = EditorGUILayout.Slider(myScript.Cluster_Flatness, 0, 1, GUILayout.Width(200));
                    EditorGUILayout.EndHorizontal();

                    EditorGUI.indentLevel--;
                }
                break;
            case GalaxyGeneration.GALXAYTYPES.Sphere:
                {
                    EditorGUI.indentLevel++;

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("Cluster Radius Min");
                    float radiusMin = EditorGUILayout.Slider(myScript.Sphere_RadiusMin, 0, 1, GUILayout.Width(200));
                    if (radiusMin < myScript.Sphere_RadiusMax)
                        myScript.Sphere_RadiusMin = radiusMin;
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("Cluster Radius Max");
                    float radiusMax = EditorGUILayout.Slider(myScript.Sphere_RadiusMax, 0, 1, GUILayout.Width(200));
                    if (radiusMax > myScript.Sphere_RadiusMin)
                        myScript.Sphere_RadiusMax = radiusMax;
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("Flatness");
                    myScript.Sphere_Flatness = EditorGUILayout.Slider(myScript.Sphere_Flatness, 0, 1, GUILayout.Width(200));
                    EditorGUILayout.EndHorizontal();


                    EditorGUI.indentLevel--;
                }
                break;
            case GalaxyGeneration.GALXAYTYPES.Spiral:
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
                    myScript.Spiral_ArmSpread = EditorGUILayout.Slider(myScript.Spiral_ArmSpread, 0, 5, GUILayout.Width(200));
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

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("DensityMean");
                    myScript.DensityMean = EditorGUILayout.Slider(myScript.DensityMean, 0, 1, GUILayout.Width(200));
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("DensityDeviation");
                    myScript.DensityDeviation = EditorGUILayout.Slider(myScript.DensityDeviation, 0, 1, GUILayout.Width(200));
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("DeviationX");
                    myScript.DeviationX = EditorGUILayout.Slider(myScript.DeviationX, 0, 1, GUILayout.Width(200));
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("DeviationY");
                    myScript.DeviationY = EditorGUILayout.Slider(myScript.DeviationY, 0, 1, GUILayout.Width(200));
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("DeviationZ");
                    myScript.DeviationZ = EditorGUILayout.Slider(myScript.DeviationZ, 0, 1, GUILayout.Width(200));
                    EditorGUILayout.EndHorizontal();

                    EditorGUI.indentLevel--;
                }
                break;
            default:
                break;

        }
*/
    }
}

