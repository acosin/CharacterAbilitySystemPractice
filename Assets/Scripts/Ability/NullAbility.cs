using UnityEngine;
using System.Collections;
using System;

[CreateAssetMenu(menuName = "Abilities/NullAbility")]
public class NullAbility : Ability
{
    public override void Initialize(GameObject obj)
    {

    }

    public override void TriggerAbility()
    {

    }

    public override Ability CreateByCloning()
    {
        return UnityEngine.Object.Instantiate(this) as NullAbility;
    }
}