using UnityEngine;
using System.Collections.Generic;
using JetBrains.Annotations;

[CreateAssetMenu(fileName = "newCharacter", menuName ="BattleGame/CharacterData")]
public class CharacterData : ScriptableObject
{
    public string CharacterName;
    public int maxHP;
}