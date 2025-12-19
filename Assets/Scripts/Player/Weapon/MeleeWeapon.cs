using UnityEngine;

public class MeleeWeapon : Weapon
{
    public float timeBetSwing = 0.75f;
    public float scaleX = 1.0f;
    public float scaleY = 1.0f;
    public Sprite sprite;
    public AudioSource hitsound;
    public AudioSource swingsonud;
    //public Animation anim;

    private Animator anim;
    private float lastSwingTime = 0.0f;
    private SpriteRenderer sr;

    private void Awake()
    {
        //TODO
        sr = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponent<Animator>();
        if(sr.sprite != null)
            sr.sprite = sprite;
        gameObject.transform.localScale = new Vector3(scaleX, scaleY, 0);
    }

    private void OnEnable()
    {
        state = State.Ready;
        lastSwingTime = 0.0f;
    }

    public override void Attack()
    {
        if(lastSwingTime > 0.0f)
        {
            return;
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            swingsonud.Play();
            anim.SetTrigger("doAttack");
            lastSwingTime = timeBetSwing;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        LivingEntity le = collision.GetComponent<LivingEntity>();
        if (collision.gameObject.tag != "Enemy" || le  == null)
            return;
        float weight = collision.GetComponent<Rigidbody2D>().mass;
        Vector2 hitpoint = collision.ClosestPoint(transform.position);
        Vector2 hitnormal = collision.transform.position - transform.position;
        if (!le.isInv)
        {
            hitsound.volume = 0.7f;
            hitsound.Play();
        }
        le.OnDamage(damage, hitpoint, hitnormal);
        /*
        if (collision.gameObject.transform.position.x >= gameObject.transform.position.x)
            collision.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(1, 1).normalized * damage / weight * 0.5f;
        else
            collision.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(1, 1).normalized * damage / weight * 0.5f;
        */
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D co = gameObject.GetComponent<Collider2D>();
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            co.enabled = false;
            state = State.Ready;
        }
        else
        {
            co.enabled = true;
            state = State.Doing;
        }
        if(lastSwingTime > 0.0f)
        {
            lastSwingTime -= Time.deltaTime;
        }
    }
}
