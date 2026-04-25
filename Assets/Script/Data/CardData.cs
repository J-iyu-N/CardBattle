using UnityEngine;
using System.Collections.Generic;

// 카드 종류
public enum CardType {Attack, Defend, Special}
// 카드 지정
public enum CardOwner {All, Char1, Char2}

[CreateAssetMenu(fileName = "NewCard", menuName = "BattleGame/CardData")]
public class CardData : ScriptableObject
{
    public int sortIndex;
    public string cardName;
    public CardType cardType;
    public CardOwner cardOwner;
    [TextArea] public string description;

    public List<CardEffect> effects;
}
