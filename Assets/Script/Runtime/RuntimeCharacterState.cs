using UnityEngine;
using System.Collections.Generic;

public class RuntimeCharacterState
{
    public CharacterData Data; // 캐릭터 정보
    public int CurrentHp;
    public bool IsDead => CurrentHp<=0;

    public RuntimeCharacterState(CharacterData data)
    {
        Data = data;
        CurrentHp = data.maxHP;
    }
    public void ApplyDamage(int amount)
    {
        // 공격 피 차감
        CurrentHp -=amount;
        if(CurrentHp<0) CurrentHp =0;
    }
    public void ApplyHeal(int amount)
    {
        // 회복
        CurrentHp += amount;
        if(CurrentHp>Data.maxHP) CurrentHp = Data.maxHP;
    }
}
