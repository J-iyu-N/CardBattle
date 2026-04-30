using UnityEngine;

public class PlayerCharacterController : MonoBehaviour
{
    public CharacterData Data;
    public RuntimeCharacterState State;
    public CardEffect cardEffect;
    public CardSlotController cardSlotController;
    public HpUIController hpUIController;
    public int shield;
    public int characterIndex;
    float defendPercent = 1f;
    void Awake()
    {
        State = new RuntimeCharacterState(Data);
    }
    void Update()
    {
        CharacterClick();
    }
    public void CharacterClick()
    {
        if(cardSlotController.selectCard == null) return;

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
            Collider2D hit = Physics2D.OverlapPoint(mousePos); // 콜라이더 2D 컴포넌트 붙어 있어야됨

            if(hit == null) return;
            if(hit.gameObject != this.gameObject) return;
            if(cardSlotController.selectCard == null) return;

            if(characterIndex == 1)
            {
                cardSlotController.Onchar1Click();
            }
            if(characterIndex == 2)
            {
                cardSlotController.Onchar2Click();
            }
        }

    }
    public void AddShield(int amount)
    {
        shield += amount; // 실드 값 추가
    }
    public void AddDefend(float percent)
    {
        defendPercent -= percent;
        Debug.Log("방어 발동, 받는피해율: " +defendPercent);
    }
    public void TakeDamage(int amount)
    {
        int afterDefend = (int)(amount*defendPercent);
        int damage = afterDefend -shield;
        shield = Mathf.Max(0, shield - afterDefend); // 실드 깎기, 0이하 안되게
        Debug.Log("전체 피해값: "+amount+", 디펜드 이후: "+ afterDefend);

        if (damage > 0)
        {
            State.ApplyDamage(damage);
        }
        hpUIController.RefreshHP();
    }
    public void Heal(int amount)
    {
        State.ApplyHeal(amount); // 회복
        hpUIController.RefreshHP();
    }
    public void OnPageEnd()
    {
        shield = 0; // 라운드 끝나면 실드 초기화
        defendPercent = 1;
    }
    public bool IsDead()
    {
        return State.IsDead;
    }
}
