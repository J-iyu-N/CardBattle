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

    public bool isbattleing; // 전투중?
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
        enemy.OnPageStart(char1.IsDead(),char2.IsDead());
        handUIDirector.RefreshHandUI();
    }
    public void OncConfrimButton()
    {
        // 카드 확정 버튼 누름 (클릭 이벤트 받기용)
        if(battleEnd==true) return;
        enemyUI.HideTargetLine();
        StartCoroutine(ResolvePage());
    }
    /// <summary>
    /// 전투 진행 
    // 방어 -> 공격 -> (적) -> 승패체크 -> 다음페이즈/전투종료
    /// </summary>
    /// <returns></returns>
    public IEnumerator ResolvePage()
    {
        isbattleing = true;
        if (enemy.State.CurrentAction == null)
        {
            yield break;
        }

        // 방어 처리
        StartCoroutine(ApplyShield(char1, cardSlot.slotCard1,char1Transform));
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(ApplyShield(char2, cardSlot.slotCard2,char2Transform));
        if (enemy.State.CurrentAction.actionType == EnemyActionType.Shield)
        {
            // EnemyController의 RuntimeEnemyState의 최근 동작의 타입이 실드일때
            enemy.ApplyShield();
            if(CheckBattleResult()==true) yield break;
        }
        yield return new WaitForSeconds(0.1f);

        // 캐릭터 1,2 행동 처리
        StartCoroutine(ApplyAtackAndHeal(char1,cardSlot.slotCard1,char1Transform));
        yield return new WaitForSeconds(0.8f);
        if(CheckBattleResult()==true) yield break;

        StartCoroutine(ApplyAtackAndHeal(char2,cardSlot.slotCard2,char2Transform));
        yield return new WaitForSeconds(1f);
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
        yield return new WaitForSeconds(0.4f);
        if(CheckBattleResult()==true) yield break;

        cardSlot.ConfirmCard();
        yield return new WaitForSeconds(0.1f);
        EndPage();
        isbattleing = false;
        yield return new WaitForSeconds(0.1f);
        StartPage();
    }
    /// <summary>
    /// 방어 계열 카드 적용
    /// </summary>
    /// <param name="target"></param>
    /// <param name="card"></param>
    public IEnumerator ApplyShield(PlayerCharacterController target, CardData card,PlayerTransformController charTransform)
    {
        // 방어 적용 메서드
        if(card == null) yield break;
        for(int i =0; i < card.effects.Count; i++)
        {
            CardEffect effect = card.effects[i];
            if( effect.effectType == CardEffectType.Shield)
            {
                int value = Random.Range(effect.rangeMin,effect.rangeMax+1);
                target.AddShield(value);
                charTransform.PlayShield();
            }
            if( effect.effectType == CardEffectType.Defend)
            {
                target.AddDefend(effect.percent);
            }
        }
    }
    /// <summary>
    /// 전투 & 힐 계열 카드 적용
    /// </summary>
    /// <param name="target"></param>
    /// <param name="card"></param>
    /// <param name="charTransform"></param>
    /// <returns></returns>
    public IEnumerator ApplyAtackAndHeal(PlayerCharacterController target, CardData card, PlayerTransformController charTransform)
    {
        // 공격&힐 적용 메서드
        if(card == null) yield break;
        for(int i =0; i < card.effects.Count; i++)
        {
            CardEffect effect = card.effects[i];
            if( effect.effectType == CardEffectType.Attack)
            {
                if(card.cardType == CardType.Special && target == char2)
                {
                    // 캐릭터 2 전용 Special 카드 행동
                    int value = Random.Range(effect.rangeMin,effect.rangeMax+1);
                    enemy.TakeDamage(value);
                    charTransform.PlayAttackGun(); // 공격 모션 적용
                    yield return new WaitForSeconds(0.5f);
                }
                else
                {
                    int value = Random.Range(effect.rangeMin,effect.rangeMax+1);
                    enemy.TakeDamage(value);
                    charTransform.PlayAttack(); // 공격 모션 적용
                }
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
    /// <summary>
    /// 전투 결과 체크
    /// </summary>
    /// <returns></returns>
    public bool CheckBattleResult()
    {
        if (char2.IsDead() == true) 
        {
            char2Transform.PlayDead();
            cardSlot.DisableChar2Slot();
            Debug.Log("캐릭터 2 죽음");
        }
        if (char1.IsDead() == true) 
        {
            char1Transform.PlayDead();
            cardSlot.DisableChar1Slot();
            Debug.Log("캐릭터 1 죽음");
        }
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