using UnityEngine;
using UnityEngine.UI;

public class CardSlotController : MonoBehaviour
{
    public HandController handController;
    public HandUIDirector handUIDirector;
    public CardData selectCard;
    public CardData slotCard1;
    public CardData slotCard2;
    [Header("슬롯 이미지")]
    public Sprite SlotEmpty;
    public GameObject Slot1;
    public GameObject Slot2;
    public void OnPageStart()
    {
        // 새 라운드 시작하면 슬롯 초기화
        selectCard = null;
        slotCard1 = null;
        slotCard2 = null;
        
        handController.RefillHand(); // 카드 리필
        handUIDirector.RefreshCardHighlite();
        RefreshSlotIcon();
    }
    public void OncardClicked(CardData card)
    {
        selectCard = card;
        handUIDirector.RefreshCardHighlite();
    }
    public void Onchar1Click()
    {
        if(selectCard==null) return;
        if(CanAssignCard(selectCard,1)==false) return;
        if(slotCard2 == selectCard){
            // 슬롯2에 이미 배정되어 있으면 2에서 빼고 1에 배치
            slotCard2 = null;
        }
        if(slotCard1 != null)
        {
            handController.ReturnCard(slotCard1); // 슬롯에 이미 카드 있으면 교환
        }
        handController.Hand.Remove(selectCard);
        slotCard1 = selectCard;
        selectCard = null;
        handUIDirector.RefreshCardHighlite();
        RefreshSlotIcon();
    }
    public void Onchar2Click()
    {
        if(selectCard==null) return;
        if(CanAssignCard(selectCard,2)==false) return;
        if(slotCard1 == selectCard){
            slotCard1 = null;
        }
        if(slotCard2 != null)
        {
            handController.ReturnCard(slotCard2); // 슬롯에 이미 카드 있으면 교환
        }
        handController.Hand.Remove(selectCard);
        slotCard2 = selectCard;
        selectCard = null;
        handUIDirector.RefreshCardHighlite();
        RefreshSlotIcon();
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
    public void RefreshSlotIcon()
    {
        if(slotCard1 != null)
        {
            Slot1.GetComponent<SpriteRenderer>().sprite = slotCard1.cardIcon;
        }
        else
        {
            Slot1.GetComponent<SpriteRenderer>().sprite = SlotEmpty;
        }
        if(slotCard2 != null)
        {
            Slot2.GetComponent<SpriteRenderer>().sprite = slotCard2.cardIcon;
        }
        else
        {
            Slot2.GetComponent<SpriteRenderer>().sprite = SlotEmpty;
        }
    }
    public void DisableChar1Slot()
    {
        Slot1.SetActive(false);
    }
        public void DisableChar2Slot()
    {
        Slot2.SetActive(false);
    }
}
