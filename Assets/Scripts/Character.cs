using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Character")]
public class Character : ScriptableObject
{

    public string characterName = "Default";
    public int startingHp = 100;

    public Ability[] characterAbilities;
    public string[] castAbilityAxisNames;

}