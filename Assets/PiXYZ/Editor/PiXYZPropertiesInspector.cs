#if UNITY_EDITOR
using UnityEditor;
using PiXYZ.PiXYZImportScript;
using UnityEngine;
#endif


namespace PiXYZ
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(PiXYZProperties), true)]
    public class PiXYZPropertiesInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            PiXYZProperties properties = target as PiXYZProperties;

            EditorGUILayout.BeginVertical();
            {
                for (int i = 0; i < properties.properties.names.Length(); ++i)
                {
                    EditorGUILayout.BeginHorizontal();
                    {
                        EditorGUILayout.LabelField(properties.properties.names[i]);
                        //EditorGUILayout.LabelField(properties.types[i].ToString());
                        EditorGUILayout.LabelField(properties.properties.values[i]);
                        GUILayout.FlexibleSpace();
                    }
                    EditorGUILayout.EndHorizontal();
                }
            }
            EditorGUILayout.EndVertical();
        }
    }
}
