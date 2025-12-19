using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Rigidbody2D rb;
    private Collider2D co;
    private PlayerAttack pa;
    private LivingEntity le;
    private Animator anim;

    public float speed;
    public float jumpspeed;
    public AudioSource jumpsound;

    public bool isLeft = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        co = GetComponent<Collider2D>();
        pa = GetComponent<PlayerAttack>();
        le = GetComponent<LivingEntity>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SkillManager.instance.isSelecting)
            {
                return;
            }
            Debug.Log("KeyDown");
            GameManager.GM.TogglePause();
        }
        if (Time.timeScale == 0.0f)
            return;
        Inputs();
    }
    void FixedUpdate()
    {
        if (le.isGrounded)
        {
            anim.SetBool("IsJumping", false);
        }
    }
    private void Inputs()
    {
        if (!le.moveAble)
        {
            return;
        }
        float xInput = Input.GetAxis("Horizontal");
        float xSpeed = xInput * speed;
        if (!pa.isDoing)        {
            if(Mathf.Abs(rb.linearVelocity.x) < 0.3f)
            {
                anim.SetBool("IsRunning", false);
            }
            else
            {
                anim.SetBool("IsRunning", true);
            }
            if (xInput < 0)
            {
                isLeft = true;
            }
            else if (xInput > 0)
            {
                isLeft = false;
            }
            pa.changePosition(isLeft);
            
        }
        rb.linearVelocityX = xSpeed;
        if (Input.GetKeyDown(KeyCode.J))
        {
            RaycastHit2D rayhit = Physics2D.Raycast(rb.position, Vector2.down, 1, LayerMask.GetMask("Platform"));
            // Debug.Log("Space");
            if (rayhit.collider != null && rayhit.distance < 0.7f)
            {
                jumpsound.Play();
                rb.linearVelocityY = jumpspeed;
                anim.SetBool("IsJumping", true);
               // Debug.Log(rb.linearVelocityY);
            }
        }
        //Debug.Log(SkillManager.instance);
        if (Input.GetKeyDown(KeyCode.K))
        {
            pa.Attack();
        }
        //스킬 발동!
        if (Input.GetKeyDown(KeyCode.U))
        {
            Debug.Log("U Pushed");
            SkillManager.instance.ActiveSkill(0);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            SkillManager.instance.ActiveSkill(1);
        }
        //일시정지
    }
}
