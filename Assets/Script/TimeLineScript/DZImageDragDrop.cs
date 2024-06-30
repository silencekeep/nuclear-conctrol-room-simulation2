using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class DZImageDragDrop : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public static List<int> DragDZTagList = new List<int>();    
    private int startIndex;
    private int originalIndex;
    private int sibling;
    private float width;
    private float spacing;
    public static GameObject DragTrack;
    public static bool DragTrackFlag;

    void Start()
    {
        DragDZTagList.Clear();
    }
    private void OnEnable()
    {
        // topPadding = transform.parent.GetComponent<HorizontalLayoutGroup>().padding.top;
        spacing = 0;// transform.parent.GetComponent<HorizontalLayoutGroup>().spacing;
        width = GetComponent<RectTransform>().sizeDelta.x;
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        DragTrack = transform.parent.gameObject;
        startIndex = transform.GetSiblingIndex();
        originalIndex = transform.GetSiblingIndex();
        transform.SetSiblingIndex(transform.parent.childCount - 1);
        GetComponent<LayoutElement>().ignoreLayout = true;
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = new Vector3(eventData.position.x, transform.position.y, transform.position.z);
        sibling = (int)((transform.localPosition.x) / (width + spacing));//sibling = -(int)((transform.localPosition.x - topPadding) / (width + spacing));
        transform.parent.GetComponent<ActImageContent>().SetEmpty(sibling);
    }
    public void OnEndDrag(PointerEventData eventData)
    {        
        ChangeSibling();
        DragDZTagList.Clear();
        for (int i = 0; i < DragTrack.transform.childCount; i++)
        {
            Transform CurDragDZAct = DragTrack.transform.GetChild(i);
            DZTagScript DblClcDZTag = CurDragDZAct.GetComponent<DZTagScript>();
           
            int DragDZTagListi = DblClcDZTag.DZActTag;
            DragDZTagList.Add(DragDZTagListi);
        }
        DragTrackFlag = true;
        //DZTimelineEditScript dZTimelineEditScript = new DZTimelineEditScript();
        //dZTimelineEditScript.GetAllActTag();
        
    }

    private void ChangeSibling()
    {
        transform.parent.GetComponent<ActImageContent>().DeActivedEmpty();
        transform.SetSiblingIndex(sibling);
        GetComponent<LayoutElement>().ignoreLayout = false;
    }
}

