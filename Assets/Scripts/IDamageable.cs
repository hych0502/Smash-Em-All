using UnityEngine;

public interface IDamageable
{
    void OnDamage(float damage, Vector2 hitPoint, Vector2 hitNormal);
}
