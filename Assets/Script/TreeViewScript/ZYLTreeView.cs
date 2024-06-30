using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using UnityEngine;
using UnityEngine.UI;
//using winforms = System.Windows.Forms;

namespace Battlehub.UIControls
{
    /// <summary>
    /// In this demo we use game objects hierarchy as data source (each data item is game object)
    /// You can use any hierarchical data with treeview.
    /// </summary>
    public class ZYLTreeView : MonoBehaviour
    {
        public TreeView TreeView;
        public InputField DZNAME;
        public Dropdown HumanChooseDropdown;
        public GameObject DZnamePanel;
        public GameObject MessagePanel;
        public Text StageText;
        public GameObject DZTrack1;
        public GameObject DZTrack2;
        public GameObject DZTrack3;
        public GameObject ZBstage;
        public GameObject SSstage;
        public GameObject CSstage;
        List<string> ZBStageDZText1 = new List<string>();
        List<string> SSStageDZText1 = new List<string>();
        List<string> CSStageDZText1 = new List<string>();
        List<string> ZBStageDZText2 = new List<string>();
        List<string> SSStageDZText2 = new List<string>();
        List<string> CSStageDZText2 = new List<string>();
        List<string> ZBStageDZText3 = new List<string>();
        List<string> SSStageDZText3 = new List<string>();
        List<string> CSStageDZText3 = new List<string>();
        //public static List<string> ZBStageDZText1;
        //public static List<string> SSStageDZText1;
        //public static List<string> CSStageDZText1;
        //public static List<string> ZBStageDZText2;
        //public static List<string> SSStageDZText2;
        //public static List<string> CSStageDZText2;
        //public static List<string> ZBStageDZText3;
        //public static List<string> SSStageDZText3;
        //public static List<string> CSStageDZText3;
        public static bool IsPrefab(Transform This)
        {
            if (UnityEngine.Application.isEditor && !UnityEngine.Application.isPlaying)
            {
                throw new InvalidOperationException("Does not work in edit mode");
            }
            return This.gameObject.scene.buildIndex < 0;
        }

        private void Start()
        {
            
            //MessagePanel.SetActive(false);
            TreeView.CanDrag = false;
            if (!TreeView)
            {
                //Debug.LogError("Set TreeView field");
                return;
            }
            //IEnumerable<GameObject> dataItems = Resources.FindObjectsOfTypeAll<GameObject>().Where(go => !IsPrefab(go.transform) && go.transform.parent == null).OrderBy(t => t.transform.GetSiblingIndex());
            IEnumerable<GameObject> dataItems = Resources.FindObjectsOfTypeAll<GameObject>().Where(go => !IsPrefab(go.transform) && go.name == "维修任务").OrderBy(t => t.transform.GetSiblingIndex());
            
            //subscribe to events
            TreeView.ItemDataBinding += OnItemDataBinding;
            TreeView.SelectionChanged += OnSelectionChanged;
            TreeView.ItemsRemoved += OnItemsRemoved;
            TreeView.ItemExpanding += OnItemExpanding;
            TreeView.ItemBeginDrag += OnItemBeginDrag;

            TreeView.ItemDrop += OnItemDrop;
            TreeView.ItemBeginDrop += OnItemBeginDrop;
            TreeView.ItemEndDrag += OnItemEndDrag;


            //Bind data items
            TreeView.Items = dataItems;


        }

        public void AddItem()
        {
            GameObject SelectedItem = (GameObject)TreeView.SelectedItem;
            //string ParentName = ParentItems.name;
            if (SelectedItem == null|| SelectedItem.name=="维修任务")
            {
                DZnamePanel.SetActive(false);
                MessagePanel.SetActive(true);
                //MessageBox.Show("请选择维修阶段！", "提示", winforms.MessageBoxButtons.OK, winforms.MessageBoxIcon.Information);
            }
            else if(SelectedItem.name == "准备阶段"|| SelectedItem.name == "实施阶段"|| SelectedItem.name == "测试阶段")
            {
                Transform SItems = SelectedItem.GetComponent<Transform>();
                if (HumanChooseDropdown.value == 0)
                {
                    Transform ParentItem = SItems.transform.GetChild(0);
                    GameObject AddItems = new GameObject();
                    AddItems.name = DZNAME.text;
                    AddItems.transform.parent = ParentItem;
                    TreeView.AddChild(ParentItem, AddItems);
                }
                else if (HumanChooseDropdown.value == 1)
                {
                    Transform ParentItem = SItems.transform.GetChild(1);
                    GameObject AddItems = new GameObject();
                    AddItems.name = DZNAME.text;
                    AddItems.transform.parent = ParentItem;
                    TreeView.AddChild(ParentItem, AddItems);
                }
                else if (HumanChooseDropdown.value == 2)
                {
                    Transform ParentItem = SItems.transform.GetChild(2);
                    GameObject AddItems = new GameObject();
                    AddItems.name = DZNAME.text;
                    AddItems.transform.parent = ParentItem;
                    TreeView.AddChild(ParentItem, AddItems);
                }

            }

        }


        public void ChooseTerm()//提示选择任务阶段
        {
            GameObject SelectedItem = (GameObject)TreeView.SelectedItem;           
            //string ParentName = ParentItems.name;
            if (SelectedItem == null ||(SelectedItem.name != "准备阶段" && SelectedItem.name != "实施阶段" && SelectedItem.name != "测试阶段"))
            {
                DZnamePanel.SetActive(false);
                MessagePanel.SetActive(true);
                // MessageBox.Show("请选择维修阶段！", "提示", winforms.MessageBoxButtons.OK, winforms.MessageBoxIcon.Information);
            }
            StageText.text = SelectedItem.name;

        }

        public void DeleteNodes()//待修改
        {
            SendKeys.Send("{DEL}");
        }

        public void MessagePanelHide()
        {
            MessagePanel.SetActive(false);
        }
        private void OnItemBeginDrop(object sender, ItemDropCancelArgs e)
        {
            //object dropTarget = e.DropTarget;
            //if(e.Action == ItemDropAction.SetNextSibling || e.Action == ItemDropAction.SetPrevSibling)
            //{
            //    e.Cancel = true;
            //}

        }

        private void OnDestroy()
        {
            if (!TreeView)
            {
                return;
            }


            //unsubscribe
            TreeView.ItemDataBinding -= OnItemDataBinding;
            TreeView.SelectionChanged -= OnSelectionChanged;
            TreeView.ItemsRemoved -= OnItemsRemoved;
            TreeView.ItemExpanding -= OnItemExpanding;
            TreeView.ItemBeginDrag -= OnItemBeginDrag;
            TreeView.ItemBeginDrop -= OnItemBeginDrop;
            TreeView.ItemDrop -= OnItemDrop;
            TreeView.ItemEndDrag -= OnItemEndDrag;
        }

        private void OnItemExpanding(object sender, ItemExpandingArgs e)
        {
            //get parent data item (game object in our case)
            GameObject gameObject = (GameObject)e.Item;
            if (gameObject.transform.childCount > 0)
            {
                //get children
                GameObject[] children = new GameObject[gameObject.transform.childCount];
                for (int i = 0; i < children.Length; ++i)
                {
                    children[i] = gameObject.transform.GetChild(i).gameObject;
                }

                //Populate children collection
                e.Children = children;
            }
        }

        private void OnSelectionChanged(object sender, SelectionChangedArgs e)
        {
#if UNITY_EDITOR
            //Do something on selection changed (just syncronized with editor's hierarchy for demo purposes)
            UnityEditor.Selection.objects = e.NewItems.OfType<GameObject>().ToArray();
#endif
        }

        private void OnItemsRemoved(object sender, ItemsRemovedArgs e)
        {
            //Destroy removed dataitems
            for (int i = 0; i < e.Items.Length; ++i)
            {
                GameObject go = (GameObject)e.Items[i];
                if (go != null)
                {
                    Destroy(go);
                }
            }
        }

        /// <summary>
        /// This method called for each data item during databinding operation
        /// You have to bind data item properties to ui elements in order to display them.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnItemDataBinding(object sender, TreeViewItemDataBindingArgs e)
        {
            //String humanNameStr = HumanChooseDropdown.captionText.text;
           // HumanChooseDropdown.options[timeDropDown.value].text
            GameObject dataItem = e.Item as GameObject;
            if (dataItem != null)
            {
                //We display dataItem.name using UI.Text 
                Text text = e.ItemPresenter.GetComponentInChildren<Text>(true);
                text.text = dataItem.name;

                //Load icon from resources

                Image icon = e.ItemPresenter.GetComponentsInChildren<Image>()[4];
                
                if (dataItem.name == "准备阶段")
                {
                    icon.sprite = Resources.Load<Sprite>("prepare (2)");
                }
                else if (dataItem.name == "实施阶段")
                {
                    icon.sprite = Resources.Load<Sprite>("done (4)");
                }
                else if (dataItem.name == "测试阶段")
                {
                    icon.sprite = Resources.Load<Sprite>("test2");
                }
                else if (dataItem.name == "HumanChooseDropdown.options[0].text")
                {
                    icon.sprite = Resources.Load<Sprite>("hum (3)");
                }
                else if (dataItem.name == "HumanChooseDropdown.options[1].text")
                {
                    icon.sprite = Resources.Load<Sprite>("hum (3)");
                }
                else if (dataItem.name == "HumanChooseDropdown.options[3].text")
                {
                    icon.sprite = Resources.Load<Sprite>("hum (3)");
                }
                else if (dataItem.name == "维修任务")
                {
                    icon.sprite = Resources.Load<Sprite>("task");
                }
                else
                {
                    icon.sprite = Resources.Load<Sprite>("cube");
                }


                //And specify whether data item has children (to display expander arrow if needed)
                if (dataItem.name != "TreeView")
                {
                    e.HasChildren = dataItem.transform.childCount > 0;
                }
            }
        }

        private void OnItemBeginDrag(object sender, ItemArgs e)
        {
            //Could be used to change cursor
        }

        private void OnItemDrop(object sender, ItemDropArgs e)
        {
            if (e.DropTarget == null)
            {
                return;
            }

            Transform dropT = ((GameObject)e.DropTarget).transform;

            //Set drag items as children of drop target
            if (e.Action == ItemDropAction.SetLastChild)
            {
                for (int i = 0; i < e.DragItems.Length; ++i)
                {
                    Transform dragT = ((GameObject)e.DragItems[i]).transform;
                    dragT.SetParent(dropT, true);
                    dragT.SetAsLastSibling();
                }
            }

            //Put drag items next to drop target
            else if (e.Action == ItemDropAction.SetNextSibling)
            {
                for (int i = e.DragItems.Length - 1; i >= 0; --i)
                {
                    Transform dragT = ((GameObject)e.DragItems[i]).transform;
                    int dropTIndex = dropT.GetSiblingIndex();
                    if (dragT.parent != dropT.parent)
                    {
                        dragT.SetParent(dropT.parent, true);
                        dragT.SetSiblingIndex(dropTIndex + 1);
                    }
                    else
                    {
                        int dragTIndex = dragT.GetSiblingIndex();
                        if (dropTIndex < dragTIndex)
                        {
                            dragT.SetSiblingIndex(dropTIndex + 1);
                        }
                        else
                        {
                            dragT.SetSiblingIndex(dropTIndex);
                        }
                    }
                }
            }

            //Put drag items before drop target
            else if (e.Action == ItemDropAction.SetPrevSibling)
            {
                for (int i = 0; i < e.DragItems.Length; ++i)
                {
                    Transform dragT = ((GameObject)e.DragItems[i]).transform;
                    if (dragT.parent != dropT.parent)
                    {
                        dragT.SetParent(dropT.parent, true);
                    }

                    int dropTIndex = dropT.GetSiblingIndex();
                    int dragTIndex = dragT.GetSiblingIndex();
                    if (dropTIndex > dragTIndex)
                    {
                        dragT.SetSiblingIndex(dropTIndex - 1);
                    }
                    else
                    {
                        dragT.SetSiblingIndex(dropTIndex);
                    }
                }
            }
        }

        private void OnItemEndDrag(object sender, ItemArgs e)
        {
        }

        public void OnDZChange()//将动作层对应阶段的动作存入相应数组，赋值给treeview（放弃使用）
        {
            ZBStageDZText1.Clear();
            SSStageDZText1.Clear();
            CSStageDZText1.Clear();
            ZBStageDZText2.Clear();
            SSStageDZText2.Clear();
            CSStageDZText2.Clear();
            ZBStageDZText3.Clear();
            SSStageDZText3.Clear();
            CSStageDZText3.Clear();
            int j = 0; int k = 0; int m = 0;
            for (int i = 0; i < DZTrack1.transform.childCount; i++)//DZTrack1
            {
                Transform TrackChildi = DZTrack1.transform.GetChild(i);
                if (TrackChildi.name != "Image Empty(Clone)")
                {
                    DZTagScript DZStageTag = TrackChildi.GetComponent<DZTagScript>();
                    if (DZStageTag.DZStageTag == "准备阶段")
                    {
                        string DZText = TrackChildi.GetChild(0).GetComponent<Text>().text;
                        ZBStageDZText1.Add(DZText);
                        ZBstage.transform.GetChild(0).transform.GetChild(j).name = ZBStageDZText1[j];
                        j = j + 1;
                    }
                    else if (DZStageTag.DZStageTag == "实施阶段")
                    {
                        string DZText = TrackChildi.GetChild(0).GetComponent<Text>().text;
                        SSStageDZText1.Add(DZText);
                        SSstage.transform.GetChild(0).transform.GetChild(k).name = ZBStageDZText1[k];
                        k = k + 1;
                    }
                    else if (DZStageTag.DZStageTag == "测试阶段")
                    {
                        string DZText = TrackChildi.GetChild(0).GetComponent<Text>().text;
                        CSStageDZText1.Add(DZText);
                        CSstage.transform.GetChild(0).transform.GetChild(m).name = ZBStageDZText1[m];
                        m = m + 1;
                    }
                }
            }
            for (int i = 0; i < DZTrack2.transform.childCount; i++)//DZTrack2
            {
                Transform TrackChildi = DZTrack2.transform.GetChild(i);
                if (TrackChildi.name != "Image Empty(Clone)")
                {
                    DZTagScript DZStageTag = TrackChildi.GetComponent<DZTagScript>();
                    if (DZStageTag.DZStageTag == "准备阶段")
                    {
                        string DZText = TrackChildi.GetChild(0).GetComponent<Text>().text;
                        ZBStageDZText2.Add(DZText);
                    }
                    else if (DZStageTag.DZStageTag == "实施阶段")
                    {
                        string DZText = TrackChildi.GetChild(0).GetComponent<Text>().text;
                        SSStageDZText2.Add(DZText);
                    }
                    else if (DZStageTag.DZStageTag == "测试阶段")
                    {
                        string DZText = TrackChildi.GetChild(0).GetComponent<Text>().text;
                        CSStageDZText2.Add(DZText);
                    }
                }
            }
            for (int i = 0; i < DZTrack3.transform.childCount; i++)//DZTrack3
            {
                Transform TrackChildi = DZTrack3.transform.GetChild(i);
                if (TrackChildi.name != "Image Empty(Clone)")
                {
                    DZTagScript DZStageTag = TrackChildi.GetComponent<DZTagScript>();
                    if (DZStageTag.DZStageTag == "准备阶段")
                    {
                        string DZText = TrackChildi.GetChild(0).GetComponent<Text>().text;
                        ZBStageDZText3.Add(DZText);
                    }
                    else if (DZStageTag.DZStageTag == "实施阶段")
                    {
                        string DZText = TrackChildi.GetChild(0).GetComponent<Text>().text;
                        SSStageDZText3.Add(DZText);
                    }
                    else if (DZStageTag.DZStageTag == "测试阶段")
                    {
                        string DZText = TrackChildi.GetChild(0).GetComponent<Text>().text;
                        CSStageDZText3.Add(DZText);
                    }
                }
            }

        }
        private void Update()
        {            
            //GameObject SelectedItem = (GameObject)TreeView.SelectedItem;
            //StageText.text = SelectedItem.name;
            if (Input.GetKeyDown(KeyCode.J))
            {
                TreeView.SelectedItems = TreeView.Items.OfType<object>().Take(5).ToArray();
            }
            else if (Input.GetKeyDown(KeyCode.K))
            {
                TreeView.SelectedItem = null;
            }
        }
    }


}

