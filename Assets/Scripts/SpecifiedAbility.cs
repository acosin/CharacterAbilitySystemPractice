using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/SpecifiedAbility")]
public class SpecifiedAbility : Ability {
    private FastMoveSpecifiedShoot script;
    public override void Initialize(GameObject obj)
    {
        script = obj.AddComponent<FastMoveSpecifiedShoot>();
    }

    public override void TriggerAbility()
    {
        script.cast();
    }
}
