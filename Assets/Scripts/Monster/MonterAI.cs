using UnityEngine;

public enum AIType {
    Fly,
    Ground
}

public class MonsterAI : MonoBehaviour
{
    public Transform target; // 플레이어 위치
    public float moveSpeed = 3f;
    public float attackRange = 2f;
    public AIType type;
    private float time1;
    private Rigidbody2D rb;
    private LivingEntity livingEntity;
    private GameManager GM;
    private SpriteRenderer sr;

    private int nextGround = 0;
    private float nextFly = 0;

    private void Start()
    {
        livingEntity = GetComponent<LivingEntity>();
        GM = GameManager.GM;
        target = GameObject.FindWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        time1 = Time.time;
        if (type == AIType.Ground)
        {
             rb.gravityScale = 1;
        }
        if(type == AIType.Fly)
            ThinkFly();
        if(type == AIType.Ground)
            ThinkGround();
        sr = GetComponent<SpriteRenderer>();
        if (sr == null)
        {
            sr = GetComponentInChildren<SpriteRenderer>();
        }
    }

    void FixedUpdate()
    {
        if (!livingEntity.moveAble || GM.isGameover)
        {
            return;
        }
        if (type == AIType.Fly)
        {
            FlyAI();
        }
        if (type == AIType.Ground)
        {
            GroundAI();
        }
        if (rb.linearVelocity.x < 0)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }
    }
    private void FlyAI()
    {
        float distance = Vector3.Distance(transform.position, target.position);
        Vector3 onevector = (transform.position - target.position) / distance;

        if (distance <= attackRange)
        {
            // 공격 애니메이션 실행 및 공격 로직 처리
        }
        else
        {
            if (nextFly < 0.8f)
                rb.linearVelocity = (-onevector * moveSpeed);
            else
                rb.linearVelocity = (onevector * moveSpeed);
        }
    }

    private void GroundAI()
    {
        if(rb.linearVelocity.y < -0.1f)
        {
            return;
        }
        rb.linearVelocity = new Vector2(nextGround * moveSpeed, rb.linearVelocity.y);
        Vector2 frontVec = new Vector2(rb.position.x + nextGround * 0.8f, rb.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayhit = Physics2D.Raycast(frontVec, Vector2.down, livingEntity.platformCheck + 0.1f, LayerMask.GetMask("Platform"));
        if(rayhit.collider == null)
        {
            //Debug.Log("rayhit Platform");
            nextGround *= -1;
            CancelInvoke("ThinkGround");
            Invoke("ThinkGround", 5);
        }
    }

    void ThinkGround()
    {
        nextGround = Random.Range(-1, 2);
        //Debug.Log("nextGroudn: " + nextGround);
        Invoke("ThinkGround", 2);
    }

    void ThinkFly()
    {
        nextFly = Random.Range(0.0f, 1.0f);
        Invoke("ThinkFly", 0.5f);
    }
}