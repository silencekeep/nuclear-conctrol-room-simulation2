 using UnityEngine;
using UnityEditor;


namespace WorldSpaceTransitions
{
    [CustomEditor(typeof(SectionSetup))]
    public class SectionSetupEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector(); 
            SectionSetup setupScript = (SectionSetup)target;

            if (setupScript.model)
            {
                GUILayout.Space(10);
                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("Recalculate bounds of " + setupScript.model.name))
                {
                    setupScript.RecalculateBounds();
                }
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
                //GUILayout.Space(10);
                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("Check shaders on " + setupScript.model.name))
                {
                    setupScript.CheckShaders();
                }
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
            }
            if (setupScript.shaderSubstitutes.Count > 0)
            {
                GUILayout.Space(10);
                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();

                if (GUILayout.Button("Create and Assign Section Materials"))
                {
                    setupScript.CreateSectionMaterials();
                }
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
            }
        }
    }
}

