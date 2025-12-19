using UnityEngine;
using UnityEngine.UIElements;

public class UpDown : MonoBehaviour
{
    public float length;
    public float speed;

    private float initY;
    public bool up { get; set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        up = true;
        initY = gameObject.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (up)
        {
            if(gameObject.transform.position.y > initY + length)
            {
                up = false;
            }
            else
            {
                gameObject.transform.Translate(new Vector2(0, speed * Time.deltaTime));
            }
        }
        else
        {
            if (gameObject.transform.position.y < initY - length)
            {
                up = true;
            }
            else
            {
                gameObject.transform.Translate(new Vector2(0, -speed * Time.deltaTime));
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        LivingEntity le = collision.gameObject.GetComponent<LivingEntity>();
        if (le != null)
        {
            le.movePlatform = this;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        LivingEntity le = collision.gameObject.GetComponent<LivingEntity>();
        if (le != null)
        {
            le.movePlatform = null;
        }
    }
}
