using TMPro;
using UnityEngine;

public class EnemyUIDirecotr : MonoBehaviour
{
    // 적 대상 표시 UI 
    // 스킬 UI
    // 스킬 설명 UI 필요... -> 호버하면 설명 캔버스 뜨게 (나중에 구현
    public RuntimeEnemyState enemyState;
    public EnemyAction enemyAction;
    public GameObject LineTo1;
    public GameObject LineTo2;
    public SpriteRenderer slot;
    public Sprite[] iconList;

    [Header("판넬")]
    public GameObject panel;
    public TextMeshProUGUI skillName;
    public TextMeshProUGUI SkillDesc;

    public void RefreshUI(RuntimeEnemyState enemyState)
    {
        Debug.Log("RefreshUI 실행 오브젝트: " + gameObject.name);

        this.enemyState = enemyState;
        if(enemyState.CurrentAction == null) return;

        RefreshSkillIcon(enemyState);
        if(enemyState.CurrentAction.actionType == EnemyActionType.Attack || enemyState.CurrentAction.actionType == EnemyActionType.Special)
        {
            // 적 공격 유형이 공격이면 라인 표시
            ShowTargetLine(enemyState.Target);
        }
        else
        {
            HideTargetLine();
        }
    }
    public void ShowTargetLine(int target)
    {
        // 표적 캐릭터 표시
        if(target == 0)
        {
            LineTo1.SetActive(true);
            LineTo2.SetActive(false);
        }
        else if(target == 1)
        {
            LineTo1.SetActive(false);
            LineTo2.SetActive(true);
        }
        else
        {
            HideTargetLine();
        }
    }
    public void HideTargetLine()
    {
        LineTo1.SetActive(false);
        LineTo2.SetActive(false);
    }
    public void RefreshSkillIcon(RuntimeEnemyState enemyState)
    {
        slot.sprite = enemyState.CurrentAction.icon;
    }
    public void ShowPanel()
    {
        if (enemyState == null) return;
        if (enemyState.CurrentAction == null) return;

        EnemyAction action = enemyState.CurrentAction;

        panel.SetActive(true);

        skillName.text = action.actionName;
        SkillDesc.text = action.desc;
    }
    public void HidePanel()
    {
        panel.SetActive(false);
    }
}
