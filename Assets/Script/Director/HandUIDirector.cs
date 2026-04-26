using System.Collections.Generic;
using UnityEngine;
public class HandUIDirector : MonoBehaviour
{
    public HandController handController;
    public Transform handArea;
    public GameObject cardPrefab;
    public CardGenerator cardGenerator;

    public List<GameObject> cardUIList;
    public List<CardData> sortedHand; // 정렬된 카드 저장

    public void Start()
    {
        RefreshHandUI();
    }
    public void RefreshHandUI()
    {
        // 핸드 UI 갱신
        SortHand(); // 순서 정렬
        UpdateCardUI(); // 카드 ui 표시
        UpdateCardData(); // 카드 내용 채우기
    }
    public void SortHand()
    {
        // 카드 정렬
        sortedHand.Clear();
        sortedHand = new List<CardData>(handController.Hand); // 기존 핸드 복사
        for(int i = 0; i < sortedHand.Count-1; i++)
        {
            for(int j = i+1; j < sortedHand.Count; j++)
            {
                if (sortedHand[i].sortIndex > sortedHand[j].sortIndex)
                {
                    (sortedHand[i],sortedHand[j]) = (sortedHand[j],sortedHand[i]);
                }
            }
        }
    }
    public void UpdateCardUI()
    {
        // UI에 카드 추가
        int needCount = sortedHand.Count; // 화면에 보여줘야 하는 카드 개수
        int currentCount = cardUIList.Count; // 현재 보여지는 카드 개수
        if (needCount > currentCount)
        {
            // 더 만들어야 되는 개수 만큼 카드 추가
            int creatCount = needCount-currentCount; 
            for(int i =0; i < creatCount; i++)
            {
                GameObject newCardUI = cardGenerator.CreateOneCardUI();
                cardUIList.Add(newCardUI);
            }
        }
        for(int i =0; i < cardUIList.Count; i++)
        {
            if (i < needCount)
            {
                cardUIList[i].SetActive(true);
            }
            else
            {
                cardUIList[i].SetActive(false);
            }
        }
    }
    public void UpdateCardData()
    {
        // 카드 이름, 이미지 등 프리팹에 데이터 넣기
        for(int i = 0; i < sortedHand.Count; i++)
        {
            GameObject cardObject = cardUIList[i];
            CardUIController cardUIController = cardObject.GetComponent<CardUIController>();
            cardUIController.FillCardData(sortedHand[i]);
        }
    }
}