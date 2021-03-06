﻿using UnityEngine;
using System.Collections;
using System;

[CreateAssetMenu(menuName = "Abilities/ProjectileAbility")]
public class ProjectileAbility : Ability
{

    public float projectileForce = 500f;
    public Rigidbody projectile;

    private ProjectileShoot launcher;

    public override void Initialize(GameObject obj)
    {
        launcher = obj.AddComponent<ProjectileShoot>();
        launcher.projectile = projectile;
        launcher.projectileForce = projectileForce;
        launcher.Initialize();
    }

    public override void TriggerAbility()
    {
        launcher.Cast();
    }

    public override Ability CreateByCloning()
    {
        return UnityEngine.Object.Instantiate(this) as ProjectileAbility;
    }
}