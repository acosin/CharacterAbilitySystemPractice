﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/SpecifiedAbility")]
public class SpecifiedAbility : Ability {
    public string specifiedType;
    private SpecifiedShoot script;
    public override void Initialize(GameObject obj)
    {
        Type stype = Type.GetType(specifiedType);
        script = (SpecifiedShoot)obj.AddComponent(stype);
    }

    public override void TriggerAbility()
    {
        script.cast();
    }
}
