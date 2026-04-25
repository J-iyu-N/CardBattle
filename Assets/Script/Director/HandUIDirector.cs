using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Microsoft.Unity.VisualStudio.Editor;
public class HandUIDirector : MonoBehaviour
{
    public HandController handController;
    public Transform handArea;
    public GameObject cardPrefab;

    public List<GameObject> cardUIList;
    public List<CardData> sortedHand; // 정렬된 카드 저장

    [SerializeField] float cardSpacing = 2f;

    [Header("카드 프리팹 UI 연결")]
    public Image icon;
    public TextMeshPro cardName;
    public TextMeshPro damage;
    public TextMeshPro desc;

    public void RefreshHandUI()
    {
        // 핸드 UI 갱신
        SortHand();
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
        int needCount = sortedHand.Count;
        int currentCount = cardUIList.Count;
        if (needCount > currentCount)
        {
            int creatCount = needCount-currentCount;
            for(int i =0; i < creatCount; i++)
            {
                // 카드 추가...
            }
        }
        if(needCount < currentCount)
        {
            // set active false
        }
    }
    public void UpdateCardData()
    {
        // 카드 이름, 이미지 등 프리팹에 데이터 넣기
    }
}
