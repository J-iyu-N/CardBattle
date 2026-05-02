using UnityEngine;
using System.Collections.Generic;

public enum EnemyActionType {Attack, Shield, Special}
[System.Serializable]
public class EnemyAction
{
    public string actionName;
    public EnemyActionType actionType;
    public int rangeMin;
    public int rangeMax;
    public Sprite icon;
    [TextArea] public string desc;
}
[CreateAssetMenu(fileName = "NewEnemy", menuName = "BattleGame/EnemyData")]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public int maxHP;
    [Header("행동")]
    public List<EnemyAction> actionPool; // 적 행동 전체 모음
}