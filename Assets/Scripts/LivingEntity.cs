using System;
using UnityEngine;
using Ilumisoft.HealthSystem; 

public class LivingEntity : MonoBehaviour, IDamageable, IPauseable
{
    //public float startHealth;

    //public float health { get; protected set; };

    public bool dead { get; protected set; }
    public event Action onDeath;
    
    private float damageTime = 0.0f;
    private Health health;
    private float setInv = 0.0f;

    public float setInvincible
    {
        set
        {
            setInv = value;
        }
    }
    
    public bool isInv
    {
        get
        {
            return setInv > 0.0f ? true : false;
        }
    }

    private bool IsGrounded = false;
    public bool isGrounded { get { return IsGrounded; } }
    public float platformCheck = 0.7f;
    public float DamageStop = 1.0f;

    [HideInInspector]
    public UpDown movePlatform = null;

    private Vector2 pauseVelocity;

    private GameObject hitEffectPrefab;
    public bool moveAble
    {
        get
        {
            if (GameManager.GM.isGameover)
                return true;
            if (dead)
                return true;
            if (damageTime > 0.0f)
                return false;
            else
                return true;
        }
    }

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();
        sr = GetComponent<SpriteRenderer>();
        if(sr == null)
        {
            sr = GetComponentInChildren<SpriteRenderer>();
        }
        hitEffectPrefab = Resources.Load<GameObject>("Effects/HitEffect");
        GameManager.GM.OnPause.AddListener(onPause);
        GameManager.GM.OnResume.AddListener(onResume);
    }
    protected virtual void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();
        dead = false;
        health.CurrentHealth = health.MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0.0f)
            return;
        if(damageTime >= 0.0f)
            damageTime -= Time.deltaTime;
        if (setInv >= 0.0f)
        {
            setInv -= Time.deltaTime;
            Color cr = sr.color;
            cr.a = 0.2f;
            sr.color = cr;
        }
        else
        {
            Color cr = sr.color;
            cr.a = 1f;
            sr.color = cr;
        }
        if (rb.linearVelocity.y < 0.0f)
        {
            RaycastHit2D rayhit = Physics2D.Raycast(rb.position, Vector2.down, platformCheck + 0.1f, LayerMask.GetMask("Platform"));
            if (rayhit.collider != null && rayhit.distance < platformCheck)
            {
                IsGrounded = true;
            }
        }
        else
        {
            IsGrounded = false;
        }
        /*
        if(movePlatform != null && !movePlatform.up && rb.linearVelocity.y < 0.0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y - movePlatform.speed);
        }
        */
    }

    public virtual void OnDamage(float damage, Vector2 hitPoint, Vector2 hitNormal)
    {
        if (setInv > 0.0f)
        {
            return;
        }
        Vector2 posVec = hitNormal.normalized;
        float weight = GetComponent<Rigidbody2D>().mass;
        if (hitNormal.x < 0)
        {
            posVec = (new Vector2(-1, 1)).normalized;
        }
        else
        {
            posVec = (new Vector2(1, 1)).normalized;
        }
        rb.linearVelocity = (posVec * damage / weight * 0.5f * (health.MaxHealth / (health.CurrentHealth + 20.0f)));
        damageTime = DamageStop; //움직일 수 없는 시간
        health.ApplyDamage(damage);
        setInv = 0.5f;
        //히트시 이펙트 구현
        GameObject he = Instantiate(hitEffectPrefab, hitPoint, Quaternion.identity);
        Destroy(he, 1f);

    }
    public virtual void RestoreHealth(float newHealth)
    {
        if (dead) return;
        health.AddHealth(newHealth);
    }
    public virtual void Die()
    {
        if(onDeath != null)
        {
            //Debug.Log("onDeath");
            onDeath();
        }
        dead = true;
    }
    private void OnDestroy()
    {
        //GameManager.GM.OnPause.RemoveListener(onPause);
        //GameManager.GM.OnResume.RemoveListener(onResume);
    }
    public void onPause()
    {
        pauseVelocity = rb.linearVelocity;
        rb.linearVelocity = Vector2.zero;
    }
    public void onResume()
    {
        rb.linearVelocity = pauseVelocity;
    }
}
