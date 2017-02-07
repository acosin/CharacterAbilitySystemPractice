using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable {
    void GetAttacked(int damage);
    void GetForce(Vector3 hitPoint, Vector3 force);
    void GetExplosionForce(float explosionForce, Vector3 explosionPosition, float explosionRadius);
}
