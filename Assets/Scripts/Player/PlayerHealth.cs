using UnityEngine;

public class PlayerHealth : LivingEntity
{
    private PlayerControl pc;
    private PlayerAttack pa;

    protected override void OnEnable()
    {
        pc = GetComponent<PlayerControl>();
        pa = GetComponent<PlayerAttack>();
        base.OnEnable();
        pc.enabled = true;
        pa.enabled = true;
    }

    public override void RestoreHealth(float newHealth)
    {
        base.RestoreHealth(newHealth);
    }

    public override void OnDamage(float damage, Vector2 hitPoint, Vector2 hitNormal)
    {
        if (!dead)
        {
            //TODO: 데미지 입을 시 사운드 구현
        }
        base.OnDamage(damage, hitPoint, hitNormal);
    }

    public override void Die()
    {
        base.Die();
        //TODO: 체력 슬라이더 비활성화, 사망음, 사망 애니메이션 or 연출
        gameObject.SetActive(false);
        pc.enabled = false;
        pa.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //TODO: 아이템 먹기 구현
    }
}
