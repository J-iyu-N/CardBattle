using UnityEngine;
using UnityEngine.EventSystems;

public class BasicMouseCardController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public BasicCardTransformController cardTransform;
    public RectTransform dragRect;
    public GameObject highlite;

    private Vector2 basePosition;
    private bool isSelected;
    private bool isDragging;

    void Awake()
    {
        if (dragRect == null)
        {
            dragRect = GetComponent<RectTransform>();
        }

        basePosition = dragRect.anchoredPosition;

        if (highlite != null)
        {
            highlite.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("호버 들어옴");
        if (isDragging == true) return;

        if (cardTransform != null)
        {
            cardTransform.PlayHoverIn();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("호버 나감");
        if (isDragging == true) return;

        if (cardTransform != null)
        {
            cardTransform.PlayHoverOut();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("클릭됨");
        if (isDragging == true) return;

        isSelected = !isSelected;
        RefreshHighlite();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("드래그 시작");
        isDragging = true;
        basePosition = dragRect.anchoredPosition;

        if (cardTransform != null)
        {
            cardTransform.PlayHoverIn();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        dragRect.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
        dragRect.anchoredPosition = basePosition;

        if (cardTransform != null)
        {
            cardTransform.PlayHoverOut();
        }
    }

    public void RefreshHighlite()
    {
        if (highlite == null) return;

        highlite.SetActive(isSelected);
    }
}