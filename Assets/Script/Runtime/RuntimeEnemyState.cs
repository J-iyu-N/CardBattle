using System.Collections.Generic;
using UnityEngine;

public class RuntimeEnemyState
{
    public EnemyData Data;
    public int CurrentHp;
    public EnemyAction CurrentAction; // 최근 공격
    public int Target; // 공격 대상 캐릭터 0, 1
    public bool IsDead => CurrentHp<=0;
    public RuntimeEnemyState(EnemyData data)
    {
        Data = data;
        CurrentHp = data.maxHP;
    }
    public void DecideActions()
    {
        List<EnemyAction> pool = Data.actionPool; // 적 행동 리스트
        if(pool.Count==0) return;

        CurrentAction = pool[Random.Range(0,pool.Count)]; // 행동 결정
        Target = Random.Range(0,2); // 공격 캐릭터 결정
    }
    public int RollValue()
    {
        // 행동 값 확정
        int value = Random.Range(CurrentAction.rangeMin,CurrentAction.rangeMax+1);
        return value;
    }
        public void ApplyDamage(int amount)
    {
        // 공격 피 차감
        CurrentHp -=amount;
        if(CurrentHp<0) CurrentHp =0;
    }
}
