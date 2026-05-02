using UnityEngine;
using System.Collections;
public class BattleDirector : MonoBehaviour
{
    public PlayerCharacterController char1;
    public PlayerCharacterController char2;
    public EnemyController enemy;
    public CardSlotController cardSlot;
    public HandUIDirector handUIDirector;
    public PlayerTransformController char1Transform;
    public PlayerTransformController char2Transform;
    public EnemyUIDirecotr enemyUI;

    public bool battleEnd;
    public void Start()
    {
        char1.State = new RuntimeCharacterState(char1.Data);
        char2.State = new RuntimeCharacterState(char2.Data);
        StartPage();
    }
    public void StartPage()
    {
        // 페이즈 시작 
        cardSlot.OnPageStart();
        enemy.OnPageStart();
        handUIDirector.RefreshHandUI();

        Debug.Log("=== 라운드 시작 ===");
        Debug.Log($"적 예고 행동: {enemy.State.CurrentAction.actionName}");
        Debug.Log($"핸드 카드 수: {cardSlot.handController.Hand.Count}");
    }
    public void OncConfrimButton()
    {
        // 카드 확정 버튼 누름 (클릭 이벤트 받기용)
        if(battleEnd==true) return;
        enemyUI.HideTargetLine();
        StartCoroutine(ResolvePage());
    }

    // 전투 진행 
    // 방어 -> 공격 -> (적) -> 승패체크 -> 다음페이즈/전투종료
    public IEnumerator ResolvePage()
    {
        if (enemy.State.CurrentAction == null)
        {
            Debug.Log("적 행동 없음");
            yield break;
        }

        // 방어 처리
        ApplyShield(char1, cardSlot.slotCard1);
        ApplyShield(char2, cardSlot.slotCard2);
        if(enemy.State.CurrentAction.actionType == EnemyActionType.Shield)
        {
            // EnemyController의 RuntimeEnemyState의 최근 동작의 타입이 실드일때
            enemy.ApplyShield();
            if(CheckBattleResult()==true) yield break;
        }

        // 캐릭터 1,2 행동 처리
        ApplyAtackAndHeal(char1,cardSlot.slotCard1,char1Transform);
        yield return new WaitForSeconds(0.8f);
        if(CheckBattleResult()==true) yield break;

        ApplyAtackAndHeal(char2,cardSlot.slotCard2,char2Transform);
        yield return new WaitForSeconds(0.8f);
        if(CheckBattleResult()==true) yield break;

        // 적 행동 처리
        if(enemy.State.CurrentAction.actionType != EnemyActionType.Shield)
        {
            if(enemy.State.Target == 0)
            {
                enemy.ExcuteAction(char1);
                char1Transform.PlayDamaged(); // 맞는 모션 재생
            }
            else if(enemy.State.Target == 1)
            {
                enemy.ExcuteAction(char2);
                char2Transform.PlayDamaged(); // 맞는 모션 재생
            }
        }
        if(CheckBattleResult()==true) yield break;

        cardSlot.ConfirmCard();
        EndPage();
        StartPage();
    }
    public void ApplyShield(PlayerCharacterController target, CardData card)
    {
        // 방어 적용 메서드
        if(card == null) return;
        for(int i =0; i < card.effects.Count; i++)
        {
            CardEffect effect = card.effects[i];
            if( effect.effectType == CardEffectType.Shield)
            {
                int value = Random.Range(effect.rangeMin,effect.rangeMax+1);
                target.AddShield(value);
            }
            if( effect.effectType == CardEffectType.Defend)
            {
                Debug.Log("==========="+effect.percent);
                target.AddDefend(effect.percent);
            }
        }
    }
    public void ApplyAtackAndHeal(PlayerCharacterController target, CardData card, PlayerTransformController charTransform)
    {
        // 공격&힐 적용 메서드
        if(card == null) return;
        for(int i =0; i < card.effects.Count; i++)
        {
            CardEffect effect = card.effects[i];
            if( effect.effectType == CardEffectType.Attack)
            {
                int value = Random.Range(effect.rangeMin,effect.rangeMax+1);
                enemy.TakeDamage(value);
                charTransform.PlayAttack(); // 공격 모션 적용
            }
            if( effect.effectType == CardEffectType.Heal)
            {
                int value = Random.Range(effect.rangeMin,effect.rangeMax+1);
                target.Heal(value);
                charTransform.PlayHeal();
                if(charTransform == char2Transform)
                {
                    char1Transform.PlayHealOther();
                }
            }
        }
    }
    public bool CheckBattleResult()
    {
        if (char2.IsDead() == true) Debug.Log("캐릭터 2 죽음");
        if (char1.IsDead() == true) Debug.Log("캐릭터 1 죽음");

        // 결과 판단 메서드
        if (enemy.IsDead() == true)
        {
            battleEnd = true;
            Debug.Log("승리");
            return true;
        }
        if (char1.IsDead() == true && char2.IsDead() == true)
        {
            battleEnd = true;
            Debug.Log("패배");
            return true;
        }
        return false;
    }
    public void EndPage()
    {
        char1.OnPageEnd();
        char2.OnPageEnd();
        enemy.OnPageEnd();
    }
}