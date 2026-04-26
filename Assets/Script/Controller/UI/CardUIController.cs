using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// 카드 데이터 매핑
// 카드 프리팹에 코드 붙이기
public class CardUIController : MonoBehaviour, IPointerClickHandler
{
    public CardData cardData;
    [Header("카드 프리팹 UI 연결")]
    public Image icon;
    public TextMeshProUGUI cardName;
    //public TextMeshPro damage;
    public TextMeshProUGUI desc;
    public CardSlotController cardSlotController;
    void Awake()
    {
        cardSlotController = FindAnyObjectByType<CardSlotController>();
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
    }
}
