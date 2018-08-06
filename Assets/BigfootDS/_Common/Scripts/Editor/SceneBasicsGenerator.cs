using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using BigfootDS;

namespace BigfootDS
{

    public class SceneBasicsGenerator : EditorWindow
    {

        public GameObject objectsToSpawn;
        public string otsName = "New GameObject";
        public Vector3 otsPosition;
        public Vector3 otsRotation;
        public Vector3 otsScale = Vector3.one;
        static string otsTag = "Untagged";
        static int otsLayer = 0;
        public bool otsStatic = false;


        bool coreVariablesShowPosition = true;
        string coreVariablesFoldoutLabel = "Core Variables";

        bool extraComponentsVariables = false;
        string extraComponentsFoldoutLabel = "Extra Components";

        [MenuItem("BigfootDS/New Object Wizard")]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow(typeof(SceneBasicsGenerator));
        }


        void OnGUI()
        {
            titleContent = new GUIContent("Object Wizard");
            GUILayout.Label("New Object Variables", EditorStyles.boldLabel);

            coreVariablesShowPosition = EditorGUILayout.Foldout(coreVariablesShowPosition, coreVariablesFoldoutLabel);
            if (coreVariablesShowPosition)
            {

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PrefixLabel("Name: ");
                otsName = EditorGUILayout.TextField(otsName);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PrefixLabel("Is static? ");
                otsStatic = EditorGUILayout.Toggle(otsStatic);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PrefixLabel("Tag: ");
                otsTag = EditorGUILayout.TagField(otsTag);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PrefixLabel("Layer: ");
                otsLayer = EditorGUILayout.LayerField(otsLayer);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PrefixLabel("Position: ");
                otsPosition = EditorGUILayout.Vector3Field("", otsPosition);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PrefixLabel("Rotation: ");
                otsRotation = EditorGUILayout.Vector3Field("", otsRotation);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PrefixLabel("Scale: ");
                otsScale = EditorGUILayout.Vector3Field("", otsScale);
                EditorGUILayout.EndHorizontal();

            }

            EditorGUILayout.Space();

            extraComponentsVariables = EditorGUILayout.Foldout(extraComponentsVariables, extraComponentsFoldoutLabel);
            if (extraComponentsVariables)
            {
                EditorGUILayout.TextField("Test text filler");
            }

            EditorGUILayout.Space();
            EditorGUILayout.Space();

            if (GUILayout.Button("Spawn Object"))
            {
                GameObject newObject = new GameObject("Temp OBJ Name");
                newObject.name = otsName;
                newObject.tag = otsTag;
                newObject.layer = otsLayer;
                newObject.transform.position = otsPosition;
                newObject.transform.rotation = Quaternion.Euler(otsRotation);
                newObject.transform.localScale = otsScale;
                newObject.isStatic = otsStatic;

                Debug.Log("Made a new gameobject called " + newObject.name);
            }

        }

    }
}