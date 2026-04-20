using UnityEngine;

public class PlayerCharacterController : MonoBehaviour
{
    public CharacterData Data;
    public RuntimeCharacterState State;
    public int shield;
    void Awake()
    {
        State = new RuntimeCharacterState(Data);
    }
    public void AddShield(int amount)
    {
        shield = amount; // 실드 값 추가
    }
    public void TakeDamage(int amount)
    {
        int damage = amount -shield;
        shield = Mathf.Max(0, shield - amount); // 실드 깎기, 0이하 안되게

        if (damage > 0)
        {
            State.ApplyDamage(damage);
        }
    }
    public void Heal(int amount)
    {
        State.ApplyHeal(amount); // 회복
    }
    public void OnPageEnd()
    {
        shield = 0; // 라운드 끝나면 실드 초기화
    }
    public bool IsDead()
    {
        return State.IsDead;
    }
}
