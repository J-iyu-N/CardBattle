// 카드 효과
public enum CardEffectType {Attack, Shield, Defend, Heal} //공격 실드 방어 힐

[System.Serializable]
public class CardEffect
{
    public CardEffectType effectType;
    public int rangeMin; // 각 능력 수치 최소
    public int rangeMax; // 최대 
    public float percent; // 피해감소 퍼센트
}