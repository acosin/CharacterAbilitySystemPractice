using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/SpecifiedAbility")]
public class SpecifiedAbility : Ability {
    public string specifiedType;
    private ICastable script;
    public override void Initialize(GameObject obj)
    {
        Type stype = Type.GetType(specifiedType);
        script = (ICastable)obj.AddComponent(stype);
    }

    public override void TriggerAbility()
    {
        script.Cast();
    }

    public override Ability CreateByCloning()
    {
        return UnityEngine.Object.Instantiate(this) as SpecifiedAbility;
    }
}
