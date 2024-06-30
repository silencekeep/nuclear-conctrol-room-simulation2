using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ActImageDragDrop : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    private int startIndex;
    private int originalIndex;
    private int sibling;

  //  private float topPadding;
    private float width;
    private float spacing;

    private void OnEnable()
    {
        // topPadding = transform.parent.GetComponent<HorizontalLayoutGroup>().padding.top;
        spacing = 0;// transform.parent.GetComponent<HorizontalLayoutGroup>().spacing;
        width = GetComponent<RectTransform>().sizeDelta.x;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startIndex = transform.GetSiblingIndex();
        originalIndex = transform.GetSiblingIndex();
        transform.SetSiblingIndex(transform.parent.childCount - 1);
        GetComponent<LayoutElement>().ignoreLayout = true;
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = new Vector3(eventData.position.x, transform.position.y, transform.position.z);
        sibling = (int)((transform.localPosition.x ) / (width + spacing));//sibling = -(int)((transform.localPosition.x - topPadding) / (width + spacing));
        transform.parent.GetComponent<ActImageContent>().SetEmpty(sibling);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        ChangeSibling();
    }

    private void ChangeSibling()
    {
        transform.parent.GetComponent<ActImageContent>().DeActivedEmpty();
        transform.SetSiblingIndex(sibling);
        GetComponent<LayoutElement>().ignoreLayout = false;
    }
}
