using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// 카드 데이터 매핑
// 카드 프리팹에 코드 붙이기
public class CardUIController : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public CardData cardData;
    public HandUIDirector handUIDirector;
    public CardTransformController cardTransform;
    [Header("카드 프리팹 UI 연결")]
    public Image icon;
    public TextMeshProUGUI cardName;
    //public TextMeshPro damage;
    public TextMeshProUGUI desc;
    public CardSlotController cardSlotController;

    [Header("하이라이트 오브젝트 연결")]
    public GameObject highlite;
    public GameObject highliteChar1;
    public GameObject highliteChar2;

    // 선택 
    bool isSelected;
    bool isHovered;
    bool isChar1Selected;
    bool isChar2Selected;
    void Awake()
    {
        cardSlotController = FindAnyObjectByType<CardSlotController>();
        handUIDirector = FindAnyObjectByType<HandUIDirector>();

        highlite.SetActive(false);
        highliteChar1.SetActive(false);
        highliteChar2.SetActive(false);
    }
    public void FillCardData(CardData data)
    {
        cardData = data;
        cardName.text = data.cardName;
        desc.text = data.description;

        if (data.cardIcon != null)
        {
            icon.sprite = data.cardIcon;
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        // 카드 클릭 인식
        // CardSlotController 의 selected에 전달
        cardSlotController.OncardClicked(cardData);
        Debug.Log($"선택 카드: {cardData.cardName}");

        // 카드 강조
        CheckSelected();
        UpdateHighlite();
        handUIDirector.RefreshCardHighlite();
        cardTransform.PlayHoverIn();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        // 호버 되면 카드 강조
        isHovered = true;
        CheckSelected();
        UpdateHighlite();
        handUIDirector.RefreshCardHighlite();
        cardTransform.PlayHoverIn();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        // 클릭되지 않은 카드면 나가면 강조 꺼짐
        // 클릭된 카드면 강조 유지
        isHovered = false;
        CheckSelected();
        UpdateHighlite();
        handUIDirector.RefreshCardHighlite();
        cardTransform.PlayHoverOut();
    }
    public void CheckSelected()
    {
        if(cardSlotController.selectCard == cardData)
        {
            isSelected = true;
        }
        else
        {
            isSelected = false;
        }
        if(cardSlotController.slotCard1 == cardData)
        {
            isChar1Selected = true;
            isSelected = false;
        }
        else
        {
            isChar1Selected = false;
        }
        if(cardSlotController.slotCard2 == cardData)
        {
            isChar2Selected = true;
            isSelected = false;
        }
        else
        {
            isChar2Selected = false;
        }
    }
    public void UpdateHighlite()
    {
        if (isChar1Selected)
        {
            highlite.SetActive(false);
            highliteChar1.SetActive(true);
            highliteChar2.SetActive(false);
        }
        else if (isChar2Selected)
        {
            highlite.SetActive(false);
            highliteChar1.SetActive(false);
            highliteChar2.SetActive(true);
        }
        else if (isSelected||isHovered)
        {
            highlite.SetActive(true);
            highliteChar1.SetActive(false);
            highliteChar2.SetActive(false);
        }
        else
        {
            highlite.SetActive(false);
            highliteChar1.SetActive(false);
            highliteChar2.SetActive(false);
        }
    }
}
