using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Abilities/ProjectileAbility")]
public class ProjectileAbility : Ability
{

    public float projectileForce = 500f;
    public Rigidbody projectile;

    private ProjectileShoot launcher;

    public override void Initialize(GameObject obj)
    {
        launcher = obj.AddComponent<ProjectileShoot>(); //.GetComponent<ProjectileShoot>();
        launcher.Initialize();
    }

    public override void TriggerAbility()
    {
        //launcher.projectileForce = projectileForce;
        //launcher.projectile = projectile;
        launcher.Launch(projectile, projectileForce);
    }

}