using UnityEngine;

public class SlotClickController : MonoBehaviour
{
    // 슬롯 클릭시 카드 반환 (카드가 있을 경우만)
    public CardSlotController cardSlot;
    public HandUIDirector handUIDirector;
    public HandController handController;
    public BattleDirector battleDirector;
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
        if(battleDirector.isbattleing) return;
        if(cardSlot.slotCard1 == null) return;

        cardSlot.slotCard1 = null;

        // UI 갱신
        cardSlot.RefreshSlotIcon();
        handUIDirector.RefreshHandUI();
    }
    public void OnClickSlot2()
    {
        if(battleDirector.isbattleing) return;
        if(cardSlot.slotCard2 == null) return;

        cardSlot.slotCard2 = null;
        
        cardSlot.RefreshSlotIcon();
        handUIDirector.RefreshHandUI();
    }
}
