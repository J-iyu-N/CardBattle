using UnityEngine;
using System.Collections.Generic;

// 하단 카드 목록 컨트롤러
public class HandController : MonoBehaviour
{
    public List<CardData> StartDeck;
    public int handSize = 6;
    private List<CardData> Deck; // 전체 카드.. 남은 카드 목록
    public List<CardData> Hand; // 지금 들고있는 카드
    private List<CardData> Discard; // 사용한 카드

    void Awake()
    {
        Deck = new List<CardData>(StartDeck);
        Hand = new List<CardData>();
        Discard = new List<CardData>();
        Shuffle(Deck);
        RefillHand();
    }
     public void RefillHand()
    {
        // 페이즈 시작때 카드 채우기
        int refill = handSize - Hand.Count;
        for (int i=0; i<refill; i++) DrawOne();
    }
    public void UseCard(CardData card)
    {
        // 핸드 안에 있는 카드 사용 처리
        Hand.Remove(card); // 사용한 카드 핸드에서 삭제
        Discard.Add(card); // 쓴 카드에 추가
    }
    public void ReturnCard(CardData card)
    {
        Hand.Add(card); // 카드 돌려두기
    }
    public void DrawOne()
    {
        // 추가할 카드 뽑기
        if(Deck.Count ==0) return;
        Hand.Add(Deck[0]);
        Deck.RemoveAt(0);
    }
    public void Shuffle(List<CardData> list)
    {
        // 카드 순서 섞기
        for(int i =list.Count-1; i>0; i--)
        {
            int j = Random.Range(0,i+1);
            (list[j], list[i]) = (list[i], list[j]);
        }
    }
}
