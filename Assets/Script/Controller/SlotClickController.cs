using UnityEngine;

public class SlotClickController : MonoBehaviour
{
    // 슬롯 클릭시 카드 반환 (카드가 있을 경우만)
    public CardSlotController cardSlot;
    public HandUIDirector handUIDirector;
    public HandController handController;
    public int slotIndex;
    private void OnMouseDown()
    {
        if(slotIndex == 1)
        {
            OnClickSlot1();
        }
        if(slotIndex == 2)
        {
            OnClickSlot2();
        }
    }
    public void OnClickSlot1()
    {
        if(cardSlot.slotCard1 == null) return;

        // 슬롯에 들어있던 카드를 저장해서 다시 핸드로 반환
        CardData returnCard = cardSlot.slotCard1;
        cardSlot.slotCard1 = null;
        handController.ReturnCard(returnCard);

        // UI 갱신
        cardSlot.RefreshSlotIcon();
        handUIDirector.RefreshHandUI();
    }
    public void OnClickSlot2()
    {
        if(cardSlot.slotCard2 == null) return;

        CardData returnCard = cardSlot.slotCard2;
        cardSlot.slotCard2 = null;
        handController.ReturnCard(returnCard);
        
        cardSlot.RefreshSlotIcon();
        handUIDirector.RefreshHandUI();
    }
}
