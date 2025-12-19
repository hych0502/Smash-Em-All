using UnityEngine;



public class CollisionDamage : MonoBehaviour
{
    public enum destroyCondition
    {
        land
    }
    public float damage;
    public destroyCondition dc;
    public AudioClip hitsound;

    private Rigidbody2D playerRB;

    private void Start()
    {
        playerRB = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        LivingEntity le = collision.GetComponent<LivingEntity>();
        if (collision.gameObject.tag != "Enemy" || le == null)
            return;
        float weight = collision.GetComponent<Rigidbody2D>().mass;
        Vector2 hitpoint = collision.ClosestPoint(transform.position);
        Vector2 hitnormal = collision.transform.position - transform.position;
        AudioSource skillsound = SkillManager.instance.skillsound;
        skillsound.clip = hitsound;
        skillsound.volume = 0.7f;
        skillsound.Play();
        le.OnDamage(damage, hitpoint, hitnormal);
        /*
        if (collision.gameObject.transform.position.x >= gameObject.transform.position.x)
            collision.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(1, 1).normalized * damage / weight * 0.5f;
        else
            collision.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(1, 1).normalized * damage / weight * 0.5f;
        */
    }
    private void Update()
    {
        if(dc == destroyCondition.land && playerRB.linearVelocity.y > -0.1)
        {
            Destroy(this);
        } 
    }
}
