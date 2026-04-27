using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using NUnit.Framework;
using System;

// 카드 데이터 매핑
// 카드 프리팹에 코드 붙이기
public class CardUIController : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public CardData cardData;
    [Header("카드 프리팹 UI 연결")]
    public Image icon;
    public TextMeshProUGUI cardName;
    //public TextMeshPro damage;
    public TextMeshProUGUI desc;
    public CardSlotController cardSlotController;
    public GameObject highlite;
    bool isSelected;
    void Awake()
    {
        cardSlotController = FindAnyObjectByType<CardSlotController>();
        highlite.SetActive(false);
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
        if (isSelected)
        {
            highlite.SetActive(true);
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        // 호버 되면 카드 강조
        highlite.SetActive(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        // 클릭되지 않은 카드면 나가면 강조 꺼짐
        // 클릭된 카드면 강조 유지
        CheckSelected();
        if (isSelected)
        {
            return;
        }
        highlite.SetActive(false);
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
    }
}
