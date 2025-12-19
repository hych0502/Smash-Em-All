using Unity.VisualScripting;
using UnityEngine;

public class Bouncing : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D col;
    private float fvy;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float jumpspeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float xInput = Input.GetAxis("Horizontal");
        float xSpeed = xInput * speed;
        rb.linearVelocityX = xSpeed;
        if(rb.linearVelocityY <= 0)
        {
            col.isTrigger = true;
            col.enabled = true;
        }
        else
        {
            col.isTrigger = false;
            col.enabled = false;
        }
    }
    private void FixedUpdate()
    {
        fvy = rb.linearVelocityY;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("enter Collision: " + fvy);
        if(fvy < 0.0f)
        {
            //Debug.Log("set Vel");
            rb.linearVelocityY = jumpspeed;
        }
    }
}
