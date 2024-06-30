using UnityEngine;
using System;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
using PiXYZ.PiXYZImportScript;
using UnityEditor.AnimatedValues;
#endif

namespace PiXYZ
{
    public class PiXYZLicenseManager : EditorWindow
    {
        public static string licenseInvalidityReason = "";
        string[] options = new string[] { };
        int index = 0;
        //PiXYZCredentialsPopup popup;
        string username = "";
        string password = "";
        int selectedTab = 0;
        bool connected = false;
        string connectionFailedMessage = "";
        AnimBool m_Informations;
        AnimBool showCredentials;
        AnimBool showOnlineTab;
        Plugin4UnityI.WebLicenseInfoList licensesList = null;

        [MenuItem("PiXYZ/License Manager", false, 51)]
        public static void Init()
        {
            PiXYZLicenseManager window = (PiXYZLicenseManager)EditorWindow.GetWindow(typeof(PiXYZLicenseManager), true, "PiXYZ License manager");
            window.position = new Rect(10000.0f, 0, 450.0f, 300.0f); //out of screen right
            window.maxSize = new Vector2(window.position.width, window.position.height);
            window.minSize = new Vector2(window.position.width, window.position.height);
            window.CenterOnMainWin();
            window.Show();
            try
            {
                PiXYZConfig.CheckLicense();
            }
            catch (Exception) { }
        }

        [MenuItem("PiXYZ/License Manager", true, 51)]
        public static bool CheckCompatibility()
        {
            string s = "";
            return PiXYZUtils.checkCompatibility(out s);
        }

        public static bool checkLicense()
        {
            try
            {
                if (Plugin4UnityI.checkLicense())
                    return true;
                else
                {
                    licenseInvalidityReason = "License/tokens invalid or connection failed";
                    return false;
                }
            }
            catch (Exception e)
            {
                licenseInvalidityReason = e.Message;
                return false;
            }
        }

        public static void resetLicenseState()
        {
            licenseInvalidityReason = "";
        }

        void OnEnable()
        {
        }

        void OnFocus()
        {
        }

        void onDestroy()
        {
        }

        void Awake()
        {
            m_Informations = new AnimBool(false);
            m_Informations.valueChanged.AddListener(Repaint);
            showCredentials = new AnimBool(true);
            showCredentials.valueChanged.AddListener(Repaint);
            showOnlineTab = new AnimBool(false);
            showOnlineTab.valueChanged.AddListener(Repaint);
        }

        void OnDestroy()
        {
            try
            {
                PiXYZImportScript.PiXYZ4UnityUtils.clear();
            }
            catch (Exception) { }
        }

        bool doRequest = false;
        bool doRelease = false;
        void OnGUI()
        {
            string[] titles = { "Current license", "Online", "Offline", "License server", "Tokens" };
            selectedTab = PiXYZUtils.Tabs(titles, selectedTab);

            switch (selectedTab)
            {
                case 0: //current
                    {
                        currentLicenseTab();
                        break;
                    }
                case 1: //online
                    {
                        showCredentials.target = !connected;
                        showOnlineTab.target = connected;
                        if (!connected)
                        {
                            if (EditorGUILayout.BeginFadeGroup(showCredentials.faded))
                            {
                                connected = creds();
                                EditorGUILayout.EndFadeGroup();
                            }
                        }
                        else
                        {
                            if (EditorGUILayout.BeginFadeGroup(showOnlineTab.faded))
                            {
                                onlineTab();
                                EditorGUILayout.EndFadeGroup();
                            }
                        }
                        break;
                    }
                case 2: //offline
                    {
                        offlineTab();
                        break;
                    }
                case 3: //server
                    {
                        licenseServer();
                        break;
                    }
                case 4: //tokens
                    {
                        tokens();
                        break;
                    }
            }
            //Outter calls
            if (doRelease)
            {
                doRelease = false;
                try
                {
                    Plugin4UnityI.releaseWebLicense(username, new Plugin4UnityI.Password(password), licensesList[index].id);
                    EditorUtility.DisplayDialog("Release complete", "The license release has been completed.", "Ok");
                }
                catch (Exception e)
                {
                    EditorUtility.DisplayDialog("Release failed", "An error has occured while releasing the license: " + e.Message, "Ok");
                    Plugin4UnityI.requestWebLicense(username, new Plugin4UnityI.Password(password), licensesList[index].id);
                }
            }
            else if (doRequest)
            {
                doRequest = false;
                try
                {
                    Plugin4UnityI.requestWebLicense(username, new Plugin4UnityI.Password(password), licensesList[index].id);
                    EditorUtility.DisplayDialog("Installation complete", "The license installation has been completed.", "Ok");
                }
                catch (Exception e)
                {
                    EditorUtility.DisplayDialog("Installation failed", "An error occured while installing the license: " + e.Message, "Ok");
                }
            }
        }

        void currentLicenseTab()
        {
            PiXYZAboutMenu.showLicenseInfos();
        }

        static bool waitEvent = true;
        static int lastIndex = -1;
        static int lastOptionLength = -1;
        void onlineTab()
        {
            if (lastIndex == -1)
                waitEvent = true;
            if (lastOptionLength != options.Length && Event.current.type != EventType.Layout)
                return;
            else if (lastOptionLength != options.Length && Event.current.type == EventType.Layout)
                lastOptionLength = options.Length;

            GUILayout.Space(10);
            EditorGUILayout.LabelField("Select your license", EditorStyles.boldLabel);
            GUILayout.BeginHorizontal();
            {
                EditorGUI.BeginDisabledGroup(options.Length < 1);
                {
                    if (Event.current.type == EventType.Layout) //Updates on layout event
                        lastIndex = index;
                    EditorStyles.popup.richText = true;
                    index = EditorGUILayout.Popup(index, options);
                    EditorStyles.popup.richText = false;
                    if (lastIndex != index)
                        waitEvent = true;
                }
                EditorGUI.EndDisabledGroup();
                GUILayout.Space(10);
                GUIStyle buttonStyle = new GUIStyle(EditorStyles.miniButton);
                if (GUILayout.Button("Refresh", buttonStyle, GUILayout.MaxWidth(Screen.width * 0.2f)))
                {
                    licensesList = Plugin4UnityI.retrieveAvailableLicenses(username, new Plugin4UnityI.Password(password));
                }
            }
            GUILayout.EndHorizontal();
            GUILayout.Space(5);
            bool installed = false;
            if (index < options.Length)
            {
                string productName, validity, licenseUse, currentlyInstalled;
                int usedIndex = index;
                if (waitEvent)
                {
                    if (Event.current.type != EventType.Layout)
                        usedIndex = lastIndex;
                    else
                        waitEvent = false;
                }
                DateTime validity_t = new DateTime(licensesList[usedIndex].validity.year, licensesList[usedIndex].validity.month, licensesList[usedIndex].validity.day);
                int daysRemaining = Math.Max(0, (validity_t - DateTime.Now).Days + 1);
                string remainingTextColor = daysRemaining > 185 ? "green" : daysRemaining > 92 ? "orange" : "red";
                installed = licensesList[usedIndex].onMachine;
                productName = licensesList[usedIndex].product;
                validity = PiXYZ4UnityUtils.convertPiXYZDateToString(licensesList[usedIndex].validity)
                    + "   (<color='" + remainingTextColor + "'><b>" + daysRemaining + "</b> Day" + (daysRemaining > 1 ? "s" : "") + " remaining</color>)";
                licenseUse = "" + (int)licensesList[usedIndex].inUse + " / " + (int)licensesList[usedIndex].count;
                currentlyInstalled = installed ? "<color='green'>true</color>" : "false";

                GUIStyle italic = new GUIStyle(GUI.skin.label);
                italic.fontStyle = FontStyle.Italic;
                EditorGUI.indentLevel = 1;
                EditorGUILayout.LabelField("License informations", italic);
                EditorGUI.indentLevel = 2;
                EditorStyles.label.richText = true;
                EditorGUILayout.LabelField("Product name: ", productName);
                EditorGUILayout.LabelField("Validity: ", validity);
                EditorGUILayout.LabelField("License use: ", licenseUse);
                EditorGUILayout.LabelField("Currently installed: ", currentlyInstalled);
                GUI.skin.label.richText = false;
                EditorGUI.indentLevel = 0;
            }
            else if (options.Length == 0)
            {
                GUIStyle labelStyle = new GUIStyle(EditorStyles.label);
                labelStyle.alignment = TextAnchor.MiddleCenter;
                labelStyle.fontStyle = FontStyle.Bold;
                labelStyle.normal.textColor = Color.red;
                GUILayout.BeginVertical();
                {
                    GUILayout.FlexibleSpace();
                    GUILayout.BeginHorizontal();
                    {
                        GUILayout.FlexibleSpace();
                        GUILayout.Label("No license available in your account.", labelStyle);
                        GUILayout.FlexibleSpace();
                    }
                    GUILayout.EndHorizontal();
                    GUILayout.FlexibleSpace();
                    GUILayout.Space(30);
                }
                GUILayout.EndVertical();
            }

            EditorGUI.BeginDisabledGroup(index >= options.Length);
            {
                GUIStyle btnContainerStyle = new GUIStyle();
                btnContainerStyle.margin.right = 5;
                GUILayout.BeginArea(new Rect(position.width * 0.05f, position.height - 30, position.width * 0.90f, 30), btnContainerStyle);
                {
                    GUILayout.BeginHorizontal();
                    string installName = installed ? "Reinstall" : "Install";
                    if (GUILayout.Button(installName))
                    {
                        //Unity don't like calls to precompiled function while in layout
                        // => delayed call to outside of layouts
                        doRequest = true;
                    }
                    if (installed)
                    {
                        GUILayout.Space(40);
                        if (GUILayout.Button("Release"))
                        {
                            if (EditorUtility.DisplayDialog("Warning", "Release (or uninstall) current license lets you install it on another computer. This action is available only once.\n\nAre you sure you want to release this license ?", "Yes", "No"))
                            {
                                //Unity don't like calls to precompiled function while in layout
                                // => delayed call to outside of layouts
                                doRelease = true;
                            }
                        }
                    }
                    GUILayout.EndHorizontal();
                }
                GUILayout.EndArea();
            }
            EditorGUI.EndDisabledGroup();

            if (licensesList != null && licensesList.Length() > 0)
            {
                options = new string[licensesList.Length()];
                for (int i = 0; i < licensesList.Length(); ++i)
                {
                    options[i] = "License " + (i + 1) + ": " + licensesList[i].product + "  [" + PiXYZ4UnityUtils.convertPiXYZDateToString(licensesList[i].validity) + "]";
                    if (licensesList[i].onMachine)
                        options[i] += "  (installed)";
                }
            }
        }

        void offlineTab()
        {
            float spacing = position.height * 0.15f;
            GUIStyle sheetStyle = new GUIStyle();
            sheetStyle.margin.right = 5;
            GUILayout.BeginArea(new Rect(position.width * 0.1f, position.height * 0.20f, position.width * 0.80f, position.height * 0.70f), sheetStyle);
            GUILayout.BeginVertical();
            GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
            buttonStyle.margin.right = 5;
            GUILayout.FlexibleSpace();
            GUILayout.Label("Generate an activation code and upload it on PiXYZ website");
            if (GUILayout.Button("Generate activation code", buttonStyle))
            {
                var path = EditorUtility.SaveFilePanel(
                     "Save activation code",
                     "",
                     "PiXYZ_activationCode.bin",
                     "Binary file;*.bin");

                if (path.Length != 0)
                {
                    try
                    {
                        Plugin4UnityI.generateActivationCode(new Plugin4UnityI.OutputFilePath(path));
                        EditorUtility.DisplayDialog("Generation succeed", "The activation code has been successfully generated.", "Ok");
                    }
                    catch (Exception e)
                    {
                        EditorUtility.DisplayDialog("Generation failed", "An error occured while generating the file: " + e.Message, "Ok");
                    }
                }

            }
            GUILayout.Space(spacing);
            GUILayout.Label("Install a new license");
            if (GUILayout.Button("Install license", buttonStyle))
            {
                var path = EditorUtility.OpenFilePanel(
                        "Open installation code (*.bin) or license file (*.lic)",
                        "",
                        "Install file;*.bin;*.lic");
                if (path.Length != 0)
                {
                    if (path.ToLower().EndsWith(".bin") || path.ToLower().EndsWith(".lic"))
                    {
                        try
                        {
                            Plugin4UnityI.installActivationCode(new Plugin4UnityI.OutputFilePath(path));
                            EditorUtility.DisplayDialog("Installation succeed", "The installation code has been installed.", "Ok");
                        }
                        catch (Exception e)
                        {
                            EditorUtility.DisplayDialog("Installation failed", "An error occured while installing: " + e.Message, "Ok");
                        }
                    }
                    else
                    {
                        EditorUtility.DisplayDialog("Error", "The file must be an installation code (bin file) or a license file (lic file)", "Ok");
                    }
                }
            }
            GUILayout.Space(spacing);
            GUILayout.Label("Generate a release code and upload it on PiXYZ website");
            if (GUILayout.Button("Generate release code", buttonStyle))
            {
                if (EditorUtility.DisplayDialog("Warning", "Release (or uninstall) current license lets you install it on another computer. This action is available only once.\n\nAre you sure you want to release this license ?", "Yes", "No"))
                {
                    var path = EditorUtility.SaveFilePanel(
                     "Save release code as BIN",
                     "",
                     "PiXYZ_releaseCode.bin",
                     "Binary file;*.bin");

                    if (path.Length != 0)
                    {
                        try
                        {
                            Plugin4UnityI.generateDeactivationCode(new Plugin4UnityI.OutputFilePath(path));
                            EditorUtility.DisplayDialog("Generation succeed", "The release code has been successfully generated.", "Ok");
                        }
                        catch (Exception e)
                        {
                            EditorUtility.DisplayDialog("Generation failed", "An error occured while generating the file: " + e.Message, "Ok");
                        }
                    }
                }
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndVertical();
            GUILayout.EndArea();
        }

        static string address = "";
        static string previousAddress = "";
        static ushort port = 0;
        static ushort previousPort = 0;
        static bool flexLM = false;
        static int previousFlexLM = -1;
        void licenseServer()
        {
            float spacing = position.height * 0.15f;
            GUIStyle sheetStyle = new GUIStyle();
            sheetStyle.margin.right = 5;
            Plugin4UnityI.getLicenseServerReturn ret = Plugin4UnityI.getLicenseServer();
            if (address == previousAddress) address = ret.serverHost;
            if (port == previousPort) port = ret.serverPort;
            if (previousFlexLM ==-1) flexLM = ret.useFlexLM;
            previousFlexLM = flexLM ? 1 : 0;
            GUILayout.BeginArea(new Rect(position.width * 0.1f, position.height * 0.20f, position.width * 0.80f, position.height * 0.70f), sheetStyle);
            {
                GUILayout.BeginVertical();
                {
                    GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
                    buttonStyle.margin.right = 5;
                    GUILayout.FlexibleSpace();
                    GUILayout.Label("Address");
                    address = GUILayout.TextField(address);
                    GUILayout.Space(spacing);
                    GUILayout.Label("Port");
                    var newPort = GUILayout.TextField(port.ToString());
                    ushort temp;
                    if (ushort.TryParse(newPort, out temp))
                    {
                        port = Math.Max((ushort)0, temp);
                    }
                    flexLM = GUILayout.Toggle(flexLM, "use FlexLM");
                    GUILayout.FlexibleSpace();

                    if (GUILayout.Button("Apply", buttonStyle))
                    {
                        previousPort = port;
                        previousAddress = address;
                        previousFlexLM = flexLM?1:0;
                        try
                        {
                            Plugin4UnityI.configureLicenseServer(address, (short)port, new Plugin4UnityI.Boolean(flexLM));
                            EditorUtility.DisplayDialog("Success", "License server has been successfuly configured", "Ok");
                            resetLicenseState();
                        }
                        catch (Exception e)
                        {
                            EditorUtility.DisplayDialog("License server error", e.Message, "Ok");
                        }
                    }
                    GUILayout.FlexibleSpace();
                }
                GUILayout.EndVertical();
            }
            GUILayout.EndArea();
        }

        static Vector2 _scrollViewPosition = new Vector2(0, 0);
        static bool allSelected = true;

        Plugin4UnityI.IntList checkTokensValidity(Plugin4UnityI.StringList tokens)
        {
            var results = new Plugin4UnityI.IntList(tokens.Length());
            for (int i = 0; i < tokens.Length(); ++i)
                try
                {
                    results[i] = Plugin4UnityI.isTokenValid(tokens[i]) ? 1 : 0;
                }
                catch(Exception e)
                {
                    results[i] = -1;
                    GUILayout.Label(e.Message);
                }
            needUpdateValidity = false;
            return results;
        }
        static bool needUpdateValidity = true;
        static Plugin4UnityI.IntList validity = new Plugin4UnityI.IntList();
        void tokens()
        {
            if (licenseInvalidityReason != "" || !checkLicense())
            {
                GUILayout.Label(licenseInvalidityReason);
                return;
            }

            bool newAllSelected = GUILayout.Toggle(allSelected, "Select all");
            bool selectAll = newAllSelected && !allSelected;
            bool deselectAll = !newAllSelected && allSelected;
            allSelected = true;
            _scrollViewPosition = GUILayout.BeginScrollView(_scrollViewPosition, GUILayout.MaxHeight(Screen.height - 30));
            {
                try
                {
                    var tokens = Plugin4UnityI.getTokens(false);
                    var mandatoryTokens = new List<Plugin4UnityI.String>(Plugin4UnityI.getTokens(true).list);
                    if(needUpdateValidity)
                        validity = checkTokensValidity(tokens);
                    for(int i=0;i<tokens.Length();++i)
                    {
                        string token = tokens[i];
                        bool valid = validity[i] == 1;
                        if (selectAll)
                        {
                            Plugin4UnityI.addWantedToken(token);
                            needUpdateValidity = true;
                        }
                        else if (deselectAll && mandatoryTokens.Contains(token))
                            Plugin4UnityI.removeWantedToken(token);

                        bool required = Plugin4UnityI.isTokenRequired(token);
                        if (required)
                        {
                            var oldColor = GUI.backgroundColor;

                            GUI.backgroundColor = valid ? Color.green : Color.red;

                            if (mandatoryTokens.Contains(token))
                            {
                                GUILayout.Toggle(true, token, "Button");
                            }
                            else if (!GUILayout.Toggle(true, token, "Button"))
                            {
                                Plugin4UnityI.removeWantedToken(token);
                                allSelected = false;
                            }

                            GUI.backgroundColor = oldColor;
                        }
                        else
                        {
                            allSelected = false;
                            Plugin4UnityI.releaseToken(token);
                            if (GUILayout.Toggle(false, token, "Button"))
                            {
                                Plugin4UnityI.addWantedToken(token);
                                needUpdateValidity = true;
                            }
                        }
                    }
                }
                catch(Exception e)
                {
                    GUILayout.Label(e.Message);
                }
                
            }
            GUILayout.EndScrollView();
        }

        bool creds()
        {
            GUILayout.BeginArea(new Rect(position.width * 0.05f, position.height * 0.36f, position.width * 0.90f, position.height * 0.24f));
            GUILayout.BeginHorizontal();
            GUILayout.Label("Username: ");
            username = EditorGUILayout.TextField("", username, GUILayout.MaxWidth(position.width / 2));
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("Password: ");
            password = EditorGUILayout.PasswordField("", password, GUILayout.MaxWidth(position.width / 2));
            GUILayout.EndHorizontal();
            EditorGUI.BeginDisabledGroup(username.Length == 0 || password.Length == 0);
            if (GUILayout.Button("Connect"))
            {
                try
                {
                    licensesList = Plugin4UnityI.retrieveAvailableLicenses(username, new Plugin4UnityI.Password(password));
                    return true;
                }
                catch (Exception e)
                {
                    connectionFailedMessage = e.Message;
                    m_Informations.target = true;
                }
            }
            EditorGUI.EndDisabledGroup();
            if (EditorGUILayout.BeginFadeGroup(m_Informations.faded))
            {
                EditorGUI.indentLevel++;
                GUIStyle a = new GUIStyle();
                a.normal.textColor = Color.red;
                if (connectionFailedMessage.Length != 0)
                {
                    if (connectionFailedMessage.Split('\n').Length > 1)
                        GUILayout.Label(connectionFailedMessage.Split('\n')[1].Trim(), a);
                    else
                        GUILayout.Label(connectionFailedMessage, a);
                }
                EditorGUI.indentLevel--;
                EditorGUILayout.EndFadeGroup();
            }
            GUILayout.EndArea();
            return false;
        }

        void setUsername(string uname)
        {
            username = uname;
        }

        void setPassword(string passwd)
        {
            password = passwd;
        }

    }
}