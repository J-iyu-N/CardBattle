using UnityEngine;
using UnityEngine.EventSystems;

public class CardSlotController : MonoBehaviour
{
    public HandController handController;
    public CardData selectCard;
    public CardData slotCard1;
    public CardData slotCard2;
    public void OnPageStart()
    {
        // 새 라운드 시작하면 슬롯 초기화
        selectCard = null;
        slotCard1 = null;
        slotCard2 = null;
        
        handController.RefillHand(); // 카드 리필
    }
    public void OncardClicked(CardData card)
    {
        selectCard = card;
    }
    public void Onchar1Click()
    {
        if(selectCard==null) return;
        if(CanAssignCard(selectCard,1)==false) return;
        if(slotCard1 != null)
        {
            handController.ReturnCard(slotCard1); // 슬롯에 이미 카드 있으면 교환
        }
        handController.Hand.Remove(selectCard);
        slotCard1 = selectCard;
        selectCard = null;
    }
    public void Onchar2Click()
    {
        if(selectCard==null) return;
        if(CanAssignCard(selectCard,2)==false) return;
        if(slotCard2 != null)
        {
            handController.ReturnCard(slotCard2); // 슬롯에 이미 카드 있으면 교환
        }
        handController.Hand.Remove(selectCard);
        slotCard2 = selectCard;
        selectCard = null;
    }
    public void ConfirmCard()
    {
        // 카드 다 선택하고 확인 버튼 누르면 실행
        if(slotCard1!=null) handController.UseCard(slotCard1);
        if(slotCard2!=null) handController.UseCard(slotCard2);
    }
    public bool CanAssignCard(CardData card, int charIndex)
    {
        //캐릭터 1 2 가 사용할 수 있는 카드인지 확인
        if(card.cardOwner==CardOwner.All) return true;
        if(card.cardOwner==CardOwner.Char1&&charIndex==1) return true;
        if(card.cardOwner==CardOwner.Char2&&charIndex==2) return true;       
        return false;
    }
}
