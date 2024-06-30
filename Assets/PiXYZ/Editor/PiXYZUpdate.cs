using UnityEngine;
using System;
using System.Collections;
using System.Xml.Serialization;
using System.IO;
using System.Text.RegularExpressions;
#if UNITY_EDITOR
using UnityEditor;
using PiXYZ.PiXYZImportScript;
#endif


namespace PiXYZ
{
    //[InitializeOnLoad]
    public class PiXYZUpdate : EditorWindow
    {
        private static Plugin4UnityI.checkForUpdatesReturn updateStatus = null;
        private static bool _automaticUpdate = false;
        private static string errorMessage = "";
        public static PiXYZImportMenu _pixyzImport = null;

        [MenuItem("PiXYZ/Check For Update", false, 52)]
        public static void Display()
        {
            checkForUpdate(false);
            createWindow();
        }

        [MenuItem("PiXYZ/Check For Update", true, 52)]
        static bool checkCompatibility()
        {
            string s = "";
            return PiXYZUtils.checkCompatibility(out s);
        }

        public static void createWindow()
        {
            PiXYZUpdate window = (PiXYZUpdate)EditorWindow.GetWindow(typeof(PiXYZUpdate), true, "Check For Update");
            window.CenterOnMainWin();
            window.maxSize = new Vector2(window.position.width, window.position.height);
            window.minSize = new Vector2(window.position.width, window.position.height);

            window.ShowPopup();
            //window.coroutineScheduler.StartCoroutine(window.GetUpdatePageContent());
        }

        public static void checkForUpdate(bool automaticUpdate = true, PiXYZImportMenu pixyzImport = null)
        {
            try
            {
                _automaticUpdate = automaticUpdate;
                _pixyzImport = pixyzImport;
                PiXYZConfig.CheckLicense();
                updateStatus = Plugin4UnityI.checkForUpdates();
                if(updateStatus!=null)
                    if(automaticUpdate && updateStatus.newVersionAvailable)
                        createWindow();
                    else if (!automaticUpdate)
                        createWindow();
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
                return;
            }
        }

        void OnGUI()
        {
            GUILayout.BeginHorizontal();
            {
                GUILayout.FlexibleSpace();
                GUILayout.BeginVertical();
                {
                    GUILayout.FlexibleSpace();

                    if (updateStatus != null && updateStatus.newVersionAvailable)
                    {
                        EditorGUILayout.LabelField("A new version is available : " + updateStatus.newVersion, EditorStyles.wordWrappedLabel);
                        GUILayout.Space(20);
                        if (_automaticUpdate)
                        {
                            EditorGUILayout.BeginHorizontal();
                            {
                                GUILayout.FlexibleSpace();
                                EditorPrefs.SetBool("PiXYZ.AutoUpdate", !EditorGUILayout.Toggle("Do not show Again", !EditorPrefs.GetBool("PiXYZ.AutoUpdate")));
                                GUILayout.FlexibleSpace();
                            }
                            EditorGUILayout.EndHorizontal();
                        }


                        GUILayout.BeginHorizontal();
                        if (GUILayout.Button("Download"))
                        {
                            Application.OpenURL(updateStatus.newVersionLink);
                            this.Close();
                        }
                        if (GUILayout.Button("Later"))
                        {
                            this.Close();
                        }
                        GUILayout.EndHorizontal();
                    }
                    else if (errorMessage == "")
                    {
                        EditorGUILayout.LabelField("Your version is up to date", EditorStyles.wordWrappedLabel);
                        GUILayout.Space(20);
                        GUILayout.BeginHorizontal();
                        if (GUILayout.Button("Close"))
                        {
                            this.Close();
                        }
                        GUILayout.EndHorizontal();
                    }
                    else
                    {
                        EditorGUILayout.LabelField(errorMessage, EditorStyles.wordWrappedLabel);
                        GUILayout.Space(20);
                        GUILayout.BeginHorizontal();
                        if (GUILayout.Button("Retry"))
                        {
                            errorMessage = "";
                            checkForUpdate();
                        }

                        if (GUILayout.Button("Close"))
                        {
                            this.Close();
                            errorMessage = "";
                        }
                        GUILayout.EndHorizontal();
                    }
                    GUILayout.FlexibleSpace();
                }
                GUILayout.EndVertical();
                GUILayout.FlexibleSpace();
            }
            GUILayout.EndHorizontal();
        }
    }
}