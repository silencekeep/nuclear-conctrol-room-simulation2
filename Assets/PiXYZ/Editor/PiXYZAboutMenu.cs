using UnityEngine;
using System;
using System.Collections;
using System.Xml.Serialization;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
using PiXYZ.PiXYZImportScript;
#endif


namespace PiXYZ
{
    public class PiXYZAboutMenu : EditorWindow
    {
        [MenuItem("PiXYZ/About PiXYZ...", false, 54)]
        public static void Display()
        {
            PiXYZAboutMenu window = (PiXYZAboutMenu)EditorWindow.GetWindow(typeof(PiXYZAboutMenu), true, "About PiXYZ PLUGIN for Unity");
            window.position = new Rect((Screen.currentResolution.width - window.position.width) / 2,
                (Screen.currentResolution.height - window.position.height) / 2,
                430.0f,
                600.0f);
            window.maxSize = new Vector2(window.position.width, window.position.height);
            window.minSize = new Vector2(window.position.width, window.position.height);

            window.Show();
            try
            {
                 PiXYZConfig.CheckLicense();
            }
            catch(Exception e)
            {
                Debug.Log(e);
            }
        }

        [MenuItem("PiXYZ/Get A Sample Model", false, 23)]
        public static void GetSample()
        {
            Application.OpenURL(Plugin4UnityI.getPiXYZWebsiteURL() + "/download/");
        }

        [MenuItem("PiXYZ/Open Plugin Documentation", false, 22)]
        public static void OpenTutorialPDF()
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                Application.OpenURL(Application.dataPath + "/PiXYZ/Resources/[DOC]-PiXYZ-PLUGIN-for-Unity_2018_1.pdf");
                string text = "A PDF documentation will be open because you are currently offline (no internet connection). If you wish to access an up-to-date online documentation, please connect here: " + Plugin4UnityI.getPiXYZWebsiteURL() + "/documentations/PiXYZ4Unity/ ";
                if (!EditorPrefs.GetBool("PiXYZ.DoNotShowAgainDocumentationPopup", false))
                    EditorPrefs.SetBool("PiXYZ.DoNotShowAgainDocumentationPopup", EditorUtility.DisplayDialog("Internet not reachable", text, "Do not show again", "Ok"));
            }
            else
            {
                Application.OpenURL(Plugin4UnityI.getProductDocumentationURL());
            }
        }

        [MenuItem("PiXYZ/About PiXYZ...", true, 54)]
        [MenuItem("PiXYZ/Get A Sample Model", true, 23)]
        [MenuItem("PiXYZ/Open Plugin Documentation", true, 22)]
        public static bool CheckCompatibility()
        {
            string s = "";
            return PiXYZUtils.checkCompatibility(out s);
        }

        public static void showLicenseInfos(bool center = true)
        {
            EditorGUILayout.BeginVertical();
            {
                if (center)
                    GUILayout.FlexibleSpace();
                if (PiXYZLicenseManager.licenseInvalidityReason == "" && PiXYZLicenseManager.checkLicense())
                {
                    String[] names;
                    String[] values;
                    if (Plugin4UnityI.isFloatingLicense())
                    {
                        Plugin4UnityI.getLicenseServerReturn ret = Plugin4UnityI.getLicenseServer();
                        names = new string[]{
                        "License",
                        "",
                        "Server address",
                        "Port"
                        };
                        values = new string[] {
                        "Floating",
                        "",
                        ret.serverHost,
                        ((ushort)ret.serverPort).ToString()
                        };
                    }
                    else
                    {
                        names = new String[]{
                        "Start date",
                        "End date",
                        "Company name",
                        "Name",
                        "E-mail"
                    };
                        Plugin4UnityI.LicenseInfos info = Plugin4UnityI.getCurrentLicenseInfos();
                        values = new string[] {
                        PiXYZ4UnityUtils.convertPiXYZDateToString(info.startDate),
                        PiXYZ4UnityUtils.convertPiXYZDateToString(info.endDate),
                        info.customerCompany,
                        info.customerName,
                        info.customerEmail,
                        };
                    }
                    GUIStyle bold = new GUIStyle(EditorStyles.boldLabel);
                    GUIStyle labelStyle = new GUIStyle(EditorStyles.label);
                    labelStyle.alignment = TextAnchor.MiddleLeft;
                    labelStyle.fontSize = 10;
                    bold.alignment = TextAnchor.MiddleLeft;
                    bold.fontSize = 10;
                    PiXYZUtils.beginGroupBox("License informations");
                    for (int i = 0; i < names.Length; ++i)
                    {
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField(names[i].Length > 0 ? names[i] + ": " : "", labelStyle, GUILayout.Width((int)(Screen.width * 0.28)));
                        EditorGUILayout.LabelField(values[i], bold);
                        EditorGUILayout.EndHorizontal();
                    }
                    PiXYZUtils.endGroupBox();
                }
                else
                {
                    GUIStyle boldRed = new GUIStyle(EditorStyles.boldLabel);
                    boldRed.alignment = TextAnchor.MiddleCenter;
                    boldRed.fontSize = 18;
                    boldRed.wordWrap = true;
                    PiXYZUtils.beginGroupBox("");
                    {
                        EditorGUILayout.LabelField("");
                        EditorGUILayout.LabelField(PiXYZLicenseManager.licenseInvalidityReason.Trim().Replace("\t",""), boldRed);
                        EditorGUILayout.LabelField("");
                    }
                    PiXYZUtils.endGroupBox();
                }
                if (center)
                    GUILayout.FlexibleSpace();
            }
            EditorGUILayout.EndVertical();
        }

        void OnGUI()
        {
            EditorGUILayout.BeginVertical();

            Rect rectangle = new Rect(0, 0, position.width, position.width);
            GUI.DrawTexture(rectangle, Resources.Load("Icon/pixyz_banner", typeof(Texture2D)) as Texture2D);

            GUILayout.Space(position.width);
            showLicenseInfos(false);

            GUIStyle centeredBold = new GUIStyle(EditorStyles.boldLabel);
            centeredBold.alignment = TextAnchor.UpperCenter;
            EditorGUILayout.LabelField("Plugin version: " + Plugin4UnityI.getVersion(), centeredBold);

            GUIStyle boldRich = new GUIStyle(EditorStyles.boldLabel);
            boldRich.alignment = TextAnchor.MiddleCenter;
            boldRich.normal.textColor = Color.blue;
            //GUI.Label(new Rect(0, Screen.height * 3 / 4 - 10, Screen.width, Screen.height / 4), "Click to see Terms & Conditions", boldRich);
            string str = "Click to see Terms & Conditions";
            TextGenerationSettings settings = new TextGenerationSettings();
            settings.fontSize = boldRich.fontSize;
            settings.fontStyle = boldRich.fontStyle;
            settings.font = boldRich.font;
            settings.color = boldRich.normal.textColor;
            settings.pivot = Vector2.zero;
            if (GUILayout.Button(str, boldRich))
            {
                Application.OpenURL(Plugin4UnityI.getPiXYZWebsiteURL() + "/general-and-products-terms-and-conditions/");
            }
            TextGenerator a = new TextGenerator();
            Rect buttonRect = GUILayoutUtility.GetLastRect();
            EditorGUIUtility.AddCursorRect(buttonRect, MouseCursor.Link);
            Rect underlineRect = new Rect(buttonRect);
            underlineRect.width = a.GetPreferredWidth(str, settings);
            underlineRect.x = position.width / 2 - underlineRect.width / 2;
            underlineRect.y += underlineRect.height - 2;
            underlineRect.height = 1;
            PiXYZUtils.GUIDrawRect(underlineRect, Color.blue);


            GUIStyle italic = new GUIStyle();
            italic.fontStyle = FontStyle.Italic;
            italic.alignment = TextAnchor.MiddleCenter;
            italic.fontSize = 10;
            italic.wordWrap = true;
            EditorGUILayout.LabelField("PiXYZ Software solutions are edited by Metaverse Technologies France", italic);
   
            EditorGUILayout.EndVertical();
        }
    }
}