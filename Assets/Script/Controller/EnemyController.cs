using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EnemyData Data;
    public RuntimeEnemyState State;
    public HpUIEnemyDirector hpUI;
    public int shield;

    void Awake()
    {
        State = new RuntimeEnemyState(Data);
    }
    public void OnPageStart()
    {
        State.DecideActions(); // 행동 결정
    }
    public void ExcuteAction(PlayerCharacterController target)
    {
        int value = State.RollValue(); // 카드 랜던값 결정
        if(State.CurrentAction.actionType == EnemyActionType.Attack)
        {
            Debug.Log("타겟: "+target);
            target.TakeDamage(value); // 타겟에게 공격
        }
    }
    public void ApplyShield()
    {
        int value = State.RollValue();
        shield += value;
        hpUI.RefreshHP();
    }
    public void TakeDamage(int amount)
    {
        // 실드 깎기, 0이하 안되게
        int damage = amount -shield;
        shield = Mathf.Max(0, shield - amount); 

        if (damage > 0)
        {
            State.ApplyDamage(damage); // 남은 데미지 주기
        }
        hpUI.RefreshHP();
    }
    public void OnPageEnd() // 라운드 끝나면 실드 초기화
    {
        shield = 0; 
        hpUI.RefreshHP();
    }
    public bool IsDead()
    {
        return State.IsDead;
    }
}
