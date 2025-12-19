using UnityEngine;

public class Stomp : Skill
{
    public float speed;
    public GameObject Col;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    public override void OnActivate()
    {
        //쿨다운 조절,필수
        GameObject player = GameObject.FindWithTag("Player");
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Min(rb.linearVelocity.y - speed, speed));
        GameObject col = Instantiate(Col, player.transform.position, Quaternion.identity);
        col.transform.parent = player.transform;
        player.GetComponent<LivingEntity>().setInvincible = 1.0f;
        Destroy(col, 1f);
        base.OnActivate();
    }
}
