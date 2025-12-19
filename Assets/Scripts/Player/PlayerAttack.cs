using UnityEngine;
public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private GameObject PlayerWeapon;

    [HideInInspector]
    public GameObject weapon;

    public GameObject HealthBarCanvas;

    private SpriteRenderer sr;

    public bool isDoing
    {
        get
        {
            if (weapon.GetComponent<Weapon>().state == Weapon.State.Doing)
                return true;
            else
                return false;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        weapon = Instantiate(PlayerWeapon, this.transform);
    }
    void Start()
    {
        Vector3 pos = new Vector3(-0.6f, 0.6f, 0f);
        weapon.transform.localPosition = pos;
        sr = weapon.GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Attack()
    {
        weapon.GetComponent<Weapon>().Attack();
    }
    public void changePosition(bool isLeft)
    {
        if (isLeft)
        {
            //sr.flipX = false;
            gameObject.transform.localScale = new Vector3(1, 1, 0);
            HealthBarCanvas.transform.localScale = new Vector3(1, 1, 0);
        }
        else
        {
            //sr.flipX = true;
            gameObject.transform.localScale = new Vector3(-1, 1, 0);
            HealthBarCanvas.transform.localScale = new Vector3(-1, 1, 0);
        }
    }
}
