using UnityEngine;

public class Rising : Skill
{
    public float speed;
    public AudioClip risingsound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    public override void OnActivate()
    {
        //쿨다운 조절,필수
        GameObject player = GameObject.FindWithTag("Player");
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Max(rb.linearVelocity.y + speed, speed));
        AudioSource source = SkillManager.instance.skillsound;
        source.clip = risingsound;
        source.Play();
        base.OnActivate();
    }
}
