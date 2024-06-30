//#define Enable_Profiler

#if !UNITY_2019_1_OR_NEWER
#define VERYANIMATION_TIMELINE
#endif

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.Assertions;
using UnityEditor;

namespace VeryAnimation
{
    [Serializable]
    public class VeryAnimationEditorWindow : EditorWindow
    {
        public static VeryAnimationEditorWindow instance;

        private VeryAnimationWindow vaw { get { return VeryAnimationWindow.instance; } }
        private VeryAnimation va { get { return VeryAnimation.instance; } }

        #region GUI
        private bool editorPoseFoldout = true;
        private bool editorBlendPoseFoldout = true;
        private bool editorMuscleFoldout = true;
        private bool editorHandPoseFoldout = true;
        private bool editorBlendShapeFoldout = true;
        private bool editorSelectionFoldout = true;

        private bool editorPoseVisible = true;
        private bool editorBlendPoseVisible = true;
        private bool editorMuscleVisible = true;
        private bool editorHandPoseVisible = true;
        private bool editorBlendShapeVisible = true;
        private bool editorSelectionVisible = true;

        public bool editorSelectionOnScene { get; private set; }

        private bool editorPoseHelp;
        private bool editorBlendPoseGroupHelp;
        private bool editorMuscleGroupHelp;
        private bool editorHandPoseHelp;
        private bool editorBlendShapeGroupHelp;
        private bool editorSelectionHelp;
        #endregion

        #region Strings
        private GUIContent[] RootCorrectionModeString = new GUIContent[(int)VeryAnimation.RootCorrectionMode.Total];
        #endregion

        #region Core
        [SerializeField]
        private BlendPoseTree blendPoseTree;
        [SerializeField]
        private MuscleGroupTree muscleGroupTree;
        [SerializeField]
        private HandPoseTree handPoseTree;
        [SerializeField]
        private BlendShapeTree blendShapeTree;
        #endregion

        private bool initialized;

        private Vector2 editorScrollPosition;

        private const int QuickSaveSize = 3;
        private PoseTemplate[] quickSaves;

        public string templateSaveDefaultDirectory { get; set; }

        void OnEnable()
        {
            if (vaw == null || va == null) return;

            instance = this;

            templateSaveDefaultDirectory = Application.dataPath;

            UpdateRootCorrectionModeString();
            Language.OnLanguageChanged += UpdateRootCorrectionModeString;

            titleContent = new GUIContent("VA Editor");
        }
        void OnDisable()
        {
            if (vaw == null || va == null) return;

            Release();

            instance = null;

            if (vaw != null)
            {
                vaw.Release();
            }
        }
        void OnDestroy()
        {
            if (vaw != null)
            {
                vaw.Release();
            }
        }

        public void Initialize()
        {
            Release();

            blendPoseTree = new BlendPoseTree();
            muscleGroupTree = new MuscleGroupTree();
            handPoseTree = new HandPoseTree();
            blendShapeTree = new BlendShapeTree();

            #region EditorPref
            {
                editorPoseFoldout = EditorPrefs.GetBool("VeryAnimation_Editor_Pose", true);
                editorBlendPoseFoldout = EditorPrefs.GetBool("VeryAnimation_Editor_BlendPose", false);
                editorMuscleFoldout = EditorPrefs.GetBool("VeryAnimation_Editor_Muscle", false);
                editorHandPoseFoldout = EditorPrefs.GetBool("VeryAnimation_Editor_HandPose", true);
                editorBlendShapeFoldout = EditorPrefs.GetBool("VeryAnimation_Editor_BlendShape", true);
                editorSelectionFoldout = EditorPrefs.GetBool("VeryAnimation_Editor_Selection", true);
                
                editorPoseVisible = EditorPrefs.GetBool("VeryAnimation_Editor_PoseVisible", true);
                editorBlendPoseVisible = EditorPrefs.GetBool("VeryAnimation_Editor_BlendPoseVisible", false);
                editorMuscleVisible = EditorPrefs.GetBool("VeryAnimation_Editor_MuscleVisible", false);
                editorHandPoseVisible = EditorPrefs.GetBool("VeryAnimation_Editor_HandPoseVisible", true);
                editorBlendShapeVisible = EditorPrefs.GetBool("VeryAnimation_Editor_BlendShapeVisible", true);
                editorSelectionVisible = EditorPrefs.GetBool("VeryAnimation_Editor_SelectionVisible", true);

                editorSelectionOnScene = EditorPrefs.GetBool("VeryAnimation_Editor_Selection_OnScene", false);

                va.clampMuscle = EditorPrefs.GetBool("VeryAnimation_ClampMuscle", false);
                va.autoFootIK = EditorPrefs.GetBool("VeryAnimation_AutoFootIK", false);
                va.mirrorEnable = EditorPrefs.GetBool("VeryAnimation_MirrorEnable", false);
                va.collisionEnable = EditorPrefs.GetBool("VeryAnimation_CollisionEnable", false);
                va.rootCorrectionMode = (VeryAnimation.RootCorrectionMode)EditorPrefs.GetInt("VeryAnimation_RootCorrectionMode", (int)VeryAnimation.RootCorrectionMode.Single);
                muscleGroupTree.LoadEditorPref();
                handPoseTree.LoadEditorPref();
                blendShapeTree.LoadEditorPref();
            }
            #endregion

            initialized = true;
        }
        private void Release()
        {
            if (!initialized) return;

            #region EditorPref
            {
                EditorPrefs.SetBool("VeryAnimation_Editor_Pose", editorPoseFoldout);
                EditorPrefs.SetBool("VeryAnimation_Editor_BlendPose", editorBlendPoseFoldout);
                EditorPrefs.SetBool("VeryAnimation_Editor_Muscle", editorMuscleFoldout);
                EditorPrefs.SetBool("VeryAnimation_Editor_HandPose", editorHandPoseFoldout);
                EditorPrefs.SetBool("VeryAnimation_Editor_BlendShape", editorBlendShapeFoldout);
                EditorPrefs.SetBool("VeryAnimation_Editor_Selection", editorSelectionFoldout);

                EditorPrefs.SetBool("VeryAnimation_Editor_PoseVisible", editorPoseVisible);
                EditorPrefs.SetBool("VeryAnimation_Editor_BlendPoseVisible", editorBlendPoseVisible);
                EditorPrefs.SetBool("VeryAnimation_Editor_MuscleVisible", editorMuscleVisible);
                EditorPrefs.SetBool("VeryAnimation_Editor_HandPoseVisible", editorHandPoseVisible);
                EditorPrefs.SetBool("VeryAnimation_Editor_BlendShapeVisible", editorBlendShapeVisible);
                EditorPrefs.SetBool("VeryAnimation_Editor_SelectionVisible", editorSelectionVisible);

                EditorPrefs.SetBool("VeryAnimation_ClampMuscle", va.clampMuscle);
                EditorPrefs.SetBool("VeryAnimation_AutoFootIK", va.autoFootIK);
                EditorPrefs.SetBool("VeryAnimation_MirrorEnable", va.mirrorEnable);
                EditorPrefs.SetBool("VeryAnimation_CollisionEnable", va.collisionEnable);
                EditorPrefs.SetInt("VeryAnimation_RootCorrectionMode", (int)va.rootCorrectionMode);
                muscleGroupTree.SaveEditorPref();
                handPoseTree.SaveEditorPref();
                blendShapeTree.SaveEditorPref();
            }
            #endregion
        }

        void OnInspectorUpdate()
        {
            if (vaw == null || va == null || !vaw.initialized || VeryAnimationControlWindow.instance == null)
            {
                Close();
                return;
            }
        }

        void OnGUI()
        {
            if (va == null || !va.edit || va.isError || !vaw.guiStyleReady)
                return;

#if Enable_Profiler
            Profiler.BeginSample("****VeryAnimationEditorWindow.OnGUI");
#endif
            Event e = Event.current;

            #region Event
            switch (e.type)
            {
            case EventType.KeyDown:
                if (focusedWindow == this)
                    va.HotKeys();
                break;
            case EventType.MouseUp:
                SceneView.RepaintAll();
                break;
            }
            va.Commands();
            #endregion

            #region ToolBar
            {
                EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);
                if (editorPoseVisible)
                {
                    editorPoseFoldout = GUILayout.Toggle(editorPoseFoldout, "Pose", EditorStyles.toolbarButton);
                }
                if (editorBlendPoseVisible)
                {
                    editorBlendPoseFoldout = GUILayout.Toggle(editorBlendPoseFoldout, "Blend Pose", EditorStyles.toolbarButton);
                }
                if (va.isHuman && editorMuscleVisible)
                {
                    editorMuscleFoldout = GUILayout.Toggle(editorMuscleFoldout, "Muscle Group", EditorStyles.toolbarButton);
                }
                if (va.isHuman && va.humanoidHasLeftHand && va.humanoidHasRightHand && editorHandPoseVisible)
                {
                    editorHandPoseFoldout = GUILayout.Toggle(editorHandPoseFoldout, "Hand Pose", EditorStyles.toolbarButton);
                }
                if (blendShapeTree.IsHaveBlendShapeNodes() && editorBlendShapeVisible)
                {
                    editorBlendShapeFoldout = GUILayout.Toggle(editorBlendShapeFoldout, "Blend Shape", EditorStyles.toolbarButton);
                }
                if (editorSelectionVisible)
                {
                    editorSelectionFoldout = GUILayout.Toggle(editorSelectionFoldout, "Selection", EditorStyles.toolbarButton);
                }
                {
                    if (EditorGUILayout.DropdownButton(vaw.uEditorGUI.GetTitleSettingsIcon(), FocusType.Passive, vaw.guiStyleIconButton, GUILayout.Width(19)))
                    {
                        GenericMenu menu = new GenericMenu();
                        menu.AddItem(new GUIContent("Pose"), editorPoseVisible, () => { editorPoseVisible = !editorPoseVisible; editorPoseFoldout = editorPoseVisible; });
                        menu.AddItem(new GUIContent("Blend Pose"), editorBlendPoseVisible, () => { editorBlendPoseVisible = !editorBlendPoseVisible; editorBlendPoseFoldout = editorBlendPoseVisible; });
                        if (va.isHuman)
                            menu.AddItem(new GUIContent("Muscle Group"), editorMuscleVisible, () => { editorMuscleVisible = !editorMuscleVisible; editorMuscleFoldout = editorMuscleVisible; });
                        if (va.isHuman && va.humanoidHasLeftHand && va.humanoidHasRightHand)
                            menu.AddItem(new GUIContent("Hand Pose"), editorHandPoseVisible, () => { editorHandPoseVisible = !editorHandPoseVisible; editorHandPoseFoldout = editorHandPoseVisible; });
                        if (blendShapeTree.IsHaveBlendShapeNodes())
                            menu.AddItem(new GUIContent("Blend Shape"), editorBlendShapeVisible, () => { editorBlendShapeVisible = !editorBlendShapeVisible; editorBlendShapeFoldout = editorBlendShapeVisible; });
                        menu.AddItem(new GUIContent("Selection"), editorSelectionVisible, () => { editorSelectionVisible = !editorSelectionVisible; editorSelectionFoldout = editorSelectionVisible; });
                        menu.ShowAsContext();
                    }
                }
                EditorGUILayout.EndHorizontal();
            }
            #endregion

            if (va.isHuman)
                HumanoidEditorGUI();
            else
                GenericEditorGUI();

#if Enable_Profiler
            Profiler.EndSample();
#endif
        }

        private void HumanoidEditorGUI()
        {
            Event e = Event.current;
            #region Tools
            {
                EditorGUILayout.BeginHorizontal();
                {
                    EditorGUILayout.LabelField("Options", EditorStyles.miniLabel, GUILayout.Width(48f));
                    {
                        EditorGUI.BeginChangeCheck();
                        var flag = GUILayout.Toggle(va.clampMuscle, Language.GetContent(Language.Help.EditorOptionsClamp), EditorStyles.miniButton);
                        if (EditorGUI.EndChangeCheck())
                        {
                            Undo.RecordObject(this, "Change Clamp");
                            va.clampMuscle = flag;
                        }
                    }
                    {
#if UNITY_2018_3_OR_NEWER
                        var help = Language.Help.EditorOptionsFootIK_2018_3;
#else
                        var help = Language.Help.EditorOptionsFootIK;
#endif
                        if (va.uAw.GetLinkedWithTimeline())
                        {
#if VERYANIMATION_TIMELINE
                            EditorGUI.BeginDisabledGroup(true);
                            GUILayout.Toggle(va.uAw.GetTimelineAnimationApplyFootIK(), Language.GetContent(help), EditorStyles.miniButton);
                            EditorGUI.EndDisabledGroup();
#else
                            Assert.IsTrue(false);
#endif
                        }
                        else
                        {
                            EditorGUI.BeginChangeCheck();
                            var flag = GUILayout.Toggle(va.autoFootIK, Language.GetContent(help), EditorStyles.miniButton);
                            if (EditorGUI.EndChangeCheck())
                            {
                                Undo.RecordObject(this, "Change Foot IK");
                                va.autoFootIK = flag;
                                va.SetUpdateSampleAnimation();
                                va.SetSynchroIKtargetAll();
                                va.SetAnimationWindowSynchroSelection();
                            }
                        }
                    }
                    {
                        EditorGUI.BeginChangeCheck();
                        var flag = GUILayout.Toggle(va.mirrorEnable, Language.GetContent(Language.Help.EditorOptionsMirror), EditorStyles.miniButton);
                        if (EditorGUI.EndChangeCheck())
                        {
                            Undo.RecordObject(this, "Change Mirror");
                            va.mirrorEnable = flag;
                            va.SetAnimationWindowSynchroSelection();
                        }
                    }
                    {
                        EditorGUI.BeginChangeCheck();
                        var flag = GUILayout.Toggle(va.collisionEnable, Language.GetContent(Language.Help.EditorOptionsCollision), EditorStyles.miniButton);
                        if (EditorGUI.EndChangeCheck())
                        {
                            Undo.RecordObject(this, "Change Collision");
                            va.collisionEnable = flag;
                        }
                    }
                }
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal();
                {
                    EditorGUILayout.LabelField(Language.GetContent(Language.Help.EditorRootCorrection), EditorStyles.miniLabel, GUILayout.Width(88f));
                    {
                        EditorGUI.BeginChangeCheck();
                        var mode = (VeryAnimation.RootCorrectionMode)GUILayout.Toolbar((int)va.rootCorrectionMode, RootCorrectionModeString, EditorStyles.miniButton);
                        if (EditorGUI.EndChangeCheck())
                        {
                            Undo.RecordObject(this, "Change Root Correction Mode");
                            va.rootCorrectionMode = mode;
                            va.SetAnimationWindowSynchroSelection();
                        }
                    }
                }
                EditorGUILayout.EndHorizontal();
            }
            #endregion

            editorScrollPosition = EditorGUILayout.BeginScrollView(editorScrollPosition);

            EditorGUI_PoseGUI();

            EditorGUI_BlendPoseGUI();

            EditorGUI_MuscleGroupGUI();

            EditorGUI_HandPoseGUI();

            EditorGUI_BlendShapeGUI();

            EditorGUI_SelectionGUI();

            EditorGUILayout.EndScrollView();
        }
        private void GenericEditorGUI()
        {
            Event e = Event.current;
            #region Tools
            {
                EditorGUILayout.BeginHorizontal();
                {
                    EditorGUILayout.LabelField("Options", GUILayout.Width(52f));
                    {
                        EditorGUI.BeginChangeCheck();
                        var flag = GUILayout.Toggle(va.mirrorEnable, Language.GetContent(Language.Help.EditorOptionsMirror), EditorStyles.miniButton);
                        if (EditorGUI.EndChangeCheck())
                        {
                            Undo.RecordObject(this, "Change Mirror");
                            va.mirrorEnable = flag;
                        }
                    }
                    {
                        EditorGUI.BeginChangeCheck();
                        var flag = GUILayout.Toggle(va.collisionEnable, Language.GetContent(Language.Help.EditorOptionsCollision), EditorStyles.miniButton);
                        if (EditorGUI.EndChangeCheck())
                        {
                            Undo.RecordObject(this, "Change Collision");
                            va.collisionEnable = flag;
                        }
                    }
                }
                EditorGUILayout.EndHorizontal();
            }
            #endregion

            editorScrollPosition = EditorGUILayout.BeginScrollView(editorScrollPosition);

            EditorGUI_PoseGUI();

            EditorGUI_BlendPoseGUI();

            EditorGUI_BlendShapeGUI();

            EditorGUI_SelectionGUI();

            EditorGUILayout.EndScrollView();
        }
        private void EditorGUI_PoseGUI()
        {
            {
                if (editorPoseFoldout && editorPoseVisible)
                {
                    EditorGUILayout.BeginHorizontal();
                    {
                        editorPoseFoldout = EditorGUILayout.Foldout(editorPoseFoldout, "Pose", true, vaw.guiStyleBoldFoldout);
                    }
                    EditorGUILayout.Space();
                    if (GUILayout.Button(vaw.uEditorGUI.GetHelpIcon(), editorPoseHelp ? vaw.guiStyleIconActiveButton : vaw.guiStyleIconButton, GUILayout.Width(19)))
                    {
                        editorPoseHelp = !editorPoseHelp;
                    }
                    EditorGUILayout.EndHorizontal();

                    if (editorPoseHelp)
                    {
                        EditorGUILayout.HelpBox(Language.GetText(Language.Help.HelpPose), MessageType.Info);
                    }

                    EditorGUILayout.BeginVertical(vaw.guiStyleSkinBox);
                    {
                        EditorGUILayout.BeginHorizontal();
                        #region Set
                        if (GUILayout.Button("Reset", vaw.guiStyleDropDown))
                        {
                            GenericMenu menu = new GenericMenu();
                            {
                                if (va.isHuman)
                                {
                                    menu.AddItem(Language.GetContent(Language.Help.EditorPoseHumanoidReset), false, () =>
                                    {
                                        Undo.RecordObject(this, "Humanoid Pose");
                                        va.SetPoseHumanoidDefault();
                                    });
                                }
                                if (va.transformPoseSave.IsEnableTPoseTransform())
                                {
                                    menu.AddItem(Language.GetContent(Language.Help.EditorPoseTPose), false, () =>
                                    {
                                        Undo.RecordObject(this, "T Pose");
                                        va.SetPoseTPose();
                                    });
                                }
                                if (va.transformPoseSave.IsEnableBindTransform())
                                {
                                    menu.AddItem(Language.GetContent(Language.Help.EditorPoseBind), false, () =>
                                    {
                                        Undo.RecordObject(this, "Bind Pose");
                                        va.SetPoseBind();
                                    });
                                }
                                if (va.transformPoseSave.IsEnablePrefabTransform())
                                {
                                    menu.AddItem(Language.GetContent(Language.Help.EditorPosePrefab), false, () =>
                                    {
                                        Undo.RecordObject(this, "Prefab Pose");
                                        va.SetPosePrefab();
                                    });
                                }
                                {
                                    menu.AddItem(Language.GetContent(Language.Help.EditorPoseStart), false, () =>
                                    {
                                        Undo.RecordObject(this, "Edit Start Pose");
                                        va.SetPoseEditStart();
                                    });
                                }
                            }
                            menu.ShowAsContext();
                        }
                        #endregion
                        #region Mirror
                        if (GUILayout.Button(Language.GetContent(Language.Help.EditorPoseMirror)))
                        {
                            Undo.RecordObject(this, "Mirror Pose");
                            va.SetPoseMirror();
                        }
                        #endregion
                        #region Template
                        if (GUILayout.Button(Language.GetContent(Language.Help.EditorPoseTemplate), vaw.guiStyleDropDown))
                        {
                            Dictionary<string, string> poseTemplates = new Dictionary<string, string>();
                            {
                                var guids = AssetDatabase.FindAssets("t:posetemplate");
                                for (int i = 0; i < guids.Length; i++)
                                {
                                    var path = AssetDatabase.GUIDToAssetPath(guids[i]);
                                    var name = path.Remove(0, "Assets/".Length);
                                    poseTemplates.Add(name, path);
                                }
                            }

                            GenericMenu menu = new GenericMenu();
                            {
                                var enu = poseTemplates.GetEnumerator();
                                while (enu.MoveNext())
                                {
                                    var value = enu.Current.Value;
                                    menu.AddItem(new GUIContent(enu.Current.Key), false, () =>
                                    {
                                        var poseTemplate = AssetDatabase.LoadAssetAtPath<PoseTemplate>(value);
                                        if (poseTemplate != null)
                                        {
                                            Undo.RecordObject(this, "Template Pose");
                                            Undo.RegisterCompleteObjectUndo(va.currentClip, "Template Pose");
                                            va.LoadPoseTemplate(poseTemplate);
                                        }
                                        else
                                        {
                                            Debug.LogErrorFormat(Language.GetText(Language.Help.LogFailedLoadPoseError), value);
                                        }
                                    });
                                }
                            }
                            menu.ShowAsContext();
                        }
                        #endregion
                        EditorGUILayout.Space();
                        #region Save as
                        if (GUILayout.Button(Language.GetContent(Language.Help.EditorPoseSaveAs)))
                        {
                            string path = EditorUtility.SaveFilePanel("Save as Pose Template", templateSaveDefaultDirectory, string.Format("{0}.asset", va.currentClip.name), "asset");
                            if (!string.IsNullOrEmpty(path))
                            {
                                if (!path.StartsWith(Application.dataPath))
                                {
                                    EditorCommon.SaveInsideAssetsFolderDisplayDialog();
                                }
                                else
                                {
                                    templateSaveDefaultDirectory = Path.GetDirectoryName(path);
                                    path = FileUtil.GetProjectRelativePath(path);
                                    var poseTemplate = ScriptableObject.CreateInstance<PoseTemplate>();
                                    va.SavePoseTemplate(poseTemplate);
                                    try
                                    {
                                        VeryAnimationWindow.CustomAssetModificationProcessor.Pause();
                                        AssetDatabase.CreateAsset(poseTemplate, path);
                                    }
                                    finally
                                    {
                                        VeryAnimationWindow.CustomAssetModificationProcessor.Resume();
                                    }
                                    Focus();
                                }
                            }
                        }
                        #endregion
                        EditorGUILayout.EndHorizontal();
                        GUILayout.Space(4);
                        EditorGUILayout.BeginHorizontal();
                        {
                            EditorGUILayout.LabelField("Quick Load", GUILayout.Width(70));
                            Action<int> QuickLoad = (index) =>
                            {
                                EditorGUI.BeginDisabledGroup(quickSaves == null || index >= quickSaves.Length || quickSaves[index] == null);
                                if (GUILayout.Button((index + 1).ToString()))
                                {
                                    Undo.RecordObject(this, "Quick Load");
                                    Undo.RegisterCompleteObjectUndo(va.currentClip, "Quick Load");
                                    va.LoadPoseTemplate(quickSaves[index]);
                                }
                                EditorGUI.EndDisabledGroup();
                            };
                            for (int i = 0; i < QuickSaveSize; i++)
                            {
                                QuickLoad(i);
                            }
                            EditorGUILayout.Space();
                            EditorGUILayout.LabelField("Quick Save", GUILayout.Width(70));
                            Action<int> QuickSave = (index) =>
                            {
                                if (GUILayout.Button((index + 1).ToString()))
                                {
                                    Undo.RecordObject(this, "Quick Save");
                                    if (quickSaves == null || quickSaves.Length != QuickSaveSize)
                                        quickSaves = new PoseTemplate[QuickSaveSize];
                                    {
                                        quickSaves[index] = ScriptableObject.CreateInstance<PoseTemplate>();
                                        va.SavePoseTemplate(quickSaves[index]);
                                    }
                                }
                            };
                            for (int i = 0; i < QuickSaveSize; i++)
                            {
                                QuickSave(i);
                            }
                        }
                        EditorGUILayout.EndHorizontal();
                    }
                    GUILayout.Space(3);
                    EditorGUILayout.EndVertical();
                }
            }
        }
        private void EditorGUI_BlendPoseGUI()
        {
            if (editorBlendPoseFoldout && editorBlendPoseVisible)
            {
                EditorGUILayout.BeginHorizontal();
                {
                    editorBlendPoseFoldout = EditorGUILayout.Foldout(editorBlendPoseFoldout, "Blend Pose", true, vaw.guiStyleBoldFoldout);
                }
                {
                    EditorGUILayout.Space();
                    blendPoseTree.BlendPoseTreeToolbarGUI();
                    EditorGUILayout.Space();
                    if (GUILayout.Button(vaw.uEditorGUI.GetHelpIcon(), editorBlendPoseGroupHelp ? vaw.guiStyleIconActiveButton : vaw.guiStyleIconButton, GUILayout.Width(19)))
                    {
                        editorBlendPoseGroupHelp = !editorBlendPoseGroupHelp;
                    }
                }
                EditorGUILayout.EndHorizontal();

                if (editorBlendPoseGroupHelp)
                {
                    EditorGUILayout.HelpBox(Language.GetText(Language.Help.HelpBlendPose), MessageType.Info);
                }

                blendPoseTree.BlendPoseTreeGUI();
            }
        }
        private void EditorGUI_MuscleGroupGUI()
        {
            if (editorMuscleFoldout && editorMuscleVisible)
            {
                EditorGUILayout.BeginHorizontal();
                {
                    editorMuscleFoldout = EditorGUILayout.Foldout(editorMuscleFoldout, "Muscle Group", true, vaw.guiStyleBoldFoldout);
                }
                {
                    EditorGUILayout.Space();
                    muscleGroupTree.MuscleGroupToolbarGUI();
                    EditorGUILayout.Space();
                    if (GUILayout.Button(vaw.uEditorGUI.GetHelpIcon(), editorMuscleGroupHelp ? vaw.guiStyleIconActiveButton : vaw.guiStyleIconButton, GUILayout.Width(19)))
                    {
                        editorMuscleGroupHelp = !editorMuscleGroupHelp;
                    }
                }
                EditorGUILayout.EndHorizontal();

                if (editorMuscleGroupHelp)
                {
                    EditorGUILayout.HelpBox(Language.GetText(Language.Help.HelpMuscleGroup), MessageType.Info);
                }

                muscleGroupTree.MuscleGroupTreeGUI();
            }
        }
        private void EditorGUI_HandPoseGUI()
        {
            if (!va.isHuman || !va.humanoidHasLeftHand || !va.humanoidHasRightHand)
                return;

            if (editorHandPoseFoldout && editorHandPoseVisible)
            {
                EditorGUILayout.BeginHorizontal();
                {
                    editorHandPoseFoldout = EditorGUILayout.Foldout(editorHandPoseFoldout, "Hand Pose", true, vaw.guiStyleBoldFoldout);
                }
                {
                    EditorGUILayout.Space();
                    handPoseTree.HandPoseToolbarGUI();
                    EditorGUILayout.Space();
                    if (GUILayout.Button(vaw.uEditorGUI.GetHelpIcon(), editorHandPoseHelp ? vaw.guiStyleIconActiveButton : vaw.guiStyleIconButton, GUILayout.Width(19)))
                    {
                        editorHandPoseHelp = !editorHandPoseHelp;
                    }
                }
                EditorGUILayout.EndHorizontal();

                if (editorHandPoseHelp)
                {
                    EditorGUILayout.HelpBox(Language.GetText(Language.Help.HelpHandPose), MessageType.Info);
                }

                handPoseTree.HandPoseTreeGUI();
            }
        }
        private void EditorGUI_BlendShapeGUI()
        {
            if (!blendShapeTree.IsHaveBlendShapeNodes())
                return;

            if (editorBlendShapeFoldout && editorBlendShapeVisible)
            {
                EditorGUILayout.BeginHorizontal();
                {
                    editorBlendShapeFoldout = EditorGUILayout.Foldout(editorBlendShapeFoldout, "Blend Shape", true, vaw.guiStyleBoldFoldout);
                }
                {
                    EditorGUILayout.Space();
                    blendShapeTree.BlendShapeTreeToolbarGUI();
                    EditorGUILayout.Space();
                    if (GUILayout.Button(vaw.uEditorGUI.GetHelpIcon(), editorBlendShapeGroupHelp ? vaw.guiStyleIconActiveButton : vaw.guiStyleIconButton, GUILayout.Width(19)))
                    {
                        editorBlendShapeGroupHelp = !editorBlendShapeGroupHelp;
                    }
                    if (EditorGUILayout.DropdownButton(vaw.uEditorGUI.GetTitleSettingsIcon(), FocusType.Passive, vaw.guiStyleIconButton, GUILayout.Width(19)))
                    {
                        blendShapeTree.BlendShapeTreeSettingsMesh();
                    }
                }
                EditorGUILayout.EndHorizontal();

                if (editorBlendShapeGroupHelp)
                {
                    EditorGUILayout.HelpBox(Language.GetText(Language.Help.HelpBlendShape), MessageType.Info);
                }

                blendShapeTree.BlendShapeTreeGUI();
            }
        }
        public void EditorGUI_SelectionGUI(bool onScene = false)
        {
            const int FoldoutSpace = 17;
            const int FloatFieldWidth = 44;

            if (editorSelectionFoldout && editorSelectionVisible && onScene == editorSelectionOnScene)
            {
                EditorGUILayout.BeginHorizontal();
                if (!onScene)
                {
                    editorSelectionFoldout = EditorGUILayout.Foldout(editorSelectionFoldout, "Selection", true, vaw.guiStyleBoldFoldout);
                }
                if (va.selectionActiveGameObject != null)
                {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField(va.selectionActiveGameObject, typeof(GameObject), false);
                    EditorGUI.EndDisabledGroup();
                }
                else if (va.animatorIK.ikActiveTarget != AnimatorIKCore.IKTarget.None && va.animatorIK.ikData[(int)va.animatorIK.ikActiveTarget].enable)
                {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.LabelField("Animator IK: " + AnimatorIKCore.IKTargetStrings[(int)va.animatorIK.ikActiveTarget]);
                    EditorGUI.EndDisabledGroup();
                }
                else if (va.originalIK.ikActiveTarget >= 0 && va.originalIK.ikData[va.originalIK.ikActiveTarget].enable)
                {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.LabelField("Original IK: " + va.originalIK.ikData[va.originalIK.ikActiveTarget].name);
                    EditorGUI.EndDisabledGroup();
                }
                else if (va.selectionHumanVirtualBones != null && va.selectionHumanVirtualBones.Count > 0)
                {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.LabelField("Virtual: " + va.selectionHumanVirtualBones[0].ToString());
                    EditorGUI.EndDisabledGroup();
                }
                else if (va.selectionMotionTool)
                {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.LabelField("Motion");
                    EditorGUI.EndDisabledGroup();
                }
                {
                    EditorGUILayout.Space();
                    if (GUILayout.Button(vaw.uEditorGUI.GetHelpIcon(), editorSelectionHelp ? vaw.guiStyleIconActiveButton : vaw.guiStyleIconButton, GUILayout.Width(19)))
                    {
                        editorSelectionHelp = !editorSelectionHelp;
                        vaw.editorWindowSelectionRect.size = Vector2.zero;
                    }
                    if (EditorGUILayout.DropdownButton(vaw.uEditorGUI.GetTitleSettingsIcon(), FocusType.Passive, vaw.guiStyleIconButton, GUILayout.Width(19)))
                    {
                        GenericMenu menu = new GenericMenu();
                        menu.AddItem(Language.GetContent(Language.Help.EditorMenuOnScene), editorSelectionOnScene, () =>
                        {
                            editorSelectionOnScene = !editorSelectionOnScene;
                            EditorPrefs.SetBool("VeryAnimation_Editor_Selection_OnScene", editorSelectionOnScene);
                            vaw.editorWindowSelectionRect.size = Vector2.zero;
                            Repaint();
                            SceneView.RepaintAll();
                        });
                        menu.ShowAsContext();
                    }
                }
                EditorGUILayout.EndHorizontal();
                {
                    if (editorSelectionHelp)
                    {
                        EditorGUILayout.HelpBox(Language.GetText(Language.Help.HelpSelection), MessageType.Info);
                    }

                    EditorGUILayout.BeginVertical(vaw.guiStyleSkinBox);
                    {
                        var humanoidIndex = va.SelectionGameObjectHumanoidIndex();
                        var boneIndex = va.selectionActiveBone;
                        if (va.isHuman && (humanoidIndex >= 0 || boneIndex == va.rootMotionBoneIndex))
                        {
                            #region Humanoid
                            if (humanoidIndex == HumanBodyBones.Hips)
                            {
                                EditorGUILayout.LabelField(Language.GetText(Language.Help.SelectionHip), EditorStyles.centeredGreyMiniLabel);
                            }
                            else if (humanoidIndex > HumanBodyBones.Hips || va.selectionActiveGameObject == vaw.gameObject)
                            {
                                EditorGUILayout.BeginHorizontal();
                                #region Mirror
                                var mirrorIndex = humanoidIndex >= 0 && va.humanoidIndex2boneIndex[(int)humanoidIndex] >= 0 ? va.mirrorBoneIndexes[va.humanoidIndex2boneIndex[(int)humanoidIndex]] : -1;
                                if (GUILayout.Button(Language.GetContentFormat(Language.Help.SelectionMirror, (mirrorIndex >= 0 ? string.Format("From '{0}'", va.bones[mirrorIndex].name) : "From self")), GUILayout.Width(100)))
                                {
                                    va.SelectionHumanoidMirror();
                                }
                                #endregion
                                EditorGUILayout.Space();
                                #region Reset
                                if (GUILayout.Button("Reset All", vaw.guiStyleDropDown, GUILayout.Width(100)))
                                {
                                    ResetAllSelectionHumanoidMenu(true, true, true);
                                }
                                #endregion
                                EditorGUILayout.EndHorizontal();
                                EditorGUILayout.Space();
                            }
                            int RowCount = 0;
                            if (boneIndex == va.rootMotionBoneIndex)
                            {
                                #region Root
                                {
                                    EditorGUILayout.BeginHorizontal(RowCount++ % 2 == 0 ? vaw.guiStyleAnimationRowEvenStyle : vaw.guiStyleAnimationRowOddStyle);
                                    if (GUILayout.Button(new GUIContent("Position", "RootT"), GUILayout.Width(64)))
                                    {
                                        va.lastTool = Tool.Move;
                                        va.SelectGameObject(vaw.gameObject);
                                    }
                                    EditorGUI.BeginChangeCheck();
                                    var rootT = EditorGUILayout.Vector3Field("", va.GetAnimationValueAnimatorRootT());
                                    if (EditorGUI.EndChangeCheck())
                                    {
                                        va.SetAnimationValueAnimatorRootT(rootT);
                                    }
                                    if (GUILayout.Button("Reset", vaw.guiStyleDropDown, GUILayout.Width(64)))
                                    {
                                        ResetAllSelectionHumanoidMenu(true, false, false);
                                    }
                                    EditorGUILayout.EndHorizontal();
                                }
                                {
                                    EditorGUILayout.BeginHorizontal(RowCount++ % 2 == 0 ? vaw.guiStyleAnimationRowEvenStyle : vaw.guiStyleAnimationRowOddStyle);
                                    if (GUILayout.Button(new GUIContent("Rotation", "RootQ"), GUILayout.Width(64)))
                                    {
                                        va.lastTool = Tool.Rotate;
                                        va.SelectGameObject(vaw.gameObject);
                                    }
                                    EditorGUI.BeginChangeCheck();
                                    var rootQ = EditorGUILayout.Vector3Field("", va.GetAnimationValueAnimatorRootQ().eulerAngles);
                                    if (EditorGUI.EndChangeCheck())
                                    {
                                        va.SetAnimationValueAnimatorRootQ(Quaternion.Euler(rootQ));
                                    }
                                    if (GUILayout.Button("Reset", vaw.guiStyleDropDown, GUILayout.Width(64)))
                                    {
                                        ResetAllSelectionHumanoidMenu(false, true, false);
                                    }
                                    EditorGUILayout.EndHorizontal();
                                }
                                #endregion
                            }
                            else if (humanoidIndex > HumanBodyBones.Hips)
                            {
                                #region Muscle
                                if (vaw.muscleRotationSliderIds == null || vaw.muscleRotationSliderIds.Length != 3)
                                    vaw.muscleRotationSliderIds = new int[3];
                                for (int i = 0; i < vaw.muscleRotationSliderIds.Length; i++)
                                    vaw.muscleRotationSliderIds[i] = -1;
                                for (int i = 0; i < 3; i++)
                                {
                                    var muscleIndex = HumanTrait.MuscleFromBone((int)humanoidIndex, i);
                                    if (muscleIndex < 0) continue;
                                    var muscleValue = va.GetAnimationValueAnimatorMuscle(muscleIndex);
                                    EditorGUILayout.BeginHorizontal(RowCount++ % 2 == 0 ? vaw.guiStyleAnimationRowEvenStyle : vaw.guiStyleAnimationRowOddStyle);
                                    if (GUILayout.Button(new GUIContent(va.musclePropertyName.Names[muscleIndex], va.musclePropertyName.Names[muscleIndex]), GUILayout.Width(vaw.editorSettings.settingEditorNameFieldWidth)))
                                    {
                                        va.lastTool = Tool.Rotate;
                                        va.SelectHumanoidBones(new HumanBodyBones[] { humanoidIndex });
                                        va.SetAnimationWindowSynchroSelection(new EditorCurveBinding[] { va.AnimationCurveBindingAnimatorMuscle(muscleIndex) });
                                    }
                                    {
                                        var mmuscleIndex = va.GetMirrorMuscleIndex(muscleIndex);
                                        if (mmuscleIndex >= 0)
                                        {
                                            if (GUILayout.Button(new GUIContent("", string.Format("Mirror: '{0}'", va.musclePropertyName.Names[mmuscleIndex])), vaw.guiStyleMirrorButton, GUILayout.Width(vaw.mirrorTex.width), GUILayout.Height(vaw.mirrorTex.height)))
                                            {
                                                var mhumanoidIndex = (HumanBodyBones)HumanTrait.BoneFromMuscle(mmuscleIndex);
                                                va.SelectHumanoidBones(new HumanBodyBones[] { mhumanoidIndex });
                                                va.SetAnimationWindowSynchroSelection(new EditorCurveBinding[] { va.AnimationCurveBindingAnimatorMuscle(mmuscleIndex) });
                                            }
                                        }
                                        else
                                        {
                                            GUILayout.Space(FoldoutSpace);
                                        }
                                    }
                                    {
                                        var saveBackgroundColor = GUI.backgroundColor;
                                        switch (i)
                                        {
                                        case 0: GUI.backgroundColor = Handles.xAxisColor; break;
                                        case 1: GUI.backgroundColor = Handles.yAxisColor; break;
                                        case 2: GUI.backgroundColor = Handles.zAxisColor; break;
                                        }
                                        EditorGUI.BeginChangeCheck();
                                        muscleValue = GUILayout.HorizontalSlider(muscleValue, -1f, 1f);
                                        vaw.muscleRotationSliderIds[i] = vaw.uEditorGUIUtility.GetLastControlID();
                                        if (EditorGUI.EndChangeCheck())
                                        {
                                            foreach (var mi in va.SelectionGameObjectsMuscleIndex(i))
                                            {
                                                va.SetAnimationValueAnimatorMuscle(mi, muscleValue);
                                            }
                                        }
                                        GUI.backgroundColor = saveBackgroundColor;
                                    }
                                    {
                                        EditorGUI.BeginChangeCheck();
                                        var value2 = EditorGUILayout.FloatField(muscleValue, GUILayout.Width(FloatFieldWidth));
                                        if (EditorGUI.EndChangeCheck())
                                        {
                                            foreach (var mi in va.SelectionGameObjectsMuscleIndex(i))
                                            {
                                                va.SetAnimationValueAnimatorMuscleIfNotOriginal(mi, value2);
                                            }
                                        }
                                    }
                                    EditorGUILayout.EndHorizontal();
                                }
                                #endregion

                                #region Rotation
                                {
                                    EditorGUILayout.BeginHorizontal(RowCount++ % 2 == 0 ? vaw.guiStyleAnimationRowEvenStyle : vaw.guiStyleAnimationRowOddStyle);
                                    if (GUILayout.Button(new GUIContent("Rotation", "Muscles"), GUILayout.Width(64)))
                                    {
                                        va.lastTool = Tool.Rotate;
                                        va.SelectHumanoidBones(new HumanBodyBones[] { humanoidIndex });
                                    }
                                    {
                                        var muscleIndex0 = HumanTrait.MuscleFromBone((int)humanoidIndex, 0);
                                        var muscleIndex1 = HumanTrait.MuscleFromBone((int)humanoidIndex, 1);
                                        var muscleIndex2 = HumanTrait.MuscleFromBone((int)humanoidIndex, 2);
                                        var euler = new Vector3(va.Muscle2EulerAngle(muscleIndex0, va.GetAnimationValueAnimatorMuscle(muscleIndex0)),
                                                                va.Muscle2EulerAngle(muscleIndex1, va.GetAnimationValueAnimatorMuscle(muscleIndex1)),
                                                                va.Muscle2EulerAngle(muscleIndex2, va.GetAnimationValueAnimatorMuscle(muscleIndex2)));
                                        EditorGUI.BeginChangeCheck();
                                        euler = EditorGUILayout.Vector3Field("", euler);
                                        if (EditorGUI.EndChangeCheck())
                                        {
                                            for (int i = 0; i < 3; i++)
                                            {
                                                var muscleValue = va.EulerAngle2Muscle(HumanTrait.MuscleFromBone((int)humanoidIndex, i), euler[i]);
                                                foreach (var mi in va.SelectionGameObjectsMuscleIndex(i))
                                                {
                                                    va.SetAnimationValueAnimatorMuscle(mi, muscleValue);
                                                }
                                            }
                                        }
                                    }
                                    if (GUILayout.Button("Reset", vaw.guiStyleDropDown, GUILayout.Width(64)))
                                    {
                                        ResetAllSelectionHumanoidMenu(false, true, false);
                                    }
                                    EditorGUILayout.EndHorizontal();
                                }
                                #endregion

                                #region Position(TDOF)
                                if (va.humanoidHasTDoF && VeryAnimation.HumanBonesAnimatorTDOFIndex[(int)humanoidIndex] != null)
                                {
                                    EditorGUILayout.BeginHorizontal(RowCount++ % 2 == 0 ? vaw.guiStyleAnimationRowEvenStyle : vaw.guiStyleAnimationRowOddStyle);
                                    if (GUILayout.Button(new GUIContent("Position", "TDOF"), GUILayout.Width(64)))
                                    {
                                        va.lastTool = Tool.Move;
                                        va.SelectHumanoidBones(new HumanBodyBones[] { humanoidIndex });
                                    }
                                    EditorGUI.BeginChangeCheck();
                                    var tdof = EditorGUILayout.Vector3Field("", va.GetAnimationValueAnimatorTDOF(VeryAnimation.HumanBonesAnimatorTDOFIndex[(int)humanoidIndex].index));
                                    if (EditorGUI.EndChangeCheck())
                                    {
                                        foreach (var hi in va.SelectionGameObjectsHumanoidIndex())
                                        {
                                            if (VeryAnimation.HumanBonesAnimatorTDOFIndex[(int)hi] == null) continue;
                                            va.SetAnimationValueAnimatorTDOF(VeryAnimation.HumanBonesAnimatorTDOFIndex[(int)hi].index, tdof);
                                        }
                                    }
                                    if (GUILayout.Button("Reset", vaw.guiStyleDropDown, GUILayout.Width(64)))
                                    {
                                        ResetAllSelectionHumanoidMenu(true, false, false);
                                    }
                                    EditorGUILayout.EndHorizontal();
                                }
                                #endregion
                            }
                            #endregion
                        }
                        else if (boneIndex >= 0)
                        {
                            #region Generic
                            if (va.isHuman && va.humanoidConflict[boneIndex])
                            {
                                EditorGUILayout.LabelField(Language.GetText(Language.Help.SelectionHumanoidConflict), EditorStyles.centeredGreyMiniLabel);
                            }
                            else
                            {
                                {
                                    EditorGUILayout.BeginHorizontal();
                                    #region Mirror
                                    if (GUILayout.Button(Language.GetContentFormat(Language.Help.SelectionMirror, (va.mirrorBoneIndexes[boneIndex] >= 0 ? string.Format("From '{0}'", va.bones[va.mirrorBoneIndexes[boneIndex]].name) : "From self")), GUILayout.Width(100)))
                                    {
                                        va.SelectionGenericMirror();
                                    }
                                    #endregion
                                    EditorGUILayout.Space();
                                    #region Reset
                                    if (GUILayout.Button("Reset All", vaw.guiStyleDropDown, GUILayout.Width(100)))
                                    {
                                        ResetAllSelectionGenericMenu(true, true, true);
                                    }
                                    #endregion
                                    EditorGUILayout.EndHorizontal();
                                }
                                EditorGUILayout.Space();
                                int RowCount = 0;
                                {
                                    #region Position
                                    {
                                        EditorGUILayout.BeginHorizontal(RowCount++ % 2 == 0 ? vaw.guiStyleAnimationRowEvenStyle : vaw.guiStyleAnimationRowOddStyle);
                                        if (GUILayout.Button("Position", GUILayout.Width(64)))
                                        {
                                            va.lastTool = Tool.Move;
                                            va.SelectGameObject(va.bones[boneIndex]);
                                        }
                                        EditorGUI.BeginChangeCheck();
                                        {
                                            var localPosition = EditorGUILayout.Vector3Field("", va.GetAnimationValueTransformPosition(boneIndex));
                                            if (EditorGUI.EndChangeCheck())
                                            {
                                                foreach (var bi in va.SelectionGameObjectsOtherHumanoidBoneIndex())
                                                {
                                                    va.SetAnimationValueTransformPosition(bi, localPosition);
                                                }
                                            }
                                        }
                                        if (GUILayout.Button("Reset", vaw.guiStyleDropDown, GUILayout.Width(64)))
                                        {
                                            ResetAllSelectionGenericMenu(true, false, false);
                                        }
                                        EditorGUILayout.EndHorizontal();
                                    }
                                    #endregion
                                    #region Rotation
                                    {
                                        EditorGUILayout.BeginHorizontal(RowCount++ % 2 == 0 ? vaw.guiStyleAnimationRowEvenStyle : vaw.guiStyleAnimationRowOddStyle);
                                        if (GUILayout.Button("Rotation", GUILayout.Width(64)))
                                        {
                                            va.lastTool = Tool.Rotate;
                                            va.SelectGameObject(va.bones[boneIndex]);
                                        }
                                        EditorGUI.BeginChangeCheck();
                                        {
                                            var localEulerAngles = EditorGUILayout.Vector3Field("", va.GetAnimationValueTransformRotation(boneIndex).eulerAngles);
                                            if (EditorGUI.EndChangeCheck())
                                            {
                                                foreach (var bi in va.SelectionGameObjectsOtherHumanoidBoneIndex())
                                                {
                                                    va.SetAnimationValueTransformRotation(bi, Quaternion.Euler(localEulerAngles));
                                                }
                                            }
                                        }
                                        if (GUILayout.Button("Reset", vaw.guiStyleDropDown, GUILayout.Width(64)))
                                        {
                                            ResetAllSelectionGenericMenu(false, true, false);
                                        }
                                        EditorGUILayout.EndHorizontal();
                                    }
                                    #endregion
                                    #region Scale
                                    {
                                        EditorGUILayout.BeginHorizontal(RowCount++ % 2 == 0 ? vaw.guiStyleAnimationRowEvenStyle : vaw.guiStyleAnimationRowOddStyle);
                                        if (GUILayout.Button("Scale", GUILayout.Width(64)))
                                        {
                                            va.lastTool = Tool.Scale;
                                            va.SelectGameObject(va.bones[boneIndex]);
                                        }
                                        EditorGUI.BeginChangeCheck();
                                        {
                                            var localScale = EditorGUILayout.Vector3Field("", va.GetAnimationValueTransformScale(boneIndex));
                                            if (EditorGUI.EndChangeCheck())
                                            {
                                                foreach (var bi in va.SelectionGameObjectsOtherHumanoidBoneIndex())
                                                {
                                                    va.SetAnimationValueTransformScale(bi, localScale);
                                                }
                                            }
                                        }
                                        if (GUILayout.Button("Reset", vaw.guiStyleDropDown, GUILayout.Width(64)))
                                        {
                                            ResetAllSelectionGenericMenu(false, false, true);
                                        }
                                        EditorGUILayout.EndHorizontal();
                                    }
                                    #endregion
                                }
                            }
                            #endregion
                        }
                        else if (va.selectionMotionTool)
                        {
                            #region Motion
                            {
                                EditorGUILayout.BeginHorizontal();
                                #region Mirror
                                if (GUILayout.Button(Language.GetContentFormat(Language.Help.SelectionMirror, "From self"), GUILayout.Width(100)))
                                {
                                    va.SetSelectionCommonMirror();
                                }
                                #endregion
                                EditorGUILayout.Space();
                                #region Reset
                                if (GUILayout.Button("Reset All", GUILayout.Width(100)))
                                {
                                    va.ResetSelectionCommonOriginal();
                                }
                                #endregion
                                EditorGUILayout.EndHorizontal();
                                EditorGUILayout.Space();
                            }
                            int RowCount = 0;
                            {
                                EditorGUILayout.BeginHorizontal(RowCount++ % 2 == 0 ? vaw.guiStyleAnimationRowEvenStyle : vaw.guiStyleAnimationRowOddStyle);
                                if (GUILayout.Button(new GUIContent("Position", "MotionT"), GUILayout.Width(64)))
                                {
                                    va.lastTool = Tool.Move;
                                    va.SelectMotionTool();
                                }
                                EditorGUI.BeginChangeCheck();
                                var motionT = EditorGUILayout.Vector3Field("", va.GetAnimationValueAnimatorMotionT());
                                if (EditorGUI.EndChangeCheck())
                                {
                                    va.SetAnimationValueAnimatorMotionT(motionT);
                                }
                                if (GUILayout.Button("Reset", GUILayout.Width(64f)))
                                {
                                    va.SetAnimationValueAnimatorMotionTIfNotOriginal(Vector3.zero);
                                }
                                EditorGUILayout.EndHorizontal();
                            }
                            {
                                EditorGUILayout.BeginHorizontal(RowCount++ % 2 == 0 ? vaw.guiStyleAnimationRowEvenStyle : vaw.guiStyleAnimationRowOddStyle);
                                if (GUILayout.Button(new GUIContent("Rotation", "MotionQ"), GUILayout.Width(64)))
                                {
                                    va.lastTool = Tool.Rotate;
                                    va.SelectMotionTool();
                                }
                                EditorGUI.BeginChangeCheck();
                                var motionQ = EditorGUILayout.Vector3Field("", va.GetAnimationValueAnimatorMotionQ().eulerAngles);
                                if (EditorGUI.EndChangeCheck())
                                {
                                    va.SetAnimationValueAnimatorMotionQ(Quaternion.Euler(motionQ));
                                }
                                if (GUILayout.Button("Reset", GUILayout.Width(64f)))
                                {
                                    va.SetAnimationValueAnimatorMotionQIfNotOriginal(Quaternion.identity);
                                }
                                EditorGUILayout.EndHorizontal();
                            }
                            #endregion
                        }
                        else if (va.animatorIK.ikActiveTarget != AnimatorIKCore.IKTarget.None)
                        {
                            va.animatorIK.SelectionGUI();
                        }
                        else if (va.originalIK.ikActiveTarget >= 0)
                        {
                            va.originalIK.SelectionGUI();
                        }
                        else
                        {
                            EditorGUILayout.LabelField(Language.GetText(Language.Help.SelectionNothingisselected), EditorStyles.centeredGreyMiniLabel);
                        }
                    }
                    EditorGUILayout.EndVertical();
                }
            }
        }

        private void UpdateRootCorrectionModeString()
        {
            for (int i = 0; i < (int)VeryAnimation.RootCorrectionMode.Total; i++)
            {
                RootCorrectionModeString[i] = new GUIContent(Language.GetContent(Language.Help.EditorRootCorrectionDisable + i));
            }
        }

        private void ResetAllSelectionHumanoidMenu(bool position, bool rotation, bool scale)
        {
            GenericMenu menu = new GenericMenu();
            {
                if (va.isHuman)
                {
                    menu.AddItem(Language.GetContent(Language.Help.EditorPoseHumanoidReset), false, () =>
                    {
                        Undo.RecordObject(this, "Humanoid Pose");
                        va.ResetSelectionHumanoidDefault(position, rotation, scale);
                    });
                }
                if (va.transformPoseSave.IsEnableTPoseTransform())
                {
                    menu.AddItem(Language.GetContent(Language.Help.EditorPoseTPose), false, () =>
                    {
                        Undo.RecordObject(this, "T Pose");
                        va.ResetSelectionHumanoidTPose(position, rotation, scale);
                    });
                }
                if (va.transformPoseSave.IsEnableBindTransform())
                {
                    menu.AddItem(Language.GetContent(Language.Help.EditorPoseBind), false, () =>
                    {
                        Undo.RecordObject(this, "Bind Pose");
                        va.ResetSelectionHumanoidBindPose(position, rotation, scale);
                    });
                }
                if (va.transformPoseSave.IsEnablePrefabTransform())
                {
                    menu.AddItem(Language.GetContent(Language.Help.EditorPosePrefab), false, () =>
                    {
                        Undo.RecordObject(this, "Prefab Pose");
                        va.ResetSelectionHumanoidPrefabPose(position, rotation, scale);
                    });
                }
                {
                    menu.AddItem(Language.GetContent(Language.Help.EditorPoseStart), false, () =>
                    {
                        Undo.RecordObject(this, "Edit Start Pose");
                        va.ResetSelectionHumanoidOriginalPose(position, rotation, scale);
                    });
                }
            }
            menu.ShowAsContext();
        }
        private void ResetAllSelectionGenericMenu(bool position, bool rotation, bool scale)
        {
            GenericMenu menu = new GenericMenu();
            {
                if (va.transformPoseSave.IsEnableBindTransform())
                {
                    menu.AddItem(Language.GetContent(Language.Help.EditorPoseBind), false, () =>
                    {
                        Undo.RecordObject(this, "Bind Pose");
                        va.ResetSelectionGenericBindPose(position, rotation, scale);
                    });
                }
                if (va.transformPoseSave.IsEnablePrefabTransform())
                {
                    menu.AddItem(Language.GetContent(Language.Help.EditorPosePrefab), false, () =>
                    {
                        Undo.RecordObject(this, "Prefab Pose");
                        va.ResetSelectionGenericPrefabPose(position, rotation, scale);
                    });
                }
                {
                    menu.AddItem(Language.GetContent(Language.Help.EditorPoseStart), false, () =>
                    {
                        Undo.RecordObject(this, "Edit Start Pose");
                        va.ResetSelectionGenericOriginalPose(position, rotation, scale);
                    });
                }
            }
            menu.ShowAsContext();
        }

        public static void ForceRepaint()
        {
            if (instance == null) return;
            instance.Repaint();
        }
    }
}
