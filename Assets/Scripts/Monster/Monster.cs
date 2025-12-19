using UnityEngine;

public class Monster : LivingEntity
{
    public LayerMask whatIsTarget;

    private LivingEntity targetEntity;
    public AudioClip hitSound;
    public AudioSource audiosource;
    public float damage = 20;
    public float timeBetAttack = 0.5f;

    public GameObject particleObject;
    public GameObject deathsound;

    private bool hasTarget
    {
        get
        {
            if (targetEntity != null && !targetEntity.dead)
                return true;
            else
                return false;
        }
    }

    /*
    private void Awake()
    {
        //TODO: 추가할 내용 있으면 넣기
        onDeath += () => Destroy(gameObject);
    }
    */

    public void Setup(MonsterData monsterData)
    {
        //TODO: 몬스터 정보 초기화
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Vector2 hitpoint = collision.ClosestPoint(transform.position);
            Vector2 hitnormal = collision.transform.position - transform.position;
            LivingEntity le = collision.GetComponent<LivingEntity>();
            if (!le.isInv)
            {
                audiosource.clip = hitSound;
                audiosource.Play();
            }
            le.OnDamage(damage, hitpoint, hitnormal);
        }
    }

    // Update is called once per frame

    public override void OnDamage(float damage, Vector2 hitPoint, Vector2 hitNormal)
    {
        if (!dead)
        {
            //TODO: 피격시 애니메이션, 효과음 구현
        }
        base.OnDamage(damage, hitPoint, hitNormal);
    }

    public override void Die()
    {
        if (!GameManager.GM.isGameover)
        {
            Vector2 Right = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height * 0.5f));
            float width = Right.x;
            Vector2 Top = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width * 0.5f, Screen.height));
            float height = Top.y;
            float camX = Camera.main.transform.position.x;
            float camY = Camera.main.transform.position.y;
            float x = gameObject.transform.position.x;
            float y = gameObject.transform.position.y;

            Vector2 view = Camera.main.WorldToViewportPoint(gameObject.transform.position);
            //Vector3 effectPoint = new Vector3(Mathf.Max(width + camX - 2f, Mathf.Min(-width + 2f + camX, x)), Mathf.Max(height - 2f + camY, Mathf.Min(-height + 2f + camY, y)));
            Vector2 effectPoint = new Vector2(Mathf.Min(0.9f, Mathf.Max(0.1f, view.x)), Mathf.Min(0.9f, Mathf.Max(0.1f, view.y)));
            //effectPoint = new Vector3(effectPoint.x, effectPoint.y);
            effectPoint = Camera.main.ViewportToWorldPoint(effectPoint);
            GameObject effectObj = Instantiate(particleObject, effectPoint, Quaternion.identity);
            GameObject deathsoundObj = Instantiate(deathsound, effectPoint, Quaternion.identity);
            effectObj.transform.parent = Camera.main.transform;

            Destroy(deathsoundObj, 1.5f);
            Destroy(effectObj, 3f);
            Debug.Log("Effect Created: " + effectPoint);
            //ParticleSystem ps = effectObj.GetComponent<ParticleSystem>();
            //ps.Play();
        }
        base.Die();
        Destroy(gameObject);
    }
}
